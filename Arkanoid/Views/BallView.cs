using Arkanoid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Arkanoid.Views
{
    internal class BallView : SpriteView
    {
        private readonly Ellipse ball;

        public BallView(BallModel ballModel)
        {
            Model = ballModel;

            ball = new Ellipse()
            {
                Width = Model.Width,
                Height = Model.Height,
                Stroke = new SolidColorBrush(Colors.Black),
                Fill = new SolidColorBrush(Colors.Black),
                Margin = new Thickness(Model.X, Model.Y, 0, 0)
            };

            UpdateElement();
        }

        public override BallModel Model { get; }
        public override Ellipse Element => ball;

        internal override void UpdateElement()
        {
            ball.Margin = new Thickness(Model.X, Model.Y, 0, 0);
            ball.Width = Model.Width;
            ball.Height = Model.Height;
        }
    }
}
