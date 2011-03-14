﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Chadsoft.CTools.Brres.Rendering {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ShaderResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ShaderResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Chadsoft.CTools.Brres.Rendering.ShaderResources", typeof(ShaderResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // BOTH
        ///int rrnm(int value, int distance, int maskEnd)
        ///{
        ///	return (value / pow(2, distance)) % pow(2, maskEnd);	
        ///}
        ///
        ///// PS
        ///float4x3 tMatrix0;
        ///float4x3 tMatrix1;
        ///float4x3 tMatrix2;
        ///float4x3 tMatrix3;
        ///float4x3 tMatrix4;
        ///float4x3 tMatrix5;
        ///float4x3 tMatrix6;
        ///float4x3 tMatrix7;
        ///
        ///sampler2D tex0Samp;
        ///sampler2D tex1Samp;
        ///sampler2D tex2Samp;
        ///sampler2D tex3Samp;
        ///sampler2D tex4Samp;
        ///sampler2D tex5Samp;
        ///sampler2D tex6Samp;
        ///sampler2D tex7Samp;
        ///
        ///struct PSInput 
        ///{
        ///	float4 col0 : COLOR0;
        ///	float4  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string PixelShader {
            get {
                return ResourceManager.GetString("PixelShader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // SIMPLE PS
        ///
        ///struct PSInput 
        ///{
        ///	float4 col0 : COLOR0;
        ///	float4 col1 : COLOR1;
        ///	float2 uv0 : TEXCOORD0;
        ///	float2 uv1 : TEXCOORD1;
        ///	float2 uv2 : TEXCOORD2;
        ///	float2 uv3 : TEXCOORD3;
        ///	float2 uv4 : TEXCOORD4;
        ///	float2 uv5 : TEXCOORD5;
        ///	float2 uv6 : TEXCOORD6;
        ///	float2 uv7 : TEXCOORD7;
        ///	int selector : PSIZE;
        ///};
        ///
        ///struct PSOutput
        ///{
        ///	float4 col : COLOR;
        ///};
        ///
        ///PSOutput mainPS(PSInput input) {
        ///	PSOutput output;
        ///	
        ///	output.col = input.col0;
        ///	
        ///	return output;
        ///}.
        /// </summary>
        internal static string SimplePixelShader {
            get {
                return ResourceManager.GetString("SimplePixelShader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // BOTH
        ///int rrnm(int value, int distance, int maskEnd)
        ///{
        ///	return (value / pow(2, distance)) % pow(2, maskEnd);	
        ///}
        ///
        ///// VS
        ///float4x4 WorldViewProj : WORLDVIEWPROJECTION;
        ///float4x3 vMatrix0;
        ///float4x3 vMatrix1;
        ///float4x3 vMatrix2;
        ///float4x3 vMatrix3;
        ///float4x3 vMatrix4;
        ///float4x3 vMatrix5;
        ///float4x3 vMatrix6;
        ///float4x3 vMatrix7;
        ///float4x3 vMatrix8;
        ///float4x3 vMatrix9;
        ///float4x3 vMatrix10;
        ///float4x3 vMatrix11;
        ///float4x3 vMatrix12;
        ///float4x3 vMatrix13;
        ///float4x3 vMatrix14;
        ///float4x3 vMatrix15;
        ///float4x3 vM [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string VertexShader {
            get {
                return ResourceManager.GetString("VertexShader", resourceCulture);
            }
        }
    }
}
