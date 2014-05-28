#region User/License
// oio * 10/9/2012 * 9:59 PM

// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
#endregion
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// http://www.c-sharpcorner.com/uploadfile/kirtan007/sliding-effect-in-windows-form-application/
namespace System.Windows.Forms
{
	/// <summary>
	/// William Henry “Blue Apples”
	/// </summary>
	public class AnimatedForm : Form
	{
		readonly string owner_key;
		
		protected override void OnLoad(EventArgs e)
		{
			// Load the Form At Position of Main Form
			int WidthOfMain=Application.OpenForms[owner_key].Width;
			int HeightofMain= Application.OpenForms[owner_key].Height;
			int LocationMainX=Application.OpenForms[owner_key].Location.X ;
			int locationMainy=Application.OpenForms[owner_key].Location.Y;
			this.Animate(new Point(LocationMainX+WidthOfMain,locationMainy+10),100,AnimationType.AW_SLIDE |AnimationType.AW_HOR_POSITIVE);
		}
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			this.Animate(200,AnimationType.AW_HIDE |AnimationType.AW_SLIDE |AnimationType.AW_HOR_NEGATIVE);
			base.OnFormClosing(e);
		}
		
		public AnimatedForm() : base()
		{
			this.InitializeComponent();
			label1.DataBindings.Add("Text",this,"Text");
		}
		
		public AnimatedForm(Form owner) : this()
		{
			owner_key = owner.Name;
		}
		
		#region Design
		
//		/// <summary>
//		/// Disposes resources used by the form.
//		/// </summary>
//		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//		protected override void Dispose(bool disposing)
//		{
//			if (disposing) {
//				if (components != null) {
//					components.Dispose();
//				}
//			}
//			base.Dispose(disposing);
//		}
//		private System.ComponentModel.IContainer components = null;
		void InitializeComponent()
		{
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(11, 41);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(269, 22);
			this.textBox1.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(241, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(53, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "X";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(296, 85);
			this.panel1.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(11, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(157, 19);
			this.label1.TabIndex = 4;
			this.label1.Text = "label1";
			// 
			// AnimatedForm
			// 
			this.ClientSize = new System.Drawing.Size(296, 85);
			this.ControlBox = false;
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "AnimatedForm";
			this.ShowInTaskbar = false;
			this.Text = "Test Window";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		#endregion
		
		#region no
//		private void button1_Click(object sender, EventArgs e)
//		{
//			bool IsOpen = false;
//			FormCollection fc = Application.OpenForms;
//			foreach (Form f in fc)
//			{
//				if (f.Name == "AnimateWin")
//				{
//					IsOpen = true;
//					f.Focus();
//					break;
//				}
//			}
//			if (IsOpen == false)
//			{
//				AnimateWin form = new AnimateWin();
//				form.Show();
//			}
//		}
		#endregion
		
		void Button1Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}