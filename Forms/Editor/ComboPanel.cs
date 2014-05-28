/* User: oIo * Date: 5/18/2010 * Time: 3:20 PM */
using System;
using System.ComponentModel;
using System.Cor3.Design;
using System.Drawing;
using System.Windows.Forms;

namespace System.Cor3.Forms
{

	[Designer(typeof(DbControlDesigner))]
	public class ComboPanel : UserControl
	{

		public FlatStyle ComboFlatStyle
		{
			get { return COMBO.FlatStyle; }
			set { 
				COMBO.FlatStyle = value;
			}
		}
		public DockStyle ComboDock
		{
			get {
				return comboBox1.Dock;
			}
			set {
				comboBox1.Dock = value;
			}
		}
		public DockStyle LabelDock
		{
			get {
				return label2.Dock;
			}
			set {
				label2.Dock = value;
			}
		}
		public Size ComboSize
		{
			get
			{
				return COMBO.Size;
			}
			set
			{
				COMBO.Size = value;
			}
		}
		public Size LabelSize
		{
			get
			{
				return LABEL.Size;
			}
			set
			{
				LABEL.Size = value;
			}
		}

		[Browsable(true)]
		[System.ComponentModel.TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
		public ComboBox COMBO { get { return comboBox1; } set { comboBox1 = value; } }
		[Browsable(true)]
		[System.ComponentModel.TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
		public Label LABEL { get { return label2; } set { label2=value; } }

		public override string Text {
			get { return comboBox1.Text; }
			set { comboBox1.Text = value; }
		}

		public ContentAlignment LabelAlignment { get { return this.label2.TextAlign; } set { this.label2.TextAlign = value; } }
		public string LabelText { get { return this.label2.Text; } set { this.label2.Text = value; } }
		public string ComboText { get { return this.comboBox1.Text; } set { this.comboBox1.Text = value; } }

		public ComboPanel() : base()
		{
			InitializeComponent();
		}

		void InitializeComponent()
		{
			this.label2 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new System.Drawing.Point(4, 4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(485, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "label2";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// comboBox1
			// 
			this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(4, 20);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(485, 21);
			this.comboBox1.TabIndex = 5;
			// 
			// ComboPanel
			// 
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.label2);
			this.Name = "ComboPanel";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.Size = new System.Drawing.Size(493, 47);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label2;
	}
	

}
