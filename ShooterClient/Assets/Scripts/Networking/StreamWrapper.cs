using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using UnityEngine;

namespace PacketManager
{
    public class StreamWrapper : IDisposable
    {
        private NetworkStream stream;

        public StreamWrapper(NetworkStream stream)
        {
            this.stream = stream;
        }

        public bool DataAvailable => stream.DataAvailable;

        #region read
        public byte[] ReadBytes(int cout)
        {
            byte[] resultBytes = new byte[cout];
            stream.Read(resultBytes, 0, cout);
            return resultBytes;
        }

        public int ReadInt32()
        {
            byte[] resultBytes = new byte[4];
            stream.Read(resultBytes, 0, 4);
            return BitConverter.ToInt32(resultBytes, 0);
        }

        public float ReadFloat()
        {
            return BitConverter.ToSingle(ReadBytes(4), 0);
        }

        public string ReadString(int length)
        {
            return Encoding.UTF8.GetString(ReadBytes(length));
        }

        public string ReadString()
        {
            var stringLength = ReadInt32();
            return ReadString(stringLength);
        }

        public Vector3 ReadVector3()
        {
            return new Vector3(ReadFloat(), ReadFloat(), ReadFloat());
        }

        public Quaternion ReadQuaternion()
        {
            return new Quaternion(ReadFloat(), ReadFloat(), ReadFloat(), ReadFloat());
        }
        #endregion

        #region write
        public void WriteBytes(byte[] bytes)
        {
            stream.Write(bytes, 0, bytes.Length);
        }

        public void WriteInt32(int value)
        {
            WriteBytes(BitConverter.GetBytes(value));
        }

        public void WriteFloat(float value)
        {
            WriteBytes(BitConverter.GetBytes(value));
        }

        public void WriteString(string value)
        {
            WriteInt32(value.Length);
            WriteBytes(Encoding.UTF8.GetBytes(value));
        }

        public void WriteVector3(Vector3 vector)
        {
            WriteFloat(vector.x);
            WriteFloat(vector.y);
            WriteFloat(vector.z);
        }

        public void WriteQuaternion(Quaternion quaternion)
        {
            WriteFloat(quaternion.x);
            WriteFloat(quaternion.y);
            WriteFloat(quaternion.z);
            WriteFloat(quaternion.w);
        }
        #endregion

        public void Dispose()
        {
            stream.Close();
            stream = null;
        }
    }
}
