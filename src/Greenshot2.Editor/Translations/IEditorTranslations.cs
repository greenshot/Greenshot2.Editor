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

using System.ComponentModel;
using Dapplo.Language;

namespace Greenshot2.Editor.Translations
{
    /// <summary>
    /// Translations for the editor
    /// </summary>
    [Language("Editor")]
    public interface IEditorTranslations : ILanguage, INotifyPropertyChanged
    {
        /// <summary>
        /// The title of the editor
        /// </summary>
        [DefaultValue("Editor")]
        string Title { get; }

        /// <summary>
        /// The about menu item
        /// </summary>
        [DefaultValue("About")]
        string About { get; }

        /// <summary>
        /// The edit menu item
        /// </summary>
        [DefaultValue("Edit")]
        string Edit { get; }
 
        /// <summary>
        /// The file menu item
        /// </summary>
        [DefaultValue("File")]
        string File { get; }

        /// <summary>
        /// The save-as menu item
        /// </summary>
        [DefaultValue("Save as")]
        string SaveAs { get; }

        /// <summary>
        /// The exit menu item
        /// </summary>
        [DefaultValue("Exit")]
        string Exit { get; }
    }
}
