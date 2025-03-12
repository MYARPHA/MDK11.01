using System.IO;
using System.Windows;

namespace LabWork22
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var ticket = new TicketInfo
            {
                TicketNumber = TicketNumberTextBox.Text,
                FilmTitle = MovieTitleTextBox.Text,
                SessionTime = SessionTimeDatePicker.SelectedDate.Value,
                CinemaName = CinemaNameTextBox.Text,
                HallNumber = int.Parse(HallNumberTextBox.Text),
                RowNumber = int.Parse(RowNumberTextBox.Text),
                SeatNumber = int.Parse(SeatNumberTextBox.Text)
            };

            string filePath = $"Ticket_{ticket.TicketNumber}.txt";

            using (StreamWriter writer = new(filePath))
            {
                writer.WriteLine($"Номер билета {ticket.TicketNumber}");
                writer.WriteLine(ticket.FilmTitle);
                writer.WriteLine($"Начало сеанса: {ticket.SessionTime:HH'h' mm dd.MM. yyyy}");
                writer.WriteLine($"Кинотеатр: {ticket.CinemaName}");
                writer.WriteLine($"Зал: {ticket.HallNumber}");
                writer.WriteLine($"Ряд: {ticket.RowNumber} Место: {ticket.SeatNumber}");
            }
            MessageBox.Show("Билет сохранён!");
        }

        //public void SaveTicketAsTxt(TicketInfo ticket)
        //{
        //    string filePath = $"Ticket_{ticket.TicketNumber}.txt";

        //    using (StreamWriter writer = new(filePath))
        //    {
        //        writer.WriteLine($"Номер билета {ticket.TicketNumber}");
        //    }
        //}


    }
}