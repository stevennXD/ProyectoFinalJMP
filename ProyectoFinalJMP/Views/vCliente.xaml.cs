using Newtonsoft.Json;
using ProyectoFinalJMP.Models;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;

namespace ProyectoFinalJMP.Views;

public partial class vCliente : ContentPage
{
    private string url = "http://127.0.0.1/moviles/cliente.php";
    private readonly HttpClient httpClient = new HttpClient();
    public ObservableCollection<Cliente> Clientes { get; set; }

    private string username;

    public vCliente(string username)
    {
        InitializeComponent();
        this.username = username;
        Clientes = new ObservableCollection<Cliente>();
        clienteCollection.ItemsSource = Clientes;
        CargarClientes();
    }

    private async void CargarClientes()
    {
        try
        {
            var content = await httpClient.GetStringAsync(url);
            var clientes = JsonConvert.DeserializeObject<List<Cliente>>(content);

            Clientes.Clear();
            foreach (var clienteObj in clientes)
            {
                Clientes.Add(clienteObj);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo cargar la lista de clientes: {ex.Message}", "Cerrar");
        }
    }

    private async void clienteCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Cliente clienteSeleccionado)
        {
            // Navegar a la vista de actualización/eliminación
            await Navigation.PushAsync(new vActualizarEliminarCliente(clienteSeleccionado, username));
        }
    }

    private async void btnAgregarCliente_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new vInsertarCliente(username));
    }

    private async void OnVerMapaClicked(object sender, EventArgs e)
    {
        // Obtener la ubicación del cliente al que se le hizo clic
        var button = sender as Button;
        var ubicacion = button?.CommandParameter.ToString();

        if (!string.IsNullOrEmpty(ubicacion))
        {
            // Separar latitud y longitud
            var coordenadas = ubicacion.Split(',');
            if (coordenadas.Length == 2)
            {
                string latitud = coordenadas[0].Trim();
                string longitud = coordenadas[1].Trim();

                await Navigation.PushAsync(new WebViewPage(latitud, longitud));
            }
            else
            {
                await DisplayAlert("Error", "Ubicación no válida", "Cerrar");
            }
        }
    }

    private void btnInicio_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Home(username));
    }
}