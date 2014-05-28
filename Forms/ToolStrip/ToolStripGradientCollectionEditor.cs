/* User: oIo * Date: 5/30/2010 * Time: 10:28 AM */

using System;
using System.ComponentModel.Design;

namespace System.Cor3.Forms
{
	public class ToolStripGradientCollectionEditor : CollectionEditor
	{
		public ToolStripGradientCollectionEditor(Type type)
			: base(type)
		{
			
		}
		
		protected override bool CanSelectMultipleInstances()
		{
			return false;
		}
		
		protected override Type CreateCollectionItemType()
		{
			return typeof(System.Cor3.Forms.ToolStripGradientSetting);
		}
	}
}
