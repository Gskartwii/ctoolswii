using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Chadsoft.CTools.Models
{
    public class Model
    {
        public Collection<Polygon> Polygons { get; private set; }
        public Collection<Material> Materials { get; private set; }
        public Collection<Bone> Bones { get; private set; }
        public Collection<ModelRenderInstruction> Instructions { get; private set; }

        public Model()
        {
            Polygons = new Collection<Polygon>();
            Materials = new Collection<Material>();
            Bones = new Collection<Bone>();
            Instructions = new Collection<ModelRenderInstruction>();
        }
        
    }

    public class ModelRenderInstruction
    {
        public int Polygon { get; set; }
        public int Material { get; set; }
    }
}
