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
		public interface ITableChecksum
		{
			long Check(long length, BinaryReader reader);
		//	long Calculate(long length, int pos, BinaryReader reader);
		}
	}

}
