using Newtonsoft.Json;
using ProyectoFinal.Models;
using System.Collections.ObjectModel;

namespace ProyectoFinal.Views.Paciente;


public partial class VListPacientes : ContentPage
{
    List<PacienteModel> listaPacientes = new List<PacienteModel>();
    private const string url = "http://192.168.100.12/APPS/Back/Controlador/controlador.php?ListaPaciente=true";
    private readonly HttpClient paciente = new HttpClient();
    private ObservableCollection<PacienteModel> est;

    public VListPacientes()
	{
		InitializeComponent();
        ObtenerDatos();
        /*
        listaPacientes.Add(new PacienteModel { Nombre = "Paciente 1",Apellido="Paciente 1", FechaNac=DateTime.Today });
        listaPacientes.Add(new PacienteModel { Nombre = "Paciente 2", Apellido = "Paciente 2", FechaNac = new DateTime() });
        listaPacientes.Add(new PacienteModel { Nombre = "Paciente 2", Apellido = "Paciente 2", FechaNac = new DateTime() });

        ListarPacientes.ItemsSource = listaPacientes;*/

    }
    public async void ObtenerDatos()
    {
        var content = await paciente.GetStringAsync(url);
        List<PacienteModel> mostra = JsonConvert.DeserializeObject<List<PacienteModel>>(content);
        est = new ObservableCollection<PacienteModel>(mostra);
        ListarPacientes.ItemsSource = est;
    }

    private async void btnNewPaciente_Clicked(object sender, EventArgs e)
    { 
        Navigation.PushAsync(new VFormPaciente());
    }

    private void btnVer_Clicked(object sender, EventArgs e)
    {
        var app = (App)Application.Current;
        app.CambiarShell("Paciente");

    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var paciente = swipeItem.BindingContext as PacienteModel;
        bool result = await DisplayAlert("Eliminar", "¿Estás seguro de que deseas eliminar al paciente " + paciente.NombreCompleto + "?", "Sí", "Cancelar");

        if (result)
        {
            DisplayAlert("Ok","Eliminado","ok");
        }

    }
}