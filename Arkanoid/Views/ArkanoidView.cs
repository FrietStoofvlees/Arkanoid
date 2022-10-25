using Arkanoid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Arkanoid.Views
{
    internal class ArkanoidView
    {
        private readonly List<SpriteView> views = new();

        public ArkanoidView(ArkanoidModel arkanoidModel) 
        {
            ArkanoidModel = arkanoidModel;
            CreateLifes();
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
            foreach (SpriteModel spriteModel in ArkanoidModel.Blocks)
            {
                if (spriteModel.GetType() == typeof(BlockModel))
                {
                    BlockView blockView = new((BlockModel)spriteModel);
                    views.Add(blockView);
                    ArkanoidModel.GameCanvas.Children.Add(blockView.Element);
                }
            }
            AddBall(ArkanoidModel.BallModel);
            AddSlider(ArkanoidModel.SliderModel);
        }

        private void CreateLifes()
        {
            foreach (Image image in ArkanoidModel.GameCanvas.Children.OfType<Image>().ToList())
            {
                ArkanoidModel.GameCanvas.Children.Remove(image);
            }
            for (int i = 0; i < ArkanoidModel.Lifes; i++)
            {
                Image heartImage = new()
                {
                    Width = 20,
                    Height = 18,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(10, 10+22*i, 0, 0),
                };

                BitmapImage bitmap = new();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(@"Assets/heart.png", UriKind.Relative);
                bitmap.EndInit();

                heartImage.Source = bitmap;

                ArkanoidModel.GameCanvas.Children.Add(heartImage);
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
            CreateLifes();
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
