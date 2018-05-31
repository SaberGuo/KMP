using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
namespace Infranstructure.Tool
{
    public static class XMLDeserializerHelper
    {
        /// <summary>
        /// Deserialization XML document
        /// </summary>
        /// <typeparam name="T">>The class type which need to deserializate</typeparam>
        /// <param name="xmlPath">XML path</param>
        /// <returns>Deserialized class object</returns>
        public static T Deserialization<T>(T obj,string xmlPath)
        {
            //check file is exists in case
            if (File.Exists(xmlPath))
            {
                //read file to memory
                StreamReader stream = new StreamReader(xmlPath);
                //declare a serializer
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                try
                {
                    //Do deserialize 
                    return (T)serializer.Deserialize(stream);
                }
                //if some error occured,throw it
                catch (InvalidOperationException error)
                {
                    throw error;
                }
                //finally close all resourece
                finally
                {
                    //close the reader stream
                    stream.Close();
                    //release the stream resource
                    stream.Dispose();
                }
            }
            //File not exists
            else
            {
                //throw data error exception
                throw new InvalidDataException("Can not open xml file,please check is file exists.");
            }
        }

        /// <summary>
        /// Serializate class object to xml document
        /// </summary>
        /// <typeparam name="T">The class type which need to serializate</typeparam>
        /// <param name="obj">The class object which need to serializate</param>
        /// <param name="outPutFilePath">The xml path where need to save the result</param>
        /// <returns>run result</returns>
        public static bool Serialization<T>(T obj, string outPutFilePath)
        {
            //Declare a boolean value to mark the run result
            bool result = true;
            //Declare a xml writer
            XmlWriter writer = null;
            MemoryStream ms = new MemoryStream();
            try
            {

                //create a stream which write data to xml document.
                writer = XmlWriter.Create(outPutFilePath, new XmlWriterSettings
                {
                    //set xml document style - auto create new line
                    Indent = true,
                });
            }
            //if some error occured,throw it
            catch (ArgumentException error)
            {
                result = false;
                throw error;
            }
            //declare a serializer.
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            try
            {
                //Serializate the object
                serializer.Serialize(writer, obj);

            }
            //if some error occured,throw it
            catch (InvalidOperationException error)
            {
                result = false;
                throw error;
            }
            //At finally close all resource
            finally
            {
                //close xml stream
                writer.Close();
            }
            return result;
        }

        public static string SerializationWithoutNameSpaceAndDeclare<T>(T obj)
        {
            //Declare a boolean value to mark the run result
            string result = string.Empty;
            //Declare a xml writer
            XmlWriter writer = null;
            XmlSerializerNamespaces nameSpace;
            MemoryStream ms = new MemoryStream();
            try
            {

                //create a stream which write data to xml document.
                writer = XmlWriter.Create(ms, new XmlWriterSettings
                {
                    //set xml document style - auto create new line
                    Indent = true,
                    //set xml has no declaration
                    OmitXmlDeclaration = true,

                    NamespaceHandling = NamespaceHandling.OmitDuplicates
                });
                nameSpace = new XmlSerializerNamespaces();
                nameSpace.Add("", "");
            }
            //if some error occured,throw it
            catch (ArgumentException error)
            {
                throw error;
            }
            //declare a serializer.
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            try
            {
                //Serializate the object
                serializer.Serialize(writer, obj, nameSpace);
                return Encoding.UTF8.GetString(ms.GetBuffer());
            }
            //if some error occured,throw it
            catch (InvalidOperationException error)
            {
                throw error;
            }
            //At finally close all resource
            finally
            {
                //close xml stream
                writer.Close();

                ms.Close();
            }
        }

        /// <summary>
        /// Serializate class object to string
        /// </summary>
        /// <typeparam name="T">The class type which need to serializate</typeparam>
        /// <param name="obj">The class object which need to serializate</param>
        /// <param name="outPutFilePath">The xml path where need to save the result</param>
        /// <returns>run result</returns>
        public static string Serialization<T>(T obj)
        {
            //Declare a boolean value to mark the run result
            string result = string.Empty;
            //Declare a MemoryStream to save result
            MemoryStream stream = null;
            try
            {
                //create a memorystream which write data to memory.
                stream = new MemoryStream();
            }
            //if some error occured,throw it
            catch (ArgumentException error)
            {
                throw error;
            }
            //declare a serializer.
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            try
            {
                //Serializate the object
                serializer.Serialize(stream, obj);
                result = Encoding.UTF8.GetString(stream.ToArray());
            }
            //if some error occured,throw it
            catch (InvalidOperationException error)
            {
                throw error;
            }
            //At finally close all resource
            finally
            {
                //close xml stream
                stream.Close();
            }
            return result;
        }
    }
}
