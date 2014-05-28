#region Using
/** tfw * 4/21/2008.9:01 AM **/

using System;
using System.Drawing;
using System.Windows.Forms;
#endregion
namespace System.Cor3.Forms
{
	[System.Drawing.ToolboxBitmapAttribute(typeof(System.Windows.Forms.CheckBox))]
	public class ToolStripCheckBox : ToolStripButton
	{
		void eClick(object sender, EventArgs e)
		{
			Checked = !Checked;
		}
	
		public ToolStripCheckBox() : base() { Click += eClick; }
		public ToolStripCheckBox(string text) : this(text,null,null)  {}
		public ToolStripCheckBox(string text, Image img) : this(text,img,null)  {}
		public ToolStripCheckBox(string text, Image img, EventHandler e) : base(text,img,e)
		{
			Click += eClick;
		}
	}
}
