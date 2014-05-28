/* User: oIo * Date: 3/9/2010 * Time: 2:13 AM */
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using fam3;

namespace System.Cor3.Forms
{
	/// <summary>
	/// The simple » “ToolStripSplitButton File-System Browser”
	/// </summary>
	public class ts_dirman : object_manager<ToolStripSplitButton>
	{
		
		#region utils
		
		static int SortByFullName(DirectoryInfo di1, DirectoryInfo di2){
			return di1.FullName.CompareTo(di2.FullName);
		}
		
		static DirectoryInfo DirectoryInfoFromTag(object o) { return (o as ToolStripItem).Tag as DirectoryInfo; }
		
		#endregion
		
		public event EventHandler DirectoryChanged;
		
		DirectoryInfo DirSelected;
		
		public string PathSelected {
			set { DirSelected = new DirectoryInfo(value); Populate(); }
		}
		
		void eScroll(object sender, MouseEventArgs e)
		{
			Client.Owner.FindForm().Text = "ES1: ‘ME’";
		}
		
		void eScroll(object sender, EventArgs e)
		{
			Client.Owner.FindForm().Text = "ES2: ‘E’";
		}
		
		const string str_driveready = "{0} [{1}] “{2}”\rType: {3}, Available: {4}\rFree/Available: {5}/{6}";
		const string str_driveN = @"{0}, Not Available";
		
		public string GetDriveToolTip(DriveInfo drive)
		{
			if (drive.IsReady) return string.Format(
				str_driveready,
				drive.Name, drive.DriveFormat, drive.RootDirectory.Name,
				drive.DriveType.ToString(),drive.IsReady,
				drive.AvailableFreeSpace,drive.TotalSize
			);
			return string.Format(str_driveN, drive.Name);
		}
		
		ToolStripMenuItem CreateDriveItem(DriveInfo dri)
		{
			ToolStripMenuItem tsm = new ToolStripMenuItem(dri.Name);
			tsm.ToolTipText = GetDriveToolTip(dri);
			if (dri.IsReady) { tsm.Tag = dri.RootDirectory; tsm.Click += eDirClick; }
			return tsm;
		}
		
		ToolStripMenuItem GetDrives()
		{
			ToolStripMenuItem tsmi = new ToolStripMenuItem("Drives");
			tsmi.Image = fam3.famfam_silky.drive;
			foreach (DriveInfo d in DriveInfo.GetDrives())
			{
				tsmi.DropDownItems.Add(CreateDriveItem(d));
			}
			return tsmi;
		}
		
		void Populate()
		{
			Client.DropDownItems.Clear();
			Client.Tag = DirSelected;
			Client.Text = DirSelected.Name;
			Client.DropDownItems.Add(GetDrives());
			
			if (DirSelected.Parent!=null)
			{
				Client.DropDownItems.Add("..",null,eDirClick)
					.Tag = DirSelected.Parent;
			}
			
			List<DirectoryInfo> dis = new List<DirectoryInfo>(DirSelected.GetDirectories());
			dis.Sort(SortByFullName);
			
			foreach (DirectoryInfo di in dis)
			{
				Client.DropDownItems.Add(di.Name,null,eDirClick)
					.Tag = di;
			}
		}
		
		void eBtnClick(object sender, EventArgs e)
		{
			PathSelected = DirectoryInfoFromTag(sender).FullName;
		}
		void eDirClick(object sender, EventArgs e)
		{
			PathSelected = DirectoryInfoFromTag(sender).FullName;
			DirectoryChanged(sender,e);
		}
		
		public override void AddEvents()
		{
			base.AddEvents();
//			Client..MouseWheel += eScroll;
			Client.ButtonClick += eDirClick;
		}
		
		public ts_dirman(ToolStripSplitButton ts, EventHandler edc) : base(ts)
		{
			PathSelected = @"d:\";
			DirectoryChanged = edc;
		}
	}
}
