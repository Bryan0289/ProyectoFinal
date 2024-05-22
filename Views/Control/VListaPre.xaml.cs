using Newtonsoft.Json;
using ProyectoFinal.Models;
using System.Collections.ObjectModel;

namespace ProyectoFinal.Views.Control;

public partial class VListaPre : ContentPage
{
    public PacienteModel Paciente { get; set; }

    private string ip;
    private string url;
    private readonly HttpClient triaje = new HttpClient();
    private ObservableCollection<TriajeModel> est;
    Config serverip = new Config();
    public VListaPre()
	{

        ip = serverip.ipserver;
        InitializeComponent();
        Paciente = App.Paciente;

        var id = Paciente.Id;
        ObtenerDatos(id);

        /*

        PacienteModel paciente1 = new PacienteModel { Id = 1, Nombre = "Juan Perez" };
        PacienteModel paciente2 = new PacienteModel { Id = 2, Nombre = "Maria Lopez" };

        List<TriajeModel> triajes = new List<TriajeModel>
        {
            new TriajeModel
            {
                Id = 1,
                NivelInsu = "Alta",
                PresionArt = 120,
                Fecha = DateTime.Now,
                PacienteId = 1,
                Paciente = paciente1
            },
            new TriajeModel
            {
                Id = 2,
                NivelInsu = "Media",
                PresionArt = 130,
                Fecha = DateTime.Now,
                PacienteId = 2,
                Paciente = paciente2
            }
        };
        listaControl.ItemsSource = triajes;

        */

    }

    public async void ObtenerDatos(int id)
    {
        string url = "http://" + ip + "/APPS/Back/Controlador/controlador.php?ListaTriaje=true&Id="+id;
        var content = await triaje.GetStringAsync(url);
        List<TriajeModel> mostra = JsonConvert.DeserializeObject<List<TriajeModel>>(content);
        est = new ObservableCollection<TriajeModel>(mostra);
        listaControl.ItemsSource = est;
    }

}