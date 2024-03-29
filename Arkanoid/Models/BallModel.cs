﻿using System;
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

namespace Arkanoid.Models
{
    internal class BallModel : SpriteModel, IDeletable
    {
        private double vx = 0;
        private double vy = 4.0;
        private int damage = 1;
        private double bounce = 0.7;
        private readonly double startX;
        private readonly double startY;

        public BallModel(double x, double y, double radius)
        {
            X = x;
            Y = y;
            startX = x;
            startY = y;
            Radius = radius;
            
            Width = 2 * Radius;
            Height = Width;
            IsDeath = false;
        }

        public double Bounce { get => bounce; set => bounce = value; }
        public int Damage { get => damage; set => damage = value; }
        public bool IsDeath { get; set; }
        public double Radius { get; set; }

        public bool IsDeletable => IsDeath;

        internal bool HasHit(BlockModel bl)
        {
            int Xc = (int)(X + Radius);
            int Yc = (int)(Y + Radius);
            int Xn = (int)Math.Max(bl.X, Math.Min(Xc, bl.X + bl.Width));
            int Yn = (int)Math.Max(bl.Y, Math.Min(Yc, bl.Y + bl.Height));

            int Dx = Xn - Xc;
            int Dy = Yn - Yc;

            if (Dx * Dx + Dy * Dy <= Math.Pow(Radius, 2))
            {
                if (vx == 0)
                {
                    vx = 4;
                }
                //TODO: fix hit detection on sides
                //if (Y + Height > sprite.Y && Y + Radius / 2 < sprite.Y + sprite.Height)// && (X >= sprite.X-Width && X <= sprite.X+sprite.Width))
                //{
                //    vx *= -1;
                //}
                if (Dy == 0)
                {
                    vx *= -1;
                }
                else vy *= -1;

                return true;
            }
            else return false;

            //if ((X + Width >= sprite.X && X + Width <= sprite.X + sprite.Width) && (Y + Height >= sprite.Y && Y + Height <= sprite.Y + sprite.Height)) vx *= -1;
        }

        internal void Move(Canvas canvas)
        {
            if (X + (2 * Radius) >= canvas.Width || X <= 0)
            {
                vx *= -1;
            }
            else if (Y + (2 * Radius) >= canvas.Height || Y <= 0)
            {
                vy *= -1;
            }

            X -= vx;
            Y -= vy;
        }

        internal override void Reset()
        {
            vx = 0;
            vy *= -1;
            X = startX;
            Y = startY;
            IsDeath = false;
        }
    }
}
