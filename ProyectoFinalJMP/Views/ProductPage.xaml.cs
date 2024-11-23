using Newtonsoft.Json;
using ProyectoFinalJMP.Models;
using System.Collections.ObjectModel;

namespace ProyectoFinalJMP.Views;

public partial class ProductPage : ContentPage
{
	private const string Url = "http://127.0.0.1/moviles/post.php";
	private readonly HttpClient cliente = new HttpClient();
	private ObservableCollection<Productos> productos { get; set; }


    private string username;

    public ProductPage(string username)
	{
		InitializeComponent();
        this.username = username;
        productos = new ObservableCollection<Productos>();
		productoCollection.ItemsSource = productos;
		ObtenerProductos();
	}

	private async void ObtenerProductos()
	{
		try
		{
			var content = await cliente.GetStringAsync(Url);
			var producto = JsonConvert.DeserializeObject<List<Productos>>(content);

			productos.Clear();
			foreach (var productoObj in producto)
			{
				productos.Add(productoObj);
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"No se pudo cargar la lista de clientes: {ex.Message}", "Cerrar");
		}
	}

	private async void btnAgregarProducto_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Views.CreateProduct(username));
	}

	private async void ProductoCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.FirstOrDefault() is Productos productoSeleccionado)
		{

			await Navigation.PushAsync(new UpdateDeleteProduct(productoSeleccionado, username));
		}
	}

    private void btnInicio_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Home(username));
    }
}