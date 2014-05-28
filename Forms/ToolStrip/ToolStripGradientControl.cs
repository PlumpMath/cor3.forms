/* User: oIo * Date: 5/30/2010 * Time: 10:28 AM */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace System.Cor3.Forms
{

	/// <summary>
	/// <para>• The renderer inherits the System-Renderer.</para>
	/// <para>• There should be another that inherits the Professional Renderer,
	/// or a switch to select between the two.</para>
	/// </summary>
	[System.Drawing.ToolboxBitmapAttribute(typeof(ToolStrip))]
	public class ToolStripGradientControl : System.Windows.Forms.ToolStrip
	{
		#region RenderMode
		public enum RendererMode { System, Professional }
		RendererMode _rendermode = RendererMode.System;
		[
			Category("Appearance"),
			DefaultValue(RendererMode.System),
		]
		public ToolStripGradientControl.RendererMode Rendermode { get { return _rendermode; } set { _rendermode = value; } }
		#endregion

		Color _fgShadow = Color.FromArgb(127,SystemColors.ButtonShadow);
		[Category("Appearance"),]
		public Color ForeGroundShadow { get { return _fgShadow; } set { _fgShadow = value; } }

		List<ToolStripGradientSetting> _grad_collection = new List<ToolStripGradientSetting>();
		[
			DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content),
			Category("Appearance"),
			TypeConverter(typeof(ArrayConverter)),
			EditorAttribute(typeof(ToolStripGradientCollectionEditor),typeof(UITypeEditor))
		]
		public List<ToolStripGradientSetting> Gradients { get { return this._grad_collection; } set { this._grad_collection = value; } }

//		public new ToolStripRenderer Renderer { get { return base.Renderer; } set { base.Renderer = value; } }
		
		void ResetRenderer()
		{
			if (Rendermode== RendererMode.System) Renderer = new ToolStripSystemGradientRenderer();
			else Renderer = new ToolStripProfessionalGradientRenderer();
		}
		
		public ToolStripGradientControl() : base()
		{
			ResetRenderer();
		}

	}
}
