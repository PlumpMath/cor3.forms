using System.Drawing;

/* User: oIo * Date:
 * oOo * 12/19/2007 : 7:56 PM */
namespace System.Cor3.Drawing
{

	public interface ICustomPaint : IRCustomPaint
	{
		GraphicsUnit Units {get;set;}
//		Graphics m_gfx { get; }
		Graphics G();
		Graphics G(bool cls);
//		void OnCPaint(Graphics gfx);
//		void DoPaint(Control ctl, Graphics gfx);
//		event CustomPaint CPaint;
	}
}
