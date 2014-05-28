/* User: oIo * Date: 9/18/2010 * Time: 5:46 PM */
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace System.Cor3.Design
{
	// This class encapsulates a log entry. It is a parameterized
	// type (also known as a template class). The parameter type T
	// defines the type of data being logged. For threshold detection
	// to work, this type must implement the IComparable interface.
	[TypeConverter("LogEntryTypeConverter")]
	public class LogEntry<T> where T : IComparable
	{
		private T entryValue;
		private DateTime entryTimeValue;
	
		public LogEntry(
			T value,
			DateTime time)
		{
			this.entryValue = value;
			this.entryTimeValue = time;
		}
	
		public T Entry
		{
			get
			{
				return this.entryValue;
			}
		}
	
		public DateTime EntryTime
		{
			get
			{
				return this.entryTimeValue;
			}
		}
	
		// This is the TypeConverter for the LogEntry class.
		public class LogEntryTypeConverter : TypeConverter
		{
			public override bool CanConvertFrom(
				ITypeDescriptorContext context,
				Type sourceType)
			{
				if (sourceType == typeof(string))
				{
					return true;
				}
	
				return base.CanConvertFrom(context, sourceType);
			}
	
			public override object ConvertFrom(
				ITypeDescriptorContext context,
				System.Globalization.CultureInfo culture,
				object value)
			{
				if (value is string)
				{
					string[] v = ((string)value).Split(new char[] { '|' });
	
					Type paramType = typeof(T);
					T entryValue = (T)paramType.InvokeMember(
						"Parse",
						BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod,
						null,
						null,
						new string[] { v[0] },
						culture);
	
					return new LogEntry<T>(
						entryValue,
						DateTime.Parse(v[2]));
				}
	
				return base.ConvertFrom(context, culture, value);
			}
	
			public override object ConvertTo(
				ITypeDescriptorContext context,
				System.Globalization.CultureInfo culture,
				object value,
				Type destinationType)
			{
				if (destinationType == typeof(string))
				{
					LogEntry<T> le = value as LogEntry<T>;
	
					string stringRepresentation =
						String.Format("{0} | {1}",
						              le.Entry,
						              le.EntryTime);
	
					return stringRepresentation;
				}
	
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
