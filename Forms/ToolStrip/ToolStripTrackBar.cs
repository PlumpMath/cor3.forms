/* User: oIo * Date: 9/3/2010 * Time: 2:59 PM */
using System;
using System.Windows.Forms.Design;

namespace System.Cor3.Forms
{
	[System.Drawing.ToolboxBitmapAttribute(typeof(System.Windows.Forms.MaskedTextBox))]
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
	public class ToolStripTrackBar : System.Windows.Forms.ToolStripControlHost {
		
		[System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Visible)]
		public System.Windows.Forms.TrackBar TrackBar
		{
			get { return Control as System.Windows.Forms.TrackBar; }
		}
		
		[System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Visible)]
		public int TrackBarHeight { get { return TrackBar.Height; } set { TrackBar.Height = value; } }

		[System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Visible)]
		public int MinValue { get { return TrackBar.Minimum; } set { TrackBar.Minimum = value; } }
		public int Maximum { get { return TrackBar.Maximum; } set { TrackBar.Maximum = value; } }
		public int Value { get { return TrackBar.Value; } set { TrackBar.Value = value; } }
		[System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Visible)]
		public bool TrackBarAutoSize { get { return TrackBar.AutoSize; } set { TrackBar.AutoSize = value; } }
		
		protected override void OnSubscribeControlEvents(System.Windows.Forms.Control c)
		{
			base.OnSubscribeControlEvents(c);
			System.Windows.Forms.TrackBar mtb = (System.Windows.Forms.TrackBar)c;
		}
		protected override void OnUnsubscribeControlEvents(System.Windows.Forms.Control c)
		{
			base.OnUnsubscribeControlEvents(c);
			System.Windows.Forms.TrackBar mtb = (System.Windows.Forms.TrackBar)c;
		}
		public ToolStripTrackBar() :  base(new System.Windows.Forms.TrackBar())
		{
			
		}
		public ToolStripTrackBar(string name) : base(new System.Windows.Forms.TrackBar(),name)
		{
			this.TrackBar.AutoSize = false;
			
		}
		
	}
}
