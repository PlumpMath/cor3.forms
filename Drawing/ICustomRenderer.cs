using System.Drawing;
using System.Windows.Forms;

/* User: oIo * Date: 9/19/2010 * Time: 8:41 PM */
namespace System.Cor3.Drawing
{
	public interface ICustomRenderer: IRCustomPaint
	{
	//		GraphicsUnit Units {get;set;}
	//		Graphics m_gfx { get; }
		Graphics G(Control ctl);
		Graphics G(Control ctl,bool cls);
	}
}
