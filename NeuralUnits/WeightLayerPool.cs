using System.Collections.Generic;
using System.IO;

namespace NeuralUnits
{
    public class WeightLayerPool
    {
        static private Dictionary<string, WeightLayer> WeightLayers = new Dictionary<string, WeightLayer>();

        static public WeightLayer find(string from, string to, int d)
        {
            string id = string.Format("C/{0}/{1}/{2}", from, to, d);

            return WeightLayers[id];
        }

        static public WeightLayer find(string from, string to, int x, int y, int d)
        {
            string id = string.Format("F/{0}/{1}/{2}/{3}/{4}", from, to, x, y, d);

            return WeightLayers[id];
        }

        static public void clear_gradients_of_weights()
        {
            foreach (WeightLayer layer in WeightLayers.Values)
            {
                layer.clear_gradients();
            }
        }
        
        static public void create_fully_connection(UnitLayer from, UnitLayer to)
        {
            for (int d = 0; d < to.Depth; d++)
            {
                for (int y = 0; y < to.Height; y++)
                {
                    for (int x = 0; x < to.Width; x++)
                    {
                        string id = string.Format("F/{0}/{1}/{2}/{3}/{4}", from.Id, to.Id, x, y, d);

                        WeightLayer layer = new WeightLayer(id, from.Width, from.Height, from.Depth);

                        layer.fill_weights(from.Width * from.Height * from.Depth);

                        WeightLayers.Add(id, layer);
                    }
                }
            }
        }

        static public void create_convolution(UnitLayer from, UnitLayer to, int width, int height)
        {
            for (int d = 0; d < to.Depth; d++)
            {
                string id = string.Format("C/{0}/{1}/{2}", from.Id, to.Id, d);
                
                WeightLayer layer = new WeightLayer(id, width, height, from.Depth);

                layer.fill_weights(from.Width * from.Height * from.Depth);

                WeightLayers.Add(id, layer);
            }
        }

        static public void save(BinaryWriter writer)
        {
            writer.Write(WeightLayers.Count);

            foreach (WeightLayer layer in WeightLayers.Values)
            {
                writer.Write(layer.Id);

                writer.Write(layer.Depth);

                writer.Write(layer.Width);

                writer.Write(layer.Height);

                layer.save(writer);
            }
        }

        static public void load(BinaryReader reader)
        {
            int count = reader.ReadInt32();

            for (int i = 0; i < count; i++)
            {
                string id = reader.ReadString();

                int depth = reader.ReadInt32();

                int width = reader.ReadInt32();

                int height = reader.ReadInt32();

                WeightLayer layer = new WeightLayer(id, width, height, depth);

                WeightLayers.Add(id, layer);

                layer.load(reader);
            }
        }
    }
}
