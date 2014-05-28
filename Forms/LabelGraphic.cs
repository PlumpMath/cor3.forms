/* User: oIo * Date: 9/23/2010 * Time: 7:09 PM */
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace System.Cor3.Forms
{

	/// <summary>
	/// Description of LabelGraphic.
	/// </summary>
	[System.Drawing.ToolboxBitmapAttribute(typeof(Label))]
	public class LabelGraphic : UserControl
	{
		ContentAlignment textAlign = ContentAlignment.MiddleCenter;
		public ContentAlignment TextAlign {
			get { return textAlign; }
			set { textAlign = value; }
		}
		[
			Browsable(true),
			DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible),
		]
		public override string Text {
			get { return base.Text; }
			set { base.Text = value; Invalidate(); }
		}
		void CheckAlignment ()
		{
//			TextFormatFlags.Bottom
//			TextFormatFlags.Top
//			TextFormatFlags.HorizontalCenter
//			TextFormatFlags.VerticalCenter
//			TextFormatFlags.Left
//			TextFormatFlags.Right
//			TextFormatFlags.LeftAndRightPadding
//			TextFormatFlags.NoPadding
//			TextFormatFlags.PathEllipsis
//			TextFormatFlags.WordEllipsis
//			TextFormatFlags
		}
//		static int _sFmt_Left = StringAlignment.Near;
//		const int _sFmt_Right = StringAlignment.Far;
//		const int _sFmt_Center = StringAlignment.Center;
		
		Color rectColour = SystemColors.ControlDark;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Color RectColour { get { return rectColour; } set { rectColour = value; Invalidate(); } }

		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			base.OnInvalidated(e);
			Recalculate();
			Redraw();
		}
		RoundRectRenderer r = null;
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Invalidate();
		}

		public RoundRectRenderer RRect { get { return r==null?Recalculate():r; } set { r = value; } }
		RoundRectRenderer Recalculate()
		{
			return r = new RoundRectRenderer( ClientRectangle,corners,cornerTension);
		}
		StringFormat FormatF
		{
			get
			{
				StringFormat sf = new StringFormat(StringFormatFlags.MeasureTrailingSpaces);
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;
				sf.Trimming = StringTrimming.EllipsisCharacter;
				return sf;
			}
		}

		PixelOffsetMode pixelOffsetMode = PixelOffsetMode.Default;
		[Category("Typography"),DefaultValueAttribute(PixelOffsetMode.HighQuality)]
		public PixelOffsetMode PixelOffsetMode { get { return pixelOffsetMode; } set { pixelOffsetMode = value== PixelOffsetMode.Invalid?PixelOffsetMode.Default:value; Invalidate(); } }
		
		TextRenderingHint hintMode = TextRenderingHint.SystemDefault;
		[Category("Typography"),DefaultValueAttribute(TextRenderingHint.SystemDefault)]
		public TextRenderingHint HintMode { get { return hintMode; } set { hintMode = value; Invalidate(); } }

//		public TextFormatFlags GetFormat
//		{
//			get
//			{
//				switch (this.TextAlign)
//				{
//						case ContentAlignment.TopLeft: return (TextFormatFlags)_fmtTopLeft;
//						case ContentAlignment.TopCenter: return (TextFormatFlags)_fmtTopCenter;
//						case ContentAlignment.TopRight: return (TextFormatFlags)_fmtTopRight;
//						case ContentAlignment.MiddleLeft: return (TextFormatFlags)_fmtMidLeft;
//						case ContentAlignment.MiddleCenter: return (TextFormatFlags)_fmtMidCenter;
//						case ContentAlignment.MiddleRight: return (TextFormatFlags)_fmtMidRight;
//						case ContentAlignment.BottomLeft: return (TextFormatFlags)_fmtBtmLeft;
//						case ContentAlignment.BottomCenter: return (TextFormatFlags)_fmtBtmCenter;
//						case ContentAlignment.BottomRight: return (TextFormatFlags)_fmtBtmRight;
//						default: return TextFormatFlags.Default;
//				}
//			}
//		}
		
		InterpolationMode textInterpolation;
		[Category("Typography"),DefaultValueAttribute(InterpolationMode.HighQualityBilinear)]
		public InterpolationMode TextInterpolation {
			get { return textInterpolation; }
			set { textInterpolation = value== InterpolationMode.Invalid?InterpolationMode.Default:value; Invalidate(); }
		}

		SmoothingMode textSmoothing = SmoothingMode.Default;
		[Category("Typography"),DefaultValueAttribute(SmoothingMode.Default)]
		public SmoothingMode TextSmoothing {
			get { return textSmoothing; }
			set { textSmoothing = value== SmoothingMode.Invalid?SmoothingMode.Default:value; Invalidate(); }
		}
		
		CompositingMode textComposite = CompositingMode.SourceOver;
		[Category("Typography"),DefaultValueAttribute(CompositingMode.SourceOver)]
		public CompositingMode TextComposite {
			get { return textComposite; }
			set { textComposite = value; Invalidate(); }
		}
		
		CompositingQuality textCompoQuality = CompositingQuality.HighQuality;
		[Category("Typography"),DefaultValueAttribute(CompositingQuality.HighQuality)]
		public CompositingQuality TextCompoQuality {
			get { return textCompoQuality; }
			set { textCompoQuality = value== CompositingQuality.Invalid?CompositingQuality.Default:value; Invalidate(); }
		}

		GraphicsPath Redraw()
		{
			textPath = new GraphicsPath();
			textPath.AddString(
				this.Text,
				this.Font.FontFamily,
				(int)this.Font.Style,
				this.Font.Size,
				ClientRectangle,
				FormatF);
			return textPath;
		}
		GraphicsPath textPath;
		GraphicsPath TextPath { get { return textPath==null?Redraw():textPath; } }
		protected override void OnPaint(PaintEventArgs e)
		{
			GraphicsState gs = e.Graphics.Save();
			e.Graphics.InterpolationMode = TextInterpolation;
			e.Graphics.PixelOffsetMode = pixelOffsetMode;
			e.Graphics.CompositingMode = TextComposite;
			e.Graphics.CompositingQuality = TextCompoQuality;
			e.Graphics.SmoothingMode = TextSmoothing;
			using (Brush sb = new SolidBrush(rectColour)) { e.Graphics.FillPath(sb,RRect.Path); }
			e.Graphics.TextRenderingHint = hintMode;
//			GraphicsUnit gu = e.Graphics.PageUnit;
//			e.Graphics.PageUnit = Font.Unit;
			using (Brush sb = new SolidBrush(ForeColor)) e.Graphics.FillPath(sb,TextPath);
//			e.Graphics.PageUnit = gu;
			e.Graphics.Restore(gs);
		}

		CORNERS corners = new CORNERS(6f);
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CORNERS Corners { get { return corners; } set { corners = value; } }

		public LabelGraphic() : base() {
			SetStyle(ControlStyles.ResizeRedraw |
			         ControlStyles.UserPaint |
			         ControlStyles.AllPaintingInWmPaint |
			         ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;
//			InitializeComponent();
		}

		float cornerTension = 0.333f;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public float CornerTension {
			get { return cornerTension; }
			set { cornerTension = value; }
		}

		FloatPoint shadowOffset = FloatPoint.Empty;
		[DefaultValue("FPoint.Empty")]
		public FloatPoint ShadowOffset { get { return shadowOffset; } set { shadowOffset = value; } }
	}
}
