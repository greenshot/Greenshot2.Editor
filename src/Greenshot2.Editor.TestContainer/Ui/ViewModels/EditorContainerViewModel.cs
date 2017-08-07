using System.ComponentModel.Composition;
using Caliburn.Micro;
using Dapplo.CaliburnMicro;
using Dapplo.CaliburnMicro.Extensions;
using Greenshot2.Editor.Ui.ViewModels;

namespace Greenshot2.Editor.TestContainer.Ui.ViewModels
{
    /// <summary>
    /// Wrapper to open an editor automatically
    /// </summary>
    [Export(typeof(IShell))]
    public class EditorContainerViewModel : Conductor<Screen>.Collection.OneActive, IShell
    {
        [ImportingConstructor]
        public EditorContainerViewModel(EditorViewModel editorViewModel)
        {
            ActiveItem = editorViewModel;
            editorViewModel.CreateDisplayNameBinding(this, nameof(editorViewModel.DisplayName));
        }
    }
}
