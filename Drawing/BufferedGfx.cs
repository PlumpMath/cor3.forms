#region tfw * 8/1/2009 * 4:22 PM ** 'LICENSE & File Header'
/** [insert license here] **
 * if no license is mentioned above
 * you are to assume a HYBRID GPL/MIT license which in general allows you to use
 * the code without limitation (commercially or not) provided that if there are
 * any alterations made to the code, you must supply us with a copy.  Also you
 * are to credit the authors and include a respective MIT/GPL license on each
 * respective script and supply each respective and/or applicable license(s)
 * with and binaries produced as a result of this property.
 ***
 * -- thanks
 **/
#endregion
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
// 
namespace System.Cor3.Drawing
{
	/// <summary>
	/// An old drawing helper.
	/// <para>
	/// This class is similar to the BufferedGfx class.
	/// </para>
	/// </summary>
	/// <see cref="System.Cor3.Drawing.BufferedGfxSurface">BufferedGfxSurface</see>
	[Obsolete("This class is depreceated—no longer intended for use.")]
	public class BufferedGfx
	{
		/// <summary>
		/// 
		/// </summary>
		public BufferedGraphicsContext c_gfx;
		/// <summary>
		/// 
		/// </summary>
		public bool Busy = false;
		/// <summary>
		/// 
		/// </summary>
		public Graphics m_gfx{ get{ return (b_gfx!=null) ? b_gfx.Graphics:null; } }
		/// <summary>
		/// 
		/// </summary>
		public BufferedGraphics b_gfx;
		/// <summary>
		/// 
		/// </summary>
		virtual public void Init() {}
		/// <summary>
		/// 
		/// </summary>
		public BufferedGfx() : base() { Init(); }
	}

}
