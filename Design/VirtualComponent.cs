/* User: oIo * Date: 9/18/2010 * Time: 5:46 PM */
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Windows.Forms;

namespace System.Cor3.Design
{
	public class VirtualComponent<T> : ComponentDescription
	{
		[CategoryAttribute("Component")]
		public T TComponent {
			get { return (T)this.CompO; }
		}
		public VirtualComponent(IComponent cp) : base(cp)
		{
		}
	}
}
