using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Arkanoid.Models
{
    internal class SliderModel : BlockModel
    {
        private int speed = 10;
        private readonly double startX;
        private readonly double startY;

        public SliderModel(double x, double y, double width, double height) : base(x, y, width, height, Colors.Black)
        {
            startX = x;
            startY = y;
        }

        public int Speed { get => speed; set => speed = value; }
        public double StartX => startX;
        public double StartY => startY;

        internal void Move(Canvas canvas, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if (X - Speed >= 0)
                    {
                        MoveLeft();
                    }
                    break;
                case Key.Right:
                    if (X + Speed <= (int)canvas.Width - Width)
                    {
                        MoveRight();
                    }
                    break;
                default:
                    break;
            }
        }

        private void MoveLeft()
        {
            X -= Speed;
        }

        private void MoveRight()
        {
            X += Speed;
        }

        internal override void Reset()
        {
            X = startX;
            Y = startY;
        }
    }
}
