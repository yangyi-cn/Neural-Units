using System.IO;
using System.Runtime.CompilerServices;
//[MethodImpl(MethodImplOptions.AggressiveInlining)]
using System.Threading;

namespace NeuralUnits
{
    //直接操作Units的方法归到此类
    abstract public class UnitCollection
    {
        protected Unit[,,] Units;
        
        public string Id;

        public int Depth, Width, Height;

        public UnitCollection(int width, int height, int depth)
        {
            Width = width;

            Height = height;

            Depth = depth;

            Units = create_units(Width, Height, Depth);
        }

        private Unit[,,] create_units(int width, int height, int depth)
        {
            Unit[,,] units = new Unit[width, height, depth];

            for (int d = 0; d < depth; d++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        units[x, y, d] = new Unit();
                    }
                }
            }

            return units;
        }

        public void resize(int width, int height)
        {
            Width = width;

            Height = height;

            Units = create_units(width, height, Depth);
        }

        protected void clear_gradients_of_units()
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Units[x, y, d].clear_gradient();
                    }
                }
            }
        }
        
        protected void set_value_at_inport(int x, int y, int depth, double value)
        {
            Units[x, y, depth].value_at_inport = value;
        }
        
        public void rise(double scale = 1)
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Units[x, y, d].rise(scale);
                    }
                }
            }
        }

        public void diff_rise(double scale = 1)
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Units[x, y, d].diff_rise(scale);
                    }
                }
            }
        }

        public void ReLU()
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Units[x, y, d].ReLU();
                    }
                }
            }
        }

        public void diff_ReLU()
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Units[x, y, d].diff_ReLU();
                    }
                }
            }
        }

        public void sigmoid(double scale = 1)
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Units[x, y, d].sigmoid(scale);
                    }
                }
            }
        }

        public void diff_sigmoid(double scale = 1)
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Units[x, y, d].diff_sigmoid(scale);
                    }
                }
            }
        }

        public void tanh(double scale = 1)
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Units[x, y, d].tanh(scale);
                    }
                }
            }
        }

        public void diff_tanh(double scale = 1)
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Units[x, y, d].diff_tanh(scale);
                    }
                }
            }
        }

        public void atan(double scale = 1)
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Units[x, y, d].atan(scale);
                    }
                }
            }
        }

        public void diff_atan(double scale = 1)
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Units[x, y, d].diff_atan(scale);
                    }
                }
            }
        }

        public void ELU(double alpha = 0.1)
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Units[x, y, d].ELU(alpha);
                    }
                }
            }
        }

        public void diff_ELU(double alpha = 0.1)
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Units[x, y, d].diff_ELU(alpha);
                    }
                }
            }
        }

        public void SELU(double scale = 1.0507009873554804934193349852946, double alpha = 1.6732632423543772848170429916717)
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Units[x, y, d].SELU(scale, alpha);
                    }
                }
            }
        }

        public void diff_SELU(double scale = 1.0507009873554804934193349852946, double alpha = 1.6732632423543772848170429916717)
        {
            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Units[x, y, d].diff_SELU(scale, alpha);
                    }
                }
            }
        }
    }
}
