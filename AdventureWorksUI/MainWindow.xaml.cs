using AdventureWorks.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace AdventureWorksUI
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private List<SalesOrderHeaderDto> _orders = new List<SalesOrderHeaderDto>();
        private List<SalesOrderDetailDto> _details = new List<SalesOrderDetailDto>();

        private int _currentIndex = 0;

        public MainWindow()
        {
            InitializeComponent();

            // Вкажи свій порт API
            _httpClient.BaseAddress = new Uri("http://localhost:5001");
        }

        private async void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            await LoadOrdersAsync();
        }

        private async Task LoadOrdersAsync()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<SalesOrderHeaderDto>>("api/SalesOrderHeaders");

                if (result != null && result.Count > 0)
                {
                    _orders = result;
                    _currentIndex = 0;

                    await ShowCurrentOrderAsync();
                }
                else
                {
                    MessageBox.Show("Замовлення не знайдено.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження замовлень: {ex.Message}");
            }
        }

        private async Task ShowCurrentOrderAsync()
        {
            if (_orders == null || _orders.Count == 0)
                return;

            var currentOrder = _orders[_currentIndex];

            // Прив'язуємо поточне замовлення до форми
            DataContext = currentOrder;

            // Завантажуємо деталі цього замовлення
            await LoadOrderDetailsAsync(currentOrder.SalesOrderId);

            // Оновлюємо стан кнопок
            UpdateNavigationButtons();
        }

        private async Task LoadOrderDetailsAsync(int salesOrderId)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<SalesOrderDetailDto>>(
                    $"api/SalesOrderDetails/by-order/{salesOrderId}");

                _details = result ?? new List<SalesOrderDetailDto>();

                detailsDataGrid.ItemsSource = null;
                detailsDataGrid.ItemsSource = _details;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження деталей замовлення: {ex.Message}");
            }
        }

        private void UpdateNavigationButtons()
        {
            backButton.IsEnabled = _currentIndex > 0;
            nextButton.IsEnabled = _currentIndex < _orders.Count - 1;
        }

        private async void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                await ShowCurrentOrderAsync();
            }
        }

        private async void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentIndex < _orders.Count - 1)
            {
                _currentIndex++;
                await ShowCurrentOrderAsync();
            }
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_orders == null || _orders.Count == 0)
                    return;

                var currentOrder = _orders[_currentIndex];

                var response = await _httpClient.PutAsJsonAsync(
                    $"api/SalesOrderHeaders/{currentOrder.SalesOrderId}",
                    currentOrder);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Зміни успішно збережено.");
                }
                else
                {
                    var errorText = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Помилка збереження: {response.StatusCode}\n{errorText}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка під час збереження: {ex.Message}");
            }
        }
    }


}