using System;

namespace System.Drawing.Utilities
{
	namespace SuperOld
	{
		[Flags] public enum secType { Gallery = 0, GallerySection = 1 }
		[Flags] public enum scaleFlags { autoScale = 0,sWidth	= 1,sHeight	= 2 }
		public enum SizeFlags { originalSize = 0, scaledSize = 1, scaledBoxed = 2 }
		public enum Format { jpg = 1, png = 0, }

		[Flags] public enum Listeners { None=0,Packets, Stroke, MouseEvent, Paint, Resize,Mouse,Position,Stack }
		public enum PosEnum { AutoCenter,Top,TopRight,Right,BottomRight,Bottom,BottomLeft,Left,TopLeft }
		public enum MouseInfo : sbyte { Down=1, Up= -1, Move=0 }
	}

	public class LineUtil
	{
		public double Length(FPoint p1)
		{
			return Math.Sqrt(Math.Pow(p1.X,2)+Math.Pow(p1.Y,2));
		}
	}
	public class ConvertUnits
	{
		static public double MetersToFeet(int met)
		{
			return 0d;
		}
		static public double FeetToMeters(int met)
		{
			return 0d;
		}
		//http://eosweb.larc.nasa.gov/EDDOCS/Wavelength_Program.html
		public const double met_to_feet = 3.28d; // meters to feet
		public const double spd_of_ligh = 299792458; // meters per second
		public enum band { am,fm }
		public enum units { feet }
		static public double WaveLengthFromFrequency(double freq , band tp)
		{
			// IF units = "feet" [Note: units are, by default, in meters]
			//THEN
			//wavelength = wavelength x METERS TO FEET
			double output = 0d;
			switch (tp)
			{
					case band.am: output = spd_of_ligh/freq; break;
					case band.fm: output = spd_of_ligh*(1000/freq); break;
			}
			return output;
		}
	}
}