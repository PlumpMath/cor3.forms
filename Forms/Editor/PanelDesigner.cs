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
	/// Extends the System.System.Cor3.Forms.Design mode behavior of a Panel control that supports nested controls.
	/// </summary>
	internal class PanelDesigner : ParentControlDesigner
	{
		#region FieldsPrivate
		#endregion
		#region MethodsPublic
		/// <summary>
		/// Initializes a new instance of the PanelDesigner class.
		/// </summary>
		public PanelDesigner()
		{
		}
		/// <summary>
		/// Initializes the designer with the specified component.
		/// </summary>
		/// <param name="component"></param>
		public override void Initialize(System.ComponentModel.IComponent component)
		{
			base.Initialize(component);
		}
		/// <summary>
		/// Gets the System.System.Cor3.Forms.Design-time action lists supported by the component associated with the designer.
		/// </summary>
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				// Create action list collection
				DesignerActionListCollection actionLists = new DesignerActionListCollection();
	
				// Add custom action list
				actionLists.Add(new PanelDesignerActionList(this.Component));
	
				// Return to the designer action service
				return actionLists;
			}
		}
	
		#endregion
		#region MethodsProtected
		/// <summary>
		/// Called when the control that the designer is managing has painted
		/// its surface so the designer can paint any additional adornments on
		/// top of the control.
		/// </summary>
		/// <param name="e">A PaintEventArgs that provides data for the event.</param>
		protected override void OnPaintAdornments(PaintEventArgs e)
		{
			base.OnPaintAdornments(e);
		}
	
		#endregion
	}
}
