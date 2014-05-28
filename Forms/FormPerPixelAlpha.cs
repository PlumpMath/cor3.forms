/**
 * oIo * 2/18/2011 3:25 AM
 **/
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
namespace System.Cor3.Forms
{
	
	public class PerPixelAlphaForm : System.Windows.Forms.Form
	{
		sealed class Win32 {
			public enum Bool
			{
				False = 0,
				True
			}

			[StructLayout(LayoutKind.Sequential)]
			public struct Point
			{
				public Int32 x;

				public Int32 y;
				public Point(Int32 x, Int32 y)
				{
					this.x = x;
					this.y = y;
				}
			}

			[StructLayout(LayoutKind.Sequential)]
			public struct Size
			{
				public Int32 cx;

				public Int32 cy;
				public Size(Int32 cx, Int32 cy)
				{
					this.cx = cx;
					this.cy = cy;
				}
			}

			[StructLayout(LayoutKind.Sequential)]
			public struct ARGB
			{
				public byte Blue;
				public byte Green;
				public byte Red;
				public byte Alpha;
			}

			[StructLayout(LayoutKind.Sequential)]
			public struct BLENDFUNCTION
			{
				public byte BlendOp;
				public byte BlendFlags;
				public byte SourceConstantAlpha;
				public byte AlphaFormat;
			}
			public const Int32 ULW_COLORKEY = 1;
			public const Int32 ULW_ALPHA = 2;
			public const Int32 ULW_OPAQUE = 4;
			public const byte AC_SRC_OVER = 0;

			public const byte AC_SRC_ALPHA = 1;
			[DllImport("user32.dll")]
			public static extern Bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, Point pptDst, Size psize, IntPtr hdcSrc, Point pprSrc, Int32 crKey, BLENDFUNCTION pblend, Int32 dwFlags);

			[DllImport("user32.dll")]
			public static extern IntPtr GetDC(IntPtr hWnd);

			[DllImport("user32.dll")]
			public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

			[DllImport("gdi32.dll")]
			public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

			[DllImport("gdi32.dll")]
			public static extern Bool DeleteDC(IntPtr hdc);

			[DllImport("gdi32.dll")]
			public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

			[DllImport("gdi32.dll")]
			public static extern Bool DeleteObject(IntPtr hObject);
		}

		public PerPixelAlphaForm()
		{
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.AllowDrop = true;
		}

		public void SetBitmap(Bitmap bitmap)
		{
			SetBitmap(bitmap, 255);
		}

		public void SetBitmap(Bitmap bitmap, byte opacity)
		{
			if (!(bitmap.PixelFormat == PixelFormat.Format32bppArgb)) {
				// this may not actually be true.
				// we have a function here that is to create such a bitmap
				// from the alpha channel.
				throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");
			}
			//CurrBitmap = bitmap
			IntPtr screenDc = Win32.GetDC(IntPtr.Zero);
			IntPtr memDc = Win32.CreateCompatibleDC(screenDc);
			IntPtr hBitmap = IntPtr.Zero;
			IntPtr oldBitmap = IntPtr.Zero;
			try {
				hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
				oldBitmap = Win32.SelectObject(memDc, hBitmap);
				Win32.Size size = new Win32.Size(bitmap.Width, bitmap.Height);
				Win32.Point pointSource = new Win32.Point(0, 0);
				Win32.Point topPos = new Win32.Point(Left, Top);
				Win32.BLENDFUNCTION blend = new Win32.BLENDFUNCTION();
				blend.BlendOp = Win32.AC_SRC_OVER;
				blend.BlendFlags = 0;
				blend.SourceConstantAlpha = opacity;
				blend.AlphaFormat = Win32.AC_SRC_ALPHA;
				Win32.UpdateLayeredWindow(Handle, screenDc, topPos, size, memDc, pointSource, 0, blend, Win32.ULW_ALPHA);
			} finally {
				Win32.ReleaseDC(IntPtr.Zero, screenDc);
				if (!(hBitmap.Equals(IntPtr.Zero))) {
					Win32.SelectObject(memDc, oldBitmap);
					Win32.DeleteObject(hBitmap);
				}
				Win32.DeleteDC(memDc);
			}
		}

		protected override CreateParams CreateParams {
			get {
				CreateParams cp = base.CreateParams;
				cp.ExStyle = cp.ExStyle | (524288);
				return cp;
			}
		}
	}
}
