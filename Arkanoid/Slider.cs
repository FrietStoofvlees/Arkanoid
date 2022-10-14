﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Arkanoid
{
    internal class Slider : Block
    {
        private int speed = 10;

		public Slider(double x, double y, double width, double height) : base(x, y, width, height)
		{
            rect.RadiusX = 5;
            rect.RadiusY = 5;

            UpdateElement();
		}

        public int Speed { get => speed; set => speed = value; }

        internal void Move(Canvas canvas, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if (X >= 0)
                    {
                        MoveLeft();
                    }
                    break;
                case Key.Right:
                    if (X <= ((int)canvas.ActualWidth - Width))
                    {
                        MoveRight();
                    }
                    break;
                default:
                    break;
            }

            UpdateElement();

        }

        private void MoveLeft()
        {
            X -= Speed;
        }

        private void MoveRight()
        {
            X += Speed;
        }

        internal override void UpdateElement()
        {
            rect.Margin = new Thickness(X, Y, 0, 0);
            rect.Width = Width;
            rect.Height = Height;
        }
    }
}