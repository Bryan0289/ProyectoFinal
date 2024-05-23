using Newtonsoft.Json;
using ProyectoFinal.Models;

namespace ProyectoFinal.Views.Paciente;

public partial class VFormPaciente : ContentPage
{
    private string ip;
    private int id;
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
    public VFormPaciente(PacienteModel paciente)
    {
        ip = serverip.ipserver;
        url = "http://" + ip + "/APPS/Back/Controlador/controlador.php?AddPaciente=true";
        InitializeComponent();
        id = paciente.Id;
        pacienteF.Source = paciente.Foto;
        txtNombre.Text = paciente.Nombre;
        txtApellido.Text = paciente.Apellido;
        dpFecha.Date= paciente.FechaNac;
        txtDetalle.Text= paciente.Detalle;
    }

    private async void btnGuardar_Clicked(object sender, EventArgs e)
    {

        if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
            string.IsNullOrWhiteSpace(txtApellido.Text) ||
            string.IsNullOrWhiteSpace(txtDetalle.Text))
        {
            await DisplayAlert("Error", "Por favor, rellena todos los campos.", "OK");
            
        }else
        {
            if (id > 0)
            {
                update();
            }
            else
            {
                create();
            }
        }

    }

    private async void create()
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
            if (respuesta == "1")
            {
                await DisplayAlert("Guardado", "Paciente registrado", "OK");
                await Navigation.PopToRootAsync();
            }
            Console.WriteLine(respuesta);
        }
        else
        {
            Console.WriteLine("Error en la solicitud. Código de estado: " + response.StatusCode);
        }
    }
    private async void update()
    {

        var nombre = txtNombre.Text;
        var apellido = txtApellido.Text;
        var FechaN = dpFecha.Date.ToString("yyyy-MM-dd");
        var detalle = txtDetalle.Text;

        url = "http://" + ip + "/APPS/Back/Controlador/controlador.php?UpdPaciente=true";
        var parametros = new Dictionary<string, string>
            {
                {"Id", id.ToString() },
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
            if (respuesta == "1")
            {
                await DisplayAlert("Editado", "Paciente Editado", "OK");
                await Navigation.PopToRootAsync();
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