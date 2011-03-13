using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using Chadsoft.CTools.Models;
using System.Globalization;

namespace Chadsoft.CTools.Brres
{
    public class Mdl0Material
    {
        public long Address { get; set; }

        public int Length { get; set; }
        public int Mdl0Offset { get; set; }
        public int NameOffset { get; set; }
        public int Index { get; set; }
        public int Unknown10 { get; set; }
        public int Unknown14 { get; set; }
        public int Unknown18 { get; set; }
        public int Unknown1C { get; set; }
        public int Unknown20 { get; set; }
        public int Unknown24 { get; set; }
        public int ShaderOffset { get; set; }
        public int LayerCount { get; set; }
        public int LayerOffset { get; set; }
        public int Unknown34 { get; set; }
        public int Unknown38 { get; set; }
        public int MaterialShaderOffset { get; set; }
        public int[] Unknown40 { get; private set; }
        public int LayerInformation { get; set; }
        public int Unknown1AC { get; set; }
        public Mdl0MaterialLayerClamp[] LayerClamps { get; private set; }
        public Mdl0MaterialLayerMatrix[] LayerMatricies { get; private set; }
        public int Unknown3F0 { get; set; }
        public Mdl0MaterialLayerColour[] LayerColours { get; private set; }

        public Collection<Mdl0MaterialLayer> Layers { get; private set; }
        public Mdl0MaterialShader MaterialShader { get; private set; }
        public string Name { get; set; }

        public Mdl0Shader Shader { get; set; }

        public Mdl0Material(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            Length = reader.ReadInt32();
            Mdl0Offset = reader.ReadInt32();
            NameOffset = reader.ReadInt32();
            Index = reader.ReadInt32();
            Unknown10 = reader.ReadInt32();
            Unknown14 = reader.ReadInt32();
            Unknown18 = reader.ReadInt32();
            Unknown1C = reader.ReadInt32();
            Unknown20 = reader.ReadInt32();
            Unknown24 = reader.ReadInt32();
            ShaderOffset = reader.ReadInt32();
            LayerCount = reader.ReadInt32();
            LayerOffset = reader.ReadInt32();
            Unknown34 = reader.ReadInt32();
            Unknown38 = reader.ReadInt32();
            MaterialShaderOffset = reader.ReadInt32();
            Unknown40 = reader.ReadInt32s(0x5a);
            LayerInformation = reader.ReadInt32();
            Unknown1AC = reader.ReadInt32();

            LayerClamps = new Mdl0MaterialLayerClamp[8];
            for (int i = 0; i < LayerClamps.Length; i++)
                LayerClamps[i] = new Mdl0MaterialLayerClamp(reader);

            LayerMatricies = new Mdl0MaterialLayerMatrix[8];
            for (int i = 0; i < LayerMatricies.Length; i++)
                LayerMatricies[i] = new Mdl0MaterialLayerMatrix(reader);

            Unknown3F0 = reader.ReadInt32();
            LayerColours = new Mdl0MaterialLayerColour[2];
            for (int i = 0; i < LayerColours.Length; i++)
                LayerColours[i] = new Mdl0MaterialLayerColour(reader);

            BrresFile.SafeSeek(reader, Address + LayerOffset, 0x34 * LayerCount); 
            Layers = new Collection<Mdl0MaterialLayer>();

            for (int i = 0; i < LayerCount; i++)
                Layers.Add(new Mdl0MaterialLayer(reader));

            BrresFile.SafeSeek(reader, Address + MaterialShaderOffset, 0x180); 
            MaterialShader = new Mdl0MaterialShader(reader);

            for (int i = 0; i < Layers.Count; i++)
                Layers[i].Name = BrresFile.ReadBrresString(reader, Layers[i].Address + Layers[i].NameOffset);

            Name = BrresFile.ReadBrresString(reader, Address + NameOffset);
        }

        public void Write(EndianBinaryWriter writer, long mdl0Address)
        {
            Address = writer.BaseStream.Position;

            Mdl0Offset = (int)(mdl0Address - Address);
            ShaderOffset = (int)(Shader.Address - Address);
            LayerCount = Layers.Count;

            writer.Write(Length);
            writer.Write(Mdl0Offset);
            writer.Write(NameOffset);
            writer.Write(Index);
            writer.Write(Unknown10);
            writer.Write(Unknown14);
            writer.Write(Unknown18);
            writer.Write(Unknown1C);
            writer.Write(Unknown20);
            writer.Write(Unknown24);
            writer.Write(ShaderOffset);
            writer.Write(LayerCount);
            writer.Write(LayerOffset);
            writer.Write(Unknown34);
            writer.Write(Unknown38);
            writer.Write(MaterialShaderOffset);
            writer.Write(Unknown40, 0, Unknown40.Length);
            writer.Write(LayerInformation);
            writer.Write(Unknown1AC);

            for (int i = 0; i < LayerClamps.Length; i++)
                LayerClamps[i].Write(writer);
            for (int i = 0; i < LayerMatricies.Length; i++)
                LayerMatricies[i].Write(writer);
            writer.Write(Unknown3F0);
            for (int i = 0; i < LayerColours.Length; i++)
                LayerColours[i].Write(writer);

            writer.WritePadding(0x8, 0);
            
            LayerOffset = (int)(writer.BaseStream.Position - Address);
            for (int i = 0; i < LayerCount; i++)
                Layers[i].Write(writer);
            
            writer.WritePadding(0x20, 0);
            MaterialShaderOffset = (int)(writer.BaseStream.Position - Address);
            MaterialShader.Write(writer);

            writer.WritePadding(0x20, 0);

            Length = (int)(writer.BaseStream.Position - Address);
        }

        public void WriteNames(EndianBinaryWriter writer, Dictionary<string, long> names)
        {
            NameOffset = (int)(BrresFile.WriteString(writer, names, Name) - Address);

            for (int i = 0; i < LayerCount; i++)
                Layers[i].WriteNames(writer, names);
        }

        public Material ToMaterial(BrresFile brres)
        {
            Material material;
            Tex0Section tex0;
            ShaderDescription shaderDescription;

            material = new Material();

            for (int i = 0; i < LayerMatricies.Length; i++)
                material.TextureMatricies.Add(LayerMatricies[i].Matrix);

            for (int i = 0; i < Layers.Count; i++)
            {
                tex0 = brres.GetTextureByName(Layers[i].Name);
                material.Textures.Add(tex0 == null ? null : tex0.GenerateImage());
                material.UCoords.Add((MaterialTextureAddress)Layers[i].TextureAddressU);
                material.VCoords.Add((MaterialTextureAddress)Layers[i].TextureAddressV);
            }
            
            shaderDescription = new ShaderDescription(MaterialShader.Mode, MaterialShader.TevRegs, MaterialShader.TextureMatricies, MaterialShader.TextureTransformations, Shader.Shader);
            material.PixelShaderSource = GenerateShader(shaderDescription);
            material.EnableAlphaBlend = shaderDescription.EnableAlphaBlend;
            material.AlphaBlendDestination = shaderDescription.AlphaDestination;
            material.AlphaBlendSource = shaderDescription.AlphaSource;
            material.EnableAlphaTest = shaderDescription.EnableAlphaTest;
            material.AlphaTestFunction = shaderDescription.AlphaTestFunction;
            material.AlphaTestValue = shaderDescription.AlphaTestValue;

            return material;
        }

        private string GenerateShader(ShaderDescription shaderDescription)
        {
            string shader;

            shader = shaderDescription.ToShader();
            
            return string.Format(CultureInfo.InvariantCulture, Rendering.ShaderResources.PixelShader.Replace("{", "{{").Replace("}", "}}").Replace("//DATAHERE", "{0}"), shader);
        }

        private class ShaderDescription
        {
            static char[] swapLetter = new char[] { 'x', 'y', 'z', 'w' };

            public bool EnableAlphaBlend;
            public AlphaBlendSource AlphaSource;
            public AlphaBlendDestination AlphaDestination;
            public BlendDescription[] Blends;
            public AlphaBlendDescription[] AlphaBlends;
            public ConstantTerm[] ColourTerms;
            public ConstantTerm[] AlphaTerms;
            public float[] KValues;
            public char[][] Swaps;
            public RasValue[] RasValues;
            public bool[] EnableTextures;
            public bool EnableAlphaTest;
            public byte AlphaTestValue;
            public AlphaTestFunction AlphaTestFunction;

            private byte[] bptemp;

            public ShaderDescription(params byte[][] codes)
            {
                bptemp = new byte[0xff * 3]; bptemp[0xfe * 3] = bptemp[0xfe * 3 + 1] = bptemp[0xfe * 3 + 2] = 0xff;
                Blends = new BlendDescription[8];
                AlphaBlends = new AlphaBlendDescription[8];
                RasValues = new RasValue[8];
                EnableTextures = new bool[8];
                ColourTerms = new ConstantTerm[16];
                AlphaTerms = new ConstantTerm[16];
                KValues = new float[16];
                Swaps = new char[4][];

                for (int i = 0; i < Swaps.Length; i++)
                {
                    Swaps[i] = new char[4];
                    Array.Copy(swapLetter, Swaps[i], 4);
                }

                for (int i = 0; i < codes.Length; i++)
                    ExecuteCode(codes[i]);

                bptemp = null;
            }

            private void ExecuteCode(byte[] shaderCode)
            {
                int location;
                byte instruction, address, current;

                location = 0;

                while (location < shaderCode.Length)
                {
                    switch (instruction = shaderCode[location++])
                    {
                        case 0x61:
                            address  = shaderCode[location++];

                            for (int j = 0; j < 3; j++)
                                shaderCode[location + j] = (byte)(shaderCode[location + j] & bptemp[0xfe * 3 + j] | bptemp[address * 3 + j] & ~bptemp[0xfe * 3 + j]);

                            switch (address)
	                        {
                                case 0x28:
                                case 0x29:
                                case 0x2a:
                                case 0x2b:
                                case 0x2c:
                                case 0x2d:
                                case 0x2e:
                                case 0x2f:
                                    current = shaderCode[location++];
                                    RasValues[address << 1 & 0xe] =(RasValue)(current >> 3 & 0x7);
                                    EnableTextures[address << 1 & 0xe] = (current >> 2 & 0x1) == 1;
                                    current = shaderCode[location++];
                                    RasValues[address << 1 & 0xe + 1] = (RasValue)(current << 1 & 0x6 | (current = shaderCode[location++]) >> 7 & 0x1);
                                    EnableTextures[address << 1 & 0xe + 1] = (current >> 6 & 0x1) == 1;
                                    break;
                                case 0x41:
                                    location++;
                                    current = shaderCode[location++];
                                    AlphaSource = (AlphaBlendSource)(current >> 4 & 0x7);
                                    AlphaDestination = (AlphaBlendDestination)(current >> 1 & 0x7);
                                    EnableAlphaBlend = (shaderCode[location++] & 0x1) == 1;
                                    break;
                                case 0xc0:
                                case 0xc2:
                                case 0xc4:
                                case 0xc6:
                                case 0xc8:
                                case 0xca:
                                case 0xcc:
                                case 0xce:
                                    Blends[address >> 1 & 0x7] = new BlendDescription(shaderCode, location);
                                    location += 3;
                                    break;
                                case 0xc1:
                                case 0xc3:
                                case 0xc5:
                                case 0xc7:
                                case 0xc9:
                                case 0xcb:
                                case 0xcd:
                                case 0xcf:
                                    AlphaBlends[address >> 1 & 0x7] = new AlphaBlendDescription(shaderCode, location);
                                    location += 3;
                                    break;
                                case 0xe0:
                                case 0xe1:
                                case 0xe2:
                                case 0xe3:
                                case 0xe4:
                                case 0xe5:
                                case 0xe6:
                                case 0xe7:
                                    KValues[(address - 0xe0) * 2 + 1] = (shaderCode[location++] << 4 & 0x7f0 | shaderCode[location] >> 4 & 0xf) / 2047.0f;
                                    KValues[(address - 0xe0) * 2 + 0] = (shaderCode[location++] << 8 & 0x700 | shaderCode[location++]) / 2047.0f;
                                    break;
                                case 0xf3:
                                    current = shaderCode[location++];
                                    AlphaTestFunction = (AlphaTestFunction)(current & 0x7);
                                    location++;
                                    AlphaTestValue = shaderCode[location++];
                                    EnableAlphaTest = true;
                                    break;
                                case 0xf6:
                                case 0xf7:
                                case 0xf8:
                                case 0xf9:
                                case 0xfa:
                                case 0xfb:
                                case 0xfc:
                                case 0xfd:
                                    current = shaderCode[location++];

                                    AlphaTerms[(address - 0xf6) * 2 + 1] = (ConstantTerm)(current >> 3 & 0x1f);
                                    ColourTerms[(address - 0xf6) * 2 + 1] = (ConstantTerm)(current << 2 & 0x1c | (current = shaderCode[location++]) >> 6 & 0x3);
                                    AlphaTerms[(address - 0xf6) * 2] = (ConstantTerm)(current >> 1 & 0x1f);
                                    ColourTerms[(address - 0xf6) * 2] = (ConstantTerm)(current << 4 & 0x10 | (current = shaderCode[location++]) >> 4 & 0xf);
                                    Swaps[(address - 0xf6) / 2][1 + (address << 1 & 0x2)] = swapLetter[current >> 2 & 0x3];
                                    Swaps[(address - 0xf6) / 2][0 + (address << 1 & 0x2)] = swapLetter[current & 0x3];
                                    break;
                                default:
                                    location += 3;
                                    break;
	                        }

                            Array.Copy(shaderCode, location - 3, bptemp, 3 * address, 3);

                            if (address != 0xfe)
                                bptemp[0xfe * 3] = bptemp[0xfe * 3 + 1] = bptemp[0xfe * 3 + 2] = 0xff;
                            break;
                        default:
                            break;
                    }
                }
            }

            public string ToShader()
            {
                string result;

                result = "";
                for (int i = 0; i < Blends.Length; i++)
                {
                    if (Blends[i] != null)
                    {
                        result += "\t" + WriteRas(i);
                        //if (EnableTextures[i])
                        result += "\t" + WriteTex(i);
                        result += "\t" + WriteConst(i);
                        result += "\t" + Blends[i].ToShader();
                        result += "\t" + AlphaBlends[i].ToShader();
                    }
                }

                return result;
            }

            private string WriteConst(int i)
            {
                string result;

                result = "konst = float4(";
                switch (ColourTerms[i])
                {
                    case ConstantTerm.One:
                        result += "1, 1, 1, ";
                        break;
                    case ConstantTerm.SevenEigths:
                        result += "0.875f, 0.875f, 0.875f, ";
                        break;
                    case ConstantTerm.ThreeQuaters:
                        result += "0.75f, 0.75f, 0.75f, ";
                        break;
                    case ConstantTerm.FiveEights:
                        result += "0.625f, 0.625f, 0.625f, ";
                        break;
                    case ConstantTerm.Half:
                        result += "0.5f, 0.5f, 0.5f, ";
                        break;
                    case ConstantTerm.ThreeEights:
                        result += "0.375f, 0.375f, 0.375f, ";
                        break;
                    case ConstantTerm.Quater:
                        result += "0.25f, 0.25f, 0.25f, ";
                        break;
                    case ConstantTerm.Eight:
                        result += "0.125f, 0.125f, 0.125f, ";
                        break;
                    case ConstantTerm.Zero:
                        result += "0, 0, 0, ";
                        break;
                    case ConstantTerm.K0:
                        result += KValues[0].ToString("0.0#") + "f, ";
                        result += KValues[2].ToString("0.0#") + "f, ";
                        result += KValues[3].ToString("0.0#") + "f, ";
                        break;
                    case ConstantTerm.K1:
                        result += KValues[4].ToString("0.0#") + "f, ";
                        result += KValues[6].ToString("0.0#") + "f, ";
                        result += KValues[7].ToString("0.0#") + "f, ";
                        break;
                    case ConstantTerm.K2:
                        result += KValues[8].ToString("0.0#") + "f, ";
                        result += KValues[10].ToString("0.0#") + "f, ";
                        result += KValues[11].ToString("0.0#") + "f, ";
                        break;
                    case ConstantTerm.K3:
                        result += KValues[12].ToString("0.0#") + "f, ";
                        result += KValues[14].ToString("0.0#") + "f, ";
                        result += KValues[15].ToString("0.0#") + "f, ";
                        break;
                    case ConstantTerm.K0Red:
                        result += KValues[0].ToString("0.0#") + "f, ";
                        result += KValues[0].ToString("0.0#") + "f, ";
                        result += KValues[0].ToString("0.0#") + "f, ";
                        break;
                    case ConstantTerm.K1Red:
                        result += KValues[4].ToString("0.0#") + "f, ";
                        result += KValues[4].ToString("0.0#") + "f, ";
                        result += KValues[4].ToString("0.0#") + "f, ";
                        break;
                    case ConstantTerm.K2Red:
                        result += KValues[8].ToString("0.0#") + "f, ";
                        result += KValues[8].ToString("0.0#") + "f, ";
                        result += KValues[8].ToString("0.0#") + "f, ";
                        break;
                    case ConstantTerm.K3Red:
                        result += KValues[12].ToString("0.0#") + "f, ";
                        result += KValues[12].ToString("0.0#") + "f, ";
                        result += KValues[12].ToString("0.0#") + "f, ";
                        break;
                    case ConstantTerm.K0Green:
                        result += KValues[2].ToString("0.0#") + "f, ";
                        result += KValues[2].ToString("0.0#") + "f, ";
                        result += KValues[2].ToString("0.0#") + "f, ";
                        break;
                    case ConstantTerm.K1Green:
                        result += KValues[6].ToString("0.0#") + "f, ";
                        result += KValues[6].ToString("0.0#") + "f, ";
                        result += KValues[6].ToString("0.0#") + "f, ";
                        break;
                    case ConstantTerm.K2Green:
                        result += KValues[10].ToString("0.0#") + "f, ";
                        result += KValues[10].ToString("0.0#") + "f, ";
                        result += KValues[10].ToString("0.0#") + "f, ";
                        break;
                    case ConstantTerm.K3Green:
                        result += KValues[14].ToString("0.0#") + "f, ";
                        result += KValues[14].ToString("0.0#") + "f, ";
                        result += KValues[14].ToString("0.0#") + "f, ";
                        break;
                    case ConstantTerm.K0Blue:
                        result += KValues[3].ToString("0.0#") + "f, ";
                        result += KValues[3].ToString("0.0#") + "f, ";
                        result += KValues[3].ToString("0.0#") + "f, ";
                        break;
                    case ConstantTerm.K1Blue:
                        result += KValues[7].ToString("0.0#") + "f, ";
                        result += KValues[7].ToString("0.0#") + "f, ";
                        result += KValues[7].ToString("0.0#") + "f, ";
                        break;
                    case ConstantTerm.K2Blue:
                        result += KValues[11].ToString("0.0#") + "f, ";
                        result += KValues[11].ToString("0.0#") + "f, ";
                        result += KValues[11].ToString("0.0#") + "f, ";
                        break;
                    case ConstantTerm.K3Blue:
                        result += KValues[15].ToString("0.0#") + "f, ";
                        result += KValues[15].ToString("0.0#") + "f, ";
                        result += KValues[15].ToString("0.0#") + "f, ";
                        break;
                    case ConstantTerm.K0Alpha:
                        result += KValues[1].ToString("0.0#") + "f, ";
                        result += KValues[1].ToString("0.0#") + "f, ";
                        result += KValues[1].ToString("0.0#") + "f, ";
                        break;
                    case ConstantTerm.K1Alpha:
                        result += KValues[5].ToString("0.0#") + "f, ";
                        result += KValues[5].ToString("0.0#") + "f, ";
                        result += KValues[5].ToString("0.0#") + "f, ";
                        break;
                    case ConstantTerm.K2Alpha:
                        result += KValues[9].ToString("0.0#") + "f, ";
                        result += KValues[9].ToString("0.0#") + "f, ";
                        result += KValues[9].ToString("0.0#") + "f, ";
                        break;
                    case ConstantTerm.K3Alpha:
                        result += KValues[13].ToString("0.0#") + "f, ";
                        result += KValues[13].ToString("0.0#") + "f, ";
                        result += KValues[13].ToString("0.0#") + "f, ";
                        break;
                    default:
                        return "error";
                }
                switch (AlphaTerms[i])
                {
                    case ConstantTerm.One:
                        return result + "1);\n";
                    case ConstantTerm.SevenEigths:
                        return result + "0.875f);\n";
                    case ConstantTerm.ThreeQuaters:
                        return result + "0.75f);\n";
                    case ConstantTerm.FiveEights:
                        return result + "0.625f);\n";
                    case ConstantTerm.Half:
                        return result + "0.5f);\n";
                    case ConstantTerm.ThreeEights:
                        return result + "0.375f);\n";
                    case ConstantTerm.Quater:
                        return result + "0.25f);\n";
                    case ConstantTerm.Eight:
                        return result + "0.125f);\n";
                    case ConstantTerm.Zero:
                        return result + "0);\n";
                    case ConstantTerm.K0Red:
                        return result + KValues[0].ToString("0.0#") + "f);\n";
                    case ConstantTerm.K1Red:
                        return result + KValues[4].ToString("0.0#") + "f);\n";
                    case ConstantTerm.K2Red:
                        return result + KValues[8].ToString("0.0#") + "f);\n";
                    case ConstantTerm.K3Red:
                        return result + KValues[12].ToString("0.0#") + "f);\n";
                    case ConstantTerm.K0Green:
                        return result + KValues[2].ToString("0.0#") + "f);\n";
                    case ConstantTerm.K1Green:
                        return result + KValues[6].ToString("0.0#") + "f);\n";
                    case ConstantTerm.K2Green:
                        return result + KValues[10].ToString("0.0#") + "f);\n";
                    case ConstantTerm.K3Green:
                        return result + KValues[14].ToString("0.0#") + "f);\n";
                    case ConstantTerm.K0Blue:
                        return result + KValues[3].ToString("0.0#") + "f);\n";
                    case ConstantTerm.K1Blue:
                        return result + KValues[7].ToString("0.0#") + "f);\n";
                    case ConstantTerm.K2Blue:
                        return result + KValues[11].ToString("0.0#") + "f);\n";
                    case ConstantTerm.K3Blue:
                        return result + KValues[15].ToString("0.0#") + "f);\n";
                    case ConstantTerm.K0Alpha:
                        return result + KValues[1].ToString("0.0#") + "f);\n";
                    case ConstantTerm.K1Alpha:
                        return result + KValues[5].ToString("0.0#") + "f);\n";
                    case ConstantTerm.K2Alpha:
                        return result + KValues[9].ToString("0.0#") + "f);\n";
                    case ConstantTerm.K3Alpha:
                        return result + KValues[13].ToString("0.0#") + "f);\n";
                }

                return "error";
            }

            private string WriteRas(int i)
            {
                switch (RasValues [i])
                {
                    case RasValue.Colour0:
                        return "ras = input.col0." + new string(Swaps[AlphaBlends[i].SwapR]) + ";\n";
                    case RasValue.Colour1:
                        return "ras = input.col1." + new string(Swaps[AlphaBlends[i].SwapR]) + ";\n";
                    case RasValue.AlphaBump:
                        // Incorrect
                        return "ras = 0;\n";
                    case RasValue.AlphaBumpCorrected:
                        // Incorrect
                        return "ras = 0;\n";
                    case RasValue.Zero:
                        return "ras = 0;\n";
                    default:
                        return "error";
                }
            }

            private string WriteTex(int i)
            {
                return "tex = tex2D(tex" + i.ToString() + "Samp, input.uv" + i.ToString() + ")." + new string(Swaps[AlphaBlends[i].SwapT]) + ";\n";
            }

            public class BlendDescription
            {
                public BlendDescription(byte[] shaderCode, int location)
                {
                    byte current;

                    current = shaderCode[location++];

                    Destination = (BlendDestination)(current >> 6 & 0x3);
                    switch (current >> 4 & 0x3)
                    {
                        case 0:
                            Multiplicand = 1.0f;
                            break;
                        case 1:
                            Multiplicand = 2.0f;
                            break;
                        case 2:
                            Multiplicand = 4.0f;
                            break;
                        case 3:
                            Multiplicand = 0.5f;
                            break;
                    }
                    Clamp = (current >> 3 & 0x1) == 1;
                    Operation = (BlendOperation)(current >> 2 & 0x1);
                    switch (current & 0x3)
                    {
                        case 0:
                            Bias = 0.0f;
                            break;
                        case 1:
                            Bias = 0.5f;
                            break;
                        case 2:
                            Bias = -0.5f;
                            break;
                        case 3:
                            // Incorrect, actually much more complicated.
                            Bias = 0.0f;
                            break;
                    }

                    current = shaderCode[location++];
                    Argument1 = (BlendArgument)(current >> 4 & 0xf);
                    Argument2 = (BlendArgument)(current & 0xf);
                    current = shaderCode[location++];
                    ArgumentLerp = (BlendArgument)(current >> 4 & 0xf);
                    ArgumentStart = (BlendArgument)(current & 0xf);

                }

                public BlendDestination Destination;
                public float Multiplicand;
                public bool Clamp;
                public BlendOperation Operation;
                public float Bias;
                public BlendArgument Argument1;
                public BlendArgument Argument2;
                public BlendArgument ArgumentLerp;
                public BlendArgument ArgumentStart;

                public string ToShader()
                {
                    string result;

                    result = WriteDestinationStart();

                    if (Clamp)
                        result += "clamp(";

                    if (Multiplicand != 1)
                        result += Multiplicand.ToString("0.0") + "f * (";

                    if (ArgumentStart != BlendArgument.Zero)
                        result += WriteArgument(ArgumentStart) + WriteOperation();
                    else if (Operation == BlendOperation.Subtract)
                        result += "-";

                    if (ArgumentLerp == BlendArgument.Zero)
                        result += WriteArgument(Argument1);
                    else if (ArgumentLerp == BlendArgument.One)
                        result += WriteArgument(Argument2);
                    else
                    {
                        result += "lerp(";
                        result += WriteArgument(Argument1);
                        result += ", ";
                        result += WriteArgument(Argument2);
                        result += ", ";
                        result += WriteArgument(ArgumentLerp);
                        result += ")";
                    }
                    if (Bias > 0)
                        result += " + " + Bias.ToString("0.0") + "f";
                    else if (Bias < 0)
                        result += " - " + (-Bias).ToString("0.0") + "f";

                    if (Multiplicand != 1)
                        result += ")";

                    if (Clamp)
                        result += ", 0.0f, 1.0f)";
                    result += WriteDestinationEnd();

                    return result;
                }

                private string WriteDestinationStart()
                {
                    switch (Destination)
                    {
                        case BlendDestination.Output:
                            return "output.col = float4(";
                        case BlendDestination.C0:
                            return "c0 = float4(";
                        case BlendDestination.C1:
                            return "c1 = float4(";
                        case BlendDestination.C2:
                            return "c2 = float4(";
                    }

                    return "error";
                }

                private string WriteDestinationEnd()
                {
                    switch (Destination)
                    {
                        case BlendDestination.Output:
                            return ", output.col.a);\n";                            
                        case BlendDestination.C0:
                            return ", c0.a);\n";                            
                        case BlendDestination.C1:
                            return ", c1.a);\n";                            
                        case BlendDestination.C2:
                            return ", c2.a);\n";                            
                    }

                    return "error";
                }

                private string WriteArgument(BlendArgument argument)
                {
                    switch (argument)
                    {
                        case BlendArgument.Previous:
                            return "output.col.xyz";                            
                        case BlendArgument.PreviousAlpha:
                            return "output.col.www";                            
                        case BlendArgument.C0:
                            return "c0.xyz";                            
                        case BlendArgument.C0Alpha:
                            return "c0.www";                            
                        case BlendArgument.C1:
                            return "c1.xyz";                            
                        case BlendArgument.C1Alpha:
                            return "c1.www";                            
                        case BlendArgument.C2:
                            return "c2.xyz";                            
                        case BlendArgument.C2Alpha:
                            return "c2.www";                            
                        case BlendArgument.Tex:
                            return "tex.xyz";                            
                        case BlendArgument.TexAlpha:
                            return "tex.www";                            
                        case BlendArgument.Ras:
                            return "ras.xyz";                            
                        case BlendArgument.RasAlpha:
                            return "ras.www";                            
                        case BlendArgument.One:
                            return "float3(1, 1, 1)";                            
                        case BlendArgument.Half:
                            return "float3(0.5f, 0.5f, 0.5f)";                            
                        case BlendArgument.Constant:
                            return "konst.xyz";                      
                        case BlendArgument.Zero:
                            return "float3(0, 0, 0)";                            
                    }

                    return "error";
                }

                private string WriteOperation()
                {
                    switch (Operation)
                    {
                        case BlendOperation.Add:
                            return " + ";
                        case BlendOperation.Subtract:
                            return " - ";
                    }

                    return "error";
                }
            }

            public class AlphaBlendDescription
            {
                public AlphaBlendDescription(byte[] shaderCode, int location)
                {
                    byte current;

                    current = shaderCode[location++];

                    Destination = (BlendDestination)(current >> 6 & 0x3);
                    switch (current >> 4 & 0x3)
                    {
                        case 0:
                            Multiplicand = 1.0f;
                            break;
                        case 1:
                            Multiplicand = 2.0f;
                            break;
                        case 2:
                            Multiplicand = 4.0f;
                            break;
                        case 3:
                            Multiplicand = 0.5f;
                            break;
                    }
                    Clamp = (current >> 3 & 0x1) == 1;
                    Operation = (BlendOperation)(current >> 2 & 0x1);
                    switch (current & 0x3)
                    {
                        case 0:
                            Bias = 0.0f;
                            break;
                        case 1:
                            Bias = 0.5f;
                            break;
                        case 2:
                            Bias = -0.5f;
                            break;
                        case 3:
                            // Incorrect, actually much more complicated.
                            Bias = 0.0f;
                            break;
                    }

                    current = shaderCode[location++];
                    Argument1 = (BlendAlphaArgument)(current >> 5 & 0x7);
                    Argument2 = (BlendAlphaArgument)(current >> 2 & 0x7);
                    ArgumentLerp = (BlendAlphaArgument)(current << 1 & 0x6 | (current = shaderCode[location++]) >> 7 & 0x1);
                    ArgumentStart = (BlendAlphaArgument)(current >> 4 & 0x7);
                    SwapR = current >> 2 & 0x3;
                    SwapT = current & 0x3;
                }

                public BlendDestination Destination;
                public float Multiplicand;
                public bool Clamp;
                public BlendOperation Operation;
                public float Bias;
                public BlendAlphaArgument Argument1;
                public BlendAlphaArgument Argument2;
                public BlendAlphaArgument ArgumentLerp;
                public BlendAlphaArgument ArgumentStart;
                public int SwapR;
                public int SwapT;

                public string ToShader()
                {
                    string result;

                    result = WriteDestinationStart();

                    if (Clamp)
                        result += "clamp(";

                    if (Multiplicand != 1)
                        result += Multiplicand.ToString("0.0") + "f * (";
                    if (ArgumentStart != BlendAlphaArgument.Zero)
                        result += WriteArgument(ArgumentStart) + WriteOperation();
                    else if (Operation == BlendOperation.Subtract)
                        result += "-";
                    if (ArgumentLerp == BlendAlphaArgument.Zero)
                        result += WriteArgument(Argument1);
                    else
                    {
                        result += "lerp(";
                        result += WriteArgument(Argument1);
                        result += ", ";
                        result += WriteArgument(Argument2);
                        result += ", ";
                        result += WriteArgument(ArgumentLerp);
                        result += ")";
                    }
                    if (Bias != 0)
                        result += " + " + Bias.ToString("0.0") + "f";

                    if (Multiplicand != 1)
                        result += ")";

                    if (Clamp)
                        result += ", 0.0f, 1.0f)";
                    result += ");";

                    return result;
                }

                private string WriteDestinationStart()
                {
                    switch (Destination)
                    {
                        case BlendDestination.Output:
                            return "output.col = float4(output.col.xyz, ";
                        case BlendDestination.C0:
                            return "c0 = float4(c0.xyz, ";
                        case BlendDestination.C1:
                            return "c1 = float4(c1.xyz, ";
                        case BlendDestination.C2:
                            return "c2 = float4(c2.xyz, ";
                    }

                    return "error";
                }

                private string WriteArgument(BlendAlphaArgument argument)
                {
                    switch (argument)
                    {
                        case BlendAlphaArgument.Previous:
                            return "output.col.w";
                        case BlendAlphaArgument.C0:
                            return "c0.w";
                        case BlendAlphaArgument.C1:
                            return "c1.w";
                        case BlendAlphaArgument.C2:
                            return "c2.w";
                        case BlendAlphaArgument.Tex:
                            return "tex.w";
                        case BlendAlphaArgument.Ras:
                            return "ras.w";
                        case BlendAlphaArgument.Constant:
                            return "konst.w";
                        case BlendAlphaArgument.Zero:
                            return "0";
                    }

                    return "error";
                }

                private string WriteOperation()
                {
                    switch (Operation)
                    {
                        case BlendOperation.Add:
                            return " + ";
                        case BlendOperation.Subtract:
                            return " - ";
                    }

                    return "error";
                }
            }

            public enum BlendDestination
            {
                Output,
                C0,
                C1,
                C2,
            }

            public enum BlendOperation
            {
                Add,
                Subtract,
            }

            public enum BlendArgument
            {
                Previous,
                PreviousAlpha,
                C0,
                C0Alpha,
                C1,
                C1Alpha,
                C2,
                C2Alpha,
                Tex,
                TexAlpha,
                Ras,
                RasAlpha,
                One,
                Half,
                Constant,
                Zero,
            }

            public enum BlendAlphaArgument
            {
                Previous,
                C0,
                C1,
                C2,
                Tex,
                Ras,
                Constant,
                Zero,
            }

            public enum RasValue
            {
                Colour0 = 0,
                Colour1 = 1,
                AlphaBump = 5,
                AlphaBumpCorrected = 6,
                Zero = 7,
            }

            public enum ConstantTerm
            {
                One = 0,
                SevenEigths = 1,
                ThreeQuaters = 2,
                FiveEights = 3,
                Half = 4,
                ThreeEights = 5,
                Quater = 6,
                Eight = 7,
                Zero = 8,
                K0 = 12,
                K1 = 13,
                K2 = 14,
                K3 = 15,
                K0Red = 16,
                K1Red = 17,
                K2Red = 18,
                K3Red = 19,
                K0Green = 20,
                K1Green = 21,
                K2Green = 22,
                K3Green = 23,
                K0Blue = 24,
                K1Blue = 25,
                K2Blue = 26,
                K3Blue = 27,
                K0Alpha = 28,
                K1Alpha = 29,
                K2Alpha = 30,
                K3Alpha = 31,
            }
        }
    }

    public class Mdl0MaterialLayerClamp
    {
        public Matrix2x1 Max { get; set; }
        public float Angle { get; set; }
        public Matrix2x1 Min { get; set; }

        public Mdl0MaterialLayerClamp(EndianBinaryReader reader)
        {
            Max = new Matrix2x1(reader);
            Angle = reader.ReadSingle();
            Min = new Matrix2x1(reader);
        }

        public void Write(EndianBinaryWriter writer)
        {
            Max.Write(writer);
            writer.Write(Angle);
            Min.Write(writer);
        }
    }

    public class Mdl0MaterialLayerMatrix
    {
        public byte Unknown1 { get; set; }
        public byte Unknown2 { get; set; }
        public byte Unknown3 { get; set; }
        public byte Unknown4 { get; set; }
        public Matrix4x3 Matrix { get; set; }

        public Mdl0MaterialLayerMatrix(EndianBinaryReader reader)
        {
            Unknown1 = reader.ReadByte();
            Unknown2 = reader.ReadByte();
            Unknown3 = reader.ReadByte();
            Unknown4 = reader.ReadByte();
            Matrix = new Matrix4x3(reader);
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(Unknown1);
            writer.Write(Unknown2);
            writer.Write(Unknown3);
            writer.Write(Unknown4);
            Matrix.Write(writer);
        }
    }

    public class Mdl0MaterialLayerColour
    {
        public int Color1 { get; set; }
        public int Color2 { get; set; }
        public int Unknown8 { get; set; }
        public int UnknownC { get; set; }

        public Mdl0MaterialLayerColour(EndianBinaryReader reader)
        {
            Color1 = reader.ReadInt32();
            Color2 = reader.ReadInt32();
            Unknown8 = reader.ReadInt32();
            UnknownC = reader.ReadInt32();
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(Color1);
            writer.Write(Color2);
            writer.Write(Unknown8);
            writer.Write(UnknownC);
        }
    }

    public class Mdl0MaterialLayer
    {
        public long Address { get; set; }

        public int NameOffset { get; set; }
        public int Unknown4 { get; set; }
        public int Unknown8 { get; set; }
        public int UnknownC { get; set; }
        public int Unknown10 { get; set; }
        public int Unknown14 { get; set; }
        public int TextureAddressU { get; set; }
        public int TextureAddressV { get; set; }
        public int Unknown20 { get; set; }
        public int Unknown24 { get; set; }
        public float Unknown28 { get; set; }
        public int Unknown2C { get; set; }
        public int Unknown30 { get; set; }

        public string Name { get; set; }

        public Mdl0MaterialLayer(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            NameOffset = reader.ReadInt32();
            Unknown4 = reader.ReadInt32();
            Unknown8 = reader.ReadInt32();
            UnknownC = reader.ReadInt32();
            Unknown10 = reader.ReadInt32();
            Unknown14 = reader.ReadInt32();
            TextureAddressU = reader.ReadInt32();
            TextureAddressV = reader.ReadInt32();
            Unknown20 = reader.ReadInt32();
            Unknown24 = reader.ReadInt32();
            Unknown28 = reader.ReadSingle();
            Unknown2C = reader.ReadInt32();
            Unknown30 = reader.ReadInt32();
        }

        public void Write(EndianBinaryWriter writer)
        {
            Address = writer.BaseStream.Position;

            writer.Write(NameOffset);
            writer.Write(Unknown4);
            writer.Write(Unknown8);
            writer.Write(UnknownC);
            writer.Write(Unknown10);
            writer.Write(Unknown14);
            writer.Write(TextureAddressU);
            writer.Write(TextureAddressV);
            writer.Write(Unknown20);
            writer.Write(Unknown24);
            writer.Write(Unknown28);
            writer.Write(Unknown2C);
            writer.Write(Unknown30);
        }

        public void WriteNames(EndianBinaryWriter writer, Dictionary<string, long> names)
        {
            NameOffset = (int)(BrresFile.WriteString(writer, names, Name) - Address);
        }
    }

    public class Mdl0MaterialShader
    {
        public long Address { get; set; }

        public byte[] Mode { get; private set; }
        public byte[] TevRegs { get; private set; }
        public byte[] TextureTransformations { get; private set; }
        public byte[] TextureMatricies { get; private set; }

        public Mdl0MaterialShader(EndianBinaryReader reader)
        {
            Address = reader.BaseStream.Position;

            Mode = reader.ReadBytes(0x20);
            TevRegs = reader.ReadBytes(0x80);
            TextureTransformations = reader.ReadBytes(0x40);
            TextureMatricies = reader.ReadBytes(0xA0);
        }

        public void Write(EndianBinaryWriter writer)
        {
            Address = writer.BaseStream.Position;

            writer.Write(Mode, 0, Mode.Length);
            writer.Write(TevRegs, 0, TevRegs.Length);
            writer.Write(TextureTransformations, 0, TextureTransformations.Length);
            writer.Write(TextureMatricies, 0, TextureMatricies.Length);
        }
    }
}
