using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralUnits
{
    abstract public class UnitPrinter : UnitCollection
    {
        public UnitPrinter(int width, int height, int depth) : base(width, height, depth)
        {
        }
        
        public double[] report_values_at_outport()
        {
            double MAX = double.MinValue, MIN = double.MaxValue;

            double sum = 0;

            int count = Width * Height * Depth;

            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        double value = Units[x, y, d].value_at_outport;

                        if (value > MAX)
                        {
                            MAX = value;
                        }

                        if (value < MIN)
                        {
                            MIN = value;
                        }

                        sum += value;
                    }
                }
            }

            return new double[3] { MAX, MIN, sum / count };
        }

        public double[] report_values_at_inport()
        {
            double MAX = double.MinValue, MIN = double.MaxValue;

            double sum = 0;

            int count = Width * Height * Depth;

            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        double value = Units[x, y, d].value_at_inport;

                        if (value > MAX)
                        {
                            MAX = value;
                        }

                        if (value < MIN)
                        {
                            MIN = value;
                        }

                        sum += value;
                    }
                }
            }

            return new double[3] { MAX, MIN, sum / count };
        }

        public double[] report_gradients_at_outport()
        {
            double MAX = double.MinValue, MIN = double.MaxValue;

            double sum = 0;

            int count = Width * Height * Depth;

            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        double value = Units[x, y, d].gradient_at_outport;

                        if (value > MAX)
                        {
                            MAX = value;
                        }

                        if (value < MIN)
                        {
                            MIN = value;
                        }

                        sum += value;
                    }
                }
            }

            return new double[3] { MAX, MIN, sum / count };
        }

        public double[] report_gradients_at_inport()
        {
            double MAX = double.MinValue, MIN = double.MaxValue;

            double sum = 0;

            int count = Width * Height * Depth;

            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        double value = Units[x, y, d].gradient_at_inport;

                        if (value > MAX)
                        {
                            MAX = value;
                        }

                        if (value < MIN)
                        {
                            MIN = value;
                        }

                        sum += value;
                    }
                }
            }

            return new double[3] { MAX, MIN, sum / count };
        }

        public double report_loss(double[,,] labels)
        {
            double error = 0;

            int count = Width * Height * Depth;

            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        error += Math.Pow(labels[x, y, d] - Units[x, y, d].value_at_outport, 2) / 2;
                    }
                }
            }

            return Math.Sqrt(error / count);
        }

        public double[] report_scores()
        {
            int index = 0;

            int count = Width * Height * Depth;

            double[] scores = new double[count];

            for (int d = 0; d < Depth; d++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        scores[index++] = Units[x, y, d].value_at_outport;
                    }
                }
            }

            return scores;
        }
    }
}
