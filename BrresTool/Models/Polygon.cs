using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;

namespace Chadsoft.CTools.Models
{
    public class Polygon
    {
        public Model Model { get; private set; }

        public PolygonVertexDataFormat VertexDataFormat { get; set; }
        public MemoryStream VertexData { get; private set; }
        public Collection<PolygonRenderInstruction> Instructions { get; private set; }

        public Polygon(Model model)
        {
            Model = model;
            Instructions = new Collection<PolygonRenderInstruction>();
            VertexData = new MemoryStream();
        }
    }

    public class PolygonRenderInstruction
    {
        public PolygonRenderInstructionCommand Command { get; set; }
        public int Count { get; set; }
        public int Index { get; set; }
        public int Destination { get; set; }
    }

    public enum PolygonRenderInstructionCommand
    {
        DrawQuads,
        DrawTriangles,
        DrawTriangleStrip,
        DrawTriangleFan,
        DrawLines,
        DrawLineStrip,
        DrawPoints,
        SetMatrix,
    }

    [Flags]
    public enum PolygonVertexDataFormat
    {
        None = 0,
        Position = 1,
        Normal = 2,

        Colour0 = 0, 
        Colour1 = 4,
        Colour2 = 8,
        ColourShift = 2,
        ColourMask = 12,

        Texture0 = 0,
        Texture1 = 16,
        Texture2 = 32,
        Texture3 = 48,
        Texture4 = 64,
        Texture5 = 80,
        Texture6 = 96,
        Texture7 = 112,
        Texture8 = 128,
        TextureShift = 4,
        TextureMask = Texture8 | Texture7,

        Transform = 256,

        //TexTransform0 = 0,
        //TexTransform1 = 512,
        //TexTransform2 = 1024,
        //TexTransform3 = 1536,
        //TexTransform4 = 2048,
        //TexTransform5 = 2560,
        //TexTransform6 = 3072,
        //TexTransform7 = 3584,
        //TexTransform8 = 4096,
        //TexTransformShift = 9,
        //TexTransformMask = TexTransform8 | TexTransform7,
    }
}
