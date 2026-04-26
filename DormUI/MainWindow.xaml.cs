using Dorm.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace DormUI;

public partial class MainWindow : Window
{
    private HttpClient _http = new HttpClient()
    {
        BaseAddress = new Uri("http://localhost:5163") // ЗАМІНИ ПОРТ
    };

    public MainWindow()
    {
        InitializeComponent();
    }

    private async void Load_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var data = await _http.GetFromJsonAsync<List<SettlementDto>>("api/Settlements");
            dataGrid.ItemsSource = data;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}