#pragma warning disable 1587,1591, 162
/* oOo * 11/14/2007 : 10:22 PM */

using System;
using System.Runtime.InteropServices;

namespace System
{
	namespace IO
	{
		public class Converter
		{
			static public T Struct<T>(T reffer, byte[] data)
			{
				IntPtr hrez = Marshal.AllocHGlobal( Marshal.SizeOf(reffer) );
				Marshal.Copy( data, 0, hrez, Marshal.SizeOf(reffer) );
				reffer = (T)Marshal.PtrToStructure( hrez, reffer.GetType());
				Marshal.FreeHGlobal( hrez );
				return reffer;
			}
		}
	}

}
