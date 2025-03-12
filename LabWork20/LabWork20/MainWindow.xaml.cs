using LabWork20.Data;
using LabWork20.Services;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace LabWork20
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CinemaService _service = new();

        public MainWindow()
        {
            InitializeComponent();
            LoadFilms();
        }

        public async Task LoadFilms()
        {
            CinemaDataGrid.ItemsSource = await _service.GetAllFilmsAsync(); 
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow window = new();
            window.ShowDialog();
        }
    }
}