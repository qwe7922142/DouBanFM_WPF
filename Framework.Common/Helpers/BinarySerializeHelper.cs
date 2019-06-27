using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Framework.Common.Helpers
{
    /// <summary>
    /// 二进制序列化帮助类
    /// </summary>
    public sealed class BinarySerializeHelper
    {
        /// <summary>
        /// 序列化对象到指定的文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        public static void Serialize(string path, object data)
        {
            using (FileStream stream = File.OpenWrite(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, data);
            }
        }
        /// <summary>
        /// 从文件中反序列化得到指定类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string path)
        {
            using (FileStream stream = File.OpenRead(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                var result = (T)formatter.Deserialize(stream);
                return result;
            }
        }
    }
}
