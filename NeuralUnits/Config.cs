using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralUnits
{
    class Config
    {
        static public double LEARNING_RATE_ATTENUATING_RATE = 0;

        static public double LEARNING_RATE = 0.01;

        static public double ATTENUATING_RATE_OF_LEARNING_RATE = 0.9;

        static public double NETWORK_LEARNING_SCALE = 1;

        static public int SGD_TYPE = 0;

        static public double ADAM_B1;

        static public double ADAM_B2;

        static public double ADAM_E;

        static public int MAX_NUMBER_OF_TASKS = 8; //最多8线程，避免task太多，建立时间大于运算时间

        static public int MIN_LENGTH_OF_TASKS = 1;//长度至少为1
    }
}
