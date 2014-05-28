/*
 * Created by SharpDevelop.
 * User: tfw
 * Date: 9/19/2008
 * Time: 3:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace System
{

	namespace Cor3
	{
		[Flags] public enum TSpec
		{
			ATTRIBUTES_DEEP,
			ATTRIBUTES,
			CONSTRUCTORS,
			EVENTS,
			FIELDS,
			MEMBERS,
			METHODS,
			NESTED_TYPES,
			PROPERTIES,
		}
	}
}
