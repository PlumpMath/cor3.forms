#pragma warning disable 1587,1591, 162
/* oOo * 11/14/2007 : 10:22 PM */

using System;
using System.Collections.Generic;
using System.Cor3.Forms;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace System
{
	namespace IO
	{



		public delegate void MruEvent(string path);
		[XmlRootAttribute("MRU")]
		public class MRU // revision 13
			: SerializableList<MRUItem>
		{
			/*———————————————————————————*/
			const string default_file_name = "mru.xml";
			[XmlIgnore] static public string filename { get { return Path.Combine(directory,default_file_name); } }
			[XmlIgnore] public EventHandler eh;
			[XmlIgnore] public Dictionary<string,MRUItem> ItemDict
			{
				get
				{
					Dictionary<string,MRUItem> dct = new Dictionary<string,MRUItem>();
					foreach (MRUItem i in Items)
					{
						dct.Add(i.FilePath,i);
					}
					return dct;
				}
			}
			/*———————————————————————————*/
			[XmlIgnore] static public string directory = string.Empty;
			[XmlIgnore] static bool Exists { get { return File.Exists(filename); } }
			/*———————————————————————————*/
			public ToolStripItem[] ToolStripItems
			{
				get
				{
					List<ToolStripItem> ix = new List<ToolStripItem>();
					foreach (MRUItem i in Items) ix.Add(i.TsItem(eh));
					return ix.ToArray();
				}
			}
			/*———————————————————————————*/
			void info() { Global.statG("NumItems (MRU): {0}",Items.Length); }
			public void AddItem(string name, long date) {
				info();
				Dictionary<string,MRUItem> ti = ItemDict;
				if (ti.ContainsKey(name)) innerList.Remove(ti[name]);
				innerList.Insert(0,new MRUItem(name,date)); Save();
				ti.Clear(); ti=null;
			}
			public void AddItem(string name) {
				AddItem(name,DateTime.Now.ToBinary()); Save();
			}
			
			public void Save() { Serial.SerializeXml(filename,typeof(MRU),this); }
			
			public MRU() : base() {  }
			public MRU(EventHandler eventh,string basepath) : base() { this.eh = eventh; directory = basepath; }
			
			static public MRU Load(string basepath,EventHandler e)
			{
				directory = basepath;
				MRU moo = (File.Exists(filename)) ? Serial.DeSerialize<MRU>(filename):
					moo = new MRU(e,basepath);
				moo.eh = e;
				return moo;
			}
		}
	}

}
