using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Chadsoft.CTools.Models
{
    public class Matrix2x1
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Matrix2x1() { }
        public Matrix2x1(float a)
        {
            X = a; Y = a;
        }
        public Matrix2x1(float x, float y)
        {
            X = x; Y = y; 
        }
        public Matrix2x1(EndianBinaryReader reader)
        {
            if (reader.BaseStream.Length - reader.BaseStream.Position < 0xc)
                throw new InvalidDataException();

            X = reader.ReadSingle();
            Y = reader.ReadSingle();
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
        }
    }

    public class Matrix3x1
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Matrix3x1() { }
        public Matrix3x1(float a)
        {
            X = a; Y = a; Z = a;
        }
        public Matrix3x1(float x, float y, float z)
        {
            X = x; Y = y; Z = z;
        }
        public Matrix3x1(EndianBinaryReader reader) 
        {
            if (reader.BaseStream.Length - reader.BaseStream.Position < 0xc)
                throw new InvalidDataException();

            X = reader.ReadSingle();
            Y = reader.ReadSingle();
            Z = reader.ReadSingle();
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
            writer.Write(Z);
        }

        public static Matrix3x1 operator +(Matrix3x1 left, Matrix3x1 right)
        {
            return new Matrix3x1(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }
        public static Matrix3x1 operator -(Matrix3x1 left, Matrix3x1 right)
        {
            return new Matrix3x1(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Matrix3x1 operator *(Matrix3x1 left, float right)
        {
            return new Matrix3x1(left.X * right, left.Y * right, left.Z * right);
        }
        public static Matrix3x1 operator /(Matrix3x1 left, float right)
        {
            return new Matrix3x1(left.X / right, left.Y / right, left.Z / right);
        }

        public Matrix3x1 Cross(Matrix3x1 right)
        {
            return new Matrix3x1(Y * right.Z - Z * right.Y, Z * right.X - X * right.Z, X * right.Y - Y * right.X);
        }

        public float Length()
        {
            return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }
    }

    public class Matrix4x3
    {
        public float M11 { get; set; }
        public float M12 { get; set; }
        public float M13 { get; set; }
        public float M14 { get; set; }
        public float M21 { get; set; }
        public float M22 { get; set; }
        public float M23 { get; set; }
        public float M24 { get; set; }
        public float M31 { get; set; }
        public float M32 { get; set; }
        public float M33 { get; set; }
        public float M34 { get; set; }

        public Matrix4x3() { }
        public Matrix4x3(float a)
        {
            M11 = a; M12 = a; M13 = a; M14 = a;
            M21 = a; M22 = a; M23 = a; M24 = a;
            M31 = a; M32 = a; M33 = a; M34 = a;
        }
        public Matrix4x3(float sx, float sy, float sz)
        {
            M11 = sx; M12 = 0; M13 = 0; M14 = 0;
            M21 = 0; M22 = sy; M23 = 0; M24 = 0;
            M31 = 0; M32 = 0; M33 = sz; M34 = 0;
        }
        public Matrix4x3(float sx, float sy, float sz, float x, float y, float z)
        {
            M11 = sx; M12 = 0; M13 = 0; M14 = x;
            M21 = 0; M22 = sy; M23 = 0; M24 = y;
            M31 = 0; M32 = 0; M33 = sz; M34 = z;
        }
        public Matrix4x3(float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34)
        {
            M11 = m11; M12 = m12; M13 = m13; M14 = m14;
            M21 = m21; M22 = m22; M23 = m23; M24 = m24;
            M31 = m31; M32 = m32; M33 = m33; M34 = m34;
        }
        public Matrix4x3(EndianBinaryReader reader)
        {
            if (reader.BaseStream.Length - reader.BaseStream.Position < 0xc)
                throw new InvalidDataException();

            M11 = reader.ReadSingle();
            M12 = reader.ReadSingle();
            M13 = reader.ReadSingle();
            M14 = reader.ReadSingle();
            M21 = reader.ReadSingle();
            M22 = reader.ReadSingle();
            M23 = reader.ReadSingle();
            M24 = reader.ReadSingle();
            M31 = reader.ReadSingle();
            M32 = reader.ReadSingle();
            M33 = reader.ReadSingle();
            M34 = reader.ReadSingle();
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(M11);
            writer.Write(M12);
            writer.Write(M13);
            writer.Write(M14);
            writer.Write(M21);
            writer.Write(M22);
            writer.Write(M23);
            writer.Write(M24);
            writer.Write(M31);
            writer.Write(M32);
            writer.Write(M33);
            writer.Write(M34);
        }

        public static Matrix4x3 Combine(Matrix4x3 first, Matrix4x3 next)
        {
            Matrix4x3 result;

            result = new Matrix4x3();
            result.M11 = first.M11 * next.M11 + first.M12 * next.M21 + first.M13 * next.M31;
            result.M12 = first.M11 * next.M12 + first.M12 * next.M22 + first.M13 * next.M32;
            result.M13 = first.M11 * next.M13 + first.M12 * next.M23 + first.M13 * next.M33;
            result.M14 = first.M11 * next.M14 + first.M12 * next.M24 + first.M13 * next.M34 + first.M14;
            result.M21 = first.M21 * next.M11 + first.M22 * next.M21 + first.M23 * next.M31;
            result.M22 = first.M21 * next.M12 + first.M22 * next.M22 + first.M23 * next.M32;
            result.M23 = first.M21 * next.M13 + first.M22 * next.M23 + first.M23 * next.M33;
            result.M24 = first.M21 * next.M14 + first.M22 * next.M24 + first.M23 * next.M34 + first.M24;
            result.M31 = first.M31 * next.M11 + first.M32 * next.M21 + first.M33 * next.M31;
            result.M32 = first.M31 * next.M12 + first.M32 * next.M22 + first.M33 * next.M32;
            result.M33 = first.M31 * next.M13 + first.M32 * next.M23 + first.M33 * next.M33;
            result.M34 = first.M31 * next.M14 + first.M32 * next.M24 + first.M33 * next.M34 + first.M34;

            return result;
        }

        public float[] ToFloat()
        {
            return new float[] { M11, M21, M31, M12, M22, M32, M13, M23, M33, M14, M24, M34 };
        }

        public static Matrix4x3 Identity { get { return new Matrix4x3(1, 1, 1); } }

        public static Matrix4x3 Transformation(Matrix3x1 Scale, Matrix3x1 Rotation, Matrix3x1 Translation)
        {
            Matrix4x3 matrix;

            matrix = new Matrix4x3(Scale.X, Scale.Y, Scale.Z, Translation.X, Translation.Y, Translation.Z);

            return Matrix4x3.Combine(matrix, Matrix4x3.Rotation(Rotation));
        }

        private static Matrix4x3 Rotation(Matrix3x1 Rotation)
        {
            const double radian = Math.PI / 180;
            double alpha, theta, phi;

            alpha = Rotation.X * radian;
            theta = Rotation.Y * radian;
            phi = Rotation.Z * radian;

            return new Matrix4x3((float)(Math.Cos(theta) * Math.Cos(alpha)), -(float)(Math.Cos(phi) * Math.Sin(alpha) + Math.Sin(phi) * Math.Sin(theta) * Math.Cos(alpha)), (float)(Math.Sin(phi) * Math.Sin(alpha) + Math.Cos(phi) * Math.Sin(theta) * Math.Cos(alpha)), 0,
                (float)(Math.Cos(theta) * Math.Sin(alpha)), (float)(Math.Cos(phi) * Math.Cos(alpha) + Math.Sin(phi) * Math.Sin(theta) * Math.Sin(alpha)), -(float)(Math.Sin(phi) * Math.Cos(alpha) + Math.Cos(phi) * Math.Sin(theta) * Math.Sin(alpha)), 0,
                -(float)(Math.Sin(theta)), (float)(Math.Sin(phi) * Math.Cos(theta)), (float)(Math.Cos(phi) * Math.Cos(theta)), 0);
        }
    }
}
