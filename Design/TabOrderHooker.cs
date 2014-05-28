/* User: oIo * Date: 9/18/2010 * Time: 5:46 PM */
using System;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.Cor3.Design
{
	
	internal class TabOrderHooker
	{
		const string _Name_ = "TabOrderHooker";
		const string str_sys_design = "System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
//		const string str_sys_design = "System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
		//http://social.msdn.microsoft.com/forums/en-us/vsx/thread/AAB9849E-2D9A-4829-A305-FAEDF602014B
		const string t_TabOrder = "System.Windows.Forms.Design.TabOrder";
		System.Reflection.Assembly DesignAssembly { get { return System.Reflection.Assembly.Load(str_sys_design); } }
		Type tabOrderType { get { return DesignAssembly.GetType( t_TabOrder ); } }
		BindingFlags DefaultBindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod;

		bool HasTabOrder { get { return _tabOrder!=null; } }
//		bool HasRoot { get { return host.RootComponent!=null; } }
		
		private object _tabOrder = null;
		public void HookTabOrder ( IDesignerHost host )
		{
			if ( host==null ) return ;
			if ( _tabOrder == null ) _tabOrder = Activator.CreateInstance ( tabOrderType, new object[] { host } );
			else DisposeTabOrder();
		}
		//- Disposes the tab order
		public void DisposeTabOrder() {
			if ( null == _tabOrder ) return;
			try {
				Type tabOrderType = DesignAssembly.GetType ( "System.Windows.Forms.Design.TabOrder" );
				tabOrderType.InvokeMember ( "Dispose", DefaultBindingFlags, null, _tabOrder, new object[] { true } );
				_tabOrder = null;
			}
			catch ( Exception ex ) {
				throw new Exception ( _Name_ + "::DisposeTabOrder() - Exception: (see Inner Exception)", ex );
			}//end_catch
		}
		
		
	}//end_class
}
