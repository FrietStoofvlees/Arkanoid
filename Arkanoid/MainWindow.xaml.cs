using System;
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
        private List<Sprite> sprites = new List<Sprite>();

        private bool isPlaying;

        private readonly int canvasWidth = 770;
        private readonly int canvasHeight = 400;
        private readonly int blockWidth = 50;
        private readonly int blockHeight = 15;
        private readonly double blockSpacing = 2.5;
        private readonly int nBlocks = 13;
        private readonly double radius = 10;

        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                if (ball != null && slider != null)
                {
                    ball.Move(gameCanvas, sprites);
                }
                //else
                //{
                //    ball = new Ball(gameCanvas.ActualWidth / 2 - radius, gameCanvas.ActualHeight * 2 / 3 - radius, radius);
                //    slider = new Slider(gameCanvas.ActualWidth / 2 - blockWidth / 2, gameCanvas.ActualHeight * 3 / 4, blockWidth, 7.5);
                //    sprites.AddRange(collection: new List<Sprite> { ball, slider });
                //    ShowElements();
                //}
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
            gameTimer.Stop();
            NewGame();
        }

        private void NewGame()
        {
            isPlaying = false;
            sprites.Clear();
            gameCanvas.Children.Clear();

            gameTimer = new DispatcherTimer(DispatcherPriority.Normal);
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Interval = TimeSpan.FromMilliseconds(25);
            gameTimer.Start();

            // -- TODO: make method for levels, difficulty enum?
            int startPos = ((canvasWidth - (nBlocks * blockWidth)) / 2) - (nBlocks - 1);
            
            for (int i = 0; i < nBlocks; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Block block = new(x: startPos + (blockWidth * i) + (blockSpacing * i), blockHeight + (blockHeight * j) + (blockSpacing * j) , blockWidth, blockHeight, Colors.Red);
                    sprites.Add(block);
                }
            }
            // --

            ball = new Ball(canvasWidth / 2 - radius, canvasHeight * 2 / 3 - radius, radius);
            slider = new Slider(canvasWidth / 2 - blockWidth / 2, canvasHeight * 3 / 4, blockWidth, 7.5);
            sprites.AddRange(collection: new List<Sprite> { ball, slider });

            ShowElements();

            gameCanvas.Focus();
        }

        private void ShowElements()
        {
            gameCanvas.Children.Clear();

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
