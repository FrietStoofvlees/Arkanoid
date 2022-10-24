using Arkanoid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Arkanoid.Views
{
    internal class BlockView : SpriteView
    {
        private readonly Rectangle rect;

        public BlockView(BlockModel blockModel)
        {
            Model = blockModel;

            rect = new Rectangle
            {
                Width = Model.Width,
                Height = Model.Height,
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 3.5,
                Fill = new SolidColorBrush(Model.Color),
                Margin = new Thickness(Model.X, Model.Y, 0, 0),
            };
        }

        public override BlockModel Model { get; }
        public override Rectangle Element => rect;

        internal override void UpdateElement()
        {
            rect.Margin = new Thickness(Model.X, Model.Y, 0, 0);
            rect.Width = Model.Width;
            rect.Height = Model.Height;
        }
    }
}
