/* User: oIo * Date: 5/18/2010 * Time: 3:20 PM */
using System;
using System.Windows.Forms;

namespace System.Cor3.Forms
{
	public class FormExceptionEvent : EventArgs
	{
		public Control ctl;
		public Exception Error;
		public string LastValue = string.Empty;
		public FormExceptionEvent(Exception ex, string lastValue) : this(ex)
		{
			LastValue = lastValue;
		}
		public FormExceptionEvent(Exception ex)
		{
			Error = ex;
		}
	}
}
