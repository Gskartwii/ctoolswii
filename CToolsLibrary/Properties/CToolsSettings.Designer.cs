﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Chadsoft.CTools.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class CToolsSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static CToolsSettings defaultInstance = ((CToolsSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new CToolsSettings())));
        
        public static CToolsSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        /// <summary>
        /// Whether or not to use binary prefixes in file sizes
        /// </summary>
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsDescriptionAttribute("Whether or not to use binary prefixes in file sizes")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool binaryUnits {
            get {
                return ((bool)(this["binaryUnits"]));
            }
            set {
                this["binaryUnits"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("63")]
        public int compressionLookBack {
            get {
                return ((int)(this["compressionLookBack"]));
            }
            set {
                this["compressionLookBack"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.0.0")]
        public string lastTimeUpdate {
            get {
                return ((string)(this["lastTimeUpdate"]));
            }
            set {
                this["lastTimeUpdate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://chadderz.is-a-geek.com/ctools/setup.exe")]
        public string updateUrl {
            get {
                return ((string)(this["updateUrl"]));
            }
            set {
                this["updateUrl"] = value;
            }
        }
    }
}
