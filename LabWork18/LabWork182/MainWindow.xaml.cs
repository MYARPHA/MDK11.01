using LabWork182.Models;
using System.Windows;

namespace LabWork182
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameService _service = new();

        public MainWindow()
        {
            InitializeComponent();
            LoadGames();
        }

        public async Task LoadGames()
        {
            GamesDataGrid.ItemsSource = await _service.GetAllGamesAsync();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.ShowDialog();
            LoadGames();
        }
        private async void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedGames = new List<Game>(GamesDataGrid.SelectedItems.Cast<Game>());

            // удаление
            foreach (Game game in selectedGames)
            {
                try
                {
                    await _service.RemoveGameAsync(game);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            await LoadGames();
        }

        private void GamesDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (GamesDataGrid.SelectedItems is Game game)
            {
                MainWindow window = new(game);
                window.ShowDialog();
                await LoadGames();
            }
        }
    }
}
