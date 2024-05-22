using Newtonsoft.Json;
using ProyectoFinal.Models;

namespace ProyectoFinal.Views.Paciente;

public partial class VFormPaciente : ContentPage
{
    private string ip;
    private string url;
    private string filePath;
    private readonly HttpClient paciente = new HttpClient();
    Config serverip = new Config();

    public VFormPaciente()
    {
        ip = serverip.ipserver;
        url = "http://"+ip+"/APPS/Back/Controlador/controlador.php?AddPaciente=true";
        InitializeComponent();
	}

    private async void btnGuardar_Clicked(object sender, EventArgs e)
    {
        var nombre = txtNombre.Text;
        var apellido = txtApellido.Text;
        var FechaN = dpFecha.Date.ToString("yyyy-MM-dd");
        var detalle = txtDetalle.Text;

        var parametros = new Dictionary<string, string>
            {
                { "nombre", nombre },
                { "apellido", apellido },
                { "FechaN", FechaN },
                { "detalle", detalle },
                { "foto",   filePath}
            };

        var content = new FormUrlEncodedContent(parametros);
        var response = await paciente.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var respuesta = await response.Content.ReadAsStringAsync();
            if(respuesta=="1")
            {
                await DisplayAlert("Alerta", "Paciente registrado", "OK");
                await Navigation.PushAsync(new VListPacientes());
            }
            Console.WriteLine(respuesta);
        }
        else
        {
            Console.WriteLine("Error en la solicitud. C�digo de estado: " + response.StatusCode);
        }
    }

    private async void btnTomarF_Clicked(object sender, EventArgs e)
    {
        
            var photo = await MediaPicker.CapturePhotoAsync();

            var customFileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            if (photo != null)
            {
            var memoriaStream = await photo.OpenReadAsync();
            pacienteF.Source = ImageSource.FromStream(() => memoriaStream);
            string cacheDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                filePath = Path.Combine(cacheDirectory, customFileName);

                using Stream photoStream = await photo.OpenReadAsync();
                using FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await photoStream.CopyToAsync(fileStream);
                
                Console.WriteLine($"Foto guardada en: {filePath}");
            
        }
    }

    private async void btnSeleccionarF_Clicked(object sender, EventArgs e)
    {
        var foto = await MediaPicker.PickPhotoAsync();

        if (foto != null)
        {
            var customFileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            var memoriaStream = await foto.OpenReadAsync();
            pacienteF.Source = ImageSource.FromStream(() => memoriaStream);
            string cacheDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            filePath = Path.Combine(cacheDirectory, customFileName);

            using Stream photoStream = await foto.OpenReadAsync();
            using FileStream fileStream = new FileStream(filePath, FileMode.Create);
            await photoStream.CopyToAsync(fileStream);

            Console.WriteLine($"Foto guardada en: {filePath}");
        }
    }
}