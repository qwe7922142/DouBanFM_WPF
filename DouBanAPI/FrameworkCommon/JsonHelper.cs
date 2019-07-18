using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Framework.Common.Helpers
{
    public sealed class JsonHelper
    {
        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                return (T)ser.ReadObject(stream);
            }
        }
    }
}
