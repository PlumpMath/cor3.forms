/* User: oIo * Date: 5/30/2010 * Time: 10:28 AM */

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace System.Cor3.Forms
{
	//[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ToolStripSystemGradientRenderer : ToolStripSystemRenderer
	{
		ToolStripGradientControl _control;
		ToolStripSystemRenderer _tssr = new ToolStripSystemRenderer();
		
		public ColorBlend ToolStripColorBlend
		{
			get
			{
				ColorBlend cb = new ColorBlend(_control.Gradients.Count);
				for (int i = 0; i < _control.Gradients.Count; i++)
				{
					cb.Colors[i] = _control.Gradients[i].Colour;
					cb.Positions[i] = _control.Gradients[i].Offset;
				}
				return cb;
			}
		}
		
		public ToolStripSystemGradientRenderer() : base()
		{
			
		}
		#region override
		protected override void Initialize(ToolStrip toolStrip)
		{
			base.Initialize(toolStrip);
			_control = toolStrip as ToolStripGradientControl;
		}
		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			try
			{
				if (e.AffectedBounds != _control.ClientRectangle)
					base.OnRenderToolStripBorder(e);
			} catch {}
		}
		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			base.OnRenderToolStripBackground(e);
			if (_control==null || _control.Gradients.Count==0) return;
			if (e.AffectedBounds==_control.ClientRectangle)
			{
				using (LinearGradientBrush lgb = new LinearGradientBrush(new PointF(0f,0f),new PointF(0f,e.AffectedBounds.Height),Color.Black,Color.Black))
				{
					lgb.InterpolationColors = ToolStripColorBlend;
					e.Graphics.FillRectangle(lgb,e.AffectedBounds);
				}
			}
			else _tssr.DrawToolStripBackground(e);
		}
		#endregion
	
	}
}
