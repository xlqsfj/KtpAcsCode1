using System.IO;
using System.Xml.Serialization;

namespace KtpAcs.Infrastructure.Serialization
{
    public static class XmlSerializationExtension
    {
        public static string ToXml<T>(this T toSerialize)
        {
            var xmlSerializer = new XmlSerializer(toSerialize.GetType());
            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }
    }
}