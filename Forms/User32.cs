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
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace System
{
	partial class WindowsInterop
	{
		public class User32
		{
			const string user32 = "user32.dll";
			// SendMessage
//		[DllImport(user32)] static public extern IntPtr SendMessage(IntPtr hWnd, stuff Msg, stuff wParam,IntPtr lParam);
			[DllImport(user32)] static public extern IntPtr SendMessage(IntPtr hWnd,uint Msg,uint wParam,uint lParam);
			[DllImport(user32)] static public extern IntPtr SendMessage(IntPtr hWnd,int Msg,int wParam, int lParam);
			[DllImport(user32)] static public extern IntPtr SendMessage(IntPtr hWnd,int Msg,IntPtr wParam, IntPtr lParam);
			[DllImport(user32)] static public extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, IntPtr lParam);
			[DllImport(user32)] static public extern IntPtr SendMessage(IntPtr hWnd, IntPtr Msg, IntPtr wParam, IntPtr lParam);
			[DllImport(user32)] static public extern IntPtr SendMessage(IntPtr hWnd, WindowsInterop.EM_MSG Msg, IntPtr wParam, IntPtr lParam);
			#region caret
			[DllImport(user32)] static public extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int width, int height );
			[DllImport(user32)] static public extern bool DestroyCaret();
			[DllImport(user32)] static public extern int GetCaretBlinkTime();
			[DllImport(user32)] static public extern bool GetCaretPos(Point lpPoint);
			[DllImport(user32)] static public extern bool HideCaret(IntPtr hWnd);
			[DllImport(user32)] static public extern bool SetCaretBlinkTime(IntPtr hWnd);
			[DllImport(user32)] static public extern bool SetCaretPos(int X, int Y);
			[DllImport(user32)] static public extern bool ShowCaret(IntPtr hWnd);
			#endregion
		}
	}
}
