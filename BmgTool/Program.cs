// CTools bmg tool - Text editing service for CTools
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
using System.Resources;
using System.Windows.Forms;

namespace Chadsoft.CTools.Bmg
{
    static class Program
    {
        private static ResourceManager manager;

        [STAThread]
        static void Main(string[] args)
        {
            BmgToolInstance instance;
            Application.EnableVisualStyles();

            if (ToolManager.CheckForUpdates())
            {
                ToolManager.Update();
            }

            using (instance = new BmgToolInstance(args))
            {
                instance.Run();
            }

            Environment.Exit(0);
        }

        private static void LoadManager()
        {
            manager = new ResourceManager("Chadsoft.CTools.Bmg.Properties.StringResource", typeof(Program).Assembly);
        }

        internal static string GetString(string key, params object[] args)
        {
            if (manager == null)
                LoadManager();

            if (string.IsNullOrEmpty(manager.GetString(key)))
                return null;

            return String.Format(manager.GetString(key), args);
        }
    }
}
