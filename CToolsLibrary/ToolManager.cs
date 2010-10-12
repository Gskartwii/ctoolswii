// CTools library - Library functions for CTools
// Copyright (C) 2010 Chadderz

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Chadsoft.CTools
{
    public static class ToolManager
    {
        public static Version CToolsVersion { get { return new Version(1, 0, 4); } }

        private static ReadOnlyCollection<Tool> tools;
        private static ReadOnlyCollection<Editor> editors;
        private static ReadOnlyCollection<FileFormat> formats;
        private static ReadOnlyCollection<NewFile> newFiles;

        public static ReadOnlyCollection<Tool> Tools
        {
            get
            {
                if (tools == null)
                    GetTools();

                return tools;
            }
        }

        public static ReadOnlyCollection<Editor> Editors
        {
            get
            {
                if (editors == null)
                    GetEditors();

                return editors;
            }
        }

        public static ReadOnlyCollection<FileFormat> FileFormats
        {
            get
            {
                if (formats == null)
                    GetFileFormats();

                return formats;
            }
        }

        public static ReadOnlyCollection<NewFile> NewFiles
        {
            get
            {
                if (newFiles == null)
                    GetNewFiles();

                return newFiles;
            }
        }

        private static void GetTools()
        {
            Collection<Assembly> assemblies;
            Collection<Tool> tools;
            DirectoryInfo info;
            string myPath;

            myPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            info = new DirectoryInfo(myPath);
            assemblies = new Collection<Assembly>();
            tools = new Collection<Tool>();

            foreach (FileInfo file in info.GetFiles("*.exe"))
                AddFile(file, assemblies);
            
            foreach (FileInfo file in info.GetFiles("*.dll"))
                AddFile(file, assemblies);

            foreach (Assembly item in assemblies)
                AddTool(item, tools);

            ToolManager.tools = new ReadOnlyCollection<Tool>(tools);
        }

        private static void AddFile(FileInfo file, Collection<Assembly> assemblies)
        {
            try
            {
                Assembly assembly;

                assembly = Assembly.LoadFrom(file.FullName);

                assemblies.Add(assembly);
            }
            catch (Exception)
            {
            }
        }

        private static void AddTool(Assembly assembly, Collection<Tool> tools)
        {
            Type toolInfo;
            PropertyInfo propertyInfo;

            try
            {
                toolInfo = null;
                foreach (Type t in assembly.GetExportedTypes())
                {
                    if (t.Name == "ToolInfo")
                    {
                        toolInfo = t;
                        break;
                    }
                }
                propertyInfo = toolInfo.GetProperty("Tool");
                tools.Add((Tool)propertyInfo.GetValue(null, null));
            }
            catch (Exception)
            {
            }
        }

        private static void GetEditors()
        {
            Collection<Editor> editors;

            editors = new Collection<Editor>();

            foreach (Tool item in Tools)
            {
                editors = new Collection<Editor>(editors.Union(item.Editors).ToArray());
            }

            ToolManager.editors = new ReadOnlyCollection<Editor>(editors);
        }

        private static void GetFileFormats()
        {
            Collection<FileFormat> formats;

            formats = new Collection<FileFormat>();

            foreach (Tool item in Tools)
            {
                formats = new Collection<FileFormat>(formats.Union(item.Formats).ToArray());
            }

            ToolManager.formats = new ReadOnlyCollection<FileFormat>(formats);
        }

        private static void GetNewFiles()
        {
            Collection<NewFile> newFiles;

            newFiles = new Collection<NewFile>();

            foreach (Tool item in Tools)
            {
                newFiles = new Collection<NewFile>(newFiles.Union(item.NewFiles).ToArray());
            }

            ToolManager.newFiles = new ReadOnlyCollection<NewFile>(newFiles);
        }

        public static FileFormat GetFormat(string fileName, byte[] data, int offset)
        {
            int bestMatch, match;
            FileFormat bestFormat;

            bestMatch = 0;
            bestFormat = null;

            foreach (FileFormat item in formats)
            {
                match = item.FormatMatch(fileName, data, offset);

                if (match > bestMatch)
                {
                    bestMatch = match;
                    bestFormat = item;
                }
            }

            return bestFormat;
        }

        public static FileFormat[] GetFormats(string fileName, byte[] data, int offset)
        {
            int match;
            bool added;
            Collection<FileFormat> formats;
            Collection<int> priorities;

            formats = new Collection<FileFormat>();
            priorities = new Collection<int>();

            foreach (FileFormat item in ToolManager.formats)
            {
                match = item.FormatMatch(fileName, data, offset);

                if (match > 0)
                {
                    added = false;

                    for (int i = 0; i < priorities.Count; i++)
                    {
                        if (priorities[i] < match)
                        {
                            priorities.Insert(i, match);
                            formats.Insert(i, item);
                            added = true;
                            break;
                        }
                    }

                    if (!added)
                    {
                        priorities.Add(match);
                        formats.Add(item);
                    }
                }
            }

            return formats.ToArray();
        }

        public static Editor GetEditor(string fileName, byte[] data, int offset)
        {
            int bestMatch, match;
            Editor bestEditor;

            bestMatch = 0;
            bestEditor = null;

            foreach (Editor item in Editors)
            {
                match = item.FormatMatch(fileName, data, offset);

                if (match > bestMatch)
                {
                    bestMatch = match;
                    bestEditor = item;
                }
            }

            return bestEditor;
        }

        public static Editor[] GetEditors(string fileName, byte[] data, int offset)
        {
            int match;
            bool added;
            Collection<Editor> editors;
            Collection<int> priorities;

            editors = new Collection<Editor>();
            priorities = new Collection<int>();

            foreach (Editor item in ToolManager.Editors)
            {
                match = item.FormatMatch(fileName, data, offset);

                if (match > 0)
                {
                    added = false;

                    for (int i = 0; i < priorities.Count; i++)
                    {
                        if (priorities[i] < match)
                        {
                            priorities.Insert(i, match);
                            editors.Insert(i, item);
                            added = true;
                            break;
                        }
                    }

                    if (!added)
                    {
                        priorities.Add(match);
                        editors.Add(item);
                    }
                }
            }

            return editors.ToArray();
        }

        public static bool CheckForUpdates()
        {
            Thread checkThread;

            if (Properties.CToolsSettings.Default.lastTimeUpdate != null && new Version (Properties.CToolsSettings.Default.lastTimeUpdate) > CToolsVersion)
            {
                return true;
            }
            else
            {
                checkThread = new Thread(CheckForUpdatesAsync);
                checkThread.Start();
                return false;
            }
        }

        private static void CheckForUpdatesAsync()
        {
            TcpClient client;
            NetworkStream stream;
            StreamWriter writer;
            StreamReader reader;
            string response;
            int time;

            try 
	        {	      
                client = new TcpClient("chadderz.is-a-geek.com", 80);
                stream = client.GetStream();
                writer = new StreamWriter(stream);
                writer.Write(Properties.Resources.UpdateRequest);
                writer.Flush();
                stream.Flush();

                time = 0;
                while (!stream.DataAvailable && time++ < 200)
                    Thread.Sleep(25);

                if (stream.DataAvailable)
                {
                    reader = new StreamReader(stream);
                    time = 0;
                    while (time++ < 20)
                    {
                        response = reader.ReadLine();

                        if (response.StartsWith("AppVersion: ", StringComparison.OrdinalIgnoreCase))
                        {
                            Properties.CToolsSettings.Default.lastTimeUpdate = response.Substring(12);
                            Properties.CToolsSettings.Default.Save();
                            break;
                        }
                    }
                }

                stream.Close();
	        }
	        catch (Exception)
	        {
	        }
        }

        public static void Update()
        {
            FormWaiting waiting;
            string file;

            if (MessageBox.Show(string.Format(Properties.ResourceSet.MessageUpdateAvailable, Properties.CToolsSettings.Default.lastTimeUpdate), Properties.ResourceSet.MessageBoxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            waiting = new FormWaiting();

            try
            {

                waiting.Show();
                Application.DoEvents();
                WebClient c;
                c = new WebClient();
                c.DownloadFile("http://chadderz.is-a-geek.com/ctools/setup.exe", file = Path.ChangeExtension(Path.GetTempFileName(), "exe"));

                Process.Start(file);

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                waiting.Close();
                MessageBox.Show(string.Format(Properties.ResourceSet.MessageErrorUpdate, ex.Message), Properties.ResourceSet.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
