using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SessionViews
{
    /// <summary>
    /// Interaction logic for GameBallUserControl.xaml
    /// </summary>
    public partial class GameBallUserControl
    {
        private BallType m_type;
        private bool m_isChecked;
        public GameBallUserControl(string letter, int number)
        {
            InitializeComponent();

            m_type = BallType.None;
            m_isChecked = false;

            LetterTextBlock.Text = letter;
            ContentTextBlock.Text = number.ToString();
        }

        public event EventHandler<RoutedEventArgs> Click;

        public enum BallType
        {
            None,
            Game,
            Bonus
        }

        public void SelectGameBall()
        {
            if (!m_isChecked)
            {
                GameBallButtonBorder.Background = (Brush)Resources["GameBallBackground"];
                LetterTextBlock.Foreground = Brushes.White;
                ContentTextBlock.Foreground = Brushes.White;

                m_isChecked = true;
                m_type = BallType.Game;
            }
        }

        public void SelectBonusBall()
        {
            if (!m_isChecked)
            {
                GameBallButtonBorder.Background = (Brush)Resources["BonusBallBackGround"];
                LetterTextBlock.Foreground = Brushes.White;
                ContentTextBlock.Foreground = Brushes.White;

                m_isChecked = true;
                m_type = BallType.Bonus;
            }
        }

        public void DeselectBall()
        {
            if (m_isChecked)
            {
                GameBallButtonBorder.Background = Brushes.White;
                LetterTextBlock.Foreground = Brushes.Black;
                ContentTextBlock.Foreground = Brushes.Black;

                m_isChecked = false;
                m_type = BallType.None;
            }
        }

        public bool IsChecked
        {
            get { return m_isChecked; }
        }

        public BallType Type
        {
            get { return m_type; }
        }

        private void GameBallButtonBorder_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var handler = Click;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
