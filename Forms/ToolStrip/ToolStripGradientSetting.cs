/* User: oIo * Date: 5/30/2010 * Time: 10:28 AM */

using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Cor3.Forms
{
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ToolStripGradientSetting
	{
		Color _Colour;
		public Color Colour { get { return _Colour; } set { _Colour = value; } }
		float _Offset;
		public float Offset { get { return _Offset; } set { _Offset = value; } }
	
		public ToolStripGradientSetting() : this(Color.Black,0.0f) { }
		public ToolStripGradientSetting(Color clr, float off) { Colour = clr; Offset = off; }
	}
}
