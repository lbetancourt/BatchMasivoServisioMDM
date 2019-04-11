using System.IO;
using System.Xml.Serialization;

namespace BatchMasivoServisioMDM
{
    public static class Extension
    {       
        public static T Deserialize<T>(this string toDeserialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringReader textReader = new StringReader(toDeserialize))
            {
                return (T)xmlSerializer.Deserialize(textReader);
            }
        }
    }
}