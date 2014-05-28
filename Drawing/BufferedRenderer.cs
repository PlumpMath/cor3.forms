using System;
using System.Drawing;
using System.Drawing.Utilities.SuperOld;
using System.Windows.Forms;


/* User: oIo * Date:
 * oOo * 12/19/2007 : 7:56 PM */
namespace System.Cor3.Drawing
{
	/// <summary>
	/// Note that if you do not turn on DoubleBuffering in the respective control,
	/// this has all been for naught.
	/// </summary>
	[Obsolete("This class is depreceated—no longer intended for use.")]
	public class BufferedRenderer : BufferedGfxSurface, IBufferedRenderer
	{
		public Bitmap Bmp { get { return Bitmap.FromHbitmap(b_gfx.Graphics.GetHdc()); } }
		[Flags] public enum Mode
		{
			Default = OnSize | OnBackground | Automate,
			OnSize = 0x1,
			OnBackground = 0x2,
			Automate = 0x4,
			Multipass = 0x8
		}
	
		public event BufferedPaintEvent RenderEvent;
		public virtual void DoRender(Graphics fx) { if (RenderEvent != null) RenderEvent(fx); }
		//internal Graphics gfx { get { return qGfx(Graphics.FromImage(bmp)); } }
		public virtual void UpdateRect(Control ctl) { HClient = new FloatRect((FloatPoint)ctl.ClientRectangle.Location, (FloatPoint)ctl.ClientSize); }
		public virtual void UpdateGraphics(Control ctl)
		{
			if ((ctl.ClientSize.Width <= 0) || (ctl.ClientSize.Height <= 0)) return;
			c_gfx = BufferedGraphicsManager.Current;
			c_gfx.MaximumBuffer = ctl.ClientSize;
			b_gfx = c_gfx.Allocate(ctl.CreateGraphics(), ctl.ClientRectangle);
		}
	
		public virtual void Update(Control ctl)
		{
			UpdateRect(ctl);
			UpdateGraphics(ctl);
			qs.Apply(b_gfx.Graphics);
			if (m_gfx == null) return;
			Busy = true;
			DoRender(m_gfx);
			Busy = false;
		}
	
		virtual public void ControlAttach(Control ctl, Mode mode)
		{
			if ((mode & Mode.OnSize) == Mode.OnSize) ctl.SizeChanged += new EventHandler(SizeChanged);
			if ((mode & Mode.Automate) == Mode.Automate) ctl.Paint += new PaintEventHandler(Apply);
		}
		virtual public void ControlDetach(Control ctl)
		{
			ctl.SizeChanged -= new EventHandler(SizeChanged);
			ctl.Paint -= new PaintEventHandler(Apply);
		}
		virtual public void SizeChanged(object sender, EventArgs args) { Update(sender as Control); }
	
		public BufferedRenderer(Control ctl) : this(ctl,Mode.Default) {}
		public BufferedRenderer(Control ctl, Mode mode) : base()
		{
			ControlAttach(ctl,mode);
			Global.cstat(ConsoleColor.Red,"BufferedRenderer");
			RenderEvent += Render;
		}
		/// <summary>
		/// the base.Render(Graphics) method clears the graphics object with the background color.
		/// Set the InitColor field to a color you would like to initialize with,
		/// or do not call the base function in your override to ignore.
		/// </summary>
		public virtual void Render(Graphics fx) { fx.Clear(InitColor); }
		/// <summary> Applies the internally rendered graphics the the Control's Graphics Object </summary>
		public virtual void Apply(object sender, PaintEventArgs e) { if (!Busy) b_gfx.Render(e.Graphics); }
		//succoro
	}
}
