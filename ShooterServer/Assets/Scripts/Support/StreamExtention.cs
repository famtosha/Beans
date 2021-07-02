using System.Text;
using System.IO;
using System;
using UnityEngine;

namespace PacketManager
{
    public static class StreamExtention
    {
        #region read
        public static byte[] ReadBytes(this Stream stream, int cout)
        {
            byte[] resultBytes = new byte[cout];
            stream.Read(resultBytes, 0, cout);
            return resultBytes;
        }

        public static int ReadInt32(this Stream stream)
        {
            byte[] resultBytes = new byte[4];
            stream.Read(resultBytes, 0, 4);
            return BitConverter.ToInt32(resultBytes, 0);
        }

        public static float ReadFloat(this Stream stream)
        {
            return BitConverter.ToSingle(stream.ReadBytes(4), 0);
        }

        public static string ReadString(this Stream stream, int length)
        {
            return Encoding.UTF8.GetString(stream.ReadBytes(length));
        }

        public static string ReadString(this Stream stream)
        {
            var stringLength = stream.ReadInt32();
            return stream.ReadString(stringLength);
        }

        public static Vector3 ReadVector3(this Stream stream)
        {
            return new Vector3(stream.ReadFloat(), stream.ReadFloat(), stream.ReadFloat());
        }

        public static Quaternion ReadQuaternion(this Stream stream)
        {
            return new Quaternion(stream.ReadFloat(), stream.ReadFloat(), stream.ReadFloat(), stream.ReadFloat());
        }

        public static bool ReadBool(this Stream stream)
        {
            int readed = stream.ReadByte();
            return Convert.ToBoolean(readed);
        }
        #endregion

        #region write
        public static void WriteBool(this Stream stream, bool value)
        {
            byte w = Convert.ToByte(value);
            stream.WriteByte(w);
        }

        public static void WriteBytes(this Stream stream, byte[] value)
        {
            stream.Write(value, 0, value.Length);
        }

        public static void WriteInt32(this Stream stream, int value)
        {
            stream.WriteBytes(BitConverter.GetBytes(value));
        }

        public static void WriteFloat(this Stream stream, float value)
        {
            stream.WriteBytes(BitConverter.GetBytes(value));
        }

        public static void WriteString(this Stream stream, string value)
        {
            stream.WriteInt32(value.Length);
            stream.WriteBytes(Encoding.UTF8.GetBytes(value));
        }

        public static void WriteVector3(this Stream stream, Vector3 vector)
        {
            stream.WriteFloat(vector.x);
            stream.WriteFloat(vector.y);
            stream.WriteFloat(vector.z);
        }

        public static void WriteQuaternion(this Stream stream, Quaternion value)
        {
            stream.WriteFloat(value.x);
            stream.WriteFloat(value.y);
            stream.WriteFloat(value.z);
            stream.WriteFloat(value.w);
        }
        #endregion
    }
}
