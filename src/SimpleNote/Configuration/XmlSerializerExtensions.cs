using System.IO;
using System.Text;

namespace System.Xml.Serialization
{
    /// <summary>
    /// Extension methods for System.Xml.Serialization.XmlSerializer
    /// </summary>
    public static class XmlSerializerExtensions
    {
        private class UTF8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }

        public static string SerializeObject(this XmlSerializer xmlSerializer, object obj, XmlWriterSettings settings)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            using (StringWriter stringWriter = new UTF8StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    xmlSerializer.Serialize(xmlWriter, obj);
                    return stringWriter.ToString();
                }
            }
        }

        public static string SerializeObject(this XmlSerializer xmlSerializer, object obj, bool indent = false)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = indent
            };

            return xmlSerializer.SerializeObject(obj, settings);
        }

        public static T DeserializeObject<T>(this XmlSerializer xmlSerializer, string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Argument is empty", nameof(value));

            using (StringReader stringReader = new StringReader(value))
            {
                return (T)xmlSerializer.Deserialize(stringReader)!;
            }
        }
    }
}