/* User: oIo * Date: 5/18/2010 * Time: 3:20 PM */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace System.Cor3.Forms
{
	public class ToolStripMenuPanel : UserControl
	{
		class tsr : ToolStripSystemRenderer { protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) { } }
		int _lim = 255;
		public int MaxLength { get { return _lim; } set { _lim = value; } }

		public event EventHandler Validity;
		static public Color ColorInvalidInput = Color.FromArgb(240,80,80);

		protected virtual void OnValidity(object sender, EventArgs e)
		{
			if (Validity != null) {
				Validity(this.btn_dropdown, e);
			}
		}
		virtual public void CheckLen(object sender, EventArgs e)
		{
			string strvalue = this.btn_dropdown.Text;
			if (strvalue.Length > _lim)
			{
				this.btn_dropdown.Text = strvalue.Substring(0,_lim);
			}
		}
	
		public string InputValue { get { return this.btn_dropdown.Text; } set { this.btn_dropdown.Text = value; } }

		//[Category("Appearance")]
		public Font ButtonFont { get { return this.btn_dropdown.Font; } set { this.btn_dropdown.Font = value; } }
	
		public ToolStripMenuPanel() : base()
		{
			InitializeComponent();
			this.toolStrip1.BackColor = this.BackColor;
			this.toolStrip1.Renderer = new tsr();
			this.btn_dropdown.TextChanged += new EventHandler(this.OnValidity);
			this.btn_dropdown.TextChanged += new EventHandler(this.CheckLen);
		}
	
		#region Design
		void InitializeComponent()
		{
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btn_dropdown = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
			this.toolStrip1.CanOverflow = false;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.btn_dropdown});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(172, 30);
			this.toolStrip1.TabIndex = 5;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// btn_dropdown
			// 
			this.btn_dropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.btn_dropdown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btn_dropdown.Name = "btn_dropdown";
			this.btn_dropdown.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
			this.btn_dropdown.Size = new System.Drawing.Size(117, 27);
			this.btn_dropdown.Text = "Enum Selector";
			// 
			// ToolStripMenuPanel
			// 
			this.Controls.Add(this.toolStrip1);
			this.Name = "ToolStripMenuPanel";
			this.Size = new System.Drawing.Size(172, 52);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		public System.Windows.Forms.ToolStripDropDownButton btn_dropdown;
		private System.Windows.Forms.ToolStrip toolStrip1;
		#endregion
	
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			int wvalue = (toolStrip1.Width - btn_dropdown.Size.Width) / 2;
			Global.statG("{0}",wvalue);
			if ( wvalue >=1 ) btn_dropdown.Margin = new Padding(wvalue,btn_dropdown.Margin.Right,btn_dropdown.Margin.Top,btn_dropdown.Margin.Bottom);
		}
	
	}
}
