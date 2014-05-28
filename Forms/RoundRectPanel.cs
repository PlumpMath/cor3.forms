/* User: oIo * Date: 9/23/2010 * Time: 7:09 PM */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Cor3.Forms
{
//	[Designer(typeof(RoundRectPanel.Monda))]
	public class RoundRectPanel : Panel
	{
		Image BackgroundRender0, BackgroundRender1;
		//#renderer
		RoundRectRenderer r = null;
		public RoundRectRenderer RRect { get { return r; } set { r = value; } }

		protected override void OnResize(EventArgs eventargs)
		{
			base.OnResize(eventargs);
			r = GetClientSizeRenderer();
			GetBitmapGradient(ClientRectangle,Gradient0Padding,InnerCorners,UseGradientFeature,false);
			Invalidate();
		}	
		#region Render Functions

		public void DrawSolid(Graphics gfx, Rectangle clip)
		{
			if (!CheckRect(clip)) return;
			SetQuality(gfx);
			using (Brush sb = new SolidBrush(rectColour)) { gfx.FillPath(sb,RRect.Path); }
			// text
//			using (Pen op = OutlinePen) gfx.DrawPath(op,r.Path);
//			using (Brush sb = new SolidBrush(ForeColor)) { gfx.FillPath(sb,Redraw()); }
		}
		
		bool CheckRect(Rectangle rect)
		{
			if ((rect.Height<=0)||(rect.Width <= 0)) return false;
			return true;
		}
		
		
		public void DrawGradient(Graphics gfx, Rectangle rect, Padding pad, CORNERS corn, bool useGradient, bool useStroke)
		{
			if (!CheckRect(rect)) return;
			SetQuality(gfx);
			
			if (UseGradientFeature)
			{
				Rectangle RX = new Rectangle(pad.Left,pad.Top,rect.Width-pad.Horizontal,rect.Height-pad.Vertical);
				if (!CheckRect(RX)) return;
				RoundRectRenderer rrr = new RoundRectRenderer(RX,corn,0.978f);
				using (Pen p = new Pen(strokeGradientBackground, StrokeGradientWidth)) gfx.DrawPath(p,rrr.Path);
				using (Brush b = new LinearGradientBrush(
					new Rectangle(RX.Location,RX.Size),gradientColor0,gradientColor1,GradientMode)) gfx.FillPath(b,rrr.Path);
				rrr=null;
			}
		}
		public void GetBitmapGradient(Rectangle rect, Padding pad, CORNERS corn, bool useGradient, bool useStroke)
		{
			if ((rect.Height<=0)||(rect.Width <= 0)) return;
			this.BackgroundRender0 = new Bitmap(rect.Width,rect.Height,PixelFormat.Format32bppArgb);
			Graphics gfx = Graphics.FromImage(this.BackgroundRender0);
			gfx.Clear(this.BackColor);
			SetQuality(gfx);
			DrawSolid(gfx,ClientPadded);
			DrawGradient(gfx,rect,pad,corn,useGradient,useStroke);
		}
		#endregion
		protected override void OnPaint(PaintEventArgs e)
		{
			P(e.ClipRectangle,e.Graphics);
//			Global.statM("{0}",e.ClipRectangle);
		}
		void P(Rectangle clip, Graphics gfx)
		{
			GraphicsState gs = gfx.Save();
			SetQuality(gfx);
			if (r==null) OnResize(EventArgs.Empty);
			gfx.DrawImage(BackgroundRender0,clip,clip,GraphicsUnit.Pixel);
			if (RenderMarkUp) using (Pen sp = new Pen(Color.Red,1.0f))
			{
				gfx.DrawRectangle(sp,ClientPadded);
				gfx.DrawRectangle(sp,LabelRect);
				gfx.DrawRectangle(sp,LabelPadded);
				gfx.DrawRectangle(Pens.Orange,LabelPadded);
			}
			gfx.Restore(gs);
		}
		RoundRectRenderer GetClientSizeRenderer()
		{
			return r = new RoundRectRenderer(
				new Rectangle(
					ClientRectangle.X+Padding.Left,ClientRectangle.Y+Padding.Top,
					ClientRectangle.Width - Padding.Horizontal,ClientRectangle.Height-Padding.Vertical),
				corners,cornerTension);
		}
		#region Enums
		public enum GradientDirection {
			TopToBottom,
			LeftToRight,			RightToLeft,
			BottomToTop,
			
			TopLeftToBottomRight,	TopRightToBottomLeft,
			BottomLeftToTopRight,	BottomRightToTopLeft,
		}
		public enum GradientSequence { Scaled, Linear }
		#endregion
		#region UseGradient
		bool useGradientFeature = false;
		public bool UseGradientFeature { get { return useGradientFeature; } set { useGradientFeature = value; Invalidator(); } }
		#endregion
		void CallRenderFunction(Delegate fun, Rectangle r, Graphics gfx)
		{
			fun.DynamicInvoke(r,gfx);
		}
		#region Verbs
		public delegate void RenderFunction(Rectangle clientRect, Graphics gfx);
		public Delegate RenderAction { get { return Delegate.CreateDelegate(typeof(RenderFunction),this,""); } }
		#endregion
		#region GradientReflection
		bool gradientReflection = false;
		public bool GradientReflection { get { return gradientReflection; } set { gradientReflection = value; } }
		#endregion
		#region GradientPoints
		List<PointF> gradientPoints = new List<PointF>();
		public List<PointF> GradientPoints { get { return gradientPoints; } set { gradientPoints = value; } }
		#endregion
		#region RenderMarkup (feature)
		bool renderMarkUp = false;
		[DefaultValue(false)]
		public bool RenderMarkUp { get { return renderMarkUp; } set { renderMarkUp = value; } }
		#endregion

//		[Category("• Round"),]

		#region InnerControl
		IComponent innerControl = null;
		[Category("• Content Settings")]
		public IComponent InnerControl { get { return innerControl; } set { innerControl = value; } }

		Padding clientPadding = new Padding(4);
		[Category("• Round"),]
		public Padding ClientPadding { get { return clientPadding; } set { clientPadding = value; Invalidator(); } }
		[Category("• Round"),]
		public Rectangle ClientPadded
		{
			get
			{
				Padding p = this.Padding;
				return new Rectangle(
					ClientRectangle.X+p.Left,
					ClientRectangle.Y+p.Top,
					ClientRectangle.Width - p.Horizontal,
					ClientRectangle.Height - p.Vertical
				);
			}
		}
		Padding labelPadding = new FloatRect(0);
		[Category("• Round")]
		public Padding LabelPadding { get { return labelPadding; } set { labelPadding = value; } }
		Rectangle LabelRect
		{
			get
			{
				FloatRect R = ClientRectangle;
				R.Width = ClientRectangle.Width - (ClientRectangle.Width-ClientPadded.X);
				return R;
			}
		}
		Rectangle LabelPadded
		{
			get
			{
				FloatRect R = ClientRectangle;
				R.Width = ClientRectangle.Width - (ClientRectangle.Width-ClientPadded.X);
				R.X = ClientPadding.Left;
				R.Y = ClientPadding.Top;
				R.Width -= ClientPadding.Horizontal;
				R.Height -= ClientPadding.Vertical;
				return R;
			}
		}
		#endregion

		[ Browsable(true), DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible), ]
		[ Editor(typeof(MultilineStringEditor),typeof(UITypeEditor)) ]
		public override string Text { get { return base.Text; } set { base.Text = value; Invalidator(); } }
		
		// don't care for any of this
		#region Text Properties
		ContentAlignment textAlign = ContentAlignment.MiddleCenter;
		[Category("• Round"),]
		public ContentAlignment TextAlign {
			get { return textAlign; }
			set { textAlign = value; }
		}

		TextRenderingHint hintMode = TextRenderingHint.SystemDefault;
		[Category("Typography"),DefaultValueAttribute(TextRenderingHint.SystemDefault)]
//		[Category("• Round"),]
		public TextRenderingHint HintMode { get { return hintMode; } set { hintMode = value; Invalidator(); } }

		TextFormatFlags GetFormat
		{
			get
			{
				switch (this.TextAlign)
				{
						case ContentAlignment.TopLeft: return (TextFormatFlags)_fmtTopLeft;
						case ContentAlignment.TopCenter: return (TextFormatFlags)_fmtTopCenter;
						case ContentAlignment.TopRight: return (TextFormatFlags)_fmtTopRight;
						case ContentAlignment.MiddleLeft: return (TextFormatFlags)_fmtMidLeft;
						case ContentAlignment.MiddleCenter: return (TextFormatFlags)_fmtMidCenter;
						case ContentAlignment.MiddleRight: return (TextFormatFlags)_fmtMidRight;
						case ContentAlignment.BottomLeft: return (TextFormatFlags)_fmtBtmLeft;
						case ContentAlignment.BottomCenter: return (TextFormatFlags)_fmtBtmCenter;
						case ContentAlignment.BottomRight: return (TextFormatFlags)_fmtBtmRight;
						default: return TextFormatFlags.Default;
				}
			}
		}
		
		#region Helpers
		const int _fmtTopLeft = (int)TextFormatFlags.Top | (int)TextFormatFlags.Left;
		const int _fmtTopCenter = (int)TextFormatFlags.Top | (int)TextFormatFlags.HorizontalCenter;
		const int _fmtTopRight = (int)TextFormatFlags.Top | (int)TextFormatFlags.Right;
		const int _fmtMidLeft = (int)TextFormatFlags.VerticalCenter | (int)TextFormatFlags.Left;
		const int _fmtMidCenter = (int)TextFormatFlags.VerticalCenter | (int)TextFormatFlags.HorizontalCenter;
		const int _fmtMidRight = (int)TextFormatFlags.VerticalCenter | (int)TextFormatFlags.Right;
		const int _fmtBtmLeft = (int)TextFormatFlags.Bottom | (int)TextFormatFlags.Left;
		const int _fmtBtmCenter = (int)TextFormatFlags.Bottom | (int)TextFormatFlags.HorizontalCenter;
		const int _fmtBtmRight = (int)TextFormatFlags.Bottom | (int)TextFormatFlags.Right;
		#endregion
		#endregion

		#region Graphics Properties


		#region CornerTension
		float cornerTension = 1.0f;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),DefaultValueAttribute(1.0f)]
		[Category("• Round Rectangle")]
		public float CornerTension {
			get { return cornerTension; }
			set { cornerTension = value; Invalidator(); }
		}
		#endregion
		#region RectColour
		Color rectColour = SystemColors.ControlDark;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("• Round Rectangle")]
		public Color RectColour { get { return rectColour; } set { rectColour = value; Invalidator(); } }
		#endregion

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

		#region PixelOffsetMode
		PixelOffsetMode pixelOffsetMode = PixelOffsetMode.HighQuality;
		[Category("• Typography"),DefaultValueAttribute(PixelOffsetMode.HighQuality)]
		public PixelOffsetMode PixelOffsetMode { get { return pixelOffsetMode; } set { pixelOffsetMode = value== PixelOffsetMode.Invalid?PixelOffsetMode.Default:value; Invalidator(); } }
		#endregion
		#region InterpolationMode
		InterpolationMode textInterpolation = InterpolationMode.NearestNeighbor;
		[Category("• Typography"),DefaultValueAttribute(InterpolationMode.NearestNeighbor)]
		public InterpolationMode TextInterpolation {
			get { return textInterpolation; }
			set { textInterpolation = value== InterpolationMode.Invalid?InterpolationMode.Default:value; Invalidator(); }
		}
		#endregion
		#region SmoothingMode
		SmoothingMode textSmoothing = SmoothingMode.Default;
		[Category("• Typography"),DefaultValueAttribute(SmoothingMode.Default)]
		public SmoothingMode TextSmoothing {
			get { return textSmoothing; }
			set { textSmoothing = value== SmoothingMode.Invalid?SmoothingMode.Default:value; Invalidator(); }
		}
		#endregion
		#region CompositingMode
		CompositingMode textComposite = CompositingMode.SourceOver;
		[Category("• Typography"),DefaultValueAttribute(CompositingMode.SourceOver)]
		public CompositingMode TextComposite {
			get { return textComposite; }
			set { textComposite = value; Invalidator(); }
		}
		#endregion
		#region CompositingQuality
		CompositingQuality textCompoQuality = CompositingQuality.HighQuality;
		[Category("• Typography"),DefaultValueAttribute(CompositingQuality.HighQuality)]
		public CompositingQuality CompositeQuality {
			get { return textCompoQuality; }
			set { textCompoQuality = value== CompositingQuality.Invalid?CompositingQuality.Default:value; Invalidator(); }
		}
		#endregion

		#endregion

		GraphicsPath textPath;
		GraphicsPath TextPath { get { return textPath==null?Redraw():textPath; } }
		
		#region Round Rectangle “Outline” Properties
		CORNERS corners = new CORNERS(6f);
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("• Round Rectangle")]
		public CORNERS Corners { get { return corners; } set { corners = value; } }
		CORNERS innerCorners = new CORNERS(6f);
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("• Round Rectangle")]
		public CORNERS InnerCorners { get { return innerCorners; } set { innerCorners = value; } }

		bool drawRectOutline = false;
		[Category("• Outline"),DefaultValueAttribute(false)]
		public bool DrawRectOutline { get { return drawRectOutline; } set { drawRectOutline = value; Invalidator(); } }
		float outlineWidth = 1F;
		[Category("• Outline"),DefaultValueAttribute(1F)]
		public float OutlineWidth { get { return outlineWidth; } set { outlineWidth = value; Invalidator(); } }
//		PenType outlineType = PenType.SolidColor;
//		[Category("• Outline"),DefaultValueAttribute(PenType.SolidColor)]
//		public PenType OutlineType { get { return outlineType; } set { outlineType = value; Invalidator(); } }
		LineJoin outlineJoin = LineJoin.Round;
		[Category("• Outline"),DefaultValueAttribute(LineJoin.Round)]
		public LineJoin OutlineJoin { get { return outlineJoin; } set { outlineJoin = value; Invalidator(); } }
		Color outlineColor = SystemColors.Desktop;
		[Category("• Outline"),DefaultValueAttribute("SystemColors.Desktop")]
		public Color OutlineColor { get { return outlineColor; } set { outlineColor = value; Invalidator(); } }
		PenAlignment outlineAlignment = PenAlignment.Center;
		[Category("• Outline"),DefaultValueAttribute("PenAlignment.Center")]
		public PenAlignment OutlineAlignment { get { return outlineAlignment; } set { outlineAlignment = value; Invalidator(); } }
		Pen OutlinePen {
			get{
				Pen op = new Pen(outlineColor,outlineWidth);
				op.Alignment = outlineAlignment;
				op.LineJoin = outlineJoin;
				return op;
			}
		}
//		Pen CornerPen= new Pen();
//		void update()
//		{
//			CornerPen.
//		}
		#endregion

		void SetQuality(Graphics g)
		{
			g.InterpolationMode = TextInterpolation;
			g.PixelOffsetMode = pixelOffsetMode;
			g.CompositingMode = TextComposite;
			g.CompositingQuality = CompositeQuality;
			g.SmoothingMode = TextSmoothing;
			g.TextRenderingHint = this.hintMode;
		}

		#region GetDPIUnit
		FloatPoint GetDPIUnit(GraphicsUnit src, GraphicsUnit dst)
		{
			FloatPoint dpi = FloatPoint.Empty;
			using (Graphics gfx = CreateGraphics())
			{
				gfx.PageUnit = src;
				dpi = GetDPIUnit(gfx);
			}
			return dpi;
		}
		FloatPoint GetDPIUnit(Graphics gfx, GraphicsUnit dst)
		{
			GraphicsUnit unit = gfx.PageUnit;
			gfx.PageUnit = dst;
			FloatPoint dpi = GetDPIUnit(gfx);
			gfx.PageUnit = unit;
			return dpi;
		}
		FloatPoint GetDPIUnit(Graphics gfx)
		{
			return new FloatPoint(gfx.DpiX,gfx.DpiY);
		}
		#endregion

		#region Draw Text
		// there really shouldn't be a need for this, just use Form.Drawing helper to render text.
		// Printing doesn't require IP (or whatever it's called) features
		// (eg: spline fonts are for printing, if needed)
		GraphicsPath DrawStr(string input)
		{
			textPath	= new GraphicsPath();
			textPath	.AddString(
				input,
				this.Font.FontFamily,
				(int)this.Font.Style,
				this.Font.SizeInPoints,
				LabelPadded,
				FormatF);
			return		textPath;
		}
		GraphicsPath Redraw()
		{
			textPath = new GraphicsPath();
			textPath.AddString(this.Text,this.Font.FontFamily,(int)this.Font.Style,this.Font.Size,LabelPadded,FormatF);
			return textPath;
		}
		#endregion
		
		public RoundRectPanel() {
			SetStyle(ControlStyles.UserPaint|ControlStyles.DoubleBuffer|ControlStyles.ResizeRedraw,true);
			base.DoubleBuffered = true;
			InitializeComponent();
			OnResize(EventArgs.Empty);
			Invalidator();
		}
		
		#region Gradient Drawing Properties
		
		Padding gradient0Padding;
		public Padding Gradient0Padding { get { return gradient0Padding; } set { gradient0Padding = value; Invalidator(); } }
		
		Padding gradient1Padding;
		public Padding Gradient1Padding { get { return gradient1Padding; } set { gradient1Padding = value; Invalidator(); } }
		
		Color gradientColor0 = Color.White;
		public Color GradientColor0 { get { return gradientColor0; } set { gradientColor0 = value; Invalidator(); } }
		
		Color gradientColor1 = Color.Transparent;
		public Color GradientColor1 { get { return gradientColor1; } set { gradientColor1 = value; Invalidator(); } }
		
		Color strokeGradientBackground = SystemColors.ActiveBorder;
		public Color StrokeGradientColor { get { return strokeGradientBackground; } set { strokeGradientBackground = value; Invalidator(); } }
		
		Color highlightColor = SystemColors.Highlight;
		public Color HighlightColor { get { return highlightColor; } set { highlightColor = value; Invalidator(); } }
		
		[Flags] public enum RenderFlags
		{
			Normal,
			Highlight,
			StrokeGradient,
			StrokeBackground,
		}
		
		RenderFlags drawiningMode = RenderFlags.Normal|RenderFlags.StrokeBackground|RenderFlags.StrokeGradient;
		
		
		
		
		public RoundRectPanel.RenderFlags DrawiningMode {
			get { return drawiningMode; }
			set { drawiningMode = value; Invalidator(); }
		}
		float strokeGradientWidth = 1f;
		[DefaultValue(0f)]
		public float StrokeGradientWidth { get { return strokeGradientWidth; } set { strokeGradientWidth = value; Invalidator(); } }
		
		bool BitFlag(RenderFlags checkSource, RenderFlags checkAgainst)
		{
			int ck0 = (int)checkSource, ck1 = (int)checkAgainst;
			if ((ck0 & ck1) == ck1) return true;
			return false;
		}
		
		LinearGradientMode GradientMode = LinearGradientMode.Vertical;
		#endregion
		
		public void Invalidator()
		{
			OnResize(EventArgs.Empty); Invalidate();
		}
		
		public new Padding Padding
		{
			get { return base.Padding; }
			set { base.Padding = value; Invalidator(); }
		}
	
//		protected override void OnInvalidated(InvalidateEventArgs e)
//		{
//			base.OnInvalidated(e);
//			r = GetClientSizeRenderer();
//			Redraw();
//		}

		#region Unused Designer Stuff
		class Monda : ControlDesigner
		{
			public override System.ComponentModel.Design.DesignerActionListCollection ActionLists {
				get
				{
					DesignerActionListCollection da = new DesignerActionListCollection();
					da.Add(new PanelInfo(Component));
					return da;
				}
			}
			public Monda() : base()
			{
			}
		}
		class PanelInfo : DesignerActionList
		{
			public DockStyle DockControl { get { return (this.Component as Control).Dock; } set { SetProp("Dock", value); } }
			virtual public Color BackgroundColor
			{
				get { return (this.Component as Control).BackColor; }
				set { SetProp("BackColor",value); }
			}
			virtual public Color ForeColor
			{
				get { return (this.Component as Control).ForeColor; }
				set { SetProp("ForeColor",value); }
			}
			public PanelInfo(IComponent c) : base(c) { }
			protected void SetProp(string name, object value)
			{
				GetPropertyByName(name).SetValue(this.Component, value);
			}
			private PropertyDescriptor GetPropertyByName(String propName)
			{
				PropertyDescriptor prop;
				prop = TypeDescriptor.GetProperties(this.Component)[propName];
				if (null == prop) throw new ArgumentException( "Matching Target property not found!",propName);
				else return prop;
			}
		}
		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
		void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// RoundRectPanel
			// 
			this.Name = "RoundRectPanel";
			this.Size = new System.Drawing.Size(281, 169);
			this.ResumeLayout(false);
		}
	}
}
