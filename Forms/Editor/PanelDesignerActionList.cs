/* User: oIo * Date: 5/18/2010 * Time: 3:20 PM */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace System.Cor3.Forms
{
	/// <summary>
	/// Provides the class for types that define a list of items used to create a smart tag panel for the Panel.
	/// </summary>
	public class PanelDesignerActionList : DesignerActionList
	{
		const string dock_style_nam = "Design";
		const string dock_style_dsc = "Dock or undock this control in it's parent container.";
		const string caption_undock = "Undock in parent container";
		const string caption_fill = "Dock in parent container";
		string DockStyleText { get { return Panel.Dock== DockStyle.Fill ? caption_undock:caption_fill; } }
		#region Properties
//		public DockStyle Docking
//		{
//			get { return this.Panel.Dock; }
//			set { SetProperty("Dock", value); }
//		}
		#endregion
		
		/// <summary>Initializes a new instance of the PanelDesignerActionList class.</summary>
		/// <param name="component">A component related to the DesignerActionList.</param>
		public PanelDesignerActionList(System.ComponentModel.IComponent component)
			: base(component)
		{
			base.AutoShow = true;
		}
		/// <summary>Returns the collection of DesignerActionItem objects contained in the list.</summary>
		/// <returns> A DesignerActionItem array that contains the items in this list.</returns>
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection actionItems = new DesignerActionItemCollection();
			actionItems.Add(new DesignerActionMethodItem(this,"ToggleDockStyle",DockStyleText,dock_style_nam,dock_style_dsc,true));
			actionItems.Add(new DesignerActionPropertyItem("Dock","Show Docking Style",GetCategory(this.Panel,"Dock")));
			return actionItems;
		}
		/// <summary>Dock/Undock designer action method implementation</summary>
		public void ToggleDockStyle()
		{
			if (this.Panel.Dock != DockStyle.Fill)  SetProperty("Dock", DockStyle.Fill);
			else SetProperty("Dock", DockStyle.None);
		}
		
		
		
		
		private RichTextPanel Panel
		{
			get { return (RichTextPanel)this.Component; }
		}
		
		private void SetProperty(string propertyName, object value)
		{
			// Get property
			System.ComponentModel.PropertyDescriptor property
				= System.ComponentModel.TypeDescriptor.GetProperties(this.Panel)[propertyName];
			// Set property value
			property.SetValue(this.Panel, value);
		}
		
		private static string GetCategory(object source, string propertyName)
		{
			System.Reflection.PropertyInfo property = source.GetType().GetProperty(propertyName);
			CategoryAttribute attribute
				= (CategoryAttribute)
				property.GetCustomAttributes(
					typeof(CategoryAttribute), true
				)[0];
			if (attribute == null) return null;
			return attribute.Category;
		}
	}
}
