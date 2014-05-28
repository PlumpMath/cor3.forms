/* oOo * 11/14/2007 : 10:32 PM */
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace System.Cor3.Forms
{
	public class ts_renderer_borderless : ToolStripSystemRenderer
	{
		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
		//	base.OnRenderToolStripBorder(e);
		}
	}
	public class ts_pro_borderless : ToolStripProfessionalRenderer
	{
		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
		//	base.OnRenderToolStripBorder(e);
		}
	}
	/// my custom ToolStripRenderer->base(ToolStripSystemRenderer)
	public class ts_renderer : ToolStripSystemRenderer
	{
		const int lefttrim = 20;
		static Color clr_mnu_bg = SystemColors.MenuBar;
		static Color clr_mnu_selected = SystemColors.MenuHighlight;
		static Color clr_mnu_lite = SystemColors.Window;
		LinearGradientBrush BackgroundGradient { get { return Gradients.GetLinearGradient(FloatPoint.Empty,new FloatPoint(0,lefttrim),clr_mnu_lite,clr_mnu_bg); } }

		LinearGradientBrush GetBackgroundGradient(FloatPoint offset)
		{
			return Gradients.GetLinearGradient(FloatPoint.Empty,offset,clr_mnu_lite,clr_mnu_bg);
		}
		LinearGradientBrush GetBackgroundGradient(FloatPoint offset,Color clr0, Color clr1)
		{
			return Gradients.GetLinearGradient(FloatPoint.Empty,offset,clr0,clr1);
		}
		LinearGradientBrush GetBackgroundGradient(FloatPoint offset, Color clr0)
		{
			return Gradients.GetLinearGradient(FloatPoint.Empty,offset,clr_mnu_bg,clr0);
		}

		protected override void OnRenderItemBackground(ToolStripItemRenderEventArgs e)
		{
			//base.OnRenderItemBackground(e);
		}
		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			if (e.Item.Selected)
			{
				FloatRect reg = e.Item.ContentRectangle;
				reg.Location.X += lefttrim;
				reg.Size.X -= lefttrim;
				using (LinearGradientBrush bsh = GetBackgroundGradient(reg.BottomRight,clr_mnu_selected))
				{
					e.Graphics.FillRectangle(bsh,reg);
				}
			}
		}
		protected override void Initialize(ToolStrip strip)
		{
			base.Initialize(strip);
			strip.ImageScalingSize = new Size(12,12);
			//strip.Padding = strip.Margin = Padding.Empty;
			strip.AutoSize = false;
			strip.Height = 19;
			strip.ShowItemToolTips = false;
			strip.GripStyle = ToolStripGripStyle.Hidden;
			//strip.Padding = new Padding(3,1,3,1);
			strip.Margin = strip.Padding = Padding.Empty;
		}
		protected override void InitializeItem(ToolStripItem item)
		{
			item.Padding = new Padding(0);
			item.Margin = new Padding(0,1,0,1);
			bool doinit = true;
			if (item is ToolStripOverflowButton)
			{
				ToolStripOverflowButton itm = item as ToolStripOverflowButton;
				itm.Text = "hehe";
//				itm.Placement = ToolStripItemPlacement.
//				itm.Image=famfam_silky.bullet_add;
				itm.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
//				doinit = false;
			}
			else if (item is ToolStripMenuItem)
			{
				ToolStripMenuItem itm = item as ToolStripMenuItem;
				itm.Margin = itm.Padding = Padding.Empty;
			}
			else if (item is ToolStripDropDownButton)
			{
				ToolStripDropDownButton itm = item as ToolStripDropDownButton;
//				itm.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
//				itm.ShowDropDownArrow = false;
			}
			else if (item is ToolStripDropDownItem)
			{
				ToolStripDropDownItem itm = item as ToolStripDropDownItem;
				itm.Margin = itm.Padding = Padding.Empty;
//				itm.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
			}
			else { Global.stat(item.GetType().Name); }
			if (doinit) base.InitializeItem(item);
		}
		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) { }
		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			FloatRect rect = e.AffectedBounds;
			rect.Width = lefttrim;
			using (LinearGradientBrush bsh = GetBackgroundGradient(new FloatPoint(0,rect.Bottom))) { e.Graphics.FillRectangle(bsh,rect); }
		}
	}
	/// [obsolete]
	public class ts_renderhelper
	{
		public ts_renderhelper(ToolStrip tstrip) : this(tstrip, SystemFonts.MenuFont,6.5f) {}
		public ts_renderhelper(ToolStrip tstrip, float fsiz) : this(tstrip, SystemFonts.MenuFont, fsiz) {}
		public ts_renderhelper(ToolStrip tstrip, Font fnt) : this(tstrip,fnt,fnt.Size) {}
		public ts_renderhelper(ToolStrip tstrip, Font fnt, float fsize)
		{
			tstrip.Font = new Font(fnt.FontFamily,fsize);
		}
	}
}
