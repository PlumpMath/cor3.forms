/* User: oIo * Date: 5/18/2010 * Time: 3:20 PM */
using System;
using System.Windows.Forms;

namespace System.Cor3.Forms
{
	public class ComboPanelPopup : ComboPanel
	{
		[System.ComponentModel.TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
		public ContextMenuStrip Cmx { get { return LABEL.ContextMenuStrip==null?null:LABEL.ContextMenuStrip; } set { LABEL.ContextMenuStrip = value; } }

		public ComboPanelPopup() : base()
		{
			InitializeComponent();
		}
		void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// ComboPanelPopup
			// 
			this.BackColor = System.Drawing.Color.Transparent;
			this.Name = "ComboPanelPopup";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.Size = new System.Drawing.Size(493, 30);
			this.ResumeLayout(false);
		}
	}
}
