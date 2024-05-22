using ProyectoFinal.Models;
using ProyectoFinal.Views.Paciente;

namespace ProyectoFinal.Views.Medicina;

public partial class VFormMedicina : ContentPage
{

    private string ip;
    private string url;
    private string filePath;
    private readonly HttpClient paciente = new HttpClient();
    Config serverip = new Config();
    public VFormMedicina()
	{
        ip = serverip.ipserver;
        InitializeComponent();
	}

    private async  void btnGuardar_Clicked(object sender, EventArgs e)
    {
		var nombre = txtNombre.Text;
		var des = txtDescription.Text;
		var dosis = txtDosificacion.Text;
		var prese = txtPresentacion.Text;

        string url = "http://"+ip+"/APPS/Back/Controlador/controlador.php?AddMedicina=true";
		try
		{
            var parametros = new Dictionary<string, string>
            {
                { "nombre",nombre },
                { "descripcion",des},
                { "dosis", dosis },
                { "presentacion", prese },
                { "foto", filePath },
            };

            var content = new FormUrlEncodedContent(parametros);
            var response = await paciente.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var respuesta = await response.Content.ReadAsStringAsync();
                if (respuesta == "1")
                {
                    await DisplayAlert("Guardado", "Medicina registrada", "OK");
                    await Navigation.PushAsync(new VListaMedicinas());
                }
                Console.WriteLine(respuesta);
            }
            else
            {
                Console.WriteLine("Error en la solicitud. Código de estado: " + response.StatusCode);
            }

        }
		catch (Exception ex)
		{

			throw;
		}

    }

    private async void btnTomarF_Clicked(object sender, EventArgs e)
    {
        var photo = await MediaPicker.CapturePhotoAsync();

        var customFileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        if (photo != null)
        {
            var memoriaStream = await photo.OpenReadAsync();
            medicinaF.Source = ImageSource.FromStream(() => memoriaStream);
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
            medicinaF.Source = ImageSource.FromStream(() => memoriaStream);
            string cacheDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            filePath = Path.Combine(cacheDirectory, customFileName);

            using Stream photoStream = await foto.OpenReadAsync();
            using FileStream fileStream = new FileStream(filePath, FileMode.Create);
            await photoStream.CopyToAsync(fileStream);

            Console.WriteLine($"Foto guardada en: {filePath}");
        }
    }
}