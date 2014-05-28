/*
 * oIo — 11/21/2010 — 10:25 AM — http://msdn.microsoft.com/en-us/library/ms404304.aspx
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Cor3.Forms.Controls
{
	[System.Drawing.ToolboxBitmapAttribute(typeof(System.Windows.Forms.DateTimePicker))]
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip|ToolStripItemDesignerAvailability.MenuStrip)]
	public class ToolStripDateTimeEntry : ToolStripControlHost
	{
		IComponent picker = null;
		[DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
		public DateTimePicker DateTimePickerControl {
			get { return picker as DateTimePicker; }
		}
		
		public new event EventHandler TextChanged
		{
			add { DateTimePickerControl.TextChanged += value; }
			remove { DateTimePickerControl.TextChanged -= value; }
		}
		public event EventHandler ValueChanged
		{
			add { DateTimePickerControl.ValueChanged += value; }
			remove { DateTimePickerControl.ValueChanged -= value; }
		}
		public new event EventHandler Validated
		{
			add { DateTimePickerControl.Validated += value; }
			remove { DateTimePickerControl.Validated -= value; }
		}
		public new event CancelEventHandler Validating
		{
			add { DateTimePickerControl.Validating += value; }
			remove { DateTimePickerControl.Validating -= value; }
		}
		public event EventHandler BindingContextChanged
		{
			add { DateTimePickerControl.BindingContextChanged += value; }
			remove { DateTimePickerControl.BindingContextChanged -= value; }
		}

		public bool ShowUpDown {
			get { return DateTimePickerControl.ShowUpDown; }
			set { DateTimePickerControl.ShowUpDown = value; }
		}
		public bool ShowCheckBox {
			get { return DateTimePickerControl.ShowCheckBox; }
			set { DateTimePickerControl.ShowCheckBox = value; }
		}
		public DateTime MinDate {
			get { return DateTimePickerControl.MinDate; }
			set { DateTimePickerControl.MinDate = value; }
		}
		public DateTime MaxDate {
			get { return DateTimePickerControl.MaxDate; }
			set { DateTimePickerControl.MaxDate = value; }
		}
		public DateTime Value {
			get { return DateTimePickerControl.Value; }
			set { DateTimePickerControl.Value = value; }
		}
		public Font CalendarFont {
			get { return DateTimePickerControl.CalendarFont; }
			set { DateTimePickerControl.CalendarFont = value; }
		}
		public Color CalendarForeColor {
			get { return DateTimePickerControl.CalendarForeColor; }
			set { DateTimePickerControl.CalendarForeColor = value; }
		}
		public Color CalendarMonthBackground {
			get { return DateTimePickerControl.CalendarMonthBackground; }
			set { DateTimePickerControl.CalendarMonthBackground = value; }
		}
		public Color CalendarTitleBackColor {
			get { return DateTimePickerControl.CalendarTitleBackColor; }
			set { DateTimePickerControl.CalendarTitleBackColor = value; }
		}
		public Color CalendarTitleForeColor {
			get { return DateTimePickerControl.CalendarTitleForeColor; }
			set { DateTimePickerControl.CalendarTitleForeColor = value; }
		}
		public Color CalendarTrailingForeColor {
			get { return DateTimePickerControl.CalendarTrailingForeColor; }
			set { DateTimePickerControl.CalendarTrailingForeColor = value; }
		}
//		public bool Checked {
//			get { return DateTimePickerControl.Checked; }
//			set { DateTimePickerControl.Checked = value; }
//		}
		public string CustomFormat {
			get { return DateTimePickerControl.CustomFormat; }
			set { DateTimePickerControl.CustomFormat = value; }
		}
		[Category("Data")]
		public ControlBindingsCollection DataBindings {
			get { return DateTimePickerControl.DataBindings; }
		}
		[DefaultValue(LeftRightAlignment.Left)]
		public LeftRightAlignment DropDownAlign {
			get { return DateTimePickerControl.DropDownAlign; }
			set { DateTimePickerControl.DropDownAlign = value; }
		}
		[DefaultValue(DateTimePickerFormat.Long)]
		public DateTimePickerFormat Format {
			get { return DateTimePickerControl.Format; }
			set { DateTimePickerControl.Format = value; }
		}
		
		public ToolStripDateTimeEntry()
			: base(new DateTimePicker())
		{
			picker = Control;
		}
	}
}
