using System.Drawing;
using System.Windows.Forms;

/* User: oIo * Date: 9/19/2010 * Time: 8:41 PM */
namespace System.Cor3.Drawing
{

	public delegate void BufferedPaintEvent(Graphics gfx);
	public delegate void CustomPaint(Control ctl, Graphics gfx);

	/// <summary>takes some sting out of ui drawing in a base class or two</summary>
	public interface IRCustomPaint
	{
		Graphics m_gfx { get; }
		void OnCPaint(Graphics gfx);
		void DoPaint(Control ctl, Graphics gfx);
		event CustomPaint CPaint;
	}
}
