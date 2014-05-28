/* User: oIo * Date: 5/18/2010 * Time: 3:20 PM */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace System.Cor3.Forms
{
	public interface IBank
	{
		object ToContent();
		string FromContent(ComponentContent content);
	}
	[Serializable] public class ComponentContent
	{
		internal Type Info=null;
		[XmlAttribute] public string Name;
		[XmlAttribute("Type")] public string ValueType;
		[XmlAttribute] public string Value;
		
		List<object> children = new List<object>();
		[XmlElement] public object[] Child
		{
			get { return children.ToArray(); }
			set { children.Clear(); children.AddRange(value); }
		}
		public ComponentContent()
		{
		}
		public ComponentContent(Type t) : this()
		{
			Info = t;
			this.Name = Info.Name;
			this.ValueType = Info.FullName;
			
		}
		public ComponentContent(IComponent component) : this(component.GetType())
		{
			
		}
		public ComponentContent(IComponent component, PropertyDescriptor descriptor)
		{
			
		}
		
	}
	public class ComponetDescriptor : ComponentContent
	{
		
		static public IList<PropertyDescriptor> GetValues(IComponent component)
		{
			List<PropertyDescriptor> pdc = new List<PropertyDescriptor>();
			foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(component,true))
			{
				if (pd.ShouldSerializeValue(component)) pdc.Add(pd);
			}
			return pdc;
		}

		public ComponetDescriptor(object component) : base() {
			Info = component.GetType();
			PropertyDescriptorCollection props =
				TypeDescriptor.GetProperties(component,true);
			foreach (PropertyDescriptor od in props)
			{
				
			}
		}
	}
//	public class DataPanelTypeConverter : TypeConverter
//	{
//		const string fmt_point = @"X = ""{0}"" Y = ""{1}""";
//		const string fmt_size = @"Width = ""{0}"" Height = ""{1}""";
//		const string fmt_standard = @"Width = ""{0}"" Height = ""{1}""";
////		const string fmt_font = @"Width = ""{0}"" Height = ""{1}""";
//		string GetPoint(Point value) { return string.Format(fmt_point,value.X,value.Y); }
//		string GetSize(Point value) { return string.Format(fmt_point,value.X,value.Y); }
//		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) { return true; }
//		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
//		{
//			if (sourceType is typeof(string)) return true;
//			else if (sourceType is typeof(Point)) return true;
//			else if (sourceType is Size) return true;
//			else if (sourceType is DockStyle) return true;
//			else if (sourceType is AnchorStyles) return true;
//			return base.CanConvertFrom(context, sourceType);
//		}
//		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
//		{
//			Type type = value.GetType();
//			return base.ConvertFrom(context, culture, value);
//		}
//		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
//		{
//			return base.CanConvertTo(context, destinationType);
//		}
//	}
}
