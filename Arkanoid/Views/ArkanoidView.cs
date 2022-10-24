using Arkanoid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Arkanoid.Views
{
    internal class ArkanoidView
    {
        private readonly List<SpriteView> views = new();

        public ArkanoidView(ArkanoidModel arkanoidModel) 
        {
            ArkanoidModel = arkanoidModel;
            AddViews();
        }

        public ArkanoidModel ArkanoidModel { get; set; }

        private void AddBall(BallModel ballModel)
        {
            BallView ballView = new(ballModel);
            views.Add(ballView);
            ArkanoidModel.GameCanvas.Children.Add(ballView.Element);

        }

        private void AddSlider(SliderModel sliderModel)
        {
            SliderView sliderView = new(sliderModel);
            views.Add(sliderView);
            ArkanoidModel.GameCanvas.Children.Add(sliderView.Element);;
        }

        private void AddViews()
        {
            foreach (SpriteModel spriteModel in ArkanoidModel.Models)
            {
                
                if (spriteModel.GetType() == typeof(BlockModel))
                {
                    BlockView blockView = new((BlockModel)spriteModel);
                    views.Add(blockView);
                    ArkanoidModel.GameCanvas.Children.Add(blockView.Element);
                }
                else if (spriteModel is BallModel ballModel)
                {
                    AddBall(ballModel);
                }
                else if (spriteModel is SliderModel sliderModel)
                {
                    AddSlider(sliderModel);
                }
            }
        }

        public void Update()
        {
            foreach (SpriteView spriteView in views.ToList())
            {
                spriteView.UpdateElement();
                if (spriteView.Model.IsDeletable)
                {
                    ArkanoidModel.GameCanvas.Children.Remove(spriteView.Element);
                    views.Remove(spriteView);
                }
            }
        }

        internal void Reset()
        {
            ArkanoidModel.GameCanvas.Children.Clear();
            ArkanoidModel.GameCanvas.Children.Add(ArkanoidModel.DeathZone);
            views.Clear();
            AddViews();
        }
    }
}
