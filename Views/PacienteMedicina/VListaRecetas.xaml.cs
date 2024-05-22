using Newtonsoft.Json;
using ProyectoFinal.Models;
using System.Collections.ObjectModel;

namespace ProyectoFinal.Views.PacienteMedicina;

public partial class VListaRecetas : ContentPage
{

    public PacienteModel Paciente { get; set; }
    List<Recordatorio> listarRecordatorio  = new List<Recordatorio>();
    private readonly HttpClient recordatorio = new HttpClient();

    Config serverip = new Config();
    private string ip;
    private string url;
    private ObservableCollection<Recordatorio> est;
    public VListaRecetas()
	{
        ip = serverip.ipserver;
        InitializeComponent();
        Paciente = App.Paciente;
        var id = Paciente.Id;
        ObtenerDatos(id);

/*
        listarRecordatorio.Add(new Recordatorio
        {
            Id = 1,
            IdPaciente = 101,
            Paciente = new PacienteModel { Id = 101, Nombre = "Juan", Apellido = "Pérez" },
            IdMedicamento = 201,
            Medicina = new MedicinaModel { Id = 201, Nombre = "Ibuprofeno" },
            FechaInicio = DateTime.Today,
            Frecuencia = TimeSpan.FromHours(8),
            DuracionDias = 7, 
            Activo = true
        });
        listarRecordatorio.Add(new Recordatorio
        {
            Id = 2,
            IdPaciente = 102,
            Paciente = new PacienteModel { Id = 102, Nombre = "María", Apellido = "Gómez" },
            IdMedicamento = 202,
            Medicina = new MedicinaModel { Id = 202, Nombre = "Paracetamol" },
            FechaInicio = DateTime.Today.AddDays(-1),  // Fecha de inicio de ayer
            Frecuencia = TimeSpan.FromHours(12),
            DuracionDias = 5,
            Activo = true
        });

        // Agregar el tercer ejemplo
        listarRecordatorio.Add(new Recordatorio
        {
            Id = 3,
            IdPaciente = 103,
            Paciente = new PacienteModel { Id = 103, Nombre = "Carlos", Apellido = "López" },
            IdMedicamento = 203,
            Medicina = new MedicinaModel { Id = 203, Nombre = "Amoxicilina" },
            FechaInicio = DateTime.Today.AddDays(-2),  // Fecha de inicio de hace dos días
            Frecuencia = TimeSpan.FromHours(6),
            DuracionDias = 10,
            Activo = false  // Este recordatorio está inactivo
        });

        listaReceta.ItemsSource = listarRecordatorio;
    */

    }

    public async void ObtenerDatos(int id)
    {
        string url = "http://" + ip + "/APPS/Back/Controlador/controlador.php?ListaDosis=true&Id=" + id;
        var content = await recordatorio.GetStringAsync(url);
        List<Recordatorio> mostra = JsonConvert.DeserializeObject<List<Recordatorio>>(content);
        est = new ObservableCollection<Recordatorio>(mostra);
        listaReceta.ItemsSource = est;
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        HttpClient recor = new HttpClient();
        var recordatorio = (Recordatorio)(sender as MenuItem).CommandParameter;
        bool result = await DisplayAlert("Eliminar", "¿Estás seguro de que deseas eliminar la receta" + recordatorio.Medicina.Nombre + "?", "Sí", "Cancelar");

        if (result)
        {
            string url = "http://" + ip + "/APPS/Back/Controlador/controlador.php?DeleteDosis=true";

            var parametros = new Dictionary<string, string>
            {
                { "id",recordatorio.Id.ToString() }
            };

            var content = new FormUrlEncodedContent(parametros);
            var response = await recor.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var respuesta = await response.Content.ReadAsStringAsync();
                if (respuesta == "1")
                {
                    await DisplayAlert("Eliminado", "Registro Eliminado", "ok");
                    var id = recordatorio.Paciente.Id;
                    ObtenerDatos(id);
                }
                Console.WriteLine(respuesta);
            }
            else
            {
                Console.WriteLine("Error en la solicitud. Código de estado: " + response.StatusCode);
            }
        }

    }
}