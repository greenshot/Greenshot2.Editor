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

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using Caliburn.Micro;
using Dapplo.CaliburnMicro;
using Dapplo.CaliburnMicro.Extensions;
using Google.Protobuf;
using Greenshot2.Editor.FileFormat;
using Greenshot2.Editor.Ui.ViewModels;

namespace Greenshot2.Editor.TestContainer.Ui.ViewModels
{
    /// <summary>
    /// Wrapper to open an editor automatically
    /// </summary>
    [Export(typeof(IShell))]
    public class EditorContainerViewModel : Conductor<EditorViewModel>.Collection.OneActive, IShell
    {
        [ImportingConstructor]
        public EditorContainerViewModel(EditorViewModel editorViewModel)
        {
            ActiveItem = editorViewModel;

            ByteString backgroundImage;
            using (var fileStream = File.OpenRead(@"D:\Projects\Google Photos.png"))
            {
                backgroundImage = ByteString.FromStream(fileStream);
            }
            
            var drawing = new Drawing
            {
                Title = "Test drawing",
                Resources = {
                    { "Background", new Resource
                        {
                            ResourceType = Resource.Types.ResourceTypes.Png,
                            Data = backgroundImage
                        }
                    }
                },
                Layers =
                {
                    new Layer
                    {
                        Shapes =
                        {
                            new List<Shape>
                            {
                                new Shape
                                {
                                    ShapeTypes = ShapeTypes.Image,
                                    Image = new ImageShape {
                                        ResourceId = "Background"
                                    }
                                },
                                new Shape
                                {
                                    ShapeTypes = ShapeTypes.Rectangle,
                                    Rectangle = new RectangleShape
                                    {
                                        Bounds = new Bounds
                                        {
                                            Location = new Point
                                            {
                                                X = 10, Y = 10
                                            },
                                            Size = new Size
                                            {
                                                Height = 100, Width = 100
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            editorViewModel.ActivateItem(new CanvasViewModel
            {
                Drawing = drawing
            });
            editorViewModel.CreateDisplayNameBinding(this, nameof(editorViewModel.DisplayName));
        }
    }
}
