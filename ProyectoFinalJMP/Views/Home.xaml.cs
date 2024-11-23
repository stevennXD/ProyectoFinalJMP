using Newtonsoft.Json;
using ProyectoFinalJMP.Models;
using System.Collections.ObjectModel;

namespace ProyectoFinalJMP.Views;

public partial class Home : ContentPage
{
	private const string Url = "http://127.0.0.1/moviles/request.php";
	private readonly HttpClient cliente = new HttpClient();
	private ObservableCollection<Requests> requests { get; set; }


	private string username;

    public Home(string username)
	{
		InitializeComponent();
		this.username = username;
		userLabel.Text = $"Hola! {username}";
		requests = new ObservableCollection<Requests>();
		requestCollection.ItemsSource = requests;
		ObtenerPedidos("cancelado");
		FilterPicker.SelectedIndex = 0;
	}

	private async void ObtenerPedidos(string filtro = null)
	{
		try
		{
			// Construir la URL dependiendo del filtro
			string filtroUrl = string.IsNullOrEmpty(filtro) ? Url : $"{Url}?estado={filtro}";

			// Realizar la solicitud a la API
			var content = await cliente.GetStringAsync(filtroUrl);
			var pedidos = JsonConvert.DeserializeObject<List<Requests>>(content);

			// Limpiar y agregar los datos al ObservableCollection
			requests.Clear();
			foreach (var pedidoObj in pedidos)
			{
				requests.Add(pedidoObj);
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"No se pudo cargar la lista de pedidos: {ex.Message}", "Cerrar");
		}
	}

	// Evento para manejar los cambios en el Picker
	private void OnFilterPickerChanged(object sender, EventArgs e)
	{
		var picker = sender as Picker;
		var selectedValue = picker.SelectedItem?.ToString();

		// Pasar el filtro seleccionado a ObtenerPedidos (null si no hay selección)
		ObtenerPedidos(selectedValue);
	}

	private async void btnProducts_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Views.ProductPage(username));
	}

	private async void btnRequests_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Views.ProductPage(username));
	}

	private async void btnCustomers_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Views.vCliente(username));
	}
}