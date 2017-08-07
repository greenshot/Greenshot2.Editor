using System.ComponentModel.Composition;
using Caliburn.Micro;
using Dapplo.CaliburnMicro;
using Greenshot2.Editor.ViewModels;

namespace Greenshot2.Editor.TestContainer.Ui.ViewModels
{
    [Export(typeof(IShell))]
    public class EditorViewModel : Conductor<Screen>.Collection.OneActive, IShell
    {
        [ImportingConstructor]
        public EditorViewModel(CanvasViewModel canvas)
        {
            Items.Add(canvas);
            ActiveItem = canvas;
        }
    }
}
