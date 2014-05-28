/* User: oIo * Date: 9/18/2010 * Time: 5:46 PM */
using System;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace System.Cor3.Design
{
	internal class DesignerOptionServiceExt4SnapLines : DesignerOptionService
	{
		public DesignerOptionServiceExt4SnapLines() : base() { }
		protected override void PopulateOptionCollection ( DesignerOptionCollection options ) {
			if ( null != options.Parent ) return;
			DesignerOptions ops = new DesignerOptions();
			ops.UseSnapLines = true;
			ops.UseOptimizedCodeGeneration = true;
			ops.UseSmartTags = true;
			ops.EnableInSituEditing = true;
			ops.ObjectBoundSmartTagAutoShow = true;
			DesignerOptionCollection wfd = this.CreateOptionCollection ( options, "WindowsFormsDesigner", null );
			this.CreateOptionCollection ( wfd, "General", ops );
		}
	}//end_class
}
