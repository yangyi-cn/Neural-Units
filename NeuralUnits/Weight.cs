using System;

namespace NeuralUnits
{
    class Weight
    {
        public double value, gradient;

        private double M = 0, V = 0;

        //更新次数
        private int T = 0;

        public void diff()
        {
            T++;

            if (Config.SGD_TYPE == 0)
            {
                momentum();
            }
            else
            {
                adam();
            }
        }

        private void momentum()
        {
            double learning_rate = Config.LEARNING_RATE * Config.NETWORK_LEARNING_SCALE;

            V = V * Config.ATTENUATING_RATE_OF_LEARNING_RATE + learning_rate * gradient;

            value = value + V;
        }

        private void adam()
        {
            //估计
            double current_M = Config.ADAM_B1 * M + (1 - Config.ADAM_B1) * gradient;

            double current_V = Config.ADAM_B2 * V + (1 - Config.ADAM_B2) * Math.Pow(gradient, 2);

            //矫正
            current_M = current_M / (1 - Math.Pow(Config.ADAM_B1, T));

            current_V = current_V / (1 - Math.Pow(Config.ADAM_B2, T));

            //更新
            double learning_rate = Config.LEARNING_RATE * Config.NETWORK_LEARNING_SCALE;

            value = value - (learning_rate * current_M) / Math.Sqrt(current_V + Config.ADAM_E);

            //记录参数
            M = current_M;

            V = current_V;
        }
    }
}
