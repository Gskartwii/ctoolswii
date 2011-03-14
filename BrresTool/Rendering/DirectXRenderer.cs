using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chadsoft.CTools.Models;
using Microsoft.DirectX.Direct3D;
using System.Windows.Forms;
using Microsoft.DirectX;
using System.Drawing;
using System.Collections.ObjectModel;
using System.IO;
using Material = Chadsoft.CTools.Models.Material;
using Chadsoft.CTools.Image;

namespace Chadsoft.CTools.Rendering
{
    public class DirectXRenderer : Renderer 
    {
        private Device graphicsDevice;
        private PresentParameters presentParameters;
        private VertexBuffer backgroundBuffer;
        private Collection<VertexBuffer[]> vertexBuffers;
        private Collection<VertexDeclaration[]> vertexDeclarations;
        private Collection<int[]> vertexBufferStrides;
        private Collection<VertexFormats[]> vertexFormats;
        private Collection<GraphicsStream[]> pixelShaderCodes;
        private Collection<PixelShader[]> pixelShaders;
        private Collection<ConstantTable[]> pixelShaderConstants;
        private Collection<Texture[][]> textures;
        private GraphicsStream vertexShaderData, pixelShaderData;
        private VertexShader vertexShader;
        private PixelShader pixelShader;
        private ConstantTable vertexShaderConstants;

        public DirectXRenderer(Panel panel) : base(panel)
        {
            string shaderErrors;
            ConstantTable psConstants;

            presentParameters = new PresentParameters()
            {
                AutoDepthStencilFormat = DepthFormat.D16,
                BackBufferFormat = Format.A8R8G8B8,
                DeviceWindow = panel,
                EnableAutoDepthStencil = true,
                PresentFlag = PresentFlag.None,
                SwapEffect = SwapEffect.Discard,
                Windowed = true,
            };

            graphicsDevice = new Device(0, DeviceType.Hardware, panel, CreateFlags.SoftwareVertexProcessing, presentParameters);
            graphicsDevice.DeviceReset += new EventHandler(graphicsDevice_DeviceReset);

            vertexBuffers = new Collection<VertexBuffer[]>();
            vertexDeclarations = new Collection<VertexDeclaration[]>();
            vertexBufferStrides = new Collection<int[]>();
            vertexFormats = new Collection<VertexFormats[]>();
            pixelShaders = new Collection<PixelShader[]>();
            pixelShaderConstants = new Collection<ConstantTable[]>();
            pixelShaderCodes = new Collection<GraphicsStream[]>();
            textures = new Collection<Texture[][]>();

            vertexShaderData = ShaderLoader.CompileShader(Chadsoft.CTools.Brres.Rendering.ShaderResources.VertexShader, "mainVS", null, null, "vs_3_0", ShaderFlags.PackMatrixColumnMajor, out shaderErrors, out vertexShaderConstants);
            vertexShader = new VertexShader(graphicsDevice, vertexShaderData);
            pixelShaderData = ShaderLoader.CompileShader(Chadsoft.CTools.Brres.Rendering.ShaderResources.SimplePixelShader, "mainPS", null, null, "ps_3_0", ShaderFlags.PackMatrixColumnMajor, out shaderErrors, out psConstants);
            pixelShader = new PixelShader(graphicsDevice, pixelShaderData);

            graphicsDevice_DeviceReset(graphicsDevice, EventArgs.Empty);
        }

        private void graphicsDevice_DeviceReset(object sender, EventArgs e)
        {
            GraphicsStream stream;

            for (int i = 0; i < vertexBuffers.Count; i++)
                for (int j = 0; j < vertexBuffers[i].Length; j++)
                {
                    if (vertexBuffers[i][j] != null)
                        vertexBuffers[i][j].Dispose();

                    vertexBuffers[i][j] = new VertexBuffer(graphicsDevice, (int)Models[i].Polygons[j].VertexData.Length, Usage.None, vertexFormats[i][j], Pool.Default);
                    PopulalteBuffer(vertexBuffers[i][j]);
                }

            for (int i = 0; i < pixelShaders.Count; i++)
                for (int j = 0; j < pixelShaders[i].Length; j++)
                {
                    for (int k = 0; k < Models[i].Materials[j].Textures.Count; k++)
                    {
                        if (textures[i][j][k] != null)
                        {

                            textures[i][j][k].Dispose();
                            textures[i][j][k] = //Texture.FromBitmap(graphicsDevice, ImageData.ToBitmap(Models[i].Materials[j].Textures[k].GetData(0), Models[i].Materials[j].Textures[k].Width, Models[i].Materials[j].Textures[k].Height), Usage.Dynamic, Pool.Default);
                                CreateTexture(Models[i].Materials[j].Textures[k]);


                        }
                    }
                }

            if (backgroundBuffer != null)
                backgroundBuffer.Dispose();

            backgroundBuffer = new VertexBuffer(typeof(CustomVertex.PositionColored), 4, graphicsDevice, Usage.None, CustomVertex.PositionColored.Format, Pool.Default);

            stream = backgroundBuffer.Lock(0, 16 * 4, LockFlags.Discard);

            stream.Write(new Vector3(-1, -4, 0));
            stream.Write(Color.White.ToArgb());
            stream.Write(new Vector3(-1, 1, 0));
            stream.Write(Color.LightBlue.ToArgb());
            stream.Write(new Vector3(1, -4, 0));
            stream.Write(Color.White.ToArgb());
            stream.Write(new Vector3(1, 1, 0));
            stream.Write(Color.LightBlue.ToArgb());

            backgroundBuffer.Unlock();
        }

        private Texture CreateTexture(ImageData texture)
        {
            Texture t;
            GraphicsStream gs;

            t = new Texture(graphicsDevice, texture.Width, texture.Height, texture.Levels, Usage.Dynamic, Format.A8R8G8B8, Pool.Default);

            for (int i = 0; i < texture.Levels; i++)
            {
                object o = t.GetLevelDescription(i);
                gs = t.LockRectangle(i, LockFlags.None);
                gs.Write(texture.GetData(i));
                t.UnlockRectangle(i);
            }
            
            return t;
        }

        protected override void OnLoadModel(Model model)
        {
            string shaderErrors;
            int[] strides;
            VertexDeclaration[] declarations;
            VertexBuffer[] buffers;
            VertexFormats[] formats;
            PixelShader[] shaders;
            GraphicsStream[] shaderCodes;
            ConstantTable[] shaderConstants;
            Texture[][] texs;

            strides = new int[model.Polygons.Count];
            declarations = new VertexDeclaration[model.Polygons.Count];
            buffers = new VertexBuffer[model.Polygons.Count];
            formats = new VertexFormats[model.Polygons.Count];
            shaders = new PixelShader[model.Materials.Count];
            shaderCodes = new GraphicsStream[model.Materials.Count];
            shaderConstants = new ConstantTable[model.Materials.Count];
            texs = new Texture[model.Materials.Count][];
            
            vertexBuffers.Add(buffers);
            vertexDeclarations.Add(declarations);
            vertexBufferStrides.Add(strides);
            vertexFormats.Add(formats);
            pixelShaders.Add(shaders);
            pixelShaderCodes.Add(shaderCodes);
            pixelShaderConstants.Add(shaderConstants);
            textures.Add(texs);

            for (int i = 0; i < model.Polygons.Count; i++)
            {
                declarations[i] = GenerateVertexDeclaration(model.Polygons[i].VertexDataFormat, out strides[i], out formats[i]);

                buffers[i] = new VertexBuffer(graphicsDevice, (int)model.Polygons[i].VertexData.Length, Usage.None, formats[i], Pool.Default);
                PopulalteBuffer(buffers[i]);
            }

            for (int i = 0; i < model.Materials.Count; i++)
            {
                texs[i] = new Texture[model.Materials[i].Textures.Count];

                shaderCodes[i] = ShaderLoader.CompileShader(model.Materials[i].PixelShaderSource, "mainPS", null, null, "ps_3_0", ShaderFlags.PackMatrixColumnMajor, out shaderErrors, out shaderConstants[i]);
                shaders[i] = new PixelShader(graphicsDevice, shaderCodes[i]);   

                for (int j = 0; j < texs[i].Length; j++)
			    {
                    if (model.Materials[i].Textures[j] != null)
                        texs[i][j] = //Texture.FromBitmap(graphicsDevice, ImageData.ToBitmap(model.Materials[i].Textures[j].GetData(0), model.Materials[i].Textures[j].Width, model.Materials[i].Textures[j].Height), Usage.Dynamic, Pool.Default);
                                CreateTexture(model.Materials[i].Textures[j]);

                    else
                        texs[i][j] = null;
			    }
            }

        }
        
        private void PopulalteBuffer(VertexBuffer buffer)
        {
            MemoryStream sourceStream;
            GraphicsStream destStream;

            for (int i = 0; i < vertexBuffers.Count; i++)
            {
                for (int j = 0; j < vertexBuffers[i].Length; j++)
                {
                    if (vertexBuffers[i][j] == buffer)
                    {
                        sourceStream = Models[i].Polygons[j].VertexData;
                        destStream = vertexBuffers[i][j].Lock(0, vertexBuffers[i][j].SizeInBytes, LockFlags.Discard);

                        sourceStream.Seek(0, SeekOrigin.Begin);
                        sourceStream.CopyTo(destStream);

                        vertexBuffers[i][j].Unlock();
                        return;
                    }
                }
            }
        }

        private VertexDeclaration GenerateVertexDeclaration(PolygonVertexDataFormat dataFormat, out int stride, out VertexFormats format)
        {
            List<VertexElement> elements;
            VertexElement[] elementArray;
            short offset;

            elements = new List<VertexElement>();
            offset = 0;
            stride = 0;
            format = VertexFormats.None;

            if (dataFormat.HasFlag(PolygonVertexDataFormat.Transform))
            {
                elements.Add(new VertexElement(0, offset, DeclarationType.Short2, DeclarationMethod.Default, DeclarationUsage.PointSize, 0));
                offset += 4;
                format |= VertexFormats.PointSize;
                stride += 4;
            }

            if (dataFormat.HasFlag(PolygonVertexDataFormat.Position))
            {
                elements.Add(new VertexElement(0, offset, DeclarationType.Float3, DeclarationMethod.Default, DeclarationUsage.Position, 0));
                offset += 12;
                format |= VertexFormats.Position;
                stride += 12;
            }

            if (dataFormat.HasFlag(PolygonVertexDataFormat.Normal))
            {
                elements.Add(new VertexElement(0, offset, DeclarationType.Float3, DeclarationMethod.Default, DeclarationUsage.Normal, 0));
                offset += 12;
                format |= VertexFormats.Normal;
                stride += 12;
            }

            if (dataFormat.HasFlag(PolygonVertexDataFormat.Colour1))
            {
                elements.Add(new VertexElement(0, offset, DeclarationType.Color, DeclarationMethod.Default, DeclarationUsage.Color, 0));
                offset += 4;
                format |= VertexFormats.Diffuse;
                stride += 4;
            }

            if (dataFormat.HasFlag(PolygonVertexDataFormat.Colour2))
            {
                elements.Add(new VertexElement(0, offset, DeclarationType.Color, DeclarationMethod.Default, DeclarationUsage.Color, 0));
                offset += 4;
                elements.Add(new VertexElement(0, offset, DeclarationType.Color, DeclarationMethod.Default, DeclarationUsage.Color, 1));
                offset += 4;
                format |= VertexFormats.Diffuse;
                stride += 8;
            }

            for (int i = 0; i < ((int)dataFormat & (int)PolygonVertexDataFormat.TextureMask) >> (int)PolygonVertexDataFormat.TextureShift; i++)
            {
                elements.Add(new VertexElement(0, offset, DeclarationType.Float2, DeclarationMethod.Default, DeclarationUsage.TextureCoordinate, (byte)i));
                offset += 8;                 
                stride += 8;           
            }

            format |= (VertexFormats)(((int)dataFormat & (int)PolygonVertexDataFormat.TextureMask) >> (int)PolygonVertexDataFormat.TextureShift << (int)VertexFormats.TextureCountShift);
            elements.Add(VertexElement.VertexDeclarationEnd);
            elementArray = elements.ToArray();

            return new VertexDeclaration(graphicsDevice, elementArray);
        }

        protected override void OnUnloadModel(Model model)
        {
            int index;

            index = Models.IndexOf(model);

            if (index == -1) return;

            vertexDeclarations.RemoveAt(index);
            vertexFormats.RemoveAt(index);
            vertexBuffers.RemoveAt(index);
            vertexBufferStrides.RemoveAt(index);
            pixelShaderCodes.RemoveAt(index);
            pixelShaderConstants.RemoveAt(index);
            pixelShaders.RemoveAt(index);
            textures.RemoveAt(index);
        }

        protected override void OnRender()
        {
            try
            {
                if (!graphicsDevice.CheckCooperativeLevel())
                    graphicsDevice.Reset(presentParameters);
            }
            catch (Exception)
            {
                return;
            }
                
            try
            {
                graphicsDevice.Clear(ClearFlags.ZBuffer, 0, 1.0f, 0);
                try
                {
                    graphicsDevice.BeginScene();
                }
                catch (Exception) {}
                
                graphicsDevice.RenderState.ZBufferEnable = false;
                graphicsDevice.RenderState.ZBufferWriteEnable = false;
                graphicsDevice.RenderState.Lighting = false;
                graphicsDevice.RenderState.CullMode = Cull.CounterClockwise;
                graphicsDevice.VertexDeclaration = null;
                graphicsDevice.PixelShader = pixelShader;

                for (int i = 0; i < 8; i++)
                {
                    graphicsDevice.SetTexture(i, null);
                    graphicsDevice.SamplerState[i].MagFilter = TextureFilter.Anisotropic;
                    graphicsDevice.SamplerState[i].MinFilter = TextureFilter.Anisotropic;
                    graphicsDevice.SamplerState[i].MipFilter = TextureFilter.Linear;
                }

                graphicsDevice.RenderState.AlphaBlendEnable = false;
                graphicsDevice.SetStreamSource(0, backgroundBuffer, 0);
                graphicsDevice.VertexFormat = CustomVertex.PositionColored.Format;
                graphicsDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);

                graphicsDevice.RenderState.ZBufferEnable = true;
                graphicsDevice.RenderState.ZBufferWriteEnable = true;

                graphicsDevice.VertexShader = vertexShader;
                vertexShaderConstants.SetValue(graphicsDevice, EffectHandle.FromString("WorldViewProj"), GenerateMatrix());

                for (int i = 0; i < Models.Count; i++)
                    Render(i);

                graphicsDevice.VertexShader = null;
                
                graphicsDevice.EndScene();
                graphicsDevice.Present();
            }
            catch (DirectXException)
            {
            }
        }

        private void Render(int modelIndex)
        {
            Model model;

            model = Models[modelIndex];

            if (!IsRendered(model)) return;

            for (int i = 0; i < model.Instructions.Count; i++)
            {
                Render(modelIndex, model.Instructions[i].Polygon, model.Instructions[i].Material);
            }
        }

        private void Render(int modelIndex, int materialIndex, int polygonIndex)
        {
            Model model;
            Material material;
            Polygon polygon;
            VertexBuffer buffer;
            int loc;

            model = Models[modelIndex];

            if (materialIndex >= model.Materials.Count || polygonIndex >= model.Polygons.Count)
                return;
            material = model.Materials[materialIndex];
            polygon = model.Polygons[polygonIndex];
            
            for (int i = 0; i < 22; i++)
                vertexShaderConstants.SetValue(graphicsDevice, EffectHandle.FromString("vMatrix" + i.ToString()), Matrix4x3.Identity.ToFloat());

            buffer = vertexBuffers[modelIndex][polygonIndex];

            graphicsDevice.RenderState.AlphaTestEnable = material.EnableAlphaTest;
            graphicsDevice.RenderState.ReferenceAlpha = material.AlphaTestValue;

            switch (material.AlphaTestFunction)
            {
                case AlphaTestFunction.Never:
                    graphicsDevice.RenderState.AlphaFunction = Compare.Never;
                    break;
                case AlphaTestFunction.Less:
                    graphicsDevice.RenderState.AlphaFunction = Compare.Less;
                    break;
                case AlphaTestFunction.Equal:
                    graphicsDevice.RenderState.AlphaFunction = Compare.Equal;
                    break;
                case AlphaTestFunction.LessEqual:
                    graphicsDevice.RenderState.AlphaFunction = Compare.LessEqual;
                    break;
                case AlphaTestFunction.Greater:
                    graphicsDevice.RenderState.AlphaFunction = Compare.Greater;
                    break;
                case AlphaTestFunction.NotEqual:
                    graphicsDevice.RenderState.AlphaFunction = Compare.NotEqual;
                    break;
                case AlphaTestFunction.GreaterEqual:
                    graphicsDevice.RenderState.AlphaFunction = Compare.GreaterEqual;
                    break;
                case AlphaTestFunction.Always:
                    graphicsDevice.RenderState.AlphaFunction = Compare.Always;
                    break;
            }

            graphicsDevice.RenderState.AlphaBlendEnable = material.EnableAlphaBlend;
            //graphicsDevice.RenderState.ZBufferEnable = !material.EnableAlphaBlend;
    
            switch (material.AlphaBlendDestination)
            {
                case AlphaBlendDestination.Zero:
                    graphicsDevice.RenderState.DestinationBlend = Blend.Zero;
                    break;
                case AlphaBlendDestination.One:
                    graphicsDevice.RenderState.DestinationBlend = Blend.One;
                    break;
                case AlphaBlendDestination.SrcColour:
                    // hacks!
                    graphicsDevice.RenderState.DestinationBlend = Blend.DestinationAlpha;
                    //graphicsDevice.RenderState.DestinationBlend = Blend.SourceColor;
                    break;
                case AlphaBlendDestination.InvSrcColour:
                    // hacks!
                    graphicsDevice.RenderState.DestinationBlend = Blend.SourceAlpha;
                    //graphicsDevice.RenderState.DestinationBlend = Blend.InvSourceColor;
                    break;
                case AlphaBlendDestination.SrcAlpha:
                    graphicsDevice.RenderState.DestinationBlend = Blend.SourceAlpha;
                    break;
                case AlphaBlendDestination.InvSrcAlpha:
                    graphicsDevice.RenderState.DestinationBlend = Blend.InvSourceAlpha;
                    break;
                case AlphaBlendDestination.DestAplha:
                    graphicsDevice.RenderState.DestinationBlend = Blend.DestinationAlpha;
                    break;
                case AlphaBlendDestination.InvDestAlpha:
                    graphicsDevice.RenderState.DestinationBlend = Blend.InvDestinationAlpha;
                    break;
            }

            switch (material.AlphaBlendSource)
            {
                case AlphaBlendSource.Zero:
                    graphicsDevice.RenderState.SourceBlend = Blend.Zero;
                    break;
                case AlphaBlendSource.One:
                    graphicsDevice.RenderState.SourceBlend = Blend.One;
                    break;
                case AlphaBlendSource.DestColour:
                    // hacks!
                    graphicsDevice.RenderState.SourceBlend = Blend.DestinationAlpha;
                    //graphicsDevice.RenderState.SourceBlend = Blend.DestinationColor;
                    break;
                case AlphaBlendSource.InvDestColour:
                    // hacks!
                    graphicsDevice.RenderState.SourceBlend = Blend.SourceAlpha;
                    //graphicsDevice.RenderState.SourceBlend = Blend.InvDestinationColor;
                    break;
                case AlphaBlendSource.SrcAlpha:
                    graphicsDevice.RenderState.SourceBlend = Blend.SourceAlpha;
                    break;
                case AlphaBlendSource.InvSrcAlpha:
                    graphicsDevice.RenderState.SourceBlend = Blend.InvSourceAlpha;
                    break;
                case AlphaBlendSource.DestAplha:
                    graphicsDevice.RenderState.SourceBlend = Blend.DestinationAlpha;
                    break;
                case AlphaBlendSource.InvDestAlpha:
                    graphicsDevice.RenderState.SourceBlend = Blend.InvDestinationAlpha;
                    break;
            }

            for (int i = 0; i < 8; i++)
            {
                if (textures[modelIndex][materialIndex].Length <= i)
                    graphicsDevice.SetTexture(i, null);
                else
                {
                    graphicsDevice.SetTexture(i, textures[modelIndex][materialIndex][i]);
                    switch (material.UCoords[i])
                    {
                        case MaterialTextureAddress.Clamp:
                            graphicsDevice.SamplerState[i].AddressU = TextureAddress.Clamp;
                            break;
                        case MaterialTextureAddress.Mirror:
                            graphicsDevice.SamplerState[i].AddressU = TextureAddress.Mirror;
                            break;
                        default:
                            graphicsDevice.SamplerState[i].AddressU = TextureAddress.Wrap;
                            break;
                    }
                    switch (material.VCoords[i])
                    {
                        case MaterialTextureAddress.Clamp:
                            graphicsDevice.SamplerState[i].AddressV = TextureAddress.Clamp;
                            break;
                        case MaterialTextureAddress.Mirror:
                            graphicsDevice.SamplerState[i].AddressV = TextureAddress.Mirror;
                            break;
                        default:
                            graphicsDevice.SamplerState[i].AddressV = TextureAddress.Wrap;
                            break;
                    }
                }
            }

            graphicsDevice.PixelShader = pixelShaders[modelIndex][materialIndex];

            graphicsDevice.VertexDeclaration = vertexDeclarations[modelIndex][polygonIndex];
            graphicsDevice.SetStreamSource(0, buffer, 0, vertexBufferStrides[modelIndex][polygonIndex]);
            loc = 0;

            for (int i = 0; i < polygon.Instructions.Count; i++)
            {
                switch (polygon.Instructions[i].Command)
                {
                    case PolygonRenderInstructionCommand.DrawQuads:
                        //graphicsDevice.DrawPrimitives(PrimitiveType.
                        loc += 4 * polygon.Instructions[i].Count;
                        break;
                    case PolygonRenderInstructionCommand.DrawTriangles:
                        graphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, loc, polygon.Instructions[i].Count);
                        loc += 3 * polygon.Instructions[i].Count;
                        break;
                    case PolygonRenderInstructionCommand.DrawTriangleStrip:
                        graphicsDevice.DrawPrimitives(PrimitiveType.TriangleStrip, loc, polygon.Instructions[i].Count);
                        loc += 2 + polygon.Instructions[i].Count;
                        break;
                    case PolygonRenderInstructionCommand.DrawTriangleFan:
                        graphicsDevice.DrawPrimitives(PrimitiveType.TriangleFan, loc, polygon.Instructions[i].Count);
                        loc += 2 + polygon.Instructions[i].Count;
                        break;
                    case PolygonRenderInstructionCommand.DrawLines:
                        graphicsDevice.DrawPrimitives(PrimitiveType.LineList, loc, polygon.Instructions[i].Count);
                        loc += 2 * polygon.Instructions[i].Count;
                        break;
                    case PolygonRenderInstructionCommand.DrawLineStrip:
                        graphicsDevice.DrawPrimitives(PrimitiveType.LineStrip, loc, polygon.Instructions[i].Count);
                        loc += 1 + polygon.Instructions[i].Count;
                        break;
                    case PolygonRenderInstructionCommand.DrawPoints:
                        graphicsDevice.DrawPrimitives(PrimitiveType.PointList, loc, polygon.Instructions[i].Count);
                        loc += polygon.Instructions[i].Count;
                        break;
                    case PolygonRenderInstructionCommand.SetMatrix:
                        if (polygon.Instructions[i].Destination < 22)
                            vertexShaderConstants.SetValue(graphicsDevice, EffectHandle.FromString("vMatrix" + polygon.Instructions[i].Destination.ToString()), model.Bones[polygon.Instructions[i].Index].Resolve().ToFloat());
                        break;
                }
            }
        }

        private Matrix GenerateMatrix()
        {
            return Matrix.Scaling(1, 1, -1) *Matrix.LookAtLH(ToVector(CameraPosition), ToVector(CameraPosition) + ToVector(CameraDirection), ToVector(CameraUp)) * Matrix.PerspectiveLH(Panel.Width, Panel.Height, 1000.0f, 400000.0f);
        }

        private Vector3 ToVector(Matrix3x1 vector)
        {
            return new Vector3() { X = vector.X, Y = vector.Y, Z = vector.Z };
        }
    }
}
