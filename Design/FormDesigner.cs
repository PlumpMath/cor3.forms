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
	public class DesignerPanel : UserControl
	{
		
	}
	
	/// <summary>
	/// See: IDesignerSurfaceExt — from ‘CodeProject.Net’
	//http://social.msdn.microsoft.com/forums/en-US/winformsdesigner/thread/89636476-c7eb-48c9-9312-8489fa75edaa/
	/// </summary>
	public class FormDesigner : DesignSurface, IFormDesigner
	{
		//, IDesignerSurfaceImpl

		#region UndoEngine
		private UndoEngineExt _undoEngine;
		private NameCreationServiceImp _nameCreationService = null;
		private DesignerSerializationServiceImpl _designerSerializationService = null;
		public DesignerSerializationServiceImpl DesignerSerializationService {
			get { return _designerSerializationService; }
		}
		private CodeDomComponentSerializationService _codeDomComponentSerializationService = null;
		#endregion

		public UndoEngineExt UndoEngine {
			get { return this._undoEngine; }
		}

		#region Verb
		//- do some Edit menu command using the MenuCommandService
		/// <summary>
		/// System.ComonentModel.StandardCommands /CommandID
		/// </summary>
		/// <param name="command"></param>
		public void DoAction(CommandID command)
		{
			MenuCommandService.GlobalInvoke(command);
		}
		public void DoAction(string command)
		{
			if (string.IsNullOrEmpty(command))
				return;
			IMenuCommandService ims = MenuCommandService;
			switch (command.ToUpper()) {
				case "CUT":
					ims.GlobalInvoke(StandardCommands.Cut);
					break;
				case "COPY":
					ims.GlobalInvoke(StandardCommands.Copy);
					break;
				case "PASTE":
					ims.GlobalInvoke(StandardCommands.Paste);
					break;
				case "DELETE":
					ims.GlobalInvoke(StandardCommands.Delete);
					break;
				default:
					break;
			}
			//end_switch
		}
		#endregion

		public FormDesigner(Control host) : base()
		{
			InitServices();
		}
//		bool IsIComponentInitializer { get { return designer is IComponentInitializer; } }

		public ICComponent<Control> CreateIControl(IDesignerHost host, Type controlType)
		{

			IComponent cpO = host.CreateComponent(controlType);
			ICComponent<Control> icpO = new ControlComponent(cpO);
			IDesigner designer = host.GetDesigner(cpO);
			icpO.TComponent.Parent = host.RootComponent as Control;
			if (designer == null)
				return null;
			try {
				if (designer is IComponentInitializer)
					((IComponentInitializer)designer).InitializeNewComponent(null);
			} catch (Exception) {

			}
			return icpO;
		}

		/// <summary>Copy/Paste</summary>
		public Control CreateControl(Type controlType, Size controlSize, Point controlLocation)
		{
			IDesignerHost host = DesignerHost;
			if (null == host)
				return null;
			if (null == DesignerHost.RootComponent)
				return null;
			ICComponent<Control> newComp = CreateIControl(host, controlType);
			newComp.Size = controlSize;
			newComp.Location = controlLocation;
			newComp.TComponent.Parent = host.RootComponent as Control;
			return newComp.TComponent;

		}

		public IDesignerHost DesignerHost {
			get { return (IDesignerHost)GetService(typeof(IDesignerHost)); }
		}

		public Control GetView()
		{
			Control ctrl = this.View as Control;
			ctrl.Dock = DockStyle.Fill;
			return ctrl;
		}

		#region TabOrder
		private TabOrderHooker _tabOrder = null;
		private bool _tabOrderMode = false;
		public bool IsTabOrderMode {
			get { return _tabOrderMode; }
		}
		TabOrderHooker TabOrder {
			get {
				if (_tabOrder == null)
					_tabOrder = new TabOrderHooker();
				return _tabOrder;
			}
			set { _tabOrder = value; }
		}
		public void InvokeTabOrder()
		{
			TabOrder.HookTabOrder(DesignerHost);
			_tabOrderMode = true;
		}
		public void DisposeTabOrder()
		{
			TabOrder.HookTabOrder(DesignerHost);
			_tabOrderMode = false;
		}
		#endregion

		public ISelectionService SelectionService {
			get { return (ISelectionService)DesignerHost.GetService(typeof(ISelectionService)); }
		}
		public IMenuCommandService MenuCommandService {
			get { return GetService(typeof(IMenuCommandService)) as IMenuCommandService; }
		}

		public void AddService(Type type, object obj)
		{
			ServiceContainer.AddService(type, obj);
		}
		public void RemoveService(Type type, bool promote)
		{
			ServiceContainer.RemoveService(type, promote);
		}
		public void RemoveService(Type type)
		{
			ServiceContainer.RemoveService(type, false);
		}
		public T AssertService<T>(object obj)
		{
			if (obj == null)
				return (T)obj;
			RemoveService(typeof(T), false);
			AddService(typeof(T), obj);
			return (T)obj;
		}

		//- The DesignSurface class provides several design-time services automatically.
		//- The DesignSurface class adds all of its services in its constructor.
		//- Most of these services can be overridden by replacing them in the
		//- protected ServiceContainer property.To replace a service, override the constructor,
		//- call base, and make any changes through the protected ServiceContainer property.
		private void InitServices2()
		{
			_nameCreationService = AssertService<NameCreationServiceImp>(new NameCreationServiceImp());
			_codeDomComponentSerializationService = AssertService<CodeDomComponentSerializationService>(new CodeDomComponentSerializationService(this.ServiceContainer));
			_designerSerializationService = AssertService<DesignerSerializationServiceImpl>(new DesignerSerializationServiceImpl(ServiceContainer));

			_undoEngine = new UndoEngineExt(this.ServiceContainer);
			_undoEngine.Enabled = false;
			if (_undoEngine != null) {
				AssertService<UndoEngineExt>(_undoEngine);
			}
			this.ServiceContainer.AddService(typeof(IMenuCommandService), new MenuCommandService(this));
		}
		private void InitServices()
		{
			_nameCreationService = new NameCreationServiceImp();
			if (_nameCreationService != null) {
				ServiceContainer.RemoveService(typeof(INameCreationService), false);
				ServiceContainer.AddService(typeof(INameCreationService), _nameCreationService);
			}
			_codeDomComponentSerializationService = new CodeDomComponentSerializationService(this.ServiceContainer);
			if (_codeDomComponentSerializationService != null) {
				ServiceContainer.RemoveService(typeof(ComponentSerializationService), false);
				ServiceContainer.AddService(typeof(ComponentSerializationService), _codeDomComponentSerializationService);
			}
			_designerSerializationService = new DesignerSerializationServiceImpl(ServiceContainer);
			if (_designerSerializationService != null) {
				ServiceContainer.RemoveService(typeof(IDesignerSerializationService), false);
				ServiceContainer.AddService(typeof(IDesignerSerializationService), _designerSerializationService);
			}
			_undoEngine = new UndoEngineExt(this.ServiceContainer);
			//- disable the UndoEngine
			_undoEngine.Enabled = false;
			if (_undoEngine != null) {
				this.ServiceContainer.RemoveService(typeof(UndoEngine), false);
				this.ServiceContainer.AddService(typeof(UndoEngine), _undoEngine);
			}
			this.ServiceContainer.AddService(typeof(IMenuCommandService), new MenuCommandService(this));
		}
		public void SwitchTabOrder()
		{
			if (false == IsTabOrderMode)
				InvokeTabOrder();
			else
				DisposeTabOrder();
		}
		public void UseSnapLines()
		{
			IServiceContainer serviceProvider = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
			DesignerOptionService opsService = serviceProvider.GetService(typeof(DesignerOptionService)) as DesignerOptionService;
			if (null != opsService) {
				serviceProvider.RemoveService(typeof(DesignerOptionService));
			}
			DesignerOptionService opsService2 = new DesignerOptionServiceExt4SnapLines();
			serviceProvider.AddService(typeof(DesignerOptionService), opsService2);
		}
		public void CheckOptions()
		{
			DesignerOptionService dss = this.GetService(typeof(DesignerOptionService)) as DesignerOptionService;
			dss.Options.ShowDialog();
			foreach (PropertyDescriptor tt in dss.Options.Properties) {
				Global.statG("{0}", tt.Name);
			}
			object o = this._designerSerializationService.Serialize(this.ComponentContainer.Components);
			//ComponentModel.Design.Serialization.CodeDomComponentSerializationService
			Global.statR("{0}: {1}", o, o.GetType().Name);
		}
		public IComponent CreateRootComponent(Type controlType, Size controlSize)
		{
			IDesignerHost host = DesignerHost;
			if (null == host)
				return null;
			if (null != host.RootComponent)
				return null;
			this.BeginLoad(controlType);
			if (this.LoadErrors.Count > 0)
				throw new Exception("the BeginLoad() failed! Some error during " + controlType.ToString() + " loding");

			IDesignerHost ihost = DesignerHost;
			Control ctrl = null;
			Type hostType = host.RootComponent.GetType();
			if (hostType == typeof(Form)) {
				ctrl = this.View as Control;
				ctrl.BackColor = Color.LightGray;
				PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(ctrl);
				PropertyDescriptor pdS = pdc.Find("Size", false);
				if (null != pdS)
					pdS.SetValue(ihost.RootComponent, controlSize);
			} else if (hostType == typeof(UserControl)) {
				ctrl = this.View as Control;
				ctrl.BackColor = Color.DarkGray;
				PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(ctrl);
				PropertyDescriptor pdS = pdc.Find("Size", false);
				if (null != pdS)
					pdS.SetValue(ihost.RootComponent, controlSize);
			} else if (hostType == typeof(Component)) {
				ctrl = this.View as Control;
//					ctrl.BackColor = Color.White;
			} else {
				throw new Exception("Undefined Host Type: " + hostType.ToString());
			}
			return ihost.RootComponent;
		}
	}
}
