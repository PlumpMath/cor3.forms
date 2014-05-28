/* User: oIo * Date: 9/18/2010 * Time: 5:46 PM */
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Windows.Forms;

namespace System.Cor3.Design
{


	/// <summary>
	/// (all) property descriptor properties are obtained each time you obtain a value.
	/// </summary>
	public class ComponentDescription
	{
		protected internal IComponent CompO = null;
		internal ISite Site {
			get { return CompO.Site; }
		}

		[Browsable(false)]
		public IComponent IComponent { get { return CompO; } }

		[Category("Info")]
		public bool IsControl { get { return CompO is Control; } }
		public virtual string Name { get { return Site.Name; } }

		PropertyDescriptorCollection Properties {
			get { return TypeDescriptor.GetProperties(CompO); }
		}

		//		public PropertyDescriptor this[string keyProp] { get { return this[keyProp,false]; } }
		public PropertyDescriptor this[string keyProp, bool ignoreCase] {
			get { return GetDescriptor(keyProp, ignoreCase); }
		}
		public PropertyDescriptor this[PropertyDescriptorCollection pdC, string keyProp, bool ignoreCase] {
			get { return GetDescriptor(pdC, keyProp, ignoreCase); }
		}
		PropertyDescriptor GetDescriptor(string prop)
		{
			return GetDescriptor(prop, false);
		}
		PropertyDescriptor GetDescriptor(string prop, bool ignoreCase)
		{
			PropertyDescriptorCollection pdC = Properties;
			PropertyDescriptor propD = pdC.Find(prop, ignoreCase);
			pdC = null;
			return propD;
		}
		/// <summary>for processing more then one property from a single ‘PDC’</summary>
		protected internal PropertyDescriptor GetDescriptor(PropertyDescriptorCollection pdC, string prop, bool ignoreCase)
		{
			return pdC.Find(prop, ignoreCase);
		}

		public void SetValue(string propName, object value)
		{
			PropertyDescriptor pd = this[propName, false];
			pd.SetValue(CompO, value);
		}
		/// <summary>
		/// Obtain a hashtable of values from the component;
		/// </summary>
		public Hashtable GetValues(params string[] property)
		{
			PropertyDescriptorCollection pdC = Properties;
			Hashtable hat = new Hashtable();
			foreach (string prop in Properties) { hat.Add(prop, this[prop, false]); }
			pdC = null;
			return hat;
		}

		public ComponentDescription(IComponent component)
		{
			CompO = component;
		}
		~ComponentDescription()
		{
			CompO = null;
		}
	}
}
