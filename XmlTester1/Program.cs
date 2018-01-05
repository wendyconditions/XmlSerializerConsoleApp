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
            GetByTitleKey();

            //bool isConnected = IsServerConnected("Server=;Database=;");
            //Console.WriteLine("DB isConnected = {0}", isConnected);

            //if (isConnected)
            //{
            //    //ExecuteInsert();

            //    //Post(6);

            //    //ExecuteDelete();

            //    //ExecuteSelect();

            //    //ExecuteSelectById();

                
            //}

            //Console.ReadLine();
        }

        //private static bool IsServerConnected(string connectionString)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            connection.Open();
        //            return true;
        //        }
        //        catch (SqlException ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //            return false;
        //        }
        //    }
        //}

        public static CustomField GetByTitleKey()
        {
            string xml = "<CustomField><Field name = \"Credits\"> Editor, Chris Wyatt; music, Nathan Wang; historical advisors, Yehuda Bauer, Michael Berenbaum, Mic</Field><Field name = \"Participants\"> Narrator, Anthony Hopkins.</Field><Field name = \"Organization: Creator\">[presented by] Steven Spielberg in association with Survivors of the Shoah Visual History Foundation </Field ></CustomField>";

            var item = DeserializeObject<CustomField>(xml);
            
            

            foreach(var prop in item.Fields)
            {
               
            }
            
            return item;
        }

        public static T DeserializeObject<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var tr = new StringReader(xml))
            {
                return (T)serializer.Deserialize(tr);
            }
        }

        [XmlRoot("CustomField")]
        public class CustomField
        {
            [XmlElement("Field")]
            public List<Field> Fields { get; set; }
        }

        public class Field
        {
            private string attribute_name;
            private string element_text;

            [XmlAttribute("name")]
            public string Name
            {
                get { return attribute_name; }
                set { attribute_name = value; }
            }

            [XmlText()]
            public string FieldText
            {
                get { return element_text; }
                set { element_text = value; }
            }
        }


        //static void Post(int titleKey, Field field)
        //{
        //    var item = SerializeObject(field);
        //}

        //private static string SerializeObject<T>(T field)
        //{
        //    var ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

        //    var serializer = new XmlSerializer(typeof(T));

        //    var settings = new XmlWriterSettings();
        //    // settings.Indent = false;
        //    settings.OmitXmlDeclaration = true;

        //    var stringWriter = new StringWriter();

        //    using (var writer = XmlWriter.Create(stringWriter, settings))
        //    {
        //        serializer.Serialize(writer, field, ns);
        //        return stringWriter.ToString();
        //    }
        //}
    }
}
