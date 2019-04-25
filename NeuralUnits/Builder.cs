using System.Collections.Generic;
using System.IO;

namespace NeuralUnits
{
    public class Builder
    {
        private Dictionary<string, UnitLayer> UnitLayers = new Dictionary<string, UnitLayer>();
        
        public UnitLayer this[string name]
        {
            get
            {
                return UnitLayers[name];
            }
        }

        public void clear_all_gradients_of_last_training()
        {
            foreach (UnitLayer layer in UnitLayers.Values)
            {
                layer.clear_gradients();
            }
        }

        public void create_unit_layer(string id, int width, int height, int depth)
        {
            UnitLayer layer = new UnitLayer(id, width, height, depth);

            UnitLayers.Add(id, layer);
        }

        public void save(string file_path_name)
        {
            FileStream stream = new FileStream(file_path_name, FileMode.Create);

            BinaryWriter writer = new BinaryWriter(stream);

            WeightLayerPool.save(writer);

            writer.Write(UnitLayers.Count);

            foreach (UnitLayer layer in UnitLayers.Values)
            {
                writer.Write(layer.Id);

                writer.Write(layer.Depth);

                writer.Write(layer.Width);

                writer.Write(layer.Height);
            }

            writer.Close();

            stream.Close();
        }

        public void load(string file_path_name)
        {
            FileStream stream = new FileStream(file_path_name, FileMode.Open);

            BinaryReader reader = new BinaryReader(stream);

            WeightLayerPool.load(reader);

            int count = reader.ReadInt32();

            for (int i = 0; i < count; i++)
            {
                string id = reader.ReadString();

                int depth = reader.ReadInt32();

                int width = reader.ReadInt32();

                int height = reader.ReadInt32();

                UnitLayer layer = new UnitLayer(id, width, height, depth);

                UnitLayers.Add(id, layer);
            }

            reader.Close();

            stream.Close();
        }

        public double[] get_location_at_forward_convolution(UnitLayer units, int width_of_kernel, int height_of_kernel, int stride, double x, double y)
        {
            int width_of_layer = units.Width;

            int height_of_layer = units.Height;

            int width = (width_of_layer - width_of_kernel) / stride + 1;

            int height = (height_of_layer - height_of_kernel) / stride + 1;

            x = x * width / width_of_layer;

            y = y * height / height_of_layer;

            return new double[] { x, y };
        }

        public double[] get_location_at_backward_convolution(UnitLayer units, int width_of_kernel, int height_of_kernel, int stride, double x, double y)
        {
            int width_of_layer = units.Width;

            int height_of_layer = units.Height;

            int width = (width_of_layer - 1) * stride + width_of_kernel;

            int height = (height_of_layer - 1) * stride + height_of_kernel;

            x = x * width / width_of_layer;

            y = y * height / height_of_layer;

            return new double[] { x, y };
        }

        public void scale_learning_rate(double scale)
        {
            double learning_scale = Config.NETWORK_LEARNING_SCALE;

            learning_scale *= scale;

            if (learning_scale > 10)
            {
                learning_scale = 10;
            }
            else if (learning_scale < 0.1)
            {
                learning_scale = 0.1;
            }

            Config.NETWORK_LEARNING_SCALE = learning_scale;
        }

        public double get_learning_rate_scale()
        {
            return Config.NETWORK_LEARNING_SCALE;
        }

        public double[] report_learning_rate()
        {
            double[] learning_rate = new double[3];

            learning_rate[0] = Config.LEARNING_RATE * Config.NETWORK_LEARNING_SCALE;

            learning_rate[1] = Config.LEARNING_RATE;

            learning_rate[2] = Config.NETWORK_LEARNING_SCALE;

            return learning_rate;
        }

        public void use_momentum_to_update_weights(double learning_rate, double attenuating_rate)
        {
            Config.SGD_TYPE = 0;

            Config.LEARNING_RATE = learning_rate;

            Config.ATTENUATING_RATE_OF_LEARNING_RATE = attenuating_rate;
        }

        public void use_adam_to_update_weights(double learning_rate = 0.001, double B1 = 0.9, double B2 = 0.999, double E = 10e-8)
        {
            Config.SGD_TYPE = 1;

            Config.LEARNING_RATE = learning_rate;

            Config.ADAM_B1 = B1;

            Config.ADAM_B2 = B2;

            Config.ADAM_E = E;
        }
    }
}
