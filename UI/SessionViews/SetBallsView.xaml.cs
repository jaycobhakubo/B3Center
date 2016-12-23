#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2015 GameTech International, Inc.
#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GameTech.Elite.Client.Modules.B3Center.Business;
using GameTech.Elite.Client.Modules.B3Center.ViewModels;

namespace GameTech.Elite.Client.Modules.B3Center.UI.SessionViews
{
    /// <summary>
    /// Interaction logic for SetBallsView.xaml
    /// </summary>
    public partial class SetBallsView
    {
        #region Local Variables

        private readonly Dictionary<GameBallUserControl, int> m_allGameNumbers;
        private readonly List<int> m_selectedGameNumbers;
        private readonly List<int> m_selectedBonusNumbers;
        private List<int> m_originalSelectedGameNumbers;
        private List<int> m_originalSelectedBonusNumbers;
        private readonly List<int> m_randomlist;

        private const int TotalNumberOfBalls = 75;
        private readonly int m_maxNumberOfGamePicks;
        private readonly int m_maxNumberOfBonusPicks;
        private readonly bool m_enforceMix;
        private bool m_isBonusBalls;
        private bool m_isInEditMode;

        private int m_bCounter;
        private int m_iCounter;
        private int m_nCounter;
        private int m_gCounter;
        private int m_oCounter;

        private int m_bBonusCounter;
        private int m_iBonusCounter;
        private int m_nBonusCounter;
        private int m_gBonusCounter;
        private int m_oBonusCounter;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SetBallsView"/> class.
        /// </summary>
        public SetBallsView()
        {
            InitializeComponent();
            m_isInEditMode = false;
            m_isBonusBalls = false;
            m_maxNumberOfGamePicks = 24;
            m_maxNumberOfBonusPicks = 6;
            m_selectedGameNumbers = new List<int>();
            m_selectedBonusNumbers = new List<int>();
            m_originalSelectedGameNumbers = new List<int>();
            m_originalSelectedBonusNumbers = new List<int>();
            m_allGameNumbers = new Dictionary<GameBallUserControl, int>();

            m_randomlist = new List<int>();

            for (int i = 1; i <= TotalNumberOfBalls / 5; i++)
            {
                //game balls
                //var bButton = new ToggleButton { Content = i.ToString(), Style = Resources["BToggleButtonStyle"] as Style };
                //var iButton = new ToggleButton { Content = (i + 15).ToString(), Style = Resources["IToggleButtonStyle"] as Style };
                //var nButton = new ToggleButton { Content = (i + 30).ToString(), Style = Resources["NToggleButtonStyle"] as Style };
                //var gButton = new ToggleButton { Content = (i + 45).ToString(), Style = Resources["GToggleButtonStyle"] as Style };
                //var oButton = new ToggleButton { Content = (i + 60).ToString(), Style = Resources["OToggleButtonStyle"] as Style };

                var bButton = new GameBallUserControl("B", i);
                var iButton = new GameBallUserControl("I", i + 15);
                var nButton = new GameBallUserControl("N", i + 30);
                var gButton = new GameBallUserControl("G", i + 45);
                var oButton = new GameBallUserControl("O", i + 60);

                m_allGameNumbers.Add(bButton, i);
                m_allGameNumbers.Add(iButton, i + 15);
                m_allGameNumbers.Add(nButton, i + 30);
                m_allGameNumbers.Add(gButton, i + 45);
                m_allGameNumbers.Add(oButton, i + 60);

                bButton.Click += GameBallButton_Click;
                iButton.Click += GameBallButton_Click;
                nButton.Click += GameBallButton_Click;
                gButton.Click += GameBallButton_Click;
                oButton.Click += GameBallButton_Click;

                BRow.Children.Add(bButton);
                IRow.Children.Add(iButton);
                NRow.Children.Add(nButton);
                GRow.Children.Add(gButton);
                ORow.Children.Add(oButton);
            }

            m_randomlist = m_allGameNumbers.Values.ToList();
            m_randomlist.Shuffle();

            //initialize B3 Settings
            var viewModel = SessionViewModel.Instance;

            m_enforceMix = viewModel.Settings.EnforceMix;

            SetGameBallButton.Visibility = Visibility.Visible;
            SetBonusBallButton.Visibility = Visibility.Hidden;
            SetBallGrid.Visibility = Visibility.Hidden;
            EditBallGrid.Visibility = Visibility.Visible;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the Click event of the Edit Button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            m_isInEditMode = true;
            ClearAllSelectedBonusBalls();
            EditBallGrid.Visibility = Visibility.Hidden;
            SetBallGrid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Called when [ball button_ click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="routedEventArgs">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void GameBallButton_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            if (!m_isInEditMode)
            {
                return;
            }
            var ball = sender as GameBallUserControl;

            //check for null
            if (ball == null)
            {
                return;
            }

            //clear status message
            StatusMessageTextBlock.Text = string.Empty;

            //make sure valid number
            if (!m_allGameNumbers.ContainsKey(ball))
            {
                return;
            }

            if (m_isBonusBalls)
            {
                HandleBonusBallClick(ball);
            }
            else
            {
                HandleGameBallClick(ball);
            }
        }

        /// <summary>
        /// Handles the game ball clicked.
        /// </summary>
        /// <param name="ball">The ball.</param>
        private void HandleGameBallClick(GameBallUserControl ball)
        {
            //if not selected add to list, else we need to deslect (remove from list)
            if (!ball.IsChecked)
            {
                if (m_selectedGameNumbers.Count < m_maxNumberOfGamePicks)
                {
                    //add number to selected numbers
                    if (TryAddSelectedGameNumber(m_allGameNumbers[ball]))
                    {
                        ball.SelectGameBall();
                    }
                    else //if failed to validate then display error
                    {
                        StatusMessageTextBlock.Text = Properties.Resources.ErrorEnforceMixGameBalls;
                    }
                }
            }
            else if (ball.IsChecked)
            {
                //if ball is in list, then remove from selected list
                if (m_selectedGameNumbers.Contains(m_allGameNumbers[ball]))
                {
                    ball.DeselectBall();
                    m_selectedGameNumbers.Remove(m_allGameNumbers[ball]);
                    DecrementGameBallCoutners(m_allGameNumbers[ball]);
                }
            }

            if (m_selectedGameNumbers.Count == m_maxNumberOfGamePicks)
            {
                SetGameBallButton.IsEnabled = true;
            }
            else
            {
                SetGameBallButton.IsEnabled = false;
            }

            CountTextBlock.Text = string.Format("{0}/{1}", m_selectedGameNumbers.Count, m_maxNumberOfGamePicks);
        }

        /// <summary>
        /// Handles the bonus ball clicked.
        /// </summary>
        /// <param name="ball">The ball.</param>
        private void HandleBonusBallClick(GameBallUserControl ball)
        {            //if not selected add to list, else we need to deslect (remove from list)
            if (!ball.IsChecked)
            {
                if (m_selectedBonusNumbers.Count < m_maxNumberOfBonusPicks)
                {
                    //add number to selected numbers
                    if (TryAddSelectedBonusNumber(m_allGameNumbers[ball]))
                    {
                        ball.SelectBonusBall();
                    }
                    else //if failed to validate then display error
                    {
                        StatusMessageTextBlock.Text = Properties.Resources.ErrorEnforceMixBonusBalls;
                    }
                }
            }
            else if (ball.IsChecked)
            {
                //if ball is in list, then remove from selected list
                if (m_selectedBonusNumbers.Contains(m_allGameNumbers[ball]))
                {
                    ball.DeselectBall();
                    m_selectedBonusNumbers.Remove(m_allGameNumbers[ball]);
                    DecrementBonusBallCoutners(m_allGameNumbers[ball]);
                }
            }

            if (m_selectedBonusNumbers.Count == m_maxNumberOfBonusPicks)
            {
                SetBonusBallButton.IsEnabled = true;
            }
            else
            {
                SetBonusBallButton.IsEnabled = false;
            }

            CountTextBlock.Text = string.Format("{0}/{1}", m_selectedBonusNumbers.Count, m_maxNumberOfBonusPicks);
        }

        /// <summary>
        /// Handles the Click event of the SetBallsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SetGameBallsButton_Click(object sender, RoutedEventArgs e)
        {
            m_isBonusBalls = true;

            TitleTextBlock.Text = "Set Bonus Balls";

            //show bonus balls
            SetBonusBallButton.Visibility = Visibility.Visible;

            //hide game balls
            SetGameBallButton.Visibility = Visibility.Hidden;

            //InitializeBonusBalls(); //DE13098 Dont show current bonus ball when trying to set up a new one.

            //clear status message
            StatusMessageTextBlock.Text = string.Empty;
            CountTextBlock.Text = string.Format("{0}/{1}", m_selectedBonusNumbers.Count, m_maxNumberOfBonusPicks);
        }

        /// <summary>
        /// Handles the Click event of the SetBallsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SetBonusBallsButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = SessionViewModel.Instance;

            Task.Factory.StartNew(() =>
            {
                try
                {
                    m_selectedGameNumbers.AddRange(m_selectedBonusNumbers);
                    //the message requires to send all 75 balls
                    //iterate and fill list with remaining 75
                    foreach (var number in m_randomlist)
                    {
                        if (!m_selectedGameNumbers.Contains(number))
                        {
                            m_selectedGameNumbers.Add(number);
                        }
                    }

                    //set balls
                    viewModel.SetBalls(m_selectedGameNumbers);

                    //remove the extra balls added for the message
                    m_selectedGameNumbers.RemoveRange(24, TotalNumberOfBalls - 24);

                    //set default
                    m_originalSelectedBonusNumbers = new List<int>(m_selectedBonusNumbers);
                    m_originalSelectedGameNumbers = new List<int>(m_selectedGameNumbers);

                    Dispatcher.Invoke(new Action(() =>
                    {
                        TitleTextBlock.Text = "Set Game Balls";

                        //hide bonus balls
                        SetBonusBallButton.Visibility = Visibility.Hidden;

                        //show game balls
                        SetGameBallButton.Visibility = Visibility.Visible;

                        EditBallGrid.Visibility = Visibility.Visible;
                        SetBallGrid.Visibility = Visibility.Hidden;

                        CountTextBlock.Text = string.Format("{0}/{1}", m_selectedGameNumbers.Count, m_maxNumberOfGamePicks);
                        //clear status message
                        StatusMessageTextBlock.Text = string.Empty;
                    }));

                    m_isInEditMode = false;
                    m_isBonusBalls = false;
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        MessageBox.Show(string.Format(CultureInfo.CurrentCulture, Properties.Resources.SessionSetBallsFailed, ex.Message),
                                        Properties.Resources.B3CenterName, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                    }));

                }
            });
        }

        /// <summary>
        /// Handles the Click event of the ClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            //clear status message
            StatusMessageTextBlock.Text = string.Empty;

            if (m_isBonusBalls)
            {
                ClearAllSelectedBonusBalls();
                CountTextBlock.Text = string.Format("{0}/{1}", m_selectedBonusNumbers.Count, m_maxNumberOfBonusPicks);
            }
            else
            {
                ClearAllSelectedGameBalls();
                CountTextBlock.Text = string.Format("{0}/{1}", m_selectedGameNumbers.Count, m_maxNumberOfGamePicks);
            }

        }

        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            m_isInEditMode = false;
            var numbers = new List<int>(m_originalSelectedGameNumbers);
            numbers.AddRange(m_originalSelectedBonusNumbers);

            InitializeSelectedBalls(numbers);

            EditBallGrid.Visibility = Visibility.Visible;
            SetBallGrid.Visibility = Visibility.Hidden;

            //clear status message
            StatusMessageTextBlock.Text = string.Empty;
        }

        /// <summary>
        /// Initializes the balls.
        /// </summary>
        /// <param name="gameNumbers">The numbers.</param>
        public void InitializeSelectedBalls(List<int> gameNumbers)
        {
            //initialize if null
            if (gameNumbers == null)
            {
                gameNumbers = new List<int>();
            }

            m_originalSelectedGameNumbers = new List<int>(gameNumbers);

            //get bonus numbers
            if (gameNumbers.Count >= 30)
            {
                m_originalSelectedBonusNumbers = new List<int>(gameNumbers.GetRange(24, 6));
            }

            TitleTextBlock.Text = "Set Game Balls";
            m_isBonusBalls = false;
            SetGameBallButton.Visibility = Visibility.Visible;
            SetBonusBallButton.Visibility = Visibility.Hidden;

            //clear
            ClearAllSelectedGameBalls();
            ClearAllSelectedBonusBalls();

            //iterate through all the game numbers and select the ball
            for (int i = 0; i < gameNumbers.Count; i++)
            {
                //if we hit max amount, then exit
                if (i >= m_maxNumberOfGamePicks)
                {
                    break;
                }

                //find the associated button
                var number = gameNumbers[i];
                var kvp = m_allGameNumbers.DefaultIfEmpty(new KeyValuePair<GameBallUserControl, int>(null, 0)).FirstOrDefault(n => n.Value == number);

                if (kvp.Key == null)
                {
                    continue;
                }

                //check button and add to list
                if (!m_selectedGameNumbers.Contains(number))
                {
                    if (TryAddSelectedGameNumber(number))
                    {
                        kvp.Key.SelectGameBall();
                    }
                }
            }

            if (m_selectedGameNumbers.Count == m_maxNumberOfGamePicks)
            {
                SetGameBallButton.IsEnabled = true;
            }
            else
            {
                SetGameBallButton.IsEnabled = false;
            }

            InitializeBonusBalls();

            //update count
            CountTextBlock.Text = string.Format("{0}/{1}", m_selectedGameNumbers.Count, m_maxNumberOfGamePicks);
        }

        /// <summary>
        /// Initializes the bonus balls.
        /// </summary>
        private void InitializeBonusBalls()
        {
            //iterate through all the Bonus numbers and select the ball
            for (int i = 0; i < m_originalSelectedBonusNumbers.Count; i++)
            {
                //if we hit max amount, then exit
                if (i >= m_maxNumberOfBonusPicks)
                {
                    break;
                }

                //find the associated button
                var number = m_originalSelectedBonusNumbers[i];
                var kvp = m_allGameNumbers.DefaultIfEmpty(new KeyValuePair<GameBallUserControl, int>(null, 0)).FirstOrDefault(n => n.Value == number);

                if (kvp.Key == null)
                {
                    continue;
                }

                //check button and add to list
                if (!m_selectedBonusNumbers.Contains(number))
                {
                    if (!kvp.Key.IsChecked)
                    {
                        if (TryAddSelectedBonusNumber(number))
                        {
                            kvp.Key.SelectBonusBall();
                        }
                    }
                }
            }

            if (m_selectedBonusNumbers.Count == m_maxNumberOfBonusPicks)
            {
                SetBonusBallButton.IsEnabled = true;
            }
            else
            {
                SetBonusBallButton.IsEnabled = false;
            }
        }

        /// <summary>
        /// Tries to add selected number to selected list.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        private bool TryAddSelectedGameNumber(int number)
        {
            bool isValid = false;

            //make sure doesn't already exist
            if (!m_selectedGameNumbers.Contains(number))
            {
                //if valid then add ball, else return false
                isValid = IsValidGameBall(number);
                if (isValid)
                {
                    //add ball number
                    m_selectedGameNumbers.Add(number);
                }
            }
            else
            {
                //already in list
                isValid = true;
            }

            return isValid;
        }

        /// <summary>
        /// Tries to add selected number to selected list.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        private bool TryAddSelectedBonusNumber(int number)
        {
            bool isValid = false;

            //make sure doesn't already exist
            if (!m_selectedBonusNumbers.Contains(number))
            {
                //if valid then add ball, else return false
                isValid = IsValidBonusBall(number);
                if (isValid)
                {
                    //add ball number
                    m_selectedBonusNumbers.Add(number);
                }
            }
            else
            {
                //already in list
                isValid = true;
            }

            return isValid;
        }

        /// <summary>
        /// Determines whether [is valid ball] [the specified number]. Enforces 
        //  5-5-4-5-5 if setting is enabled
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        private bool IsValidGameBall(int number)
        {
            //if enforce mix balls is disabled then all balls are valid
            if (!m_enforceMix)
            {
                return true;
            }

            var range = (number - 1) / 15;
            var retValue = false;

            switch (range)
            {
                case 0:
                    if (m_bCounter < 5)
                    {
                        m_bCounter++;
                        retValue = true;
                    }
                    break;
                case 1:
                    if (m_iCounter < 5)
                    {
                        m_iCounter++;
                        retValue = true;
                    }
                    break;
                case 2: //"N" is special because of free space.

                    if (m_nCounter < 4)
                    {
                        m_nCounter++;
                        retValue = true;
                    }
                    break;
                case 3:
                    if (m_gCounter < 5)
                    {
                        m_gCounter++;
                        retValue = true;
                    }
                    break;
                case 4:
                    if (m_oCounter < 5)
                    {
                        m_oCounter++;
                        retValue = true;
                    }
                    break;
            }

            return retValue;
        }

        /// <summary>
        /// Determines whether [is valid ball] [the specified number].
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        private bool IsValidBonusBall(int number)
        {
            //if enforce mix balls is disabled then all balls are valid
            if (!m_enforceMix)
            {
                return true;
            }

            var range = (number - 1) / 15;
            var retValue = false;

            switch (range)
            {
                case 0:
                    if (m_bBonusCounter < 1)
                    {
                        m_bBonusCounter++;
                        retValue = true;
                    }
                    break;
                case 1:
                    if (m_iBonusCounter < 1)
                    {
                        m_iBonusCounter++;
                        retValue = true;
                    }
                    break;
                case 2: //"N" is special because of free space.

                    if (m_nBonusCounter < 2)
                    {
                        m_nBonusCounter++;
                        retValue = true;
                    }
                    break;
                case 3:
                    if (m_gBonusCounter < 1)
                    {
                        m_gBonusCounter++;
                        retValue = true;
                    }
                    break;
                case 4:
                    if (m_oBonusCounter < 1)
                    {
                        m_oBonusCounter++;
                        retValue = true;
                    }
                    break;
            }

            return retValue;
        }

        /// <summary>
        /// Clears all selected balls.
        /// </summary>
        private void ClearAllSelectedGameBalls()
        {
            //reset all game balls
            foreach (var number in m_selectedGameNumbers)
            {
                foreach (var kvp in m_allGameNumbers)
                {
                    if (number == kvp.Value)
                    {
                        kvp.Key.DeselectBall();
                        break;
                    }
                }
            }

            ResetGameBallCounter();
            m_selectedGameNumbers.Clear();
        }

        /// <summary>
        /// Clears all selected bonus balls.
        /// </summary>
        private void ClearAllSelectedBonusBalls()
        {
            //reset all bonus balls
            foreach (var number in m_selectedBonusNumbers)
            {
                foreach (var kvp in m_allGameNumbers)
                {
                    if (number == kvp.Value)
                    {
                        kvp.Key.DeselectBall();
                        break;
                    }
                }
            }

            ResetBonusMixBallCounter();
            m_selectedBonusNumbers.Clear();

        }

        /// <summary>
        /// Resets the enforce mix ball counter.
        /// </summary>
        private void ResetGameBallCounter()
        {
            m_bCounter = 0;
            m_iCounter = 0;
            m_nCounter = 0;
            m_gCounter = 0;
            m_oCounter = 0;
        }

        /// <summary>
        /// Resets the enforce mix ball counter.
        /// </summary>
        private void ResetBonusMixBallCounter()
        {
            m_bBonusCounter = 0;
            m_iBonusCounter = 0;
            m_nBonusCounter = 0;
            m_gBonusCounter = 0;
            m_oBonusCounter = 0;
        }

        private void DecrementGameBallCoutners(int number)
        {
            switch ((number - 1) / 15)
            {
                case 0:
                    if (m_bCounter > 0)
                    {
                        m_bCounter--;
                    }
                    break;
                case 1:
                    if (m_iCounter > 0)
                    {
                        m_iCounter--;
                    }
                    break;
                case 2:

                    if (m_nCounter > 0)
                    {
                        m_nCounter--;
                    }
                    break;
                case 3:
                    if (m_gCounter > 0)
                    {
                        m_gCounter--;
                    }
                    break;
                case 4:
                    if (m_oCounter > 0)
                    {
                        m_oCounter--;
                    }
                    break;
            }
        }

        private void DecrementBonusBallCoutners(int number)
        {
            switch ((number - 1) / 15)
            {
                case 0:
                    if (m_bBonusCounter > 0)
                    {
                        m_bBonusCounter--;
                    }
                    break;
                case 1:
                    if (m_iBonusCounter > 0)
                    {
                        m_iBonusCounter--;
                    }
                    break;
                case 2:

                    if (m_nBonusCounter > 0)
                    {
                        m_nBonusCounter--;
                    }
                    break;
                case 3:
                    if (m_gBonusCounter > 0)
                    {
                        m_gBonusCounter--;
                    }
                    break;
                case 4:
                    if (m_oBonusCounter > 0)
                    {
                        m_oBonusCounter--;
                    }
                    break;
            }
        }

        #endregion
    }
}
