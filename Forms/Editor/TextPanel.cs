/* User: oIo * Date: 5/18/2010 * Time: 3:20 PM */
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Cor3.Design;
using System.Drawing;
using System.Windows.Forms;

namespace System.Cor3.Forms
{
	//	[DefaultEvent("CloseClick")]
	//	[Designer(typeof(PanelDesigner)),DesignTimeVisibleAttribute(true)]
	
	[Designer(typeof(Monda))]
	[System.Drawing.ToolboxBitmapAttribute(typeof(TextBox))]
	public class TextPanel : ScrollableControl, IDataValue
	{
		public string backupvalue;
	
		#region Custom Events
		public event EventHandler EnterPressed;
		protected virtual void OnRichTextEnterPressed(EventArgs e)
		{
			if (EnterPressed != null) {
				EnterPressed(this.TextInputBox, e);
			}
		}
		public event EventHandler Validity;
		protected virtual void OnValidity(object sender, EventArgs e)
		{
			if (Validity != null) {
				Validity(this.TextInputBox, e);
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
			string strvalue = this.TextInputBox.Text;
			if (strvalue.Length > _lim)
			{
				TextInputBox.Text = strvalue.Substring(0,_lim);
			}
		}
		#endregion
		
		#region General Properties
		public string InputValue { get { return this.TextInputBox.Text; } set { this.TextInputBox.Text = value; } }
		public BorderStyle InputBorder { get { return this.TextInputBox.BorderStyle; } set { this.TextInputBox.BorderStyle = value; } }
		public Font InputFont { get { return this.TextInputBox.Font; } set { this.TextInputBox.Font = value; } }
		[System.ComponentModel.DefaultValueAttribute(DockStyle.Left)]
		public DockStyle TextDocking { get { return TextInputBox.Dock; } set { TextInputBox.Dock = value; } }
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
		
		public TextPanel() : base()
		{
			InitializeComponent();
			backupvalue = this.TextInputBox.Text;
			this.TextInputBox.TextChanged += new EventHandler(this.OnValidity);
			this.TextInputBox.TextChanged += new EventHandler(this.CheckLen);
			this.EnterPressed += eCheckValue;
			TextInputBox.LostFocus += eCheckValue;
		}
	
		#region Event Handlers
		void eCheckValue(object sender, EventArgs e)
		{
			try
			{
				switch(AutoFormatMode)
				{
						case AutoFormat.None: break;
						case AutoFormat.DateTime: TextInputBox.Text = String.Format("{0}",DateTime.Parse(TextInputBox.Text)); break;
						case AutoFormat.Date: TextInputBox.Text = String.Format("{0}",DateTime.Parse(TextInputBox.Text).ToShortDateString()); break;
						case AutoFormat.Integer: TextInputBox.Text = String.Format("{0:##,###,##0}",int.Parse(TextInputBox.Text)); break;
						case AutoFormat.Custom: TextInputBox.Text = String.Format(ReformatValue,TextInputBox.Text); break;
						case AutoFormat.Currency: TextInputBox.Text = String.Format("${0:###,##0.00}",float.Parse(TextInputBox.Text.Trim('$'))); break;
						default: break;
				}
			}
			catch (Exception error)
			{
				OnFormatError(new FormExceptionEvent(error,backupvalue));
			}
			backupvalue =  TextInputBox.Text;
		}
		void RTFKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13) OnRichTextEnterPressed(new EventArgs());
		}
		void LabelClick(object sender, EventArgs e)
		{
			TextInputBox.SelectAll();
		}
		void RichTextPanelClick(object sender, EventArgs e) { TextInputBox.SelectAll(); }
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
			this.TextInputBox = new System.Windows.Forms.TextBox();
			this.label = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// TextInputBox
			// 
			this.TextInputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextInputBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TextInputBox.Location = new System.Drawing.Point(104, 4);
			this.TextInputBox.Multiline = false;
			this.TextInputBox.Name = "TextInputBox";
			this.TextInputBox.Size = new System.Drawing.Size(153, 21);
			this.TextInputBox.TabIndex = 3;
			this.TextInputBox.Text = "";
			this.TextInputBox.WordWrap = false;
			this.TextInputBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RTFKeyPress);
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
			this.Controls.Add(this.TextInputBox);
			this.Controls.Add(this.label);
			this.Name = "RichTextPanel";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.Size = new System.Drawing.Size(261, 29);
			this.Click += new System.EventHandler(this.RichTextPanelClick);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label label;
		private System.Windows.Forms.TextBox _textbox;
		public TextBox TextInputBox { get { return _textbox; } set { _textbox = value; } }
		#endregion
		
		class Monda : DbControlDesigner
		{
			public override System.ComponentModel.Design.DesignerActionListCollection ActionLists {
				get 
				{
					DesignerActionListCollection da = new DesignerActionListCollection();
					da.Add(new DbActionList(this.Component));
					da.Add(new Mondo(this.Component));
					da.Add(new LabelNfo(Component));
					return da;
				}
			}
			public Monda() : base()
			{
			}
		}
		class LabelNfo : DbActionList
		{
			public LabelNfo(IComponent l) : base(l) { }
			TextPanel txt { get { return Component as TextPanel; } }
			public string LabelText { get { return txt.label.Text; } set { txt.label.Text = value; } }
		}
		class Mondo : DbActionList
		{
			TextPanel txt { get { return Component as TextPanel; } }
			virtual public Font InputFont { get { return txt.TextInputBox.Font; } set { txt.TextInputBox.Font = value; } }
			[TypeConverterAttribute(typeof(MultilineStringConverter))]
			virtual public string Text { get { return txt.TextInputBox.Text; } set { txt.TextInputBox.Text = value; } }
			public Mondo(IComponent c) : base(c)
			{
			}
		}
	}
}
