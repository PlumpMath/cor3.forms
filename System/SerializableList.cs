#pragma warning disable 1587,1591, 162
/* oOo * 11/14/2007 : 10:22 PM */

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace System
{
	
	namespace IO
	{
		using System;
		using System.Collections.Generic;
		using System.Cor3.Forms;
		using System.IO;
		using System.Windows.Forms;
		using System.Xml.Serialization;




		[Serializable]
		public class SerializableList<T>
		{
			[XmlIgnore] protected internal List<T> innerList = new List<T>();
			[XmlElement("item")]
			public T[] Items { get { return innerList.ToArray(); } set { innerList = new List<T>(value); } }
			virtual public void Add(T item) { innerList.Add(item); }
			public void Clear() { innerList.Clear(); }
			/// Empty Initialization (for serialization)
			public SerializableList() {}
		}

	}

}
