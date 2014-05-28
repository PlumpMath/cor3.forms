#pragma warning disable 1587,1591, 162
/* oOo * 11/14/2007 : 10:22 PM */
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace System
{
	namespace Cor3.Forms
	{

		/// <summary>
		/// Originally the class was designed to run static (with a static path definition in-side),
		/// but now, that's all changed — and it's not working yet.
		/// <br/><br/>
		/// Note that the old “Host” application is no longer working with this.
		/// </summary>
		[XmlRootAttribute("MRU")]
		public class MRUClass : SerializableList<MRUItem>
		{
			[XmlIgnore] public EventHandler toolstripitem_click_handler;
			
			#region Static & Constant
			internal protected const string default_file_name = "mru.xml";
			internal protected static string default_directory_path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
			#endregion
			
			#region Paths
			[XmlIgnore] internal protected string mru_directory_path = default_directory_path;
			[XmlIgnore] internal protected string mru_file_name = default_file_name;
			[XmlIgnore] virtual public string FilePath { get { return System.IO.Path.Combine(mru_directory_path,mru_file_name); } }
			#endregion
			
			#region Path Information
			[XmlIgnore] internal protected System.IO.DirectoryInfo DirectoryInfo { get { return new System.IO.DirectoryInfo(mru_directory_path); } }
			[XmlIgnore] internal protected System.IO.FileInfo FileInfo { get { return new System.IO.FileInfo(FilePath); } }
			// —————————————————————————————
			[XmlIgnore] internal protected bool Exists { get { return System.IO.File.Exists(FilePath); } }
			#endregion
			#region Item Dictionary
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
			// —————————————————————————————
			public void AddItem(string name, long date)
			{
				print_info();
				Dictionary<string,MRUItem> ti = ItemDict;
				if (ti.ContainsKey(name))
				{
					Global.statR("DID THE ITEM GET REMOVED?... {0}",innerList.Remove(ti[name]));
				}
				else Global.statR("NOT REMOVED");
				innerList.Insert(0,new MRUItem(name,date));
				innerList.Sort(SortByDate);
				Save();
				ti.Clear();
				ti=null;
			}
			public void AddItem(string name) { AddItem(name,DateTime.Now.ToBinary()); }
			int SortByDate(MRUItem a, MRUItem b)
			{
				return b.FileDate.CompareTo(a.FileDate);
			}
			#endregion
			
			public ToolStripItem[] ToolStripItems
			{
				get
				{
					List<ToolStripItem> ix = new List<ToolStripItem>();
					foreach (MRUItem i in Items) ix.Add(i.TsItem(toolstripitem_click_handler));
					return ix.ToArray();
				}
			}
			
			internal protected void print_info() { Global.statG("NumItems (MRU): {0}",Items.Length); }
			[XmlIgnore] public string InfoString { get{ return string.Format("NumItems (MRU): {0}",Items.Length); } }
			
			virtual public void Save() { Serial.SerializeXml(FilePath,typeof(MRUClass),this); }
			
			
			/// <param name="mru_full_file_path">
			/// <para>if the file doesn't exist, then it should be created when serialization takes place.</para>
			/// </param>
			/// <param name="handler">
			/// <para>the default event, assigned to the created tool-strip items.</para>
			/// </param>
			public MRUClass(string mru_full_file_path, EventHandler handler) : base()
			{
				mru_directory_path = System.IO.Path.GetDirectoryName(mru_full_file_path);
				mru_file_name = System.IO.Path.GetFileName(mru_full_file_path);
				toolstripitem_click_handler = handler;
				MRUClass moo = (System.IO.File.Exists(FilePath)) ?
					Serial.DeSerialize<MRUClass>(FilePath):
					new MRUClass(handler,mru_directory_path,mru_file_name);
				this.innerList = moo.innerList;
			}
			public MRUClass() : this(null,default_directory_path,default_file_name) {}
			
			public MRUClass(EventHandler eventh, string initial_directory_path, string initial_mru_file_name) : base()
			{
				toolstripitem_click_handler = eventh;
				mru_file_name = initial_mru_file_name;
				mru_directory_path = initial_directory_path;
			}
			/// <param name="eventh">Attached to the toolstrip-menu-item reflecting the MRU-file</param>
			/// <param name="initial_directory_path">the default directory to store the mru file</param>
			public MRUClass(EventHandler eventh, string initial_directory_path)
				: this(eventh,initial_directory_path,default_file_name)
			{
			}
		}
	}

}
