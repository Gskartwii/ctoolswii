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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace Chadsoft.CTools.Image
{
    public abstract class ImageData : IDisposable
    {
        public ImageData() { }
        ~ImageData()
        {
            Dispose(false);
        }

        public bool Changed { get; set; }
        public abstract string Type { get; }
        public abstract ImageDataFormat Format { get; }
        public abstract int Levels { get; }
        public abstract int Width { get; }
        public abstract int Height { get; }

        public abstract int GetWidth(int level);
        public abstract int GetHeight(int level);

        public abstract ImageDataFormat[] GetFormats();
        public abstract byte[] GetData(int level, ProgressChangedEventHandler progress);
        public virtual int[] GetColorData(int level, ProgressChangedEventHandler progress)
        {
            int[] result;
            byte[] data;

            data = GetData(level, progress);
            result = new int[data.Length >> 2];

            Marshal.Copy(data, 0, Marshal.UnsafeAddrOfPinnedArrayElement(result, 0), data.Length);

            return result;
        }
        public virtual byte[] GetData(int level)
        {
            return GetData(level, null);
        }
        public virtual int[] GetColorData(int level)
        {
            return GetColorData(level, null);
        }

        public abstract void ImportTo(byte[] data, int level, ProgressChangedEventHandler progress);
        public virtual void ImportTo(int[] data, int level, ProgressChangedEventHandler progress)
        {
            byte[] rawData;

            rawData = new byte[data.Length << 2];

            Marshal.Copy(Marshal.UnsafeAddrOfPinnedArrayElement(data, 0), rawData, 0, rawData.Length);

            ImportTo(rawData, level, progress);
        }
        public virtual void ImportTo(byte[] data, int level)
        {
            ImportTo(data, level, null);
        }
        public virtual void ImportTo(int[] data, int level)
        {
            ImportTo(data, level, null);
        }

        public abstract void Import(byte[] data, ImageDataFormat format, int levels, int width, int height, ProgressChangedEventHandler progress);
        public virtual void Import(int[] data, ImageDataFormat format, int levels, int width, int height, ProgressChangedEventHandler progress)
        {
            byte[] rawData;

            rawData = new byte[data.Length << 2];

            Marshal.Copy(Marshal.UnsafeAddrOfPinnedArrayElement(data, 0), rawData, 0, rawData.Length);

            Import(rawData, format, levels, width, height, progress);
        }
        public virtual void Import(byte[] data, ImageDataFormat format, int levels, int width, int height)
        {
            Import(data,format, levels, width, height, null);
        }
        public virtual void Import(int[] data, ImageDataFormat format, int levels, int width, int height)
        {
            Import(data, format, levels, width, height, null);
        }

        public virtual void Save(Stream output)
        {
            throw new NotImplementedException();
        }

        public abstract void Reformat(ImageDataFormat format, int levels, int width, int height, ProgressChangedEventHandler progress);
        public virtual void Reformat(ImageDataFormat format, int levels, int width, int height)
        {
            Reformat(format, levels, width, height, null);
        }

        public static Bitmap ToBitmap(byte[] data, int width, int height)
        {
            Bitmap result;
            BitmapData resultData;

            result = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            resultData = result.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(data, 0, resultData.Scan0, data.Length);

            result.UnlockBits(resultData);

            return result;
        }
        public static Bitmap ToBitmap(int[] data, int width, int height)
        {
            byte[] rawData;

            rawData = new byte[data.Length << 2];

            Marshal.Copy(Marshal.UnsafeAddrOfPinnedArrayElement(data, 0), rawData, 0, rawData.Length);

            return ToBitmap(rawData, width, height);
        }

        public static byte[] GetData(Bitmap bitmap)
        {
            BitmapData bitmapData;
            byte[] data;

            bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            data = new byte[bitmapData.Height * bitmapData.Stride];

            Marshal.Copy(bitmapData.Scan0, data, 0, data.Length);

            bitmap.UnlockBits(bitmapData);

            return data;
        }

        public static byte[] Resize(byte[] data, int oldWidth, int oldHeight, int width, int height)
        {
            return GetData(new Bitmap(ToBitmap(data, oldWidth, oldHeight), width, height));
        }
        
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            
        }
    }
}
