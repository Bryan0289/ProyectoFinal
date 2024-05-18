using Newtonsoft.Json;
using ProyectoFinal.Models;
using System.Collections.ObjectModel;

namespace ProyectoFinal.Views.Paciente;


public partial class VListPacientes : ContentPage
{
    List<PacienteModel> listaPacientes = new List<PacienteModel>();
    private const string url = "http://192.168.100.19/APPS/Back/Controlador/controlador.php?ListaPaciente=true";
    private readonly HttpClient paciente = new HttpClient();
    private ObservableCollection<PacienteModel> est;

    public VListPacientes()
	{
		InitializeComponent();
        ObtenerDatos();
      

    }
    public async void ObtenerDatos()
    {
        var content = await paciente.GetStringAsync(url);
        List<PacienteModel> mostra = JsonConvert.DeserializeObject<List<PacienteModel>>(content);
        est = new ObservableCollection<PacienteModel>(mostra);
        listaPaciente.ItemsSource = est;
    }

    private async void btnNewPaciente_Clicked(object sender, EventArgs e)
    { 
        Navigation.PushAsync(new VFormPaciente());
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {


        var paciente = (PacienteModel)(sender as MenuItem).CommandParameter;
        bool result = await DisplayAlert("Eliminar", "¿Estás seguro de que deseas eliminar al paciente " + paciente.NombreCompleto + "?", "Sí", "Cancelar");

        if (result)
        {
            DisplayAlert("Ok", "Eliminado", "ok");
        }
    }

    private void listaPaciente_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        PacienteModel paciente = e.SelectedItem as PacienteModel;
        var app = (App)Application.Current;
        
        app.CambiarShell("Paciente", paciente);
    }
}