using Logic;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System.Threading.Tasks;

namespace View
{
    public partial class MainWindow : Window
    {
        private Simulator simulator;

        public MainWindow()
        {
            InitializeComponent();
            simulator = new Simulator();

            simulator.AddBall(50, 50);
            simulator.AddBall(100, 100);

            StartSimulation();
        }

        private void DrawBalls()
        {
            DrawingCanvas.Children.Clear();

            foreach (var ball in simulator.GetAllBalls())
            {
                Ellipse ellipse = new Ellipse
                {
                    Width = 20,
                    Height = 20,
                    Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0))
                };

                Canvas.SetLeft(ellipse, ball.PosX);
                Canvas.SetTop(ellipse, ball.PosY);
                DrawingCanvas.Children.Add(ellipse);
            }
        }

        private async void StartSimulation()
        {
            while (true)
            {
                simulator.SimulateBalls();
                DrawBalls();
                await Task.Delay(100);
            }
        }
    }
}
