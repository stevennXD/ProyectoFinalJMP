using System.Net;

namespace ProyectoFinalJMP.Views;

public partial class CreateProduct : ContentPage
{
	private const string Url = "http://127.0.0.1/moviles/post.php";
	private readonly HttpClient cliente = new HttpClient();

    private string username;

    public CreateProduct(string username)
	{
		InitializeComponent();
        this.username = username;
    }

	private void OnGuardarClicked(object sender, EventArgs e)
	{
		try
		{
			WebClient cliente = new WebClient();

			var parametros = new System.Collections.Specialized.NameValueCollection();

			parametros.Add("codigo", codigoEntry.Text);
			parametros.Add("nombre", nombreEntry.Text);
			parametros.Add("descripcion", descripcionEntry.Text);
			parametros.Add("precio", decimal.TryParse(precioEntry.Text, out decimal precio) ? precio.ToString() : "0");
			parametros.Add("cantidad", int.TryParse(cantidadEntry.Text, out int cantidad) ? cantidad.ToString() : "0");

			cliente.UploadValues("http://10.0.2.2/productos/post.php", "POST", parametros);
			Navigation.PushAsync(new ProductPage(username));
		}
		catch (Exception ex)
		{
			DisplayAlert("Alerta", ex.Message, "Cerrar");
		}

	}
}