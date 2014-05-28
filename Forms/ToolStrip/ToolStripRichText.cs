/* User: oIo * Date: 9/3/2010 * Time: 2:59 PM */
using System;
using System.Windows.Forms.Design;

namespace System.Cor3.Forms
{
	/**
	 * I've geneally concluded that I would like to see some additional customization
	 * done to any control that is used in such a manner as this.
	 * …That is, I would like to see work either done to a owner such as an over-ridden
	 * Control that would contain the TextBox, or Customize the TextBox it's self (with
	 * the exception being that this is a RichTextBox of course).
	 **/
	[System.Drawing.ToolboxBitmapAttribute(typeof(System.Windows.Forms.RichTextBox))]
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip|ToolStripItemDesignerAvailability.MenuStrip)]
	public class ToolStripRichText : System.Windows.Forms.ToolStripControlHost
	{
		[System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Content)]
		public System.Windows.Forms.RichTextBox RichTextBox { get { return Control as System.Windows.Forms.RichTextBox; } }
		public ToolStripRichText() : base(new System.Windows.Forms.RichTextBox())
		{
			RichTextBox.Multiline = false;
			RichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			RichTextBox.Font = System.Drawing.SystemFonts.MenuFont;
		}
	}
}
