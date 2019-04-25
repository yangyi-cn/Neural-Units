using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralUnits
{
    public class Learning_Rate_Adapter
    {
        private Builder builder;

        private int batch, index;

        private double previous_loss, current_loss;

        private double Upper_Bound_Of_no_change, Lower_Bound_Of_no_change;

        private double Upper_Bound_Of_increasing, Lower_Bound_Of_decreasing;

        private double scale_of_no_change, scale_of_increasing, scale_of_decreasing;

        public Learning_Rate_Adapter(Builder builder, int batch)
        {
            this.builder = builder;

            this.batch = batch;
        }

        public void input_loss(double loss)
        {
            if (index < batch)
            {
                current_loss += loss;

                index++;
            }
            else
            {
                current_loss /= batch;

                if (previous_loss > 0)
                {
                    double change = (current_loss - previous_loss) / previous_loss;

                    if (change >= Lower_Bound_Of_no_change && change <= Upper_Bound_Of_no_change)
                    {
                        builder.scale_learning_rate(scale_of_no_change);
                    }
                    else
                    {
                        if (change < Lower_Bound_Of_decreasing)
                        {
                            builder.scale_learning_rate(scale_of_decreasing);
                        }
                        else
                        {
                            if (change > Upper_Bound_Of_increasing)
                            {
                                builder.scale_learning_rate(scale_of_increasing);
                            }
                            else
                            {
                                //不在任何区间内，逐步趋近到1
                                double scale = builder.get_learning_rate_scale();
                                
                                if (scale < 1)
                                {
                                    builder.scale_learning_rate(1.1);
                                }
                                else if (scale > 1)
                                {
                                    builder.scale_learning_rate(0.9);
                                }
                            }
                        }
                    }
                }

                previous_loss = current_loss;

                current_loss = 0;

                index = 0;
            }
        }

        public void config_for_no_change_in_loss(double upper_bound, double lower_bound, double scale)
        {
            Upper_Bound_Of_no_change = upper_bound;

            Lower_Bound_Of_no_change = lower_bound;

            scale_of_no_change = scale;
        }

        public void config_for_increasing_loss(double upper_bound, double scale)
        {
            Upper_Bound_Of_increasing = upper_bound;

            scale_of_increasing = scale;
        }

        public void config_for_decreasing_loss(double lower_bound, double scale)
        {
            Lower_Bound_Of_decreasing = lower_bound;

            scale_of_decreasing = scale;
        }
    }
}
