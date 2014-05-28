/*
 * User: tfw
 * Date: 8/12/2009
 * Time: 3:05 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 *//* User: oIo * Date: 8/18/2010 * Time: 4:27 AM */
/* The file had been moved over to Cor3.Forms */
using System;
using System.Windows.Forms;

namespace System.Cor3.Forms
{
	/// <summary>
	/// <para>don't forget to explicitly set the intitial Status or Color if you
	/// want to override a default.</para>
	/// <para>define your own constructor</para>
	/// </summary>
	public class StatusFlash
	{
		internal protected Timer statTimer = new Timer();
		internal protected bool Ticking { get { return statTimer.Enabled; } set { statTimer.Enabled = value; } }
		
		internal protected System.Drawing.Color lastColour = System.Drawing.SystemColors.ControlText;
		internal protected string lastStatus = string.Empty;
	
		virtual public System.Drawing.Color ColourErrorFg { get{ return System.Drawing.SystemColors.HighlightText; } }
		virtual public System.Drawing.Color ColourErrorBg { get{ return System.Drawing.SystemColors.HighlightText; } }
		virtual public System.Drawing.Color ColourFg { get { throw new NotImplementedException(); } set {  } }
		virtual public System.Drawing.Color ColourBg { get { throw new NotImplementedException(); } set {  } }
	
		virtual public string StatusMain { get { throw new NotImplementedException(); } set {  } }
	
		public string FlashStatus { set { StartStatus(value); } }
		void StartStatus(string value)
		{
			StartWithColor(value,ColourFg);
		}
		void ErrorStatus(string value)
		{
			StartWithColor(value,ColourErrorFg);
		}
		void StartWithColor(string value, System.Drawing.Color clr)
		{
			if (Ticking) { statTimer.Stop(); StatusMain = value; ColourFg=clr; statTimer.Start(); }
			else { lastStatus = StatusMain; lastColour = ColourFg; StatusMain = value; ColourFg=clr; statTimer.Start(); }
		}
		internal protected void eTick (object s, EventArgs e) { statTimer.Stop(); StatusMain = lastStatus; }
		
	}
}
