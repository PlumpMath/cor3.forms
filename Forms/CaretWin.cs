/*
 * Created by SharpDevelop.
 * User: tfw
 * Date: 8/12/2009
 * Time: 3:05 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace System.Cor3.Forms
{

	public class CaretWin : UserControl
	{
		#region Caret Variables
		internal FloatPoint _caret = FloatPoint.Empty;
		internal FloatPoint CaretPosition
		{
			get{ return /*_caret+AutoScrollOffset*/new FloatPoint(PointToClient(MousePosition)); }
			set { _caret = new Point(value); UpdateCaret(value); }
		}
		const int default_caret_height = 12, default_caret_width = 2;
		int caret_height = default_caret_height;
		int caret_width = default_caret_width;
		internal bool UseFontHeight = true;
		internal int CaretHeight { get { return (UseFontHeight) ? (int)Font.GetHeight(72f) : caret_height; } set { caret_height = value; } }
		internal int CaretWidth { get { return caret_width; } set { caret_height = value; } }
		#endregion
		#region Methods (internal)
		internal void UpdateCaret(int X, int Y) { if (Focused) WindowsInterop.User32.SetCaretPos(X,Y); }
		internal void UpdateCaret(Point p) { if (Focused) WindowsInterop.User32.SetCaretPos((int)CaretPosition.X,(int)CaretPosition.Y); }
		#endregion
		#region Methods (virtual public)
		/// automatically called when the form's selected font changes.
		virtual public void ResetCaret()
		{
			WindowsInterop.User32.DestroyCaret();
			WindowsInterop.User32.CreateCaret(Handle,IntPtr.Zero,CaretWidth,CaretHeight);
			WindowsInterop.User32.SetCaretPos((int)CaretPosition.X,(int)CaretPosition.Y);
			WindowsInterop.User32.ShowCaret(Handle);
		}
		virtual public void CaretDestroy()
		{
			WindowsInterop.User32.DestroyCaret();
		}
		virtual public void CaretCreate()
		{
			WindowsInterop.User32.CreateCaret(Handle,IntPtr.Zero,3,(int)this.Font.GetHeight(72f));
			WindowsInterop.User32.SetCaretPos((int)CaretPosition.X,(int)CaretPosition.Y);
			WindowsInterop.User32.ShowCaret(Handle);
		}
		#endregion
		
		public CaretWin() : base()
		{
			InitializeComponent();
		}
		
		#region Overrides
		protected override void OnFontChanged(EventArgs e) { base.OnFontChanged(e); ResetCaret(); }
		protected override void OnGotFocus(EventArgs e) { base.OnGotFocus(e); CaretCreate(); }
		protected override void OnLostFocus(EventArgs e) { base.OnLostFocus(e); CaretDestroy(); }
		#endregion
		
		void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// CaretWin
			// 
			this.Name = "CaretWin";
			this.Size = new System.Drawing.Size(293, 185);
			this.ResumeLayout(false);
		}
	}
}
