using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Converters;
using System.Windows.Shapes;
using System.Xml;

namespace Arkanoid
{
    internal class Ball : Sprite
    {
        private readonly Ellipse ball;
        private double vx = 4.0;
        private double vy = 4.0;
        private double damage = 1;
        private double bounce = 0.7;

        public Ball(double x, double y, double radius)
        {
            X = x;
            Y = y;
            Radius = radius;
            //X = canvas.ActualWidth / 2 - Radius;
            //Y = canvas.ActualHeight * 2 / 3 - Radius;
            
            Width = 2 * Radius;
            Height = Width;
            IsDeath = false;

            ball = new Ellipse()
            {
                Width = Width,
                Height = Height,
                Stroke = new SolidColorBrush(Colors.Black),
                Fill = new SolidColorBrush(Colors.Black),
                Margin = new Thickness(X, Y, 0, 0)
            };

            UpdateElement();
        }

        public double Bounce { get => bounce; set => bounce = value; }
        public double Damage { get => damage; set => damage = value; }
        public double Radius { get; set; }
        public bool IsDeath { get; set; }

        public Ellipse GetBall()
        {
            return ball;
        }

        internal void Move(Canvas canvas, List<Sprite> sprites)
        {
            if (X + (2 * Radius) >= canvas.ActualWidth || X <= 0)
            {
                vx *= -1;
            }

            else if (Y + (2 * Radius) >= canvas.ActualHeight || Y <= 0)
            {
                vy *= -1;
            }

            X -= vx;
            Y -= vy;

            foreach (Sprite sprite in sprites)
            {
                if (sprite is Block b)
                {
                    HasHit(b);
                }
            }

            UpdateElement();
        }

        internal override void UpdateElement()
        {
            ball.Margin = new Thickness(X, Y, 0, 0);
            ball.Width = Width;
            ball.Height = Height;
        }

        internal void HasHit(Sprite sprite)
        {
            //if (sprite.X == X + Width && sprite.Y >= Y + Radius)
            //{
            //    vx *= -1;
            //}
            //else
            //{
                int Xc = (int)(X + Radius);
                int Yc = (int)(Y + Radius);
                int Xn = (int)Math.Max(sprite.X, Math.Min(Xc, sprite.X + sprite.Width));
                int Yn = (int)Math.Max(sprite.Y, Math.Min(Yc, sprite.Y + sprite.Height));

                int Dx = Xn - Xc;
                int Dy = Yn - Yc;

                if (Dx * Dx + Dy * Dy <= Math.Pow(Radius, 2))
                {
                    vy *= -1;
                }
            //}
            

            //if ((X + Width >= sprite.X && X + Width <= sprite.X + sprite.Width) && (Y + Height >= sprite.Y && Y + Height <= sprite.Y + sprite.Height)) vx *= -1;
        }
    }
}
