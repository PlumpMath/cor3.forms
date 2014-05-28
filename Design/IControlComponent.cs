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
	public interface ICComponent<T>
	{
		T TComponent {get;}
		IComponent IComponent {get;}
		string Name { get; set; }
		Size Size { get; set; }
		Point Location { get; set; }
		DockStyle Dock { get; set; }
		AnchorStyles Anchor { get; set; }
	}
}
