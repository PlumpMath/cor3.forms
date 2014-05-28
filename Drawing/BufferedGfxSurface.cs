#region tfw * 8/1/2009 * 4:22 PM ** 'LICENSE & File Header'
/* [insert license here] **
 * if no license is mentioned above
 * you are to assume a HYBRID GPL/MIT license which in general allows you to use
 * the code without limitation (commercially or not) provided that if there are
 * any alterations made to the code, you must supply us with a copy.  Also you
 * are to credit the authors and include a respective MIT/GPL license on each
 * respective script and supply each respective and/or applicable license(s) 
 * with and binaries produced as a result of this property.
***
 * -- thanks
 */
#endregion
using System;
using System.Drawing;

namespace System.Cor3.Drawing
{
	/// <summary>
	/// An old drawing helper.
	/// <para>
	/// This class is similar to the BufferedGfx class.
	/// </para>
	/// </summary>
	/// <see cref="System.Cor3.Drawing.BufferedGfx">BufferedGfx</see>
	[Obsolete("This class is depreceated—no longer intended for use.")]
	public class BufferedGfxSurface : BufferedGfx
	{
		/// <summary>
		/// 
		/// </summary>
		public SurfaceQuality qs = default(SurfaceQuality);
		/// <summary>
		/// 
		/// </summary>
		public FloatRect HClient = default(FloatRect);
		/// <summary>
		/// 
		/// </summary>
		public Color iclr = Color.White;
		/// <summary>
		/// 
		/// </summary>
		public Color InitColor { get { return iclr; } set { iclr = value; } }
		/// <summary>
		/// 
		/// </summary>
		public override void Init() { qs = new SurfaceQuality(); }
		/// <summary>
		/// 
		/// </summary>
		public BufferedGfxSurface() : base() { }
	}
}
