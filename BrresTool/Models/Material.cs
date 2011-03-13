using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chadsoft.CTools.Image;
using System.Collections.ObjectModel;

namespace Chadsoft.CTools.Models
{
    public class Material
    {
        public string PixelShaderSource { get; set; }

        public Collection<ImageData> Textures { get; private set; }
        public Collection<Matrix4x3> TextureMatricies { get; private set; }
        public Collection<MaterialTextureAddress> UCoords { get; private set; }
        public Collection<MaterialTextureAddress> VCoords { get; private set; }
        public bool EnableAlphaBlend { get; set; }
        public AlphaBlendSource AlphaBlendSource { get; set; }
        public AlphaBlendDestination AlphaBlendDestination { get; set; }
        public bool EnableAlphaTest { get; set; }
        public byte AlphaTestValue { get; set; }
        public AlphaTestFunction AlphaTestFunction { get; set; }

        public Material()
        {
            Textures = new Collection<ImageData>();
            TextureMatricies = new Collection<Matrix4x3>();
            UCoords = new Collection<MaterialTextureAddress>();
            VCoords = new Collection<MaterialTextureAddress>();
        }
    }

    public enum MaterialTextureAddress
    {
        Clamp,
        Repeat,
        Mirror,        
    }


    public enum AlphaBlendSource
    {
        Zero,
        One,
        DestColour,
        InvDestColour,
        SrcAlpha,
        InvSrcAlpha,
        DestAplha,
        InvDestAlpha,
    }

    public enum AlphaBlendDestination
    {
        Zero,
        One,
        SrcColour,
        InvSrcColour,
        SrcAlpha,
        InvSrcAlpha,
        DestAplha,
        InvDestAlpha,
    }

    public enum AlphaTestFunction
    {
        Never,
        Less,
        Equal,
        LessEqual,
        Greater,
        NotEqual,
        GreaterEqual,
        Always,
    }
}
