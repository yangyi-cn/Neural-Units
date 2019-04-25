using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace NeuralUnits
{
    public class WeightLayer
    {
        private static Random RN_generator = new Random();

        private Weight[,,] Weights;

        private Weight Bias;

        public string Id;

        public int Depth, Width, Height;

        public WeightLayer(string id, int width, int height, int depth)
        {
            Id = id;

            Width = width;

            Height = height;

            Depth = depth;

            Weights = create_weights(Width, Height, Depth);

            Bias = create_bias();
        }

        private Weight[,,] create_weights(int width, int height, int depth)
        {
            Weight[,,] weights = new Weight[width, height, depth];

            for (int d = 0; d < depth; d++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        weights[x, y, d] = new Weight();
                    }
                }
            }

            return weights;
        }

        private Weight create_bias()
        {
            Weight bias = new Weight();

            return bias;
        }

        public void fill_weights(int number_of_units)
        {
            //生成正态分布的随机数
            double[] numbers = generate_ND_random_numbers(Width * Height * Depth);

            //填充
            int index = 0;

            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        double number = numbers[index++] * Math.Sqrt(2 / (double)number_of_units);

                        Weights[x, y, d].value = number;
                    }
                }
            }
        }

        //生成服从正态分布N(0,1)的随机数
        private double[] generate_ND_random_numbers(int count)
        {
            double[] numbers = new double[count];

            for (int i = 0; i < count; i++)
            {
                double U1, U2, V1 = 0, V2 = 0, S = 0;

                while (S > 1 || S == 0)
                {
                    U1 = RN_generator.NextDouble();

                    U2 = RN_generator.NextDouble();

                    V1 = 2 * U1 - 1;

                    V2 = 2 * U2 - 1;

                    S = V1 * V1 + V2 * V2;
                }

                double number1 = Math.Sqrt(-2 * Math.Log(S) / S) * V1;

                double number2 = Math.Sqrt(-2 * Math.Log(S) / S) * V2;

                numbers[i] = number1;

                if (i < count - 1)
                {
                    numbers[++i] = number2;
                }
            }

            return numbers;
        }

        public void clear_gradients()
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Weights[x, y, d].gradient = 0;
                    }
                }
            }

            Bias.gradient = 0;
        }

        private double set_bias(double value)
        {
            return Bias.value = value;
        }

        public void diff_weights()
        {
            Bias.diff();

            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Weights[x, y, d].diff();
                    }
                }
            }
        }

        public double fully_product(Unit[,,] units)
        {
            double sum = 0;

            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        sum += units[x, y, d].value_at_outport * Weights[x, y, d].value;
                    }
                }
            }

            return sum + Bias.value;
        }

        public void diff_fully_product(double gradient, Unit[,,] units)
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Unit unit = units[x, y, d];

                        Weight weight = Weights[x, y, d];

                        //更新权值梯度
                        weight.gradient += gradient * unit.value_at_outport;

                        //更新单元梯度
                        add_gradient_to_unit_with_interlocked(gradient * weight.value, unit);
                    }
                }
            }

            //更新偏置项
            Bias.gradient += gradient;
        }

        public double convolute_product(int left, int top, int right, int bottom, Unit[,,] units)
        {
            double sum = 0;

            for (int d = 0; d < Depth; d++)
            {
                for (int y = top; y < bottom; y++)
                {
                    for (int x = left; x < right; x++)
                    {
                        sum += units[x, y, d].value_at_outport * Weights[x - left, y - top, d].value;
                    }
                }
            }

            return sum + Bias.value;
        }

        public void diff_convolute_product(int left, int top, int right, int bottom, double gradient, Unit[,,] units)
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = top; y < bottom; y++)
                {
                    for (int x = left; x < right; x++)
                    {
                        Unit unit = units[x, y, d];

                        Weight weight = Weights[x - left, y - top, d];

                        //更新权值梯度
                        weight.gradient += unit.value_at_outport * gradient;

                        //更新单元梯度
                        add_gradient_to_unit_with_interlocked(gradient * weight.value, unit);
                    }
                }
            }

            //更新偏置项
            Bias.gradient += gradient;
        }

        private void add_gradient_to_unit_with_interlocked(double gradient, Unit unit)
        {
            while (true)
            {
                double gradient_at_outport = unit.gradient_at_outport;

                if (gradient_at_outport == Interlocked.CompareExchange(ref unit.gradient_at_outport, gradient_at_outport + gradient, gradient_at_outport))
                {
                    break;
                }
            }
        }

        public void save(BinaryWriter writer)
        {
            writer.Write(Bias.value);

            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        writer.Write(Weights[x, y, d].value);
                    }
                }
            }
        }

        public void load(BinaryReader reader)
        {
            double value = reader.ReadDouble();

            Bias.value = value;

            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        value = reader.ReadDouble();

                        Weights[x, y, d].value = value;
                    }
                }
            }
        }
    }
}
