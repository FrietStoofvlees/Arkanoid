using Arkanoid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Views
{
    internal abstract class SpriteView
    {
        public virtual dynamic Model { get; }
        public virtual dynamic Element { get; }
        internal virtual void UpdateElement() { } 
    }
}
