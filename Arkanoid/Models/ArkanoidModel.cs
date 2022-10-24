using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Arkanoid.Models
{
    internal class ArkanoidModel
    {
        private readonly List<SpriteModel> models = new();
        private readonly Canvas gameCanvas;
        private readonly Rectangle deathZone;
        private readonly int blockWidth = 50;
        private readonly int blockHeight = 15;
        private readonly double blockSpacing = 2.5;
        private readonly int nBlocks = 13;
        private readonly double radius = 10;

        public ArkanoidModel(Canvas canvas, Rectangle rect)
        {
            gameCanvas = canvas;
            deathZone = rect;

            CreateElements();
        }

        public List<SpriteModel> Models => models;
        public BallModel BallModel { get; private set; }
        public ScoreModel ScoreModel { get; } = new();
        public SliderModel SliderModel { get; private set; }
        public Canvas GameCanvas => gameCanvas;
        public Rectangle DeathZone => deathZone;
        public bool IsBallDeath
        {
            get
            {
                if (BallModel.Y + BallModel.Radius >= gameCanvas.Height - deathZone.Height)
                {
                    BallModel.IsDeath = true;
                }
                return BallModel.IsDeath;
            }
        }
        public bool IsPlaying { get; set; }
        public int Lifes { get; internal set; } = 2;

        private void AddScore(int score) => ScoreModel.Score += score;

        internal void CreateElements()
        {
            models.Clear();

            // TODO: add Method from arkanoidLevels.cs to create level
            int startPos = (((int)gameCanvas.Width - (nBlocks * blockWidth)) / 2) - (nBlocks - 1);

            for (int i = 0; i < nBlocks; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    BlockModel block = new(x: startPos + (blockWidth * i) + (blockSpacing * i), y: blockHeight + (blockHeight * j) + (blockSpacing * j), width: blockWidth, height: blockHeight, color: Colors.Red);
                    models.Add(block);
                }
            }

            BallModel = new BallModel(x: gameCanvas.Width / 2 - radius, y: gameCanvas.Height * 2 / 3 - radius, radius: radius);
            SliderModel = new SliderModel(x: gameCanvas.Width / 2 - blockWidth / 2, y: gameCanvas.Height * 0.85, width: blockWidth, height: 7.5);

            models.AddRange(collection: new List<SpriteModel> { BallModel, SliderModel });
        }

        //internal void RemoveElement(BallModel b)
        //{
        //    models.Remove(b);
        //    gameCanvas.Children.Remove(b.Ball);
        //}

        //private void RemoveElement(BlockModel bl)
        //{
        //    models.Remove(bl);
        //    gameCanvas.Children.Remove(bl.Block);
        //}

        //internal void ShowElements()
        //{
        //    gameCanvas.Children.Clear();
        //    gameCanvas.Children.Add(deathZone);

        //    foreach (SpriteModel s in models)
        //    {
        //        if (s is BallModel b)
        //        {
        //            gameCanvas.Children.Add(b.Ball);
        //        }
        //        else if (s is BlockModel bl)
        //        {
        //            gameCanvas.Children.Add(bl.Block);
        //        }
        //    }
        //}

        internal bool GameOver()
        {
            Play();

            if (!IsPlaying)
            {
                if (--Lifes == 0)
                {
                    return true;
                }
                else
                {
                    SliderModel.Reset();
                    BallModel.Reset();
                }
            }
            return false;
        }

        internal void MoveSlider(KeyEventArgs e)
        {
            SliderModel.Move(gameCanvas, e);
        }

        private void Play()
        {
            if (IsBallDeath)
            {
                IsPlaying = false;
            }
            else if (BallModel != null && SliderModel != null)
            {
                BallModel.Move(gameCanvas);

                foreach (SpriteModel s in models.ToList())
                {
                    if (s is BallModel) continue;
                    else if (s is SliderModel sl)
                    {
                        BallModel.HasHit(sl);
                        break;
                    }
                    else if (s is BlockModel bl && BallModel.HasHit(bl))
                    {
                        if (bl.IsBroken(BallModel.Damage))
                        {
                            AddScore(bl.Bonus);
                            models.Remove(bl);
                            break;
                        }
                        AddScore(bl.Score);
                        break;
                    }
                }
            }
        }

        internal void Reset()
        {
            IsPlaying = false;
            ScoreModel.Score = 0;
            Lifes = 2;
            CreateElements();
        }
    }
}
