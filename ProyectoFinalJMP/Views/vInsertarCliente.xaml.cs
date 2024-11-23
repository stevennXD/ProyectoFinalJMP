using System.Collections.Specialized;
using System.Net;

namespace ProyectoFinalJMP.Views;

public partial class vInsertarCliente : ContentPage
{
    private string url = "http://127.0.0.1/moviles/cliente.php";

    private string username;

    public vInsertarCliente(string username)
    {
        InitializeComponent();
        this.username = username;   
    }

    private async void btnGuardar_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();

            var parametros = new NameValueCollection
                {
                    { "Identificacion", txtIdentificacion.Text },
                    { "Nombre", txtNombre.Text },
                    { "Telefono", txtTelefono.Text },
                    { "Email", txtEmail.Text },
                    { "Direccion", txtDireccion.Text },
                    { "Ciudad", pickerCiudad.SelectedItem?.ToString() ?? "" },
                    { "Ubicacion", txtUbicacion.Text },
                    { "FechaRegistro", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                    { "Estado", "1" } // Estado activo por defecto
                };

            cliente.UploadValues(url, "POST", parametros);

            await DisplayAlert("Éxito", "Cliente registrado correctamente", "OK");

            // Navegar de vuelta a la lista de clientes
            await Navigation.PushAsync(new vCliente(username));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error al guardar el cliente: {ex.Message}", "Cerrar");
        }
    }
}