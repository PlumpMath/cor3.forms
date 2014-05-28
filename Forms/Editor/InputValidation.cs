/* User: oIo * Date: 5/18/2010 * Time: 3:20 PM */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace System.Cor3.Forms.validity
{
	public class InputValidation
	{
		Color ColorControlDefault;
		public Color ColorInvalidInput = Color.FromArgb(240,80,80);

		public enum ValidationMode
		{
			None,
			DateTime,
			NumberLimit,
		}
		public ValidationMode ValidateMode = ValidationMode.None;

		#region Validation
		public delegate void ValidateTextBoxInput(TextBox textbox, string inputvalue);
		public delegate void ValidateRichTextInput(RichTextBox richtext, string inputvalue);

		public event ValidateTextBoxInput	ValidateTextBox;
		protected virtual void OnValidateTextBox(TextBox textbox, string inputvalue)
		{
			if (ValidateTextBox != null) {
				ValidateTextBox(textbox, inputvalue);
			}
		}

		public event ValidateRichTextInput	ValidateRichText;
		protected virtual void OnValidateRichText(RichTextBox richtext, string inputvalue)
		{
			if (ValidateRichText != null) {
				ValidateRichText(richtext, inputvalue);
			}
		}
		#endregion

		public InputValidation(RichTextBox rtf, ValidationMode mode)
		{
			ColorControlDefault = rtf.BackColor;
			rtf.TextChanged += delegate { OnValidateRichText(rtf,string.Copy(rtf.Text)); };
		}
		public InputValidation(TextBox rtf, ValidationMode mode)
		{
			ColorControlDefault = rtf.BackColor;
			rtf.TextChanged += delegate { OnValidateTextBox(rtf,string.Copy(rtf.Text)); };
		}

		static public bool IsValidDateTime(string inputvalue)
		{
			DateTime dt;
			if(DateTime.TryParse(inputvalue, out dt))
				return true;
			return false;

		}
		
	}
}
