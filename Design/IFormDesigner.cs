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
	public interface IFormDesigner
	{
		void DoAction(CommandID command);
		void DoAction(string command);
		ICComponent<Control> CreateIControl(IDesignerHost host, Type controlType);
		Control CreateControl(Type controlType, Size controlSize, Point controlLocation);
		Control GetView();
		void InvokeTabOrder();
		void DisposeTabOrder();
		void AddService(Type type, object obj);
		void RemoveService(Type type, bool promote);
		void RemoveService(Type type);
		T AssertService<T>(object obj);
		void SwitchTabOrder();
		void UseSnapLines();
		void CheckOptions();
		IComponent CreateRootComponent(Type controlType, Size controlSize);
		DesignerSerializationServiceImpl DesignerSerializationService { get; }
		UndoEngineExt UndoEngine { get; }
		IDesignerHost DesignerHost { get; }
		bool IsTabOrderMode { get; }
		ISelectionService SelectionService { get; }
		IMenuCommandService MenuCommandService { get; }
	}
}
