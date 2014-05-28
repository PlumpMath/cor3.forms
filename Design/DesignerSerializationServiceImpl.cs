/* User: oIo * Date: 9/18/2010 * Time: 5:46 PM */
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms;

namespace System.Cor3.Design
{
	public class DesignerSerializationServiceImpl : /*ComponentSerializationService, */ IDesignerSerializationService {
		
		private IServiceProvider _serviceProvider;
//
//		public DesignerSerializationServiceImpl() : base()
//		{
//		}
		public DesignerSerializationServiceImpl ( IServiceProvider serviceProvider ) : base() {
			this._serviceProvider = serviceProvider;
		}

		public System.Collections.ICollection Deserialize(object serializationData)
		{
			SerializationStore serializationStore = serializationData as SerializationStore;
			if (serializationStore != null)
			{
				ComponentSerializationService componentSerializationService = _serviceProvider.GetService(typeof(ComponentSerializationService)) as ComponentSerializationService;
				ICollection collection = componentSerializationService.Deserialize(serializationStore);
				return collection;
			}
			return new object[] { };
		}

		public object Serialize(System.Collections.ICollection objects)
		{
			
			ComponentSerializationService componentSerializationService = _serviceProvider.GetService(typeof(ComponentSerializationService)) as ComponentSerializationService;
			Global.statR("SEr ----------------- {0}",objects.Count);
			SerializationStore serializationStore = componentSerializationService.CreateStore();
			foreach (object obj in objects)
			{
				try {
				componentSerializationService.SerializeAbsolute(serializationStore, obj);
				} catch (Exception) {
				Global.statB("We Need a serializable type for the component.\r\n\t{0}",obj.GetType());
				}
//				componentSerializationService.Serialize(serializationStore, obj);
			}
////			componentSerializationService.
//			serializationStore.Close();
//			Global.statR("#Errors = {0}",serializationStore.Errors.Count);
			return serializationStore;
		}
		//public override SerializationStore CreateStore()
		//{
		//    throw new NotImplementedException();
		//}
	}
}
