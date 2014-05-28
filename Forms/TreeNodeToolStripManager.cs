/*
 * Created by SharpDevelop.
 * User: tfw
 * Date: 8/12/2009
 * Time: 3:05 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace System.Cor3.Forms
{
	
	public class TreeViewMover : System.Windows.Forms.TreeView
	{
		public event EventHandler Downer { add { if (ButtonDown==null) return; ButtonDown.Click+=value; } remove { if (ButtonDown==null) return; ButtonDown.Click -= value; } }

		private IComponent nodeDown=null,nodeUp=null,nodeDelete=null;
		public ToolStripItem ButtonUp { get { return (ToolStripItem)nodeUp; } set { nodeUp=value; } }
		public ToolStripItem ButtonDown { get { return (ToolStripItem)nodeDown; } set { nodeDown=value; } }
		public ToolStripItem ButtonDelete { get { return (ToolStripItem)nodeDelete; } set { nodeDelete=value; } }

		#region Unused EventHandlers
		ToolStripItem AddButton(IComponent newComponent, ToolStripItem oldComponent, EventHandler handler)
		{
			if (oldComponent!=null) oldComponent.Click -= handler;
			if ((oldComponent = (ToolStripItem)newComponent)==null) return null;
			oldComponent.Click += handler;
			return oldComponent;
		}
		public event EventHandler NodeUpClick;
		protected virtual void OnNodeUpClick(EventArgs e) { if (NodeUpClick != null) { NodeUpClick(this, e); } }
		public event EventHandler NodeDownClick;
		protected virtual void OnNodeDownClick(EventArgs e) { if (NodeDownClick != null) { NodeDownClick(this, e); } }
		public event EventHandler NodeDeleteClick;
		protected virtual void OnNodeDeleteClick(EventArgs e) { if (NodeDeleteClick != null) { NodeDeleteClick(this, e); } }
		#endregion

		void eMsgReport(object sender, EventArgs args) { Global.statG("{0}",sender); }
		
		treeviewMover manager=null;
		public void InitializeManager()
		{
			manager = new treeviewMover(this);
			manager.SetButtons(ButtonUp,ButtonDown,ButtonDelete);
		}
		public TreeViewMover() : base()
		{
		}
		//base.OnCreateControl
	}
//	public class TreeViewMover1 : System.Windows.Forms.TreeView
//	{
//		
//		public event EventHandler Downer { add { if (ButtonDown==null) return; ButtonDown.Click+=value; } remove { if (ButtonDown==null) return; ButtonDown.Click -= value; } }
//
//		private IComponent nodeDown=null,nodeUp=null,nodeDelete=null;
//		public ToolStripItem ButtonUp { get { return (ToolStripItem)nodeUp; } set { nodeUp=value; } }
//		public ToolStripItem ButtonDown { get { return (ToolStripItem)nodeDown; } set { nodeDown=value; } }
//		public ToolStripItem ButtonDelete { get { return (ToolStripItem)nodeDelete; } set { nodeDelete=value; } }
//
//		void eMsgReport(object sender, EventArgs args)
//		{
//			Global.statG("{0}",sender);
//		}
//		
//		treeviewMover manager=null;
//		public void InitializeManager()
//		{
//			manager = new treeviewMover(this);
//			manager.SetButtons(ButtonUp,ButtonDown,ButtonDelete);
//		}
//		public TreeViewMover1() : base()
//		{
//		}
//	}
}
