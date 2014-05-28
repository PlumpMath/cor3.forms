#pragma warning disable 1587,1591, 162
/* oOo * 11/14/2007 : 10:22 PM */
using System.Collections.Generic;
using System.Cor3;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.WTF;

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
		using System;
		using System.Collections.Generic;
		using System.IO;
		using System.Xml.Serialization;

	}
	
	namespace WTF
	{
		public partial class TreeUtil
		{
			static public TreeNode tree_find_first_node(TreeNode node)
			{
				TreeNode tn = node;
				while (tn.Parent!=null) tn = tn.Parent;
				return tn;
			}

			#region the old stuff
			#region ' ToTag '
			/// <param name="ctl">Acts on ToolStripItem,Menu,Control. All else is 'null'.</param>
			static public object ToTag(object ctl)
			{
				object tag = null;
				if (ctl is ToolStripItem) return (ctl as ToolStripItem).Tag;
				else if (ctl is Menu) return (ctl as Menu).Tag;
				else if (ctl is Control) return (ctl as Control).Tag;
				return tag;
			}
			/// <param name="ctl">Acts on ToolStripItem,Menu,Control. All else is 'null'.</param>
			static public T ToTag<T>(object ctl) { return (T)ToTag(ctl); }
			#endregion
			#region ' TnTag '
			static public bool TnTag(TreeView tv, object tag) { foreach (TreeNode ts in tv.Nodes) if (ts.Tag == tag) return true; return false; }
			static public bool TnTag(TreeNode tn, object tag) { foreach (TreeNode ts in tn.Nodes) if (ts.Tag == tag) return true; return false; }
			/// <summary>returns null if the tag is not in the root nodes, the treenode otherwise.</summary>
			#endregion
			#region ' TnByTag '
			static public TreeNode TnByTag(TreeView tv, object tag) { TreeNode tr = null; if (TnTag(tv,tag)){ foreach (TreeNode tn in tv.Nodes) { if (tn.Tag==tag) tr = tn; } } return tr; }
			/// <summary> returns null if the tag is not in tn's child nodes, the treenode otherwise. </summary>
			static public TreeNode TnByTag(TreeNode tn, object tag) { TreeNode tr = null; if (TnTag(tn,tag)){ foreach (TreeNode tc in tn.Nodes) { if (tc.Tag==tag) tr = tn; } } return tr; }
			#endregion
			#region ' TnInsGet '
			static public TreeNode TnInsGet( TreeView tv, object tag, string text, object ntag ) { TreeNode tr = null; if (TnTag(tv,tag)) { return TnByTag(tv,tag); } else { tr = tv.Nodes.Add(text); tr.Tag = ntag; } return tr; }
			static public TreeNode TnInsGet(TreeNode tn, object tag,string text, object ntag ) { TreeNode tr = null; if (TnTag(tn,tag)) { return TnByTag(tn,tag); } else { tr = tn.Nodes.Add(text); tr.Tag = ntag; } return tr; }
			#endregion
			#region ' TnTryInsert '
			static public TreeNode TnTryInsert( TreeView tv, object tag, string text, object ntag ) { TreeNode tr = null; if (TnTag(tv,tag)) { tr = (TnByTag(tv,tag).Nodes.Add(text)); tr.Tag = ntag; } else { tr = tv.Nodes.Add(text); tr.Tag = ntag; } return tr; }
			static public TreeNode TnTryInsert(TreeNode tn, object tag,string text, object ntag) { TreeNode tr = null; if (TnTag(tn,tag)) { tr = TnByTag(tn,tag).Nodes.Add(text); tr.Tag = ntag; } else { tr = tn.Nodes.Add(text); tr.Tag = ntag; } return tr; }
			#endregion
			#region ' TnFromString '
			static public TreeNode TnFromString(TreeView tv)
			{
				return null;
			}
			#endregion
			#region ' TreeList '
			/// <summary>
			/// THIS FUNCTION DOES NOTHING!
			/// </summary>
			/// <param name="tv"></param>
			/// <returns></returns>
			static public string TreeList(TreeView tv)
			{
				foreach (TreeNode tn in tv.Nodes)
				{
					
				}
				return null;
			}
			#endregion
			#endregion
		}
	}
	public partial class ControlUtil
	{
		#region ' lvcols '
		static public void lvcols(ListView lv, params string[] columns) {
			lv.Columns.Clear();
			foreach (string str in columns) { lv.Columns.Add(str);  }
		}
		#endregion
		#region ' lvsize '
		static public void lvsize(ListView lv, ColumnHeaderAutoResizeStyle style) {
			try { foreach (ColumnHeader ch in lv.Columns) ch.AutoResize(style);
			} catch {}
		}
		static public void lvsize(ListView[] lv, ColumnHeaderAutoResizeStyle style) { for (int i=0;i<lv.Length;i++) lvsize(lv[i],style); }
		#endregion
		#region ' lVisi '
		static public void lVisi(ListView lv, bool flag) { lv.Visible = flag; }
		
		static public void lVisi(ListView[] lvz, bool flag) { foreach (ListView lv in lvz) lv.Visible = flag; }
		#endregion
		#region ' lvAG '
		public static void lvAG(ListView lv, string groupName) { if (lv.Groups[groupName]==null) lv.Groups.Add(groupName,groupName); }
		public static ListViewItem lvAG(ListView lv, string groupName, string itemText) { lvAG(lv,groupName); ListViewItem lvi = lv.Items.Add(itemText); lvi.Group = lv.Groups[groupName]; return lvi; }
		public static ListViewItem lvAG(ListView lv, string groupName, string itemText, int ndx) { lvAG(lv,groupName); ListViewItem lvi = lv.Items.Add(itemText,ndx); lvi.Group = lv.Groups[groupName]; return lvi; }
		#endregion
		#region ' LvCh '
		public static void LvCh(ListView lv, params ColumnHeader[] item) { lv.Columns.AddRange(item); }
		public static void LvCh(ListView lv, params string[] item) { foreach (string i in item) lv.Columns.Add(i); }
		#endregion
		#region ' LvAdd ' (also old-as .Net v1.0-then upgraded to 2 or upgrading?.. don-remember)
		// try not to reference anything old like this
		public static void LvAdd(ListView lv, ListViewItem[] lvi) { lv.Items.AddRange(lvi); }
		public static ListViewItem LvAdd(ListView lv, ListViewItem lvi) { return lv.Items.Add(lvi); }
		public static ListViewItem LvAdd(ListView lv, string item) { return lv.Items.Add(item); }
		#endregion
		#region ' addlv ' (also old)
		// I think this started around CSMID, but elaborated here.
		public static void addlv(ListView lvx, string[] ast, System.Drawing.Color colar, int indx) { ListViewItem lvi = new ListViewItem(ast[0], indx); lvi.BackColor=colar; for (int i=1;i<ast.Length;i++) lvi.SubItems.Add(ast[i]); lvx.Items.Add(lvi); }
		public static void addlv(ListView lvx, string[] ast, System.Drawing.Color colar) { ListViewItem lvi = new ListViewItem(ast[0]); lvi.BackColor=colar; for (int i=1;i<ast.Length;i++) lvi.SubItems.Add(ast[i]); lvx.Items.Add(lvi); }
		public static void addlv(ListView lvx, string[] ast, int indx) { ListViewItem lvi = new ListViewItem(ast[0], indx); for (int i=1;i<ast.Length;i++) { ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem(); lvsi = lvi.SubItems.Add(ast[i]); lvsi.Tag = i; } lvx.Items.Add(lvi); }
		public static ListViewItem addlv(ListView lvx, string[] ast) { ListViewItem lvi = new ListViewItem(ast[0]); for (int i=1;i<ast.Length;i++) lvi.SubItems.Add(ast[i]); lvx.Items.Add(lvi); return lvi; }
		#endregion
		#region ' lvCodePage ' ??? must be from when I was looking more into unicode
		static public void lvCodePage(ListView lvCodePages){lvCodePage(lvCodePages,false);}
		static public void lvCodePage(ListView lvCodePages, bool UseTag){
			lvCodePages.View = View.Details;
			lvCodePages.FullRowSelect = true;
			lvCodePages.Clear();
			ControlUtil.lvcols(lvCodePages,new string[]{"CodePage", "Name", "Display Name"});
			foreach (EncodingInfo E in Encoding.GetEncodings()) { ListViewItem lvi = addlv(lvCodePages,new string[]{E.CodePage.ToString(),E.Name,E.DisplayName}); if (UseTag) lvi.Tag = E.CodePage; }
			ControlUtil.lvsize(lvCodePages, ColumnHeaderAutoResizeStyle.HeaderSize);
		}
		#endregion
	}
	public partial class ControlUtil
	{
		#region " Filter Types "
		public const string AllFiles = "All Files|*";
		public const string XmlFile = "Xml File|*.xml";
		public const string TextFile = "Text File|*.txt";
		
		public const string RawFile = "Raw|*.raw";
		public const string BinFile = "Bin|*.bin";

		public const string PngImage = "Png Image|*.png";
		public const string BmpImage = "Bitmap (Microsoft)|*.bmp";
		public const string GifImage = "GIF Image|*.gif";
		public const string JpegImage = "JPGE Image|*.jpg;*.jpeg";
		
		static public bool HasCat(string refr) { return HasCat(refr,'|'); }
		static public bool HasCat(string refr,char str) { return refr.IndexOf(str)!=-1; }
		static public bool HasCat(string refr,string str) { return refr.IndexOf(str)!=-1; }
		static public string Make(char sep,params string[] input)
		{
			string r = string.Empty;
			foreach (string s in input)
			{
				r = string.Concat(r,(!hs(s,sep)) ? sep.ToString():string.Empty,s);
			}
			return r.Trim(sep);
		}

		static public bool hs(string input, char split) { return input.IndexOf(split)!=-1; }
		static public string s0(string input, char split) { return input.Split(split)[0]; }
		static public string s1(string input, char split) { return input.Split(split)[1]; }
		static public string s2(string input, char split) { return input.Split(split)[2]; }

		public const string AS3File = "ActionScript3|*.as3";
		
		public const string WaveFile = "Wave File (Microsoft)|*.wav;*.wave";
		public const string AifFile = "Wave File (Apple)|*.aif;*.aiff;*.aifc";

		public const string SmfFile = "Standard Midi Format|*.smf";
		public const string MidiFile = "General Midi Format|*.mid;*.midi";

		public const string RmfFile = "Rich Midi Format|*.rmf";
		public static string WaveFiles { get { return string.Concat(WaveFile,"|",AifFile); } }
		public static string XmlFull { get { return string.Concat(XmlFile,"|",AllFiles); } }
		public static string MidiFiles { get { return string.Concat(SmfFile,"|",MidiFile,"|",RmfFile); } }

		public const string RtfFile = "Rich Text Format|*.rtf";
		public const string HtmlFiles = "Html Formats|*.htm;*.html";
		#endregion
		#region ' FGet '
		/// <summary>
		/// uses a OpenFileDialog to return a file
		/// </summary>
		/// <param name="F">File-Filter</param>
		/// <returns>string path</returns>
		static public string FGet(string F) {
			string relay = string.Empty;
			OpenFileDialog of = new OpenFileDialog();
			of.Filter = F;
			if (of.ShowDialog() == DialogResult.OK) relay = of.FileName;
			of.Dispose();
			of = null;
			return relay;
		}
		/// <summary>
		/// uses a OpenFileDialog to return a file
		/// </summary>
		/// <param name="F">File-Filter</param>
		/// <param name="T">Dialog Title</param>
		/// <returns></returns>
		static public string FGet(string F, string T) {
			string relay = string.Empty;
			OpenFileDialog of = new OpenFileDialog();
			of.Filter = F;
			of.Title = T;
			if (of.ShowDialog() == DialogResult.OK)
				relay = of.FileName;
			of.Dispose();
			of = null;
			return relay;
		}
		#endregion
		#region ' FSave '
		static public string FSave(string S,string F, string T) {
			string relay = string.Empty;
			SaveFileDialog sf = new SaveFileDialog();
			sf.FileName = S;
			sf.Filter = F; sf.Title = T;
			if (sf.ShowDialog() == DialogResult.OK) relay = sf.FileName;
			sf.Dispose();
			return relay;
		}
		static public string FSave(string F) {
			string relay = string.Empty;
			SaveFileDialog sf = new SaveFileDialog();
			sf.Filter = F;
			if (sf.ShowDialog() == DialogResult.OK) relay = sf.FileName;
			sf.Dispose();
			return relay;
		}
		static public string FSave(string F, string T) {
			string relay = string.Empty;
			SaveFileDialog sf = new SaveFileDialog();
			sf.Title = T;
			sf.Filter = F;
			if (sf.ShowDialog() == DialogResult.OK)
				relay = sf.FileName;
			sf.Dispose();
			return relay;
		}
		#endregion
	}
	public partial class ControlUtil : TreeUtil
	{
		/// <returns>null on error.</returns>
		static public Control Obj2Ctl(object obj) { if (obj is Control) return obj as Control; return null; }
		
		/// <summary>This is generally abstraction for the Windows OS user-controls native to the dotnet environment.</summary>
		static public bool CkBox(CheckBox cb){ CkBox(cb,!cb.Checked); return cb.Checked; }
		static public bool CkBox(CheckBox cb, bool Validator){ cb.Checked = Validator; return cb.Checked; }
	}
	public partial class ControlUtil {

		#region ' EmumMenuItem<T> '
		static public void ToolStripDropdownEnum<T>(ToolStripDropDownButton btn, EventHandler handler)
		{
			List<ToolStripItem> list = new List<ToolStripItem>();
			foreach (object o in Enum.GetValues(typeof(T)))
			{
				if (btn.DropDownItems.ContainsKey(o.ToString())) continue;
				ToolStripItem tsi = new ToolStripMenuItem(o.ToString());
				tsi.Name = o.ToString();
				tsi.Tag = o;
				tsi.Click += handler;
				list.Add(tsi);
			}
			btn.DropDownItems.AddRange(list.ToArray());
		}
		static public void ToolStripDropdownEnum<T>(ToolStripDropDownButton btn, EventHandler handler, bool SortItems)
		{
			List<ToolStripItem> list = new List<ToolStripItem>();
			foreach (object o in Enum.GetValues(typeof(T)))
			{
				if (btn.DropDownItems.ContainsKey(o.ToString())) continue;
				ToolStripItem tsi = new ToolStripMenuItem(o.ToString());
				tsi.Name = o.ToString();
				tsi.Tag = o;
				tsi.Click += handler;
				list.Add(tsi);
			}
			if (SortItems) list.Sort(SortMenuItems);
			btn.DropDownItems.AddRange(list.ToArray());
		}
		static public int SortMenuItems(ToolStripItem ts0, ToolStripItem ts1)
		{
			return ts0.Text.CompareTo(ts1.Text);
		}
		static public ToolStripMenuItem EmumMenuItem<T>(string name,EventHandler ItemHandler)
		{
			ToolStripMenuItem tss = new ToolStripMenuItem(name);
			string[] names = Enum.GetNames(typeof(T));
			foreach (string nam in Enum.GetNames(typeof(T)))
			{
				T im = (T)Enum.Parse((typeof(T)),nam);
				ToolStripMenuItem tsii = ControlUtil.TSItem(nam,(T)(Enum.Parse(typeof(T),nam)),ItemHandler);
				tsii.Tag = im;
				tss.DropDownItems.Add(tsii);
			}
			return tss;
		}
		#endregion
		#region ' bool IsCk '
		static public bool IsCk(ToolStripMenuItem ts){ return ts.Checked = !ts.Checked; }
		#endregion
		#region ' TSBtn, TSBStyle '
		static public void TSBStyle(MenuStrip menu, ToolStripItemDisplayStyle ds) { foreach (ToolStripItem btn in menu.Items) { if (btn is ToolStripButton) btn.DisplayStyle = ds; } }

		static public ToolStripButton TSBtn(string N, ToolStripItemAlignment A) { ToolStripButton item = TSBtn(N); item.Alignment = A; return item; }
		static public ToolStripButton TSBtn(string N, EventHandler E, Padding P, ToolStripItemAlignment A) { ToolStripButton item = TSBtn(N,E,P); item.Alignment = A; return item; }
		static public ToolStripButton TSBtn(string N, EventHandler E, ToolStripItemAlignment A) { ToolStripButton item = TSBtn(N,E); item.Alignment = A; return item; }
		static public ToolStripButton TSBtn(string N, object T, EventHandler E, ToolStripItemAlignment A) { ToolStripButton item = TSBtn(N,T,E); item.Alignment = A; return item; }
		static public ToolStripButton TSBtn(string N, object T, EventHandler E, Padding P) { ToolStripButton item = TSBtn(N,T,E); item.Padding = P; return item; }
		static public ToolStripButton TSBtn(string N, object T, EventHandler E, Bitmap B) { ToolStripButton item = TSBtn(N,T,E); item.Image = (System.Drawing.Image)B; return item; }
		static public ToolStripButton TSBtn(string N, object T, EventHandler E, Image I) { ToolStripButton item = TSBtn(N,T,E); item.Image = I; return item; }
		static public ToolStripButton TSBtn(string N, EventHandler E,Padding P) { ToolStripButton item = TSBtn(N,E,null,P); return item; }
		static public ToolStripButton TSBtn(string N, EventHandler E) { ToolStripButton item = TSBtn(N); item.Click += new EventHandler(E); return item; }
		static public ToolStripButton TSBtn(string N, object T, EventHandler E) { ToolStripButton item = TSBtn(N,T); item.Click += new EventHandler(E); return item; }
		static public ToolStripButton TSBtn(string N, Image img, EventHandler E) { ToolStripButton item = TSBtn(N); item.Click += new EventHandler(E); item.Image = img; return item; }
		static public ToolStripButton TSBtn(string N, Image img, object T, EventHandler E) { ToolStripButton item = TSBtn(N,T); item.Click += new EventHandler(E); item.Image = img; return item; }
		static public ToolStripButton TSBtn(string N, Image img, object T, EventHandler E, ToolStripItemAlignment A) { ToolStripButton item = TSBtn(N,T); item.Click += new EventHandler(E); item.Image = img; item.Alignment = A; return item; }
		static public ToolStripButton TSBtn(string N, object T) { ToolStripButton item = new ToolStripButton(N); item.Tag = T; return item; }
		static public ToolStripButton TSBtn(string N) { ToolStripButton item = new ToolStripButton(N); return item; }
		static public ToolStripSplitButton TSBtn(string N,ToolStripItem[] btns) { ToolStripSplitButton item = new ToolStripSplitButton(N); item.DropDownItems.AddRange(btns); return item; }
		#endregion
		#region ' TSDd '
		static public ToolStripDropDownButton TSDd(string title, Image image, object tag, EventHandler handler, ToolStripItem[] items) { ToolStripDropDownButton tsmi = TSDd(title); if (image!=null) tsmi.Image = image; tsmi.DropDownItems.AddRange(items); tsmi.Click += handler; tsmi.Tag = tag; return tsmi; }
		static public ToolStripDropDownButton TSDd(string T, ToolStripItem[] items) { ToolStripDropDownButton item = TSDd(T); item.DropDownItems.AddRange(items); return item; }
		static public ToolStripDropDownButton TSDd(string T) { ToolStripDropDownButton item = new ToolStripDropDownButton(T); return item; }
		#endregion
		#region ' TSCondEnable '
		static public void TSCondEnable(ToolStripItem I, bool Condition) { TSCondEnable(I,Condition,false); }
		static public void TSCondEnable(ToolStripItem I, bool Condition, bool invert) { if (!invert) { if (Condition) I.Enabled = true; else I.Enabled = false; } else { if (Condition) I.Enabled = !true; else I.Enabled = !false; } }
		#endregion
		#region ' TSItem '
		//static public ToolStripMenuItem TSItem<T>(string ItemName, object ItemTag, ApiEvent<T> Handler) { System.Windows.Forms.ToolStripMenuItem Item = new System.Windows.Forms.ToolStripMenuItem(ItemName); Item.Name = ItemName; Item.Tag = ItemTag; Item.Click += Handler; return Item; }
		static public ToolStripSeparator TSItem() { return new System.Windows.Forms.ToolStripSeparator(); }
		static public ToolStripSeparator TSItem(ToolStripItemAlignment A) { ToolStripSeparator item = TSItem(); item.Alignment = A; return TSItem(); }
		static public ToolStripMenuItem TSItem(string ItemName,Image img) { System.Windows.Forms.ToolStripMenuItem Item = new System.Windows.Forms.ToolStripMenuItem(ItemName); Item.Image = img; Item.Name = ItemName; return Item; }
		static public ToolStripMenuItem TSItem(string ItemName) { System.Windows.Forms.ToolStripMenuItem Item = new System.Windows.Forms.ToolStripMenuItem(ItemName); Item.Name = ItemName; return Item; }
		static public ToolStripMenuItem TSItem(string ItemName, EventHandler Handler) { System.Windows.Forms.ToolStripMenuItem Item = new System.Windows.Forms.ToolStripMenuItem(ItemName); Item.Name = ItemName; Item.Click += Handler; return Item; }
		static public ToolStripMenuItem TSItem(string ItemName, Image img, object ItemTag, EventHandler Handler, ToolStripItem[] SubItems) { System.Windows.Forms.ToolStripMenuItem tsmi = TSItem(ItemName,img,ItemTag,Handler); tsmi.DropDownItems.AddRange(SubItems); return tsmi; }
		static public ToolStripMenuItem TSItem(string ItemName, Image img, object ItemTag, EventHandler Handler) { System.Windows.Forms.ToolStripMenuItem tsmi = TSItem(ItemName,ItemTag,Handler); if (img != null) tsmi.Image = img; return tsmi; }
		static public ToolStripMenuItem TSItem(string ItemName, object ItemTag, EventHandler Handler) { System.Windows.Forms.ToolStripMenuItem Item = new System.Windows.Forms.ToolStripMenuItem(ItemName); Item.Name = ItemName; Item.Tag = ItemTag; Item.Click += Handler; return Item; }
		static public ToolStripMenuItem TSItem(string I, Image img, object T, EventHandler E, Keys K) { System.Windows.Forms.ToolStripMenuItem Item = new ToolStripMenuItem(I,img,E); Item.Tag = T; Item.ShortcutKeys = Keys.Control | K; Item.ShortcutKeyDisplayString = "Ctrl+"+K.ToString(); return Item; }
		static public ToolStripMenuItem TSItem(string I, object T, EventHandler E, Keys K) { System.Windows.Forms.ToolStripMenuItem Item = TSItem(I,T,E); Item.ShortcutKeys = Keys.Control | K; Item.ShortcutKeyDisplayString = "Ctrl+"+K.ToString(); return Item; }
		static public ToolStripMenuItem TSItem(string ItemName, object tag, EventHandler even, ToolStripItemAlignment align) { System.Windows.Forms.ToolStripMenuItem Item = new System.Windows.Forms.ToolStripMenuItem(ItemName); Item.Tag = tag; Item.Alignment = align; Item.Click += even; Item.Name = ItemName; return Item; }
		#endregion
		#region ' TSSBtn '
		static public ToolStripSplitButton TSSBtn(string name, object tag, EventHandler bob,ToolStripItemAlignment align) { ToolStripSplitButton item = new ToolStripSplitButton(name,null,bob); item.Name = name; item.Tag = tag; item.Alignment = align; return item; }
		static public ToolStripSplitButton TSSBtn(string title, Image image, object tag, EventHandler handler, ToolStripItem[] items) { ToolStripSplitButton tsmi = TSSBtn(title,tag,handler,image); tsmi.DropDownItems.AddRange(items); return tsmi; }
		static public ToolStripSplitButton TSSBtn(string title, Image image, ToolStripItem[] items) { ToolStripSplitButton tsmi = TSSBtn(title,null,delegate{},image); tsmi.DropDownItems.AddRange(items); return tsmi; }
		static public ToolStripSplitButton TSSBtn(string name, object tag, EventHandler bob, Image img) { ToolStripSplitButton item = new ToolStripSplitButton(name,null,bob); item.Name = name; item.Tag = tag; item.Image = img; return item; }
		#endregion

	}

}
