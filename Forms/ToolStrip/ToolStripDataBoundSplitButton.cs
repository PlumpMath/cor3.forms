/*
 * oIo — 11/16/2010 — 5:50 PM
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Cor3.Forms.Controls
{
	[System.Drawing.ToolboxBitmapAttribute(typeof(System.Windows.Forms.ToolStripSplitButton))]
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip|ToolStripItemDesignerAvailability.MenuStrip)]	/// <summary>
	/// this would be particularly for DataTable and DataView (Binding),
	/// except Binding is not supported as the title suggests, considering
	/// the fact that the items are re-built in the Memo View each time
	/// a new entry is selected.
	/// </summary>
	public class ToolStripDataBoundSplitButton : ToolStripSplitButton
	{
		bool useClickedItemText;
		[DefaultValue(true)]
		public bool UseClickedItemText {
			get { return useClickedItemText; }
			set { useClickedItemText = value; }
		}
		
		string defaultText = "“Nothing”";
		public string DefaultText { get { return defaultText; } set { defaultText = value; } }
		
		#region event – ItemClicked
		public event EventHandler ItemClicked;
		protected virtual void OnItemClicked(EventArgs e)
		{
			if (ItemClicked != null) { ItemClicked(this, e); }
		}
		#endregion
		#region event - (default/inner) Item EventHandler
		public EventHandler ItemEventHandler = null;
		void DefaultEvent(object sender, EventArgs args)
		{
			Tag = (sender as ToolStripMenuItem).Tag;
			DataRowView row = Tag as DataRowView;
			
			if (UseClickedItemText)
			{
				if (DisplayMember!=null)
				{
					ToolTipText = Text = string.Format("{0}",row[DisplayMember]);
					if (Text.Length > LimitDisplayString)
						Text = string.Concat(Text.Substring(0,LimitDisplayString),"…");
					this.Parent.ShowItemToolTips = true;
					this.AutoToolTip = true;
				}
				else Text = string.Format("{0}",Tag);
			}
			if (ValueMember!=null) { }
			
			if (ItemClicked!=null) ItemClicked(sender,EventArgs.Empty);
		}
		#endregion
		
		#region Binding Properties
		object itemsSource;
		public object ItemsSource
		{
			get { return itemsSource; }
			set {
				if (!DesignMode) RefreshData(itemsSource = value);
				else itemsSource = value;
			}
		}
		string displayMember;
		public string DisplayMember { get { return displayMember; } set { displayMember = value; } }
		string valueMember;
		public string ValueMember { get { return valueMember; } set { valueMember = value; } }
		public int LimitDisplayString = 15;
		bool useToolStripFormat = false;
		public bool UseToolStripFormat {
			get { return useToolStripFormat; }
			set { useToolStripFormat = value; }
		}
		string toolStripFormat = string.Empty;
		public string ToolStripFormat { get { return toolStripFormat; } set { toolStripFormat = value; } }
		
		List<string> toolStripFormatMembers = new List<string>();
		public string[] ToolStripFormatMembers {
			get { return toolStripFormatMembers.ToArray(); }
			set { toolStripFormatMembers = new List<string>(value); }
		}
		#endregion
		
		void GetTableItems(object tbl)
		{
			GetViewItems((tbl as DataTable).DefaultView);
		}
		void GetViewItems(object tbl)
		{
			DataView table = tbl as DataView;
			foreach (DataRowView row in table)
			{
				string itemName = string.Format("{0}",row);
				ToolStripItem item = DropDownItems.Add(itemName);
				item.Tag = row;
				item.Click += ItemEventHandler;
				item.Click += DefaultEvent;
				if ((displayMember!=null) && (displayMember!=string.Empty))
				{
					item.Text = string.Format("{0}",row[displayMember]);
				}
				if (UseToolStripFormat)
				{
					string tooltip = toolStripFormat;
					try {
						foreach (string str in toolStripFormatMembers)
						{
							int ndx = -1;
							if ((ndx=str.IndexOf(':')) >= 0)
							{
								string fstr = str.Substring(ndx)+1;
								Global.statG(fstr);
								tooltip = tooltip
									.Replace(string.Format("$({0:$(fmt)})".Replace("$(fmt)",fstr),str),row[str].ToString())
									.Replace(string.Format("[{0:$(fmt)}]".Replace("$(fmt)",fstr),str),row[str].ToString())
									.Replace(string.Format("{0:$(fmt)}".Replace("$(fmt)",fstr),str),row[str].ToString());
							}
							else tooltip = tooltip
								.Replace(string.Format("$({0})",str),row[str].ToString())
								.Replace(string.Format("[{0}]",str),row[str].ToString())
								.Replace(string.Format("{0}",str),row[str].ToString());
						}
						item.ToolTipText = tooltip;
					} catch (Exception) {
						
					}
				}
			}
		}
		
		void GetListItems(object inList)
		{
			IList list = inList as IList;
			IEnumerator enumerator = list.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string itemName = string.Format("{0}",enumerator.Current);
				ToolStripItem item = DropDownItems.Add(itemName);
				if ((displayMember!=null) && (displayMember!=string.Empty))
				{
					item.Text = string.Format("{0}",GetItemElement(enumerator.Current,DisplayMember));
				}
			}
		}
		
		object GetItemElement(object o, string name)
		{
			foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(o))
			{
				if (pd.DisplayName == name) return pd.GetValue(o);
			}
			return null;
		}
		void GetObjectItems(object obj)
		{
			throw new NotSupportedException("Function “RefreshData” is not supported.");
		}
		
		void RefreshData(object obj)
		{
			DropDownItems.Clear();
			if (obj==null) { Tag = null; Text = DefaultText; return; }
			else if (obj.GetType() == typeof(DataTable)) GetTableItems(obj);
			else if (obj.GetType() == typeof(DataView)) GetViewItems(obj);
			else if (obj is IList) GetListItems(obj);
			else GetObjectItems(obj);
		}
		
		public ToolStripDataBoundSplitButton() : base()
		{
			
		}
	}
}
