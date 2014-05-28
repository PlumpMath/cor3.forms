/* User: oIo * Date: 5/18/2010 * Time: 3:20 PM */
using System;

namespace System.Cor3.Forms
{
	public enum AutoFormat
	{
		None,
		@Integer,
		@DateTime,
		@Date,
		@Currency,
		Custom
	}
	public interface IDataValue
	{
		AutoFormat AutoFormatMode { get; }
		string Text { get; }
		string InputValue { get;set; }
		int MaxLength { get;set; }
	}
}
