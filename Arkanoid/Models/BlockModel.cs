using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Arkanoid.Models
{
    internal class BlockModel : SpriteModel, IDeletable
    {
        private int health = 1;

        public BlockModel(double x, double y, double width, double height, Color color)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Color = color;
            IsDeletable = false;
        }

        public int Bonus { get; } = 50;
        public Color Color { get; set; }
        public int Score { get; } = 10;

        public bool IsDeletable { get; private set; }

        internal bool IsBroken(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                IsDeletable = true;
                return true;
            }
            return false;
        }
    }
}
