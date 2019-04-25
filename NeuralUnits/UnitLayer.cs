using System.Threading.Tasks;
using System.Threading;
using System;

namespace NeuralUnits
{
    public class UnitLayer : UnitPrinter
    {
        public UnitLayer(string id, int width, int height, int depth) : base(width, height, depth)
        {
            Id = id;
        }

        public void clear_gradients()
        {
            clear_gradients_of_units();

            WeightLayerPool.clear_gradients_of_weights();
        }

        public void create_fully_connection(UnitLayer to)
        {
            WeightLayerPool.create_fully_connection(this, to);
        }

        public void create_convolution(UnitLayer to, int width, int height)
        {
            WeightLayerPool.create_convolution(this, to, width, height);
        }

        public void input_samples(double[,,] inputs)
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Units[x, y, d].value_at_inport = inputs[x, y, d];
                    }
                }
            }
        }

        protected Unit find_max_unit_in_rectangle(int left, int top, int right, int bottom, int depth)
        {
            double MAX = double.MinValue;

            int MAX_x = left, MAX_y = top;

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    double value = Units[x, y, depth].value_at_outport;

                    if (value > MAX)
                    {
                        MAX = value;

                        MAX_x = x;

                        MAX_y = y;
                    }
                }
            }

            return Units[MAX_x, MAX_y, depth];
        }

        public void diff_fully_product(double gradient, WeightLayer weights)
        {
            weights.diff_fully_product(gradient, Units);
        }

        public void diff_convolute_product(int left, int top, int right, int bottom, double gradient, WeightLayer weights)
        {
            weights.diff_convolute_product(left, top, right, bottom, gradient, Units);
        }

        private double max_pool(int left, int top, int right, int bottom, int depth)
        {
            Unit unit = find_max_unit_in_rectangle(left, top, right, bottom, depth);

            return unit.value_at_outport;
        }

        private void diff_max_pool(int left, int top, int right, int bottom, int depth, double gradient)
        {
            Unit unit = find_max_unit_in_rectangle(left, top, right, bottom, depth);

            unit.gradient_at_outport += gradient;
        }

        private double average_pool(int left, int top, int right, int bottom, int depth)
        {
            double sum = 0;

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    sum += Units[x, y, depth].value_at_outport;
                }
            }

            return sum / ((right - left) * (bottom - top));
        }

        private void diff_average_pool(int left, int top, int right, int bottom, int depth, double gradient)
        {
            gradient /= ((right - left) * (bottom - top));

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Units[x, y, depth].gradient_at_outport += gradient;
                }
            }
        }

        public void forward_fully_connect(UnitLayer to)
        {
            int index = 0, length = to.Depth / Config.MAX_NUMBER_OF_TASKS + Config.MIN_LENGTH_OF_TASKS;

            Task[] tasks = new Task[Config.MAX_NUMBER_OF_TASKS];

            for (int d = 0; d < to.Depth; d += length)
            {
                tasks[index++] = Task.Factory.StartNew((depth) => forward_fully_connect_on_depth(to, (int)depth, length), d);
            }

            Array.Resize(ref tasks, index);

            Task.WaitAll(tasks);
        }

        private void forward_fully_connect_on_depth(UnitLayer to, int depth, int length)
        {
            int width = to.Width, height = to.Height;

            int to_depth = depth + length;

            if (to_depth >= to.Depth)
            {
                to_depth = to.Depth;
            }

            for (int d = depth; d < to_depth; d++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        WeightLayer weights = WeightLayerPool.find(Id, to.Id, x, y, d);

                        to.set_value_at_inport(x, y, d, weights.fully_product(Units));
                    }
                }
            }
        }

        public void backward_fully_connect(UnitLayer to)
        {
            int index = 0, length = Depth / Config.MAX_NUMBER_OF_TASKS + Config.MIN_LENGTH_OF_TASKS;

            Task[] tasks = new Task[Config.MAX_NUMBER_OF_TASKS];

            for (int d = 0; d < Depth; d += length)
            {
                tasks[index++] = Task.Factory.StartNew((depth) => backward_fully_connect_on_depth(to, (int)depth, length), d);
            }

            Array.Resize(ref tasks, index);

            Task.WaitAll(tasks);
        }

        private void backward_fully_connect_on_depth(UnitLayer to, int depth, int length)
        {
            int to_depth = depth + length;

            if (to_depth >= Depth)
            {
                to_depth = Depth;
            }

            for (int d = depth; d < to_depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        WeightLayer weights = WeightLayerPool.find(to.Id, Id, x, y, d);

                        to.diff_fully_product(Units[x, y, d].gradient_at_inport, weights);

                        weights.diff_weights();
                    }
                }
            }
        }

        public void forward_convolute(UnitLayer to, int stride)
        {
            int index = 0, length = to.Depth / Config.MAX_NUMBER_OF_TASKS + Config.MIN_LENGTH_OF_TASKS;

            Task[] tasks = new Task[Config.MAX_NUMBER_OF_TASKS];

            for (int d = 0; d < to.Depth; d += length)
            {
                tasks[index++] = Task.Factory.StartNew((depth) => forward_convolute_on_depth(to, stride, (int)depth, length), d);
            }

            Array.Resize(ref tasks, index);

            Task.WaitAll(tasks);
        }

        private void forward_convolute_on_depth(UnitLayer to, int stride, int depth, int length)
        {
            int to_depth = depth + length;

            if (to_depth >= to.Depth)
            {
                to_depth = to.Depth;
            }

            for (int d = depth; d < to_depth; d++)
            {
                WeightLayer weights = WeightLayerPool.find(Id, to.Id, d);

                int width_of_weights = weights.Width, height_of_weights = weights.Height;

                int width_of_units = to.Width, height_of_units = to.Height;

                for (int y = 0; y < height_of_units; y++)
                {
                    int top = y * stride;

                    int bottom = top + height_of_weights;

                    for (int x = 0; x < width_of_units; x++)
                    {
                        int left = x * stride;

                        to.set_value_at_inport(x, y, d, weights.convolute_product(left, top, left + width_of_weights, bottom, Units));
                    }
                }
            }
        }

        public void backward_convolute(UnitLayer to, int stride)
        {
            int index = 0, length = Depth / Config.MAX_NUMBER_OF_TASKS + Config.MIN_LENGTH_OF_TASKS;

            Task[] tasks = new Task[Config.MAX_NUMBER_OF_TASKS];

            for (int d = 0; d < Depth; d += length)
            {
                tasks[index++] = Task.Factory.StartNew((depth) => backward_convolute_on_depth(to, stride, (int)depth, length), d);
            }

            Array.Resize(ref tasks, index);

            Task.WaitAll(tasks);
        }

        private void backward_convolute_on_depth(UnitLayer to, int stride, int depth, int length)
        {
            int to_depth = depth + length;

            if (to_depth >= Depth)
            {
                to_depth = Depth;
            }

            for (int d = depth; d < to_depth; d++)
            {
                WeightLayer weights = WeightLayerPool.find(to.Id, Id, d);

                int width_of_weights = weights.Width, height_of_weights = weights.Height;

                for (int y = 0; y < Height; y++)
                {
                    int top = y * stride;

                    int bottom = top + height_of_weights;

                    for (int x = 0; x < Width; x++)
                    {
                        int left = x * stride;

                        to.diff_convolute_product(left, top, left + width_of_weights, bottom, Units[x, y, d].gradient_at_inport, weights);
                    }
                }

                weights.diff_weights();
            }
        }

        public void forward_max_pool(UnitLayer to, int width, int height, int stride)
        {
            int width_of_units = to.Width, height_of_units = to.Height;

            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < height_of_units; y++)
                {
                    int top = y * stride;

                    int bottom = top + height;

                    for (int x = 0; x < width_of_units; x++)
                    {
                        int left = x * stride;

                        to.set_value_at_inport(x, y, d, max_pool(left, top, left + width, bottom, d));
                    }
                }
            }
        }

        public void backward_max_pool(UnitLayer to, int width, int height, int stride)
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int top = y * stride;

                    int bottom = top + height;

                    for (int x = 0; x < Width; x++)
                    {
                        int left = x * stride;

                        to.diff_max_pool(left, top, left + width, bottom, d, Units[x, y, d].gradient_at_inport);
                    }
                }
            }
        }

        public void forward_average_pool(UnitLayer to, int width, int height, int stride)
        {
            int width_of_units = to.Width, height_of_units = to.Height;

            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < height_of_units; y++)
                {
                    int top = y * stride;

                    int bottom = top + height;

                    for (int x = 0; x < width_of_units; x++)
                    {
                        int left = x * stride;

                        to.set_value_at_inport(x, y, d, average_pool(left, top, left + width, bottom, d));
                    }
                }
            }
        }

        public void backward_average_pool(UnitLayer to, int width, int height, int stride)
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int top = y * stride;

                    int bottom = top + height;

                    for (int x = 0; x < Width; x++)
                    {
                        int left = x * stride;

                        to.diff_average_pool(left, top, left + width, bottom, d, Units[x, y, d].gradient_at_inport);
                    }
                }
            }
        }

        //损失×导数=梯度
        public void find_loss_by_MSE(double[,,] labels)
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Unit unit = Units[x, y, d];

                        unit.gradient_at_outport = labels[x, y, d] - unit.value_at_outport;
                    }
                }
            }
        }
        
        public double[,,] create_image()
        {
            double[,,] pixels = new double[Width, Height, 1];

            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        pixels[x, y, 0] += Units[x, y, d].value_at_outport;
                    }
                }
            }

            return pixels;
        }
    }
}
