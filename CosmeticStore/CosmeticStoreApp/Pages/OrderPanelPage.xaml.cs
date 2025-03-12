using CosmeticStoreLib.Models;
using CosmeticStoreLib.Services;
using System.Windows;
using System.Windows.Controls;
using CosmeticStore.Pages;

namespace Desktop.Pages
{
    public partial class OrderPanelPage : Page
    {
        private readonly OrderService _orderService = new();
        private readonly UserService _userService = new();
        private Order _currentOrder;

        public OrderPanelPage()
        {
            InitializeComponent();
        }

        private async void SearchOrder_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(OrderIdTextBox.Text, out int orderId))
            {
                try
                {
                    _currentOrder = await _orderService.GetOrderByIdAsync(orderId);
                    await DisplayOrderDetailsAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Заказ не найден", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Указан неверный номер заказа", "Неверный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async Task DisplayOrderDetailsAsync()
        {
            if (_currentOrder != null)
            {
                OrderDateTextBlock.Text = _currentOrder.OrderDate.ToString("d");

                DeliveryDatePicker.SelectedDate = _currentOrder.OrderDeliveryDate;

                StatusComboBox.SelectedItem = StatusComboBox.Items.Cast<ComboBoxItem>()
                    .FirstOrDefault(item => item.Content.ToString() == _currentOrder.OrderStatus);

                if (_currentOrder.UserId != null)
                {
                    var user = await _userService.(_currentOrder.UserId);
                    CustomerNameTextBlock.Text = user?.UserName ?? "N/A";
                }
                else
                {
                    CustomerNameTextBlock.Text = "N/A";
                }
            }
        }

    }
}
