using Newtonsoft.Json;
using ProyectoFinalJMP.Models;
using System.Net;
using System.Text;

namespace ProyectoFinalJMP.Views;

public partial class UpdateDeleteProduct : ContentPage
{
	private const string Url = "http://127.0.0.1/moviles/post.php";
	private Productos productActual;


    private string username;

    public UpdateDeleteProduct(Productos producto, string username)
	{
		InitializeComponent();
		productActual = producto;
        this.username = username;
        CargarDatosProducto();
	}


	private void CargarDatosProducto()
	{

		codigoEntry.Text = productActual.codigo.ToString();
		nombreEntry.Text = productActual.nombre.ToString();
		descripcionEntry.Text = productActual.descripcion.ToString();
		precioEntry.Text = productActual.precio.ToString("F2");
		cantidadEntry.Text = productActual.cantidad.ToString();
	}

	private async void btnActualizar_Clicked(object sender, EventArgs e)
	{
		try
		{
			using (var client = new HttpClient())
			{
				// Preparamos los datos a enviar como JSON
				var parametros = new
				{
					id = productActual.id,
					codigo = codigoEntry.Text,
					nombre = nombreEntry.Text,
					descripcion = descripcionEntry.Text,
					precio = decimal.TryParse(precioEntry.Text, out decimal precio) ? precio : 0,
					cantidad = int.TryParse(cantidadEntry.Text, out int cantidad) ? cantidad : 0
				};

				// Serializamos los datos a JSON
				var content = new StringContent(JsonConvert.SerializeObject(parametros), Encoding.UTF8, "application/json");

				// Realizamos la solicitud PUT
				var response = await client.PutAsync(Url, content);

				if (response.IsSuccessStatusCode)
				{
					await DisplayAlert("Éxito", "Producto actualizado correctamente.", "Cerrar");
					await Navigation.PushAsync(new ProductPage(username));
				}
				else
				{
					await DisplayAlert("Error", "No se pudo actualizar el producto.", "Cerrar");
				}
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"No se pudo actualizar el producto: {ex.Message}", "Cerrar");
		}
	}

	private async void btnEliminar_Clicked(object sender, EventArgs e)
	{
		bool confirmacion = await DisplayAlert("Confirmación", "Estas seguro de eliminar este cliente?", "Sí", "No");
		if (!confirmacion) return;

		try
		{
			WebClient cliente = new WebClient();
			string urlConParametros = $"{Url}?id={productActual.id}";
			cliente.UploadString(urlConParametros, "DELETE", "");

			await DisplayAlert("Exito", "Cliente eliminado correctamente.", "Cerrar");
			await Navigation.PushAsync(new ProductPage(username));
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"No se pudo eliminar el cliente: {ex.Message}", "Cerrar");
		}
	}
}