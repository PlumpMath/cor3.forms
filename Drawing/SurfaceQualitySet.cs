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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace System.Cor3.Drawing
{
    /// <summary>
    /// Now-a-days, we should probably provide a Extension class as this is a little old,
    /// it used to be used like an extension class might.
    /// <br/>
    /// It does happen to get the job done though and is attached to a few personal projects thus it remains.
    /// </summary>
	[Obsolete("This class is depreceated—no longer intended for use.")]
	public class SurfaceQualitySet : SurfaceQualitySettings
	{
		static public SurfaceQualitySettings Default
		{
			get
			{
				SurfaceQualitySettings settings = new SurfaceQualitySettings();
				settings.interpolation = InterpolationMode.Default;
				settings.smoothing = SmoothingMode.Default;
				settings.units = GraphicsUnit.Display;
				settings.typerenderinghint = TextRenderingHint.SystemDefault;
				settings.typecontrast = 6;
				return settings;
			}
		}
		static public SurfaceQualitySettings Quality
		{
			get
			{
				SurfaceQualitySettings settings = Default;
				settings.interpolation = InterpolationMode.HighQualityBicubic;
				settings.smoothing = SmoothingMode.HighQuality;
				return settings;
			}
		}
		static public SurfaceQualitySettings Quantity
		{
			get
			{
				SurfaceQualitySettings settings = Default;
				settings.smoothing = SmoothingMode.None;
				settings.interpolation = InterpolationMode.NearestNeighbor;
				return settings;
			}
		}
		static public SurfaceQualitySettings PixelQuality
		{
			get
			{
				SurfaceQualitySettings settings = Quality;
				settings.units = GraphicsUnit.Pixel;
				return settings;
			}
		}
		static public SurfaceQualitySettings PixelQuantity
		{
			get
			{
				SurfaceQualitySettings settings = Quantity;
				settings.units = GraphicsUnit.Pixel;
				return settings;
			}
		}
		static public SurfaceQualitySettings PointQuality {
			get
			{
				SurfaceQualitySettings settings = Quality;
				settings.units = GraphicsUnit.Point;
				return settings;
			}
		}
		static public SurfaceQualitySettings PointQuantity {
			get
			{
				SurfaceQualitySettings settings = Quantity;
				settings.units = GraphicsUnit.Point;
				return settings;
			}
		}
	}
}
