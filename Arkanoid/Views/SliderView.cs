using Arkanoid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows;

namespace Arkanoid.Views
{
    internal class SliderView : BlockView
    {
        public SliderView(SliderModel sliderModel)
            : base(sliderModel)
        {
            Element.RadiusX = 5;
            Element.RadiusY = 5;//TODO: fix radius

            UpdateElement();
        }
    }
}
