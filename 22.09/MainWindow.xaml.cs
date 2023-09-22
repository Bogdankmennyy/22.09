using System;
using System.Windows;
using System.Windows.Media;

namespace NumberGuessingGame
{
    public partial class MainWindow : Window
    {
        private int targetNumber;
        private int numberOfTries;
        private bool gameInProgress;

        public MainWindow()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            targetNumber = new Random().Next(1, 101);
            numberOfTries = 0;
            gameInProgress = true;
            UpdateUI("Загадайте число від 1 до 100.", Brushes.Black);
            EnableUserInput(true);
        }

        private void SubmitGuess_Click(object sender, RoutedEventArgs e)
        {
            if (gameInProgress)
            {
                if (int.TryParse(userGuessTextBox.Text, out int userGuess))
                {
                    numberOfTries++;

                    if (userGuess < targetNumber)
                    {
                        UpdateUI("Ваше число менше загаданого.", Brushes.Black);
                    }
                    else if (userGuess > targetNumber)
                    {
                        UpdateUI("Ваше число більше загаданого.", Brushes.Black);
                    }
                    else
                    {
                        UpdateUI($"Вітаємо, ви вгадали число {targetNumber} за {numberOfTries} спроб!", Brushes.Green);
                        EnableUserInput(false);
                        gameInProgress = false;
                    }
                }
                else
                {
                    MessageBox.Show("Будь ласка, введіть ціле число.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                userGuessTextBox.Clear();
            }
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        private void UpdateUI(string message, Brush color)
        {
            resultLabel.Content = message;
            resultLabel.Foreground = color;
        }

        private void EnableUserInput(bool enable)
        {
            userGuessTextBox.IsEnabled = enable;
           // SubmitGuessButton.IsEnabled = enable;
        }
    }
}

