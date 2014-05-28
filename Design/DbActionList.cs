/* User: oIo * Date: 9/21/2010 * Time: 11:44 AM */

/* User: oIo * Date: 9/18/2010 * Time: 5:46 PM */
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Cor3.Design
{
	//http://msdn.microsoft.com/en-us/library/ms171830.aspx
	public class DbActionList : DesignerActionList
	{
		public DbActionList(IComponent c) : base(c)
		{
		}
		
		string profileName = string.Empty;
		//[Editor]
		public string TableProfileName { get { return profileName; } set { profileName = value; } }
		/*public void RefreshDataTables()
		{
		}
		public void SomeOtherMethod()
		{
		}*/
        public DockStyle DockControl { get { return (this.Component as Control).Dock; } set { SetProp("Dock", value); } }
		virtual public Color BackgroundColor
		{
            get { return (this.Component as Control).BackColor; }
			set { SetProp("BackColor",value); }
		}
		virtual public Color ForeColor
		{
            get { return (this.Component as Control).ForeColor; }
			set { SetProp("ForeColor",value); }
		}
		
		protected void SetProp(string name, object value)
		{
            GetPropertyByName(name).SetValue(this.Component, value);
		}
		private PropertyDescriptor GetPropertyByName(String propName)
		{
			PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(this.Component)[propName];
			if (null == prop) throw new ArgumentException( "Matching Target property not found!",propName);
			else return prop;
		}
	}

}
