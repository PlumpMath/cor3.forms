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
	public class DbControlDesigner : ControlDesigner
	{
		DesignerVerbCollection DesignerVerbs = new DesignerVerbCollection();
		public override DesignerVerbCollection Verbs { get { return DesignerVerbs; } }
	
		DesignerActionListCollection actionList;
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				if (null == actionList)
				{
					actionList = new DesignerActionListCollection();
					actionList.Add(new DbActionList(this.Component));
				}
				return actionList;
			}
		}
	
		void eAg(object sender, EventArgs eag)
		{
			
		}
		
		public DbControlDesigner() : base()
		{
			DesignerVerbs.Add(new DesignerVerb("crappo0",eAg));
			DesignerVerbs.Add(new DesignerVerb("crappo5",eAg));
		}
		public override void Initialize(IComponent c) {
			
			base.Initialize(c);
			Control control = c as Control;
		}
		
	}
}
