/* User: oIo * Date: 5/18/2010 * Time: 3:20 PM */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace System.Cor3.Forms
{
	public class DateTimePanel : UserControl
	{
		public event EventHandler MonthClicked;
		protected virtual void OnMonthClicked(object sender, EventArgs e) { ModifyMonth(sender as ToolStripMenuItem); if (MonthClicked != null) { MonthClicked(sender, e); } }
		public event EventHandler AmPmClicked;
		protected virtual void OnAmPmClicked(object sender, EventArgs e) { ModifyAMPM(sender as ToolStripMenuItem); if (AmPmClicked != null) { AmPmClicked(sender, e); } }

//		public override string Text {
//			get { return base.Text; }
//			set { base.Text = value; }
//		}
		
		public ContentAlignment LabelAlignment { get { return this.label2.TextAlign; } set { this.label2.TextAlign = value; } }
		public string LabelText { get { return this.label2.Text; } set { this.label2.Text = value; } }

		ContextMenuStrip cmMonths, cmAMPM;
		DateTimeInfo _dtime = new DateTimeInfo(DateTime.Now);
		
		public DateTimePanel() : base()
		{
			InitializeComponent();
//			tMonth.AutoCompleteCustomSource.AddRange(MonthNames);
			cmMonths = new ContextMenuStrip();
			cmAMPM = new ContextMenuStrip();
			int i = 0;
			foreach (string mon in DateTimeInfo.MonthNames)
			{
				cmMonths.Items.Add(
					new ToolStripMenuItem(DateTimeInfo.MonthNames[i++],null,OnMonthClicked)
				);
			}
			cmAMPM.Items.Add( new ToolStripMenuItem("AM",null,OnAmPmClicked) );
			cmAMPM.Items.Add( new ToolStripMenuItem("PM",null,OnAmPmClicked) );
		}

		void ModifyMonth(ToolStripMenuItem sender)
		{
			lMonth.Text = sender.Text;
		}
		void ModifyAMPM(ToolStripMenuItem sender)
		{
//			if (sender.Text=="AM") _dtime.am
		}
		
		void InitializeComponent()
		{
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.tHour = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tMin = new System.Windows.Forms.TextBox();
			this.lAmPm = new System.Windows.Forms.Label();
			this.lMonth = new System.Windows.Forms.Label();
			this.tYear = new System.Windows.Forms.TextBox();
			this.tDate = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new System.Drawing.Point(4, 4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(247, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Date/Time";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.SystemColors.Window;
			this.label3.Dock = System.Windows.Forms.DockStyle.Left;
			this.label3.Font = new System.Drawing.Font("Bitstream Vera Sans Mono", 9F);
			this.label3.Location = new System.Drawing.Point(80, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(14, 14);
			this.label3.TabIndex = 1;
			this.label3.Text = ",";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Left;
			this.label4.Font = new System.Drawing.Font("Bitstream Vera Sans Mono", 9F);
			this.label4.Location = new System.Drawing.Point(133, 20);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(14, 14);
			this.label4.TabIndex = 3;
			this.label4.Text = " ";
			// 
			// tHour
			// 
			this.tHour.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tHour.Dock = System.Windows.Forms.DockStyle.Left;
			this.tHour.Font = new System.Drawing.Font("Bitstream Vera Sans Mono", 9F);
			this.tHour.Location = new System.Drawing.Point(147, 20);
			this.tHour.Name = "tHour";
			this.tHour.Size = new System.Drawing.Size(20, 14);
			this.tHour.TabIndex = 4;
			this.tHour.Text = "00";
			this.tHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.SystemColors.Window;
			this.label5.Dock = System.Windows.Forms.DockStyle.Left;
			this.label5.Font = new System.Drawing.Font("Bitstream Vera Sans Mono", 9F);
			this.label5.Location = new System.Drawing.Point(167, 20);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(14, 14);
			this.label5.TabIndex = 5;
			this.label5.Text = ":";
			// 
			// tMin
			// 
			this.tMin.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tMin.Dock = System.Windows.Forms.DockStyle.Left;
			this.tMin.Font = new System.Drawing.Font("Bitstream Vera Sans Mono", 9F);
			this.tMin.Location = new System.Drawing.Point(181, 20);
			this.tMin.Name = "tMin";
			this.tMin.Size = new System.Drawing.Size(20, 14);
			this.tMin.TabIndex = 6;
			this.tMin.Text = "00";
			this.tMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// lAmPm
			// 
			this.lAmPm.AutoSize = true;
			this.lAmPm.BackColor = System.Drawing.SystemColors.Window;
			this.lAmPm.Dock = System.Windows.Forms.DockStyle.Left;
			this.lAmPm.Font = new System.Drawing.Font("Bitstream Vera Sans Mono", 9F);
			this.lAmPm.Location = new System.Drawing.Point(201, 20);
			this.lAmPm.Name = "lAmPm";
			this.lAmPm.Size = new System.Drawing.Size(28, 14);
			this.lAmPm.TabIndex = 7;
			this.lAmPm.Text = " AM";
			this.lAmPm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label7MouseDown);
			// 
			// lMonth
			// 
			this.lMonth.AutoSize = true;
			this.lMonth.BackColor = System.Drawing.Color.White;
			this.lMonth.Dock = System.Windows.Forms.DockStyle.Left;
			this.lMonth.Font = new System.Drawing.Font("Bitstream Vera Sans Mono", 9F);
			this.lMonth.Location = new System.Drawing.Point(4, 20);
			this.lMonth.Name = "lMonth";
			this.lMonth.Size = new System.Drawing.Size(56, 14);
			this.lMonth.TabIndex = 0;
			this.lMonth.Text = "January";
			this.lMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lMonth.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label7MouseDown);
			// 
			// tYear
			// 
			this.tYear.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tYear.Dock = System.Windows.Forms.DockStyle.Left;
			this.tYear.Font = new System.Drawing.Font("Bitstream Vera Sans Mono", 9F);
			this.tYear.Location = new System.Drawing.Point(94, 20);
			this.tYear.Name = "tYear";
			this.tYear.Size = new System.Drawing.Size(39, 14);
			this.tYear.TabIndex = 2;
			this.tYear.Text = "2010";
			this.tYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tDate
			// 
			this.tDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tDate.Dock = System.Windows.Forms.DockStyle.Left;
			this.tDate.Font = new System.Drawing.Font("Bitstream Vera Sans Mono", 9F);
			this.tDate.Location = new System.Drawing.Point(60, 20);
			this.tDate.Name = "tDate";
			this.tDate.Size = new System.Drawing.Size(20, 14);
			this.tDate.TabIndex = 1;
			this.tDate.Text = "00";
			this.tDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// DateTimePanel
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.lAmPm);
			this.Controls.Add(this.tMin);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.tHour);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.tYear);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.tDate);
			this.Controls.Add(this.lMonth);
			this.Controls.Add(this.label2);
			this.MaximumSize = new System.Drawing.Size(270, 0);
			this.MinimumSize = new System.Drawing.Size(0, 40);
			this.Name = "DateTimePanel";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.Size = new System.Drawing.Size(255, 40);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label lAmPm;
		private System.Windows.Forms.TextBox tMin;
		private System.Windows.Forms.TextBox tHour;
		private System.Windows.Forms.TextBox tDate;
		private System.Windows.Forms.Label lMonth;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tYear;
		private System.Windows.Forms.Label label2;
		
		
		void Label7MouseDown(object sender, MouseEventArgs e)
		{
			cmMonths.Show(PointToScreen(new Point(lMonth.Left,lMonth.Bottom)));
		}
	}
}
