/* User: oIo * Date: 9/18/2010 * Time: 5:46 PM */
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace System.Cor3.Design
{
	public class UndoEngineExt : UndoEngine {
		public event EventHandler CanUndoTrigger;
		public event EventHandler CantUndoTrigger;
		public event EventHandler CanRedoTrigger;
		public event EventHandler CantRedoTrigger;
//		private string _Name_ = "UndoEngineExt";
		private Stack<UndoEngine.UndoUnit> undoStack = new Stack<UndoEngine.UndoUnit>();
		private Stack<UndoEngine.UndoUnit> redoStack = new Stack<UndoEngine.UndoUnit>();
		
		public UndoEngineExt ( IServiceProvider provider ) : base ( provider ) {}

		public int StackSizeUndo { get { return undoStack.Count; } }
		public int StackSizeRedo { get { return redoStack.Count; } }
		
		public bool EnableUndo { get { return undoStack.Count > 0; } }
		public bool EnableRedo { get { return redoStack.Count > 0; } }
		#pragma warning disable 168
		public void Undo() {
			if ( EnableUndo ) {
				try {
					UndoEngine.UndoUnit unit = undoStack.Pop();
					unit.Undo();
					redoStack.Push ( unit );
				}
				catch ( Exception ex ) {
					
				}
			}
			else {
			}
			CheckTrigger();
		}
		#pragma warning restore 168
		public void Redo() {
			if ( EnableRedo ) {
				try {
					UndoEngine.UndoUnit unit = redoStack.Pop();
					unit.Undo();
					undoStack.Push ( unit );
				}
				catch ( Exception ) {
				}
			}
			else {
			}
			CheckTrigger();
		}
		void CheckTrigger()
		{
			if (EnableRedo && (CanUndoTrigger!=null))
				CanRedoTrigger(this,EventArgs.Empty);
			else if (CantUndoTrigger!=null) CantRedoTrigger(this,EventArgs.Empty);
			if (EnableUndo && (CanUndoTrigger!=null))
				CanUndoTrigger(this,EventArgs.Empty);
			else if (CantUndoTrigger!=null) CantUndoTrigger(this,EventArgs.Empty);
		}
		protected override void AddUndoUnit ( UndoEngine.UndoUnit unit ) { undoStack.Push ( unit ); }
		
		
	}//end_class
}
