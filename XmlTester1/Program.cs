using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XmlTester1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Deserialize();

            Post();

        }

        [XmlRoot("CustomField")]
        public class CustomField
        {
            [XmlElement("Field")]
            public List<Field> Fields { get; set; }
        }

        public class Field
        {
            [XmlAttribute("name")]
            public string Name { get; set; }

            [XmlText()]
            public string FieldText { get; set; }
        }

        public static CustomField Deserialize()
        {
            string xml = "<CustomField><Field name = \"Credits\"> Editor, Chris Wyatt; music, Mic</Field><Field name = \"Participants\"> Narrator, Anthony Hopkins.</Field><Field name = \"Organization: Creator\">[presented by] Cooln </Field ></CustomField>";

            var item = DeserializeObject<CustomField>(xml);

            foreach(var prop in item.Fields)
            {
                Console.WriteLine("===========NAME============ \n" + prop.Name);
                Console.WriteLine("=========FIELD TEXT======== \n" + prop.FieldText);
            }
            
            return item;
        }

        private static T DeserializeObject<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var tr = new StringReader(xml))
            {
                return (T)serializer.Deserialize(tr);
            }
        }

        // Serializing
        public static void Post()
        {
            CustomField cf = new CustomField
            {
                Fields = new List<Field>()
            };


            Field field1 = new Field
            {
                Name = "Destination",
                FieldText = "Oregon Trail"
            };

            Field field2 = new Field
            {
                Name = "Location",
                FieldText = "None of your biz"
            };

            cf.Fields.Add(field1);

            var item = SerializeObject(cf);

            Console.ReadLine();
            Console.WriteLine("===========XML============ \n" + item);
            Console.WriteLine();
        }

        private static string SerializeObject<T>(T field)
        {
            var ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            var serializer = new XmlSerializer(typeof(T));

            var settings = new XmlWriterSettings();

            settings.OmitXmlDeclaration = true;

            var stringWriter = new StringWriter();

            using (var writer = XmlWriter.Create(stringWriter, settings))
            {
                serializer.Serialize(writer, field, ns);
                return stringWriter.ToString();
            }
        }
    }
}
