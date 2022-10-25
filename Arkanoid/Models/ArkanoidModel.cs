using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Arkanoid.Models
{
    internal class ArkanoidModel
    {
        private List<BlockModel> blocks = new();
        private readonly Canvas gameCanvas;
        private readonly Rectangle deathZone;
        private readonly int blockWidth = 50;
        private readonly int blockHeight = 15;
        private readonly double blockSpacing = 2.5;
        private readonly int nBlocks = 13;
        private readonly double radius = 10;

        public ArkanoidModel() { }

        public ArkanoidModel(Canvas canvas, Rectangle rect)
        {
            gameCanvas = canvas;
            deathZone = rect;

            CreateElements();
        }

        public List<BlockModel> Blocks { get => blocks; set => blocks = value; }
        public BallModel BallModel { get; set; }
        public ScoreModel ScoreModel { get; set; } = new();
        public SliderModel SliderModel { get; set; }

        [JsonIgnore]
        public Canvas GameCanvas => gameCanvas;
        [JsonIgnore]
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
            Blocks.Clear();

            // TODO: add Method from arkanoidLevels.cs to create level
            int startPos = (((int)gameCanvas.Width - (nBlocks * blockWidth)) / 2) - (nBlocks - 1);

            for (int i = 0; i < nBlocks; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    BlockModel block = new(x: startPos + (blockWidth * i) + (blockSpacing * i), y: blockHeight + (blockHeight * j) + (blockSpacing * j), width: blockWidth, height: blockHeight, color: Colors.Red);
                    Blocks.Add(block);
                }
            }

            BallModel = new BallModel(x: gameCanvas.Width / 2 - radius, y: gameCanvas.Height * 2 / 3 - radius, radius: radius);
            SliderModel = new SliderModel(x: gameCanvas.Width / 2 - blockWidth / 2, y: gameCanvas.Height * 0.85, width: blockWidth, height: 7.5);
        }

        //internal void RemoveElement(BallModel b)
        //{
        //    models.Remove(b);
        //    gameCanvas.Children.Remove(b.Ball);
        //}

        private void RemoveElement(BlockModel bl)
        {
            Blocks.Remove(bl);
        }

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
                    ResetElements();
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

                List<SpriteModel> sprites = Blocks.Cast<SpriteModel>().ToList();
                sprites.Add(SliderModel);

                foreach (SpriteModel s in sprites)
                {
                    if (s is SliderModel sl)
                    {
                        BallModel.HasHit(sl);
                        break;
                    }
                    else if (s is BlockModel bl && BallModel.HasHit(bl))
                    {
                        if (bl.IsBroken(BallModel.Damage))
                        {
                            AddScore(bl.Bonus);
                            RemoveElement(bl);
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

        private void ResetElements()
        {
            BallModel.Reset();
            SliderModel.Reset();
        }

        internal void LoadGame()
        {
            string fileName = "data.json";
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string path = Path.Combine(docPath, fileName);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string jsonString = File.ReadAllText(path);
            ArkanoidModel arkanoidModel = JsonSerializer.Deserialize<ArkanoidModel>(jsonString, options)!;

            Blocks.Clear();
            Blocks.AddRange(arkanoidModel.Blocks);

            BallModel = arkanoidModel.BallModel;
            SliderModel = arkanoidModel.SliderModel;
            ScoreModel = arkanoidModel.ScoreModel;
            Lifes = arkanoidModel.Lifes;
        }

        internal void SaveGame()
        {
            ResetElements();

            string fileName = "data.json";
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string path = Path.Combine(docPath, fileName);
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(path, jsonString);
        }
    }
}