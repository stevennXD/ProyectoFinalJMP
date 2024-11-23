using ProyectoFinalJMP.Models;
using System.Net;

namespace ProyectoFinalJMP.Views;

public partial class vActualizarEliminarCliente : ContentPage
{
    private string url = "http://127.0.0.1/moviles/cliente.php";
    private Cliente clienteActual;

    private string username;

    public vActualizarEliminarCliente(Cliente cliente, string username)
    {
        InitializeComponent();

        clienteActual = cliente;
        this.username = username;
        CargarDatosCliente();
    }

    private void CargarDatosCliente()
    {
        txtCodigo.Text = clienteActual.Codigo.ToString();
        txtIdentificacion.Text = clienteActual.Identificacion;
        txtNombre.Text = clienteActual.Nombre;
        txtTelefono.Text = clienteActual.Telefono;
        txtEmail.Text = clienteActual.Email;
        txtDireccion.Text = clienteActual.Direccion;
		pickerCiudad.SelectedItem = clienteActual.Ciudad;
        txtUbicacion.Text = clienteActual.Ubicacion;
        swEstado.IsToggled = clienteActual.Estado;
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
			var cliente = new HttpClient();

            var estadoInsert = swEstado.IsToggled ? "1" : "0";

			var url = $"http://127.0.0.1/moviles/cliente.php?codigo={txtCodigo.Text}" +
                $"&Identificacion={txtIdentificacion.Text}" +
                $"&Nombre={txtNombre.Text}" +
                $"&Telefono={txtTelefono.Text}" +
                $"&Email={txtEmail.Text}" +
                $"&Direccion={txtDireccion.Text}" +
                $"&Ciudad={pickerCiudad.SelectedItem?.ToString() ?? ""}" +
                $"&Ubicacion={txtUbicacion.Text}" +
                $"&Estado={estadoInsert}";

			var respuesta = await cliente.PutAsync(url, null);  // Enviar PUT sin cuerpo (sin contenido en el body)

			if (respuesta.IsSuccessStatusCode)
			{
				await DisplayAlert("Éxito", "Cliente actualizado correctamente", "OK");
				await Navigation.PushAsync(new vCliente(username));
			}
			else
			{
				await DisplayAlert("Error", "No se pudo actualizar el estudiante", "Cerrar");
			}
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo actualizar el cliente: {ex.Message}", "Cerrar");
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        bool confirmacion = await DisplayAlert("Confirmación", "¿Estás seguro de eliminar este cliente?", "Sí", "No");
        if (!confirmacion) return;

        try
		{
			var cliente = new HttpClient();

			var deleteUrl = $"http://127.0.0.1/moviles/cliente.php?codigo={txtCodigo.Text}";

			var respuesta = await cliente.DeleteAsync(deleteUrl);

			if (respuesta.IsSuccessStatusCode)
			{
				await DisplayAlert("Éxito", "Cliente eliminado correctamente", "OK");
				await Navigation.PushAsync(new vCliente(username));
			}
			else
			{
				await DisplayAlert("Error", "No se pudo eliminar el estudiante", "Cerrar");
			}
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo eliminar el cliente: {ex.Message}", "Cerrar");
        }
    }
}