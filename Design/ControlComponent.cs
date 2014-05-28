/* User: oIo * Date: 9/18/2010 * Time: 5:46 PM */
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace System.Cor3.Design
{
	public class ControlComponent : VirtualComponent<Control>, ICComponent<Control>
	{
		public new string Name {
			get { return base.Name; }
			set { SetValue("Name", value); } }
		public string Text {
			get { return (string)this["Text", false].GetValue(CompO); }
			set { SetValue("Text", value); } }

		[CategoryAttribute("Layout")]
		public DockStyle Dock {
			get { return (DockStyle)this["Dock", false].GetValue(CompO); }
			set { SetValue("Dock", value); } }
		public AnchorStyles Anchor {
			get { return (AnchorStyles)this["Anchor", false].GetValue(CompO); }
			set { SetValue("Anchor", value); } }

		[BrowsableAttribute(false),XmlAttribute]
		public string Position
		{
			get { return string.Format("{0},{1},{2},{3}",Location.X,Location.Y,Size.Width,Size.Height); }
			set
			{
				string[] nums = value.Split(',');
				Location = new Point(int.Parse(nums[0]),int.Parse(nums[1]));
				Size = new Size(int.Parse(nums[2]),int.Parse(nums[3]));
			}
		}
		string typename;
		Type t;
		public string type
		{
			get { return typename; }
			set { typename = value; }
		}

		[CategoryAttribute("Position"),XmlIgnore]
		public Size Size {
			get { return (Size)this["Size", false].GetValue(CompO); }
			set { SetValue("Size", value); } }
		[CategoryAttribute("Position"),XmlIgnore]
		public Point Location {
			get { return (Point)this["Location", false].GetValue(CompO); }
			set { SetValue("Location", value); } }

		public ControlComponent(IComponent cpO) : base(cpO)
		{
			t = cpO.GetType();
			typename = CompO.GetType().FullName;
		}
//		/// <summary>
//		/// Don't forget to give him a parent.
//		/// </summary>
//		/// <param name="host"></param>
//		/// <param name="controlType"></param>
//		public ControlComponent(IDesignerHost host, Type controlType)
//		{
//			IComponent cpO = host.CreateComponent ( controlType );
//			IConticpO = new ControlComponent( cpO );
//			IDesigner designer = host.GetDesigner ( cpO );
//			if ( designer==null ) return null;
//			if ( designer is IComponentInitializer )
//				( ( IComponentInitializer ) designer )
//					. InitializeNewComponent ( null );
//			return icpO;
//		}
	}
}
