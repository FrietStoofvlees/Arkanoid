using Arkanoid.Models;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Arkanoid
{
    /// <summary>
    /// Interaction logic for ArkanoidXAML.xaml
    /// </summary>
    public partial class ArkanoidXAML : Window
    {
        private readonly ArkanoidModel arkanoidModel;

        private DispatcherTimer gameTimer;
        private Stopwatch stopwatch;
        private int fontIncr = 5;

        public ArkanoidXAML()
        {
            InitializeComponent();
            arkanoidModel = new(gameCanvas, deathZone);
            lblScore.DataContext = arkanoidModel.ScoreModel;
            lblBestScore.DataContext = arkanoidModel.ScoreModel;
            InitGame();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (arkanoidModel.IsPlaying)
            {
                if (arkanoidModel.GameOver()) EndGame();
            }
            else
            {
                if (!stopwatch.IsRunning)
                {
                    stopwatch.Start();
                    txtAnimation.BeginAnimation(TextBlock.FontSizeProperty, new DoubleAnimation() {
                        By = fontIncr,
                        Duration = TimeSpan.FromSeconds(2.5),
                        FillBehavior = FillBehavior.HoldEnd
                    });
                    fontIncr *= -1;
                }
                else if (stopwatch.Elapsed.TotalSeconds >= 2.5)
                {
                    stopwatch.Reset();
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!arkanoidModel.IsPlaying)
            {
                switch (e.Key)
                {
                    case Key.Space:
                        StartGame();
                        break;
                    case Key.Left:
                    case Key.Right:
                        StartGame();
                        arkanoidModel.MoveSlider(e);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                arkanoidModel.MoveSlider(e);
            }
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            EndGame();
            GameMenuXAML gameMenuXAML = new();
            gameMenuXAML.Show();
            //TODO: switching menu's in same window
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
        }

        private void InitGame()
        {
            gameTimer = new(DispatcherPriority.Normal);
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Interval = TimeSpan.FromMilliseconds(25);

            stopwatch = new();

            gameTimer.Start();
            gameCanvas.Focus();
        }

        private void StartGame()
        {
            arkanoidModel.IsPlaying = true;
            txtAnimation.Visibility = Visibility.Collapsed;
            stopwatch.Reset();
        }

        private void EndGame()
        {
            gameTimer.Stop();
        }

        private void ResetGame()
        {
            arkanoidModel.Reset();
            txtAnimation.Visibility = Visibility.Visible;

            gameTimer.Start();
            gameCanvas.Focus();
        }
    }
}
