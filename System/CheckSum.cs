#pragma warning disable 1587,1591, 162
/* oOo * 11/14/2007 : 10:22 PM */

using System;
using System.Collections.Generic;
using System.Cor3.Forms;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace System
{
	namespace IO
	{
		#pragma warning disable 436
		public class CheckSum : ITableChecksum {
			#pragma warning restore 436
			public long Check(long length, BinaryReader reader)
			{
				long len = length/4, i =-1, Sum = 0;
				while (i++ < len) Sum += reader.ReadInt32();
				return Sum;
			}
			//		public ULONG CalcTableChecksum(ULONG Table, ULONG Length)
			//		{
			//			ULONG Sum = 0L;
			//			public long cursor=0, data;
			//			ULONG *Endptr = Table+((Length+3) & ~3) / sizeof(ULONG);
			//			while (Table < EndPtr) Sum += *Table++;
			//			return Sum;
			//		}
		}
	}

}
