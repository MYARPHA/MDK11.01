using ServiceLayer;
using ServiceLayer.Services;
using System.Windows;

namespace PractiseWork2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FilmService _filmService = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadAllFilmsAsync();
        }

        // метод для загрузки списка фильмов
        private async Task LoadAllFilmsAsync()
        {
            try
            {
                var films = await _filmService.GetAllFilmsAsync();
                FilmsDataGrid.ItemsSource = films;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Получение по Id
        private async void LoadByIdButton_Click(object sender, RoutedEventArgs e)
        {
            var film = await _filmService.GetFilmByIdAsync(4);
            if (film != null)
            {
                FilmInfoTextBox.Text = $"Название: {film.FilmTitle} Возрастной рейтинг: {film.AgeRating}";
            }
            else
            {
                MessageBox.Show("Фильм не найден!");
            }
        }
        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var film = new Film() { FilmTitle = TitleTextBox.Text, AgeRating = AgeRatingTextBox.Text };
                await _filmService.AddFilmAsync(film);
                await LoadAllFilmsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }

        }
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool result = await _filmService.DeleteFilmAsync(5);
                if (result)
                {
                    await LoadAllFilmsAsync();
                }
                else
                {
                    MessageBox.Show("Фильм не найден!");
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }





    }
}