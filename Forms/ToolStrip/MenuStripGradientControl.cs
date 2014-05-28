/* User: oIo * Date: 5/30/2010 * Time: 10:28 AM */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace cor3.forms.controls
{
	/// <summary>
	/// This class was merely intended to test the ToolStripGradient renderer.
	/// </summary>
	public class MenuStripGradientControl : System.Windows.Forms.MenuStrip
	{
		Color _fgShadow = Color.FromArgb(127,SystemColors.ButtonShadow);
		[Category("Appearance")]
		public Color ForeGroundShadow { get { return _fgShadow; } set { _fgShadow = value; } }
	
		List<GradientSetting> _grad_collection = new List<GradientSetting>();
		[
			DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content),
			Category("Appearance"),
			TypeConverter(typeof(ArrayConverter)),
			EditorAttribute(typeof(ToolStripGradientControlRenderer.GradientCollectionEditor),typeof(UITypeEditor))
		]
		public List<GradientSetting> Gradients { get { return this._grad_collection; } set { this._grad_collection = value; } }

		ToolStripGradientControlRenderer _GradientRenderer;

		public new ToolStripRenderer Renderer
		{
			get { return base.Renderer; } set { base.Renderer = value; }
		}
		
		public MenuStripGradientControl()
		{
			this.Renderer = _GradientRenderer = new ToolStripGradientControlRenderer();
		}
	}
}
