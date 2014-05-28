/* User: oIo * Date: 5/18/2010 * Time: 3:20 PM */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace System.Cor3.Forms
{
	public class data_rtf : UserControl, IDataValue
	{
		bool isRtfMode = false;
		[CategoryAttribute("Data IO")]
		public bool IsRtfMode { get { return isRtfMode; } set { isRtfMode = value; } }

		public override string Text {
			get { return IsRtfMode ? RTF.Rtf : RTF.Text; }
			set
			{
				if (IsRtfMode) RTF.Rtf = value;
				else RTF.Text = value;
			}
		}

		public string backupvalue;

		#region Custom Events
		public event EventHandler EnterPressed;
		protected virtual void OnRichTextEnterPressed(EventArgs e)
		{
			if (EnterPressed != null) {
				EnterPressed(this.RTF, e);
			}
		}
		public event EventHandler Validity;
		protected virtual void OnValidity(object sender, EventArgs e)
		{
			if (Validity != null) {
				Validity(this.RTF, e);
			}
		}
		#region Old Formatter?
		public event EventHandler <FormExceptionEvent> FormatError;
		protected virtual void OnFormatError(FormExceptionEvent e)
		{
			if (FormatError != null) { FormatError(this, e); }
		}
		#endregion
		#endregion

		#region IDataValue.AutoFormatHelper
		int _lim = 255;
		static public Color ColorInvalidInput = Color.FromArgb(240,80,80);

		AutoFormat _AutoFormatMode = AutoFormat.None;
		[DefaultValue(AutoFormat.None)]
		public AutoFormat AutoFormatMode { get { return _AutoFormatMode; } set { _AutoFormatMode = value; } }
	
		string _ReformatValue = "{0}";
		public string ReformatValue { get { return _ReformatValue; } set { _ReformatValue = value; } }
	
		#endregion
		#region Constrict String Size
		public int MaxLength { get { return _lim; } set { _lim = value; } }
		virtual public void CheckLen(object sender, EventArgs e)
		{
			string strvalue = this.RTF.Text;
			if (strvalue.Length > _lim)
			{
				RTF.Text = strvalue.Substring(0,_lim);
			}
		}
		#endregion
		
		#region General Properties
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string InputValue { get { return Text; } set { Text = value; } }

		public BorderStyle RichTextBorder { get { return this.RTF.BorderStyle; } set { this.RTF.BorderStyle = value; } }
		public Font RichTextFont { get { return this.RTF.Font; } set { this.RTF.Font = value; } }

		[System.ComponentModel.DefaultValueAttribute(DockStyle.Left)]
		public DockStyle TextDocking { get { return RTF.Dock; } set { RTF.Dock = value; } }
		#endregion
	
		#region Label Properties
		public bool LabelAutoSize { get { return label.AutoSize; } set { label.AutoSize = value; } }
		public ContentAlignment LabelAlignment { get { return this.label.TextAlign; } set { this.label.TextAlign = value; } }
		public string LabelText { get { return this.label.Text; } set { this.label.Text = value; } }
		public Font LabelFont { get { return this.label.Font; } set { this.label.Font = value; } }

		[Browsable(true)]
		[System.ComponentModel.TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
		public Label LABEL { get { return label; } set { label=value; } }

		public int LabelWidth { get { return label.Width;} set {label.Width = value; }  }
		public DockStyle LabelDocking
		{
			get
			{
				return label.Dock;
			}
			set
			{
				label.Dock = value;
				label.Width =
					(label.Dock== DockStyle.Left || label.Dock== DockStyle.Right) ?
					100:ClientSize.Width;
			}
		}
		#endregion
		
		public data_rtf() : base()
		{
			InitializeComponent();
			backupvalue = this.RTF.Text;
			this.RTF.TextChanged += new EventHandler(this.OnValidity);
			this.RTF.TextChanged += new EventHandler(this.CheckLen);
			this.EnterPressed += eCheckValue;
			RTF.LostFocus += eCheckValue;
		}
	
		#region Event Handlers
		void eCheckValue(object sender, EventArgs e)
		{
			try
			{
				switch(AutoFormatMode)
				{
						case AutoFormat.None: break;
						case AutoFormat.DateTime: RTF.Text = String.Format("{0}",DateTime.Parse(RTF.Text)); break;
						case AutoFormat.Date: RTF.Text = String.Format("{0}",DateTime.Parse(RTF.Text).ToShortDateString()); break;
						case AutoFormat.Integer: RTF.Text = String.Format("{0:##,###,##0}",int.Parse(RTF.Text)); break;
						case AutoFormat.Custom: RTF.Text = String.Format(ReformatValue,RTF.Text); break;
						case AutoFormat.Currency: RTF.Text = String.Format("${0:###,##0.00}",float.Parse(RTF.Text.Trim('$'))); break;
						default: break;
				}
			}
			catch (Exception error)
			{
				OnFormatError(new FormExceptionEvent(error,backupvalue));
			}
			backupvalue =  RTF.Text;
		}
		void RTFKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13)
				OnRichTextEnterPressed(new EventArgs());
		}
		void LabelClick(object sender, EventArgs e)
		{
			RTF.SelectAll();
		}
		void RichTextPanelClick(object sender, EventArgs e)
		{
			RTF.SelectAll();
		}
		#endregion
	
		#region Design
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
		private System.ComponentModel.IContainer components = null;
		void InitializeComponent()
		{
			this.RTF = new System.Windows.Forms.RichTextBox();
			this.label = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// RTF
			// 
			this.RTF.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.RTF.DetectUrls = false;
			this.RTF.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RTF.EnableAutoDragDrop = true;
			this.RTF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.RTF.Location = new System.Drawing.Point(104, 4);
			this.RTF.Multiline = false;
			this.RTF.Name = "RTF";
			this.RTF.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.RTF.ShowSelectionMargin = true;
			this.RTF.Size = new System.Drawing.Size(153, 21);
			this.RTF.TabIndex = 3;
			this.RTF.Text = "";
			this.RTF.WordWrap = false;
			this.RTF.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RTFKeyPress);
			// 
			// label
			// 
			this.label.Dock = System.Windows.Forms.DockStyle.Left;
			this.label.Location = new System.Drawing.Point(4, 4);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(100, 21);
			this.label.TabIndex = 4;
			this.label.Text = "label2";
			this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label.Click += new System.EventHandler(this.LabelClick);
			// 
			// RichTextPanel
			// 
			this.Controls.Add(this.RTF);
			this.Controls.Add(this.label);
			this.Name = "RichTextPanel";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.Size = new System.Drawing.Size(261, 29);
			this.Click += new System.EventHandler(this.RichTextPanelClick);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label label;
		private System.Windows.Forms.RichTextBox _rtf;
		public RichTextBox RTF { get { return _rtf; } set { _rtf = value; } }
		#endregion
	
	}
}
