using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Arkanoid
{
    internal class Block : Sprite
    {
        protected Rectangle rect;
        public Block(double x, double y, double width, double height)
        {
            Width = width;
            Height = height;
            X = x;
            Y = y;

            rect = new Rectangle
            {
                Width = Width,
                Height = Height,
                Stroke = new SolidColorBrush(Colors.Black),
                Fill = new SolidColorBrush(Colors.Black),
                Margin = new Thickness(X, Y, 0, 0),
            };
        }

        public Rectangle GetBlock()
        {
            return rect;
        }
    }
}
