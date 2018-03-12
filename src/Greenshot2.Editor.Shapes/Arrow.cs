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

using System;
using System.Windows;
using System.Windows.Media;

namespace Greenshot2.Editor.Shapes
{
	/// <summary>
	/// This defines the Arrow Shape which is visible inside the editor
	/// </summary>
	public class Arrow : Line
	{
		internal override void CacheDefiningGeometry()
		{
			Point startPoint = new Point(X1, Y1);
			Point endPoint = new Point(X2, Y2);

			var length = Math.Sqrt(Math.Pow(endPoint.Y - startPoint.Y, 2) + Math.Pow(endPoint.X - startPoint.X, 2));
			var f = length / 15d;
			var f2 = length / 30d;

			var arrowBase = new Point(endPoint.X - (endPoint.X - startPoint.X) / f2, endPoint.Y - (endPoint.Y - startPoint.Y) / f2);
			var arrowTip1 = new Point(arrowBase.X + (endPoint.Y - startPoint.Y) / f, arrowBase.Y - (endPoint.X - startPoint.X) / f);
			var arrowTip2 = new Point(arrowBase.X - (endPoint.Y - startPoint.Y) / f, arrowBase.Y + (endPoint.X - startPoint.X) / f);
			var arrowTipExt1 = new Point(arrowBase.X + (endPoint.Y - startPoint.Y) / f2, arrowBase.Y - (endPoint.X - startPoint.X) / f2);
			var arrowTipExt2 = new Point(arrowBase.X - (endPoint.Y - startPoint.Y) / f2, arrowBase.Y + (endPoint.X - startPoint.X) / f2);

			// Create PathFigure with start-point
			var arrow = new PathFigure(startPoint, new[] { new PolyLineSegment(new[] { arrowTip1, arrowTipExt1, endPoint, arrowTipExt2, arrowTip2 }, true), }, true)
			{
				IsFilled = true
			};
			CachedGeometry = new PathGeometry(new[] { arrow }, FillRule.Nonzero, null);
		}
	}
}
