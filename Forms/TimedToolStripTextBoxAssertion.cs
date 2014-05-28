/*
 * Created by SharpDevelop.
 * User: tfw
 * Date: 8/12/2009
 * Time: 3:05 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace System.Cor3.Forms
{
	public class TimedToolStripTextBoxAssertion : Component
	{
		public event EventHandler Trigger;
		
		IComponent statusMain = null;
		public ToolStripTextBox TextControl
		{
			get { return (ToolStripTextBox) statusMain; }
			set { statusMain = value; }
		}
		ToolStripTextBox SetTextBoxText(IComponent c)
		{
			statusMain = c;
			return (ToolStripTextBox)statusMain;
		}
		bool HasNoControl
		{
			get { return (statusMain==null); }
		}
		
		private Timer statTimer;
		
		bool Ticking { get { return statTimer.Enabled; } set { statTimer.Enabled = value; } }
		string lastStatus = string.Empty;
		
		public string StatusMain
		{
			get { if (HasNoControl) return null; return TextControl.Text; }
			set {
				if (!HasNoControl) TextControl.Text = value;
			}
		}
		public string FlashStatus
		{
			set
			{
				if (HasNoControl) return;
				StartStatus(value);
			}
		}
		
		int timerInterval = 1800;
		[DefaultValue(1800)]
		public int TimerInterval
		{
			get { return timerInterval; }
			set {
				if (statTimer==null)
				{
					timerInterval = value;
				}
				else
				{
					statTimer.Interval = timerInterval = value;
				}
				ResetTimer();
			}
		}

		void StartStatus(string value)
		{
			/*StatusMain = value; */ 
			if (Ticking) statTimer.Stop();
			/*else lastStatus = StatusMain; StatusMain = value; */
			statTimer.Start();
		}
		void eTick (object s, EventArgs e) { statTimer.Stop(); if (Trigger!=null) Trigger(this,EventArgs.Empty); }
		
		public void ResetTimer()
		{
			if (DesignMode) return;
			TextControl.KeyPress -= TextControl_KeyPress;
			TextControl.KeyPress += TextControl_KeyPress;
			if (statTimer==null) statTimer = new Timer();
			statTimer.Interval = timerInterval;
			statTimer.Tick += eTick;
		}
		void TextControl_KeyPress(object sender, KeyPressEventArgs args)
		{
			StartStatus(TextControl.Text);
		}
		public TimedToolStripTextBoxAssertion()
		{
			if (DesignMode) return;
			try {
				ResetTimer();
			} catch (Exception) {
				
			}
		}
	}
}
