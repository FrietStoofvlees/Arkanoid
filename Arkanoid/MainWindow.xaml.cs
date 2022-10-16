﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Arkanoid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Ball ball;
        private Slider slider;
        private DispatcherTimer gameTimer;
        private readonly List<Sprite> sprites = new();

        private bool isPlaying;
        private readonly int blockWidth = 50;
        private readonly int blockHeight = 15;
        private readonly double blockSpacing = 2.5;
        private readonly int nBlocks = 13;
        private readonly double radius = 10;
        private int lifes = 2;

        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                if (CheckBall())
                {
                    GameOver();
                    ShowElements();

                }
                else if (ball != null && slider != null)
                {
                    ball.Move(gameCanvas, sprites);
                }
            }
            else
            {
                // TODO: add press space to play animation?
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isPlaying)
            {
                switch (e.Key) 
                {
                    case Key.Space:
                        isPlaying = true;
                        break;
                    case Key.Left:
                    case Key.Right:
                        isPlaying = true;
                        slider.Move(gameCanvas, e);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                 slider.Move(gameCanvas, e);
            }
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            //gameTimer.Stop();
            NewGame();
        }

        private bool CheckBall()
        {
            if (ball.Y + ball.Radius >= gameCanvas.Height-deathZone.ActualHeight)
            {
                ball.IsDeath = true;
            }
            return ball.IsDeath;
        }

        private void CreateElements()
        {
            sprites.Clear();

            // TODO: add Method from arkanoidLevels.cs to create level
            int startPos = (((int)gameCanvas.Width - (nBlocks * blockWidth)) / 2) - (nBlocks - 1);

            for (int i = 0; i < nBlocks; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Block block = new(x: startPos + (blockWidth * i) + (blockSpacing * i), y: blockHeight + (blockHeight * j) + (blockSpacing * j), width: blockWidth, height: blockHeight, color: Colors.Red);
                    sprites.Add(block);
                }
            }

            ball = new Ball(x: gameCanvas.Width / 2 - radius, y: gameCanvas.Height * 2 / 3 - radius, radius: radius);
            slider = new Slider(x: gameCanvas.Width / 2 - blockWidth / 2, y: gameCanvas.Height * 0.85, width: blockWidth, height: 7.5);

            sprites.AddRange(collection: new List<Sprite> { ball, slider });
        }

        private void NewGame()
        {
            isPlaying = false;
            CreateElements();
            ShowElements();

            if (gameTimer == null)
            {
                gameTimer = new DispatcherTimer(DispatcherPriority.Normal);
                gameTimer.Tick += GameTimer_Tick;
                gameTimer.Interval = TimeSpan.FromMilliseconds(25);
            }
            gameTimer.Start();
            gameCanvas.Focus();
        }

        private void GameOver()
        {
            if (ball.IsDeath)
            {
                isPlaying = false;
                lifes--;

                if (lifes == 0)
                {
                    //NewGame(); // TODO: End game
                }
                else
                {
                    slider.Reset();
                    ball.Reset();
                }
            }
        }

        private void ShowElements()
        {
            gameCanvas.Children.Clear();
            gameCanvas.Children.Add(deathZone);

            foreach (Sprite sprite in sprites)
            {
                if (sprite is Ball b)
                {
                    gameCanvas.Children.Add(b.GetBall());
                }
                else if (sprite is Block bl)
                {
                    gameCanvas.Children.Add(bl.GetBlock());
                }
            }
        }
    }
}
