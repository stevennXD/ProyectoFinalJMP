namespace ProyectoFinalJMP.Views;

public partial class LoginPage : ContentPage
{
	private Dictionary<string, string> users = new Dictionary<string, string>
		{
			{ "cris", "123" },
			{ "edi", "123" },
			{ "bryan", "123" }
		};

	public LoginPage()
	{
		InitializeComponent();
	}

	private async void OnLoginClicked(object sender, EventArgs e)
	{
		if (users.TryGetValue(usernameEntry.Text, out var password) && password == passwordEntry.Text)
		{
			await Navigation.PushAsync(new Views.Home(usernameEntry.Text));
		}
		else
		{
			await DisplayAlert("Error", "Datos incorrectos", "OK");
		}
	}
}