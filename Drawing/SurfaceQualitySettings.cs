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
	public class SurfaceQualitySettings : QualitySettings
	{
		virtual public SmoothingMode Smoothing { get { return smoothing; } set { smoothing = value; } }
		virtual public InterpolationMode Interpolation { get { return interpolation; } set { interpolation = value; } }
		virtual public GraphicsUnit Units { get { return units; } set { units = value; } }
		virtual public TextRenderingHint TypeRenderingHint { get { return typerenderinghint; } set { typerenderinghint = value; } }
		virtual public int TypeContrast { get { return typecontrast; } set { typecontrast = value; } }
		/*
		virtual public SmoothingMode Smoothing { get { return SmoothingMode.HighQuality; } }
		virtual public InterpolationMode Interpolation { get { return InterpolationMode.HighQualityBicubic; } }
		virtual public GraphicsUnit Units { get { return GraphicsUnit.Pixel; } }
		virtual public TextRenderingHint TypeRenderingHint { get { return TextRenderingHint.SystemDefault; } }
		virtual public int TypeContrast { get { return 6; } }*/
	}
}
