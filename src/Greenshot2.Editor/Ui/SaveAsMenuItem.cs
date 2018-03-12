//  Greenshot - a free and open source screenshot tool
//  Copyright (C) 2007-2018 Thomas Braun, Jens Klingen, Robin Krom
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

using System.ComponentModel.Composition;
using Dapplo.CaliburnMicro.Extensions;
using Dapplo.CaliburnMicro.Menu;
using Dapplo.Utils.Extensions;
using Greenshot2.Editor.Translations;
using MahApps.Metro.IconPacks;

namespace Greenshot2.Editor.Ui
{
    /// <summary>
    ///     This will add the File item to menu
    /// </summary>
    [Export("editormenu", typeof(IMenuItem))]
    public sealed class SaveAsMenuItem : MenuItem
    {
        private readonly IEditorTranslations _editorTranslations;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editorTranslations"></param>
        [ImportingConstructor]
        public SaveAsMenuItem(IEditorTranslations editorTranslations)
        {
            _editorTranslations = editorTranslations;
        }

        /// <inheritdoc />
        public override void Initialize()
        {
            Id = "A_SaveAs";
            ParentId = EditorKnownMenuItems.File.EnumValueOf();
            // automatically update the DisplayName
            _editorTranslations.CreateDisplayNameBinding(this, nameof(IEditorTranslations.SaveAs));
            Icon = new PackIconMaterial
            {
                Kind = PackIconMaterialKind.ContentSave
            };
        }
    }
}
