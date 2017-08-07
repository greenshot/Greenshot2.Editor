//  Greenshot - a free and open source screenshot tool
//  Copyright (C) 2007-2017 Thomas Braun, Jens Klingen, Robin Krom
// 
//  For more information see: http://getgreenshot.org/
//  The Greenshot project is hosted on GitHub: https://github.com/greenshot
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 1 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Reactive.Disposables;
using Caliburn.Micro;
using Dapplo.CaliburnMicro;
using Dapplo.CaliburnMicro.Dapp;
using Dapplo.CaliburnMicro.Extensions;
using Dapplo.CaliburnMicro.Menu;
using Greenshot2.Editor.Translations;
using System.Linq;

namespace Greenshot2.Editor.Ui.ViewModels
{
    /// <summary>
    /// This is the actual editor view model
    /// </summary>
    [Export]
    public class EditorViewModel : Conductor<Screen>.Collection.OneActive, IMaintainPosition
    {
        private readonly IEnumerable<Lazy<IMenuItem>> _menuItems;

        /// <summary>
        /// The translations for the editor, used internally and in the view
        /// </summary>
        public IEditorTranslations EditorTranslations { get; }

        /// <summary>
        ///     Here all disposables are registered, so we can clean the up
        /// </summary>
        private CompositeDisposable _disposables;

        /// <summary>
        /// The menu items for the view
        /// </summary>
        // ReSharper disable once CollectionNeverQueried.Global
        public ObservableCollection<ITreeNode<IMenuItem>> MenuItems { get; } = new ObservableCollection<ITreeNode<IMenuItem>>();

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="canvas">CanvasViewModel</param>
        /// <param name="menuItems">The menu items</param>
        /// <param name="editorTranslations">IEditorTranslations for the translations</param>
        [ImportingConstructor]
        public EditorViewModel(CanvasViewModel canvas,
            [ImportMany("editormenu", typeof(IMenuItem))]
            IEnumerable<Lazy<IMenuItem>> menuItems,
            IEditorTranslations editorTranslations
            )
        {
            _menuItems = menuItems;
            EditorTranslations = editorTranslations;
            Items.Add(canvas);
            ActiveItem = canvas;
        }

        /// <inheritdoc />
        protected override void OnActivate()
        {
            // Prepare disposables
            _disposables?.Dispose();
            _disposables = new CompositeDisposable();

            // Remove all items, so we can build them
            Items.Clear();

            var contextMenuNameBinding = EditorTranslations.CreateDisplayNameBinding(this, nameof(IEditorTranslations.Title));

            // Make sure the contextMenuNameBinding is disposed when this is no longer active
            _disposables.Add(contextMenuNameBinding);

            var items = _menuItems.Select(x => x.Value).ToList();
            var fileMenuItem = new MenuItem
            {
                Id = "1_File"
            };
            var menuNameBinding = EditorTranslations.CreateDisplayNameBinding(fileMenuItem, nameof(IEditorTranslations.File));
            // Make sure the menuNameBinding is disposed when this is no longer active
            _disposables.Add(menuNameBinding);
            items.Add(fileMenuItem);

            var editMenuItem = new MenuItem
            {
                Id = "2_Edit"
            };
            menuNameBinding.AddDisplayNameBinding(editMenuItem, nameof(IEditorTranslations.Edit));
            items.Add(editMenuItem);

            var aboutMenuItem = new MenuItem
            {
                Id = "3_About"
            };
            menuNameBinding.AddDisplayNameBinding(aboutMenuItem, nameof(IEditorTranslations.About));
            items.Add(aboutMenuItem);

            items.Add(new MenuItem
            {
                Style = MenuItemStyles.Separator,
                Id = "Y_Separator",
                ParentId = "1_File"
            });

            var exitMenuItem = new ClickableMenuItem
            {
                Id = "Z_Exit",
                ParentId = "1_File",
                ClickAction = clickedMenuItem => Dapplication.Current.Shutdown()
            };
            contextMenuNameBinding.AddDisplayNameBinding(exitMenuItem, nameof(IEditorTranslations.Exit));
            items.Add(exitMenuItem);

            // Make sure all items are initialized
            foreach (var menuItem in items)
            {
                menuItem.Initialize();
            }
            // Add the root elements to the Items
            foreach (var item in items.CreateTree())
            {
                MenuItems.Add(item);
            }

            base.OnActivate();
        }
    }
}
