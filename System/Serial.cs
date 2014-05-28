#pragma warning disable 1587,1591, 162
/* oOo * 11/14/2007 : 10:22 PM */

using System;
using System.Xml.Serialization;

namespace System
{

	namespace IO
	{
		/* oOo * 11/15/2007 : 5:53 AM */
		/**
		 * my intention is to call a open and save function when the program launches
		 * and closes.  all i would need to do is send the required data such as a built
		 * in configuration that would initialize a window to it's previous posision.
		 * so you would Open(), and then Add(Component,'Setting',bool) as the bool value
		 * would be set to false for items that are not serializable.
		 **/
		public class Serial
		{
			
			static public void SerializeXml(string filepath,Type T,object o)
			{
				XmlSerializer xser = new XmlSerializer(T);
				{
					if (File.Exists(filepath)) File.Delete(filepath);
					using (FileStream fs = new FileStream(filepath,FileMode.OpenOrCreate))
						xser.Serialize(fs,o);
				}
			}
			static public void SerializeXml(string filepath,Type T,object o, XmlAttributeOverrides xao)
			{
				XmlSerializer xser;
				xser = new XmlSerializer(T,xao);
				if (File.Exists(filepath)) File.Delete(filepath);
				using (FileStream fs = new FileStream(filepath,FileMode.OpenOrCreate))
					xser.Serialize(fs,o);
			}
			static public string SerializeString(Type T,object o)
			{
				XmlSerializer xser;
				string str = null;
				using (MemoryStream ms = new MemoryStream())
				{
					xser = new XmlSerializer(T);
					xser.Serialize(ms,o);
					str = System.Text.Encoding.Default.GetString(ms.ToArray());
				}
				return str;
			}
			static public T DeSerialize<T>(string file)
			{
				XmlSerializer xser = new XmlSerializer(typeof(T));
				object result = null;
				using (FileStream fs = File.Open(file,FileMode.OpenOrCreate))
				{
					result = xser.Deserialize(fs);
				}
				return (T)result;
			}
			static public T DeSerialize<T>(string file, XmlAttributeOverrides xao)
			{
				XmlSerializer xser = new XmlSerializer(typeof(T),xao);
				FileStream fs = File.Open(file,FileMode.Open);
				T obj = (T)xser.Deserialize(fs);
				fs.Dispose();
				fs = null;
				return obj;
			}
			static public object DeSerialize(string file, Type T)
			{
				XmlSerializer xser = new XmlSerializer(T);
				FileStream fs = File.Open(file,FileMode.Open);
				object obj = (object)xser.Deserialize(fs);
				fs.Dispose();
				fs = null;
				return obj;
			}
			static public T DeSerialize<T>(Stream fs)
			{
				XmlSerializer xser = new XmlSerializer(typeof(T));
				T obj = (T)xser.Deserialize(fs);
				return obj;
			}
		}
	}

}
