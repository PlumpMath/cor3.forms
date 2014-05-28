/* User: oIo * Date: 9/18/2010 * Time: 5:46 PM */
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace System.Cor3.Design
{

	/// <summary>
	/// See: IDesignerSurfaceExt — from ‘CodeProject.Net’
	/// </summary>
	public interface IDesignerSurfaceImpl
	{
		Control CreateControl(Type controlType, Size controlSize, Point controlLocation);
		IComponent CreateRootComponent(Type controlType, Size controlSize);
		void DoAction(string command);
		IDesignerHost DesignerHost { get; }
//		UndoEngineExt GetUndoEngineExt();
		Control GetView();
//		        void SwitchTabOrder();
		//        void UseGrid(Size gridSize);
		//        void UseGridWithoutSnapping(Size gridSize);
		//        void UseNoGuides();
		void UseSnapLines();
	}





}
