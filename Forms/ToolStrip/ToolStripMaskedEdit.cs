/* User: oIo * Date: 9/3/2010 * Time: 2:59 PM */
using System;
using System.Windows.Forms.Design;

namespace System.Cor3.Forms
{

	/// <summary>
	/// Description of Class1.
	/// </summary>
	[System.Drawing.ToolboxBitmapAttribute(typeof(System.Windows.Forms.MaskedTextBox))]
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
	public class ToolStripMaskedEdit : System.Windows.Forms.ToolStripControlHost
	{
		[System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Content)]
		public System.Windows.Forms.MaskedTextBox MaskedEdit
		{
			get { return Control as System.Windows.Forms.MaskedTextBox; }
		}

		[
			System.ComponentModel.CategoryAttribute("Appearance"),
			System.ComponentModel.DefaultValueAttribute(typeof(System.Windows.Forms.Padding),"1,0,1,0")
		]
		public new System.Windows.Forms.Padding Margin
		{
			get { return MaskedEdit.Margin; }
			set { MaskedEdit.Margin = value; }
		}
		[
			System.ComponentModel.CategoryAttribute("Appearance"),
			System.ComponentModel.DefaultValueAttribute(System.Windows.Forms.HorizontalAlignment.Left)
		]
		public System.Windows.Forms.HorizontalAlignment MaskedTextAlign { get { return MaskedEdit.TextAlign; } set { MaskedEdit.TextAlign = value; } }
		protected override void OnSubscribeControlEvents(System.Windows.Forms.Control c)
		{
			base.OnSubscribeControlEvents(c);
			System.Windows.Forms.MaskedTextBox mtb = (System.Windows.Forms.MaskedTextBox)c;
//			c.GotFocus += new EventHandler(GotF);
//			c.LostFocus += new EventHandler(LostF);
		}
		protected override void OnUnsubscribeControlEvents(System.Windows.Forms.Control c)
		{
			base.OnUnsubscribeControlEvents(c);
			System.Windows.Forms.MaskedTextBox mtb = (System.Windows.Forms.MaskedTextBox)c;
//			c.GotFocus -= new EventHandler(GotF);
//			c.LostFocus -= new EventHandler(LostF);
		}
		public ToolStripMaskedEdit() : base(new System.Windows.Forms.MaskedTextBox())
		{
		}
	}

}
