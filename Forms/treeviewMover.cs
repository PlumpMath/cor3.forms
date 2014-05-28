/*
 * Created by SharpDevelop.
 * User: tfw
 * Date: 8/12/2009
 * Time: 3:05 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace System.Cor3.Forms
{

	delegate void ContextD(treeviewMover mover, ContextMenuStrip strip);
	class treeviewMover : object_manager<TreeViewMover>
	{
		public event TreeNodeDelete NodeDeleting;
		public event TreeNodeDelete NodeDropped;
		public event ContextD CreatedContext;
	
		protected virtual void OnCreatedContext(ContextMenuStrip strip) {
			if (CreatedContext != null) CreatedContext(this,strip);
		}
		protected virtual void OnNodeDropped(TreeView parent_tree, TreeNode selected, bool cancel) {
			if (NodeDropped != null)
				NodeDropped(parent_tree, selected, cancel);
		}
		protected virtual void OnNodeDeleting(TreeView parent_tree, TreeNode selected, bool cancel)
		{
			if (NodeDeleting != null)
			{
				NodeDeleting(parent_tree, selected, cancel);
			}
		}
		TreeNodeCollection SelectedParent
		{
			get
			{
				return (Client.SelectedNode.Level==0) ? 
					Client.Nodes : Client.SelectedNode.Parent.Nodes;
			}
		}
	
		//int SelectedLevel { return Client.SelectedNode.Level; }
		/// <summary>
		/// <para>In stead of cloning the selected node, we seem to be missing the 
		/// tag of the cloned Node, so it would be a good idea at least to copy
		/// the Node's Tag (and test this of course) so that when a node is moved,
		/// it's Tag property is also the same.</para>
		/// <para>• the above change has been made April 13th.</para>
		/// </summary>
		TreeNode SelectedClone
		{
			get
			{
				TreeNode tn = Client.SelectedNode;
				object tag = tn.Tag;
				TreeNode tn_clone = Client.SelectedNode.Clone() as TreeNode;
				tn_clone.Tag = tag;
				return tn_clone;
			}
		}
	
		internal int SelectedIndex { get { return Client.SelectedNode.Index; } }
		internal int SelectedCount { get { return SelectedParent.Count; } }
		internal bool IsFirst { get { return Client.SelectedNode.Index == 0; } }
		internal bool IsLast { get { return SelectedIndex == SelectedCount-1; } }
		internal bool IsDeletable
		{
			get
			{
				return SelectedCount>0;
			}
		}
	
		virtual public bool CanMoveUp
		{
			get
			{
				if (SelectedCount<= 1) return false;
				if (IsFirst) return false;
				return true;
			}
		}
		virtual public bool CanMoveDown
		{
			get
			{
				if (IsLast) return false;
				if (SelectedCount<=1) return false;
				return true;
			}
		}
	
		public ToolStripItem BtnUp = null,BtnDown=null,BtnDel=null;
		internal bool HasUp { get { return BtnUp!=null; } }
		internal bool HasDown { get { return BtnDown!=null; } }
		internal bool CanDelete { get { return BtnDown!=null; } }
	
		internal void MoveNode(bool move_up)
		{
			int ndx = SelectedIndex;
			TreeNodeCollection ParentN = SelectedParent;
			TreeNode tn = SelectedClone;
			ParentN.Remove(Client.SelectedNode);
			ParentN.Insert((move_up)?ndx-1:ndx+1,tn);
			Client.SelectedNode = tn;
		}
	
		public TreeNode AddNode(string name, object tag)
		{
			TreeNode tn = NodeFromObject(name,tag);
			Client.Nodes.Add(tn);
			return tn;
		}
		public void RemoveByKey(string Key)
		{
			Client.Nodes.RemoveByKey(Key);
		}
		public TreeNode FromKey(string Key)
		{
			TreeNode[] tn = Client.Nodes.Find(Key,true);
			if (tn.Length==0) return null;
			if (tn[0]==null) return null;
			return tn[0];
		}
		public void RemoveNode(string name,object tag)
		{
			Client.Nodes.Remove(NodeFromObject(name,tag));
		}
	
		public bool SameNode(params TreeNode[] node)
		{
			return ( (node[0].Tag == node[1].Tag) && (node[0].Text == node[1].Text));
		}
		/// based on treeview.ContainsKey()
		public bool HasKey(TreeNode tn)
		{
			return Client.Nodes.ContainsKey(tn.Name);
		}
		/// based on treeview.Contains()
		public bool HasNode(TreeNode tn)
		{
			return Client.Nodes.Contains(tn);
		}
	
		public TreeNode NodeFromObject(string name, object tag)
		{
			TreeNode tn = new TreeNode(name);
			tn.Name = (tn.Tag = tag).ToString();
			return tn;
		}
	
		internal void eUp(object sen, EventArgs arg) { if (CanMoveUp) MoveNode(true); }
		internal void eDwn(object sen, EventArgs arg) { if (CanMoveDown) MoveNode(false); }
		internal void eAfterSel(object s, TreeViewEventArgs e)
		{
//			Application.DoEvents();
			if (CanDelete && BtnDel!=null) BtnDel.Enabled=true;
			if (HasUp) BtnUp.Enabled = CanMoveUp;
			if (HasDown) BtnDown.Enabled = CanMoveDown;
		}
		public void eDelete(object s, EventArgs e)
		{
			bool bdel = false;
			OnNodeDeleting(this.Client,Client.SelectedNode,bdel);
			if (!bdel) Client.SelectedNode.Remove();
		}
	
		public void AddNodeBefore(TreeNode sel, params TreeNode[] nodes)
		{
			foreach (TreeNode node in nodes)
				sel.Parent.Nodes.Insert(sel.PrevNode.Index,node);
		}
		public void AddNodeAfter(TreeNode sel, params TreeNode[] nodes)
		{
			foreach (TreeNode node in nodes)
				sel.Parent.Nodes.Insert(sel.Index,node);
		}
		public void AddChildNodes(TreeNode sel, params TreeNode[] nodes)
		{
			foreach (TreeNode node in nodes)
				sel.Nodes.Add(node);
		}
	
		public void SetButtons(ToolStripItem up, ToolStripItem down, ToolStripItem del)
		{
			BtnUp = up;
			BtnDown = down;
			BtnDel=del;
			if (BtnUp!=null)
			{
				BtnUp.MouseDown -= eUp; BtnUp.MouseDown += eUp;
			}
			if (BtnDown!=null)
			{
				BtnDown.MouseDown -= eDwn; BtnDown.MouseDown += eDwn;
			}
			if (BtnDel!=null)
			{
				BtnDel.Click -= eDelete; BtnDel.Click += eDelete;BtnDel.Enabled = false;
			}
		}
		void eenter(object s, DragEventArgs e) // eenter
		{
			e.Effect = DragDropEffects.Move;
		}
		void eover(object s, DragEventArgs e) // DragOver
		{
			cck();
		}
		void edrag(object s, EventArgs e) { }
		void edrag(object s, ItemDragEventArgs e) { Client.DoDragDrop(e.Item, DragDropEffects.All); }
		
		ContextMenuStrip Contex {
			get {
				ContextMenuStrip c = new ContextMenuStrip();
				OnCreatedContext(c);
				return c;
			}
		}
	
		void edrop(object s, DragEventArgs e) {
			OnNodeDropped(Client,NodeAtPoint.Node,false);
			Contex.Show(MousePosition-24);
		}
		void cck()
		{
			FloatPoint fp = Client.PointToClient(MousePosition);
			if (fp==null) return;
			if (fp.Y <= 0) 
			if (Client.TopNode==NodeAtPoint.Node)
			if (Client.TopNode.PrevNode!=null)
				Client.TopNode.PrevNode.EnsureVisible();
		}
		void e_unused_drag(object s, DragEventArgs e) { if (NodeAtPoint.Node!=null) { Client.SelectedNode = NodeAtPoint.Node; } }
		void e_unused_drag(object s, QueryContinueDragEventArgs e) { if (NodeAtPoint.Node!=null) { Client.SelectedNode = NodeAtPoint.Node; } }
	
		public TreeViewHitTestInfo NodeAtPoint { get { return Client.HitTest(Client.PointToClient(MousePosition)); } }
		public FloatPoint MousePosition { get { return new FloatPoint(Control.MousePosition.X,Control.MousePosition.Y); } }
		public bool drag = false;
		
		public override void AddEvents()
		{
			base.AddEvents();
			Client.DragEnter += new DragEventHandler(eenter);
			Client.DragOver += new DragEventHandler(eover);
			Client.DragLeave += new EventHandler(edrag);
			Client.ItemDrag += new ItemDragEventHandler(edrag);
			Client.DragDrop += new DragEventHandler(edrop);
			Client.QueryContinueDrag += new QueryContinueDragEventHandler(edrag);
	//			Client.MouseDown += emoused;
	//			Client.MouseUp += emouseu;
	//			Client.MouseMove += emousem;
			Client.FullRowSelect = true;
			Client.AllowDrop = true;
		}
	
		public treeviewMover(TreeViewMover ctl) : base(ctl) { Client.AfterSelect += eAfterSel; }
		public treeviewMover(TreeViewMover ctl, ToolStripItem up, ToolStripItem down) : this(ctl,up,down,null)
		{
		}
		public treeviewMover(TreeViewMover ctl, ToolStripItem up, ToolStripItem down, ToolStripItem dlete) : this(ctl)
		{
			SetButtons(up,down,dlete);
		}
	}
}
