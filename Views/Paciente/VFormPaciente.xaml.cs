using Newtonsoft.Json;

namespace ProyectoFinal.Views.Paciente;

public partial class VFormPaciente : ContentPage
{
    private const string url = "http://192.168.100.19/APPS/Back/Controlador/controlador.php?AddPaciente=true";
    private readonly HttpClient paciente = new HttpClient();

    public VFormPaciente()
	{
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
                { "detalle", detalle }
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
            Console.WriteLine("Error en la solicitud. Código de estado: " + response.StatusCode);
        }
    }

    private async void btnTomarF_Clicked(object sender, EventArgs e)
    {
        var foto = await MediaPicker.CapturePhotoAsync();

        if (foto != null)
        {
            var memoriaStream = await foto.OpenReadAsync();
            pacienteF.Source = ImageSource.FromStream(() => memoriaStream);
        }
    }

    private async void btnSeleccionarF_Clicked(object sender, EventArgs e)
    {
        var foto = await MediaPicker.PickPhotoAsync();

        if (foto != null)
        {
            var memoriaStream = await foto.OpenReadAsync();
            pacienteF.Source = ImageSource.FromStream(() => memoriaStream);
        }
    }
}