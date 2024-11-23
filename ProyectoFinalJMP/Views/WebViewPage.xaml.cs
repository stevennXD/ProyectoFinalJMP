namespace ProyectoFinalJMP.Views;

public partial class WebViewPage : ContentPage
{
    public WebViewPage(string latitud, string longitud)
    {
        InitializeComponent();

        // Define tu API Key de Google Maps aquí
        string apiKey = "AIzaSyAJyHye156_EMSPSn6qZ51GwEcyLo_dDfM";  // Sustituye por tu clave API

        // Construye la URL para Google Maps
        string url = $"https://www.google.com/maps?q={latitud},{longitud}&key={apiKey}";

        // Asigna la URL al WebView para cargar el mapa
        webView.Source = url;
    }
}