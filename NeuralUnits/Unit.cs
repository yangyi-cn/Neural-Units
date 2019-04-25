using System;

namespace NeuralUnits
{
    public class Unit
    {
        public double value_at_inport, value_at_outport;

        public double gradient_at_inport, gradient_at_outport;

        //不再使用可学习的激活函数
        //public double PReLU_parameter = 0.1, PReLU_velocity = 0;

        public void clear_gradient()
        {
            value_at_inport = 0;

            value_at_outport = 0;

            gradient_at_inport = 0;

            gradient_at_outport = 0;
        }

        public void rise(double scale)
        {
            value_at_outport = value_at_inport * scale;
        }

        public void diff_rise(double scale)
        {
            gradient_at_inport = gradient_at_outport * scale;
        }

        public void ReLU()
        {
            if (value_at_inport > 0)
            {
                value_at_outport = value_at_inport;
            }
            else
            {
                value_at_outport = 0;
            }
        }

        public void diff_ReLU()
        {
            if (value_at_inport > 0)
            {
                gradient_at_inport = gradient_at_outport;
            }
            else
            {
                gradient_at_inport = 0;
            }
        }

        /*
        public void PReLU()
        {
            if (value_at_inport > 0)
            {
                value_at_outport = value_at_inport;
            }
            else
            {
                value_at_outport = value_at_inport * PReLU_parameter;
            }
        }

        public void diff_PReLU()
        {
            if (value_at_inport > 0)
            {
                gradient_at_inport = gradient_at_outport;
            }
            else
            {
                gradient_at_inport = gradient_at_outport * PReLU_parameter;

                PReLU_velocity = PReLU_velocity * Config.PReLU_ATTENUATING_RATE + Config.PReLU_LEARNING_RATE * gradient_at_outport * value_at_inport;

                PReLU_parameter += PReLU_velocity;
            }
        }*/

        public void sigmoid(double scale = 1)
        {
            double x = value_at_inport * scale;

            double exp = Math.Exp(-x);

            value_at_outport = 1 / (1 + exp);
        }

        public void diff_sigmoid(double scale = 1)
        {
            double derivative = value_at_outport * (1 - value_at_outport);

            derivative *= scale;

            gradient_at_inport = gradient_at_outport * derivative;
        }

        public void tanh(double scale = 1)
        {
            double x = value_at_inport * scale;

            double exp = Math.Exp(-x);

            value_at_outport = (1 - exp) / (1 + exp);
        }

        public void diff_tanh(double scale = 1)
        {
            double x = value_at_inport * scale;

            double exp = Math.Exp(-x);

            double derivative = exp * scale / (exp + 1) + exp * scale / Math.Pow(exp + 1, 2) + Math.Pow(exp, 2) * scale / Math.Pow(exp + 1, 2);

            gradient_at_inport = gradient_at_outport * derivative;
        }

        public void atan(double scale = 1)
        {
            double half_of_PI = Math.PI * 0.5;

            double x = value_at_inport * scale;

            value_at_outport = (Math.Atan(x) / half_of_PI + 1) * 0.5;
        }

        public void diff_atan(double scale = 1)
        {
            double half_of_PI = Math.PI * 0.5;

            double x = value_at_inport;

            double derivative = 0.5 * scale / (half_of_PI + half_of_PI * Math.Pow(value_at_inport, 2) * Math.Pow(scale, 2));

            gradient_at_inport = gradient_at_outport * derivative;
        }

        public void softsign()
        {
            double x = value_at_inport;

            value_at_outport = x / (1 + Math.Abs(x));
        }

        public void diff_softsign()
        {
            double x = value_at_inport;

            double derivative = 1 / Math.Pow(1 + Math.Abs(x), 2);

            gradient_at_inport = gradient_at_outport * derivative;
        }

        public void ELU(double alpha)
        {
            if (value_at_inport >= 0)
            {
                value_at_outport = value_at_inport;
            }
            else
            {
                value_at_outport = alpha * (Math.Exp(value_at_inport) - 1);
            }
        }

        public void diff_ELU(double alpha)
        {
            if (value_at_inport > 0)
            {
                gradient_at_inport = gradient_at_outport;
            }
            else
            {
                gradient_at_inport = (value_at_outport + alpha) * gradient_at_outport;
            }
        }

        public void SELU(double scale, double alpha)
        {
            if (value_at_inport >= 0)
            {
                value_at_outport = scale * value_at_inport;
            }
            else
            {
                value_at_outport = scale * (alpha * Math.Exp(value_at_inport) - alpha);
            }
        }

        public void diff_SELU(double scale, double alpha)
        {
            if (value_at_inport > 0)
            {
                gradient_at_inport = scale * gradient_at_outport;
            }
            else
            {
                gradient_at_inport = scale * (value_at_outport + alpha) * gradient_at_outport;
            }
        }
    }
}
