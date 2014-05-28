/* User: oIo * Date: 3/9/2010 * Time: 2:13 AM */
using System;
using System.IO;
using System.Windows.Forms;

using efx.Forms.Managers;

namespace EmptyDemo2
{
	public class DirComboManager : ObjectManager<ComboBox>
	{
		DirectoryInfo DirLoaded;
		DirectoryInfo DirLast;
		string ParDir { get { return DirLoaded.Parent.FullName; } }
		string CurDir { get { return Client.Text; } }
		void eTextChanged(object sender, EventArgs e)
		{
			if (Directory.Exists(CurDir))
			{
				DirLast = DirLoaded;
				if (CurDir.IndexOf("..")==0) {
					if (DirLoaded.Root!=DirLoaded) DirLoaded = DirLast.Parent;
					else return;
				}
				else DirLoaded = new DirectoryInfo(Client.Text);
				Reset();
			}
		}
		void Reset()
		{
			Client.Items.Clear(); // nope, didn't quite much but quirk
			Client.Items.Add(string.Concat(@"..\",DirLoaded.Parent.Name));
			foreach (DirectoryInfo p in DirLoaded.GetDirectories())
			{
				Client.Items.Add(p.FullName);
			}
			Client.Text = DirLoaded.FullName;
		}
		public override void AddEvents()
		{
			base.AddEvents();
			Client.TextChanged += eTextChanged;
		}
		public DirComboManager(ComboBox cbo) : base(cbo)
		{
			
		}
	}
}
