using Newtonsoft.Json;
using ProyectoFinal.Models;
using System.Collections.ObjectModel;

namespace ProyectoFinal.Views.Paciente;


public partial class VListPacientes : ContentPage
{
    List<PacienteModel> listaPacientes = new List<PacienteModel>();
    private  string url;
    private string ip;
    private readonly HttpClient paciente = new HttpClient();
    private ObservableCollection<PacienteModel> est;
    Config serverip = new Config();

    public VListPacientes()
    {

        ip = serverip.ipserver;
        url = "http://"+ip+"/APPS/Back/Controlador/controlador.php?ListaPaciente=true";
        InitializeComponent();
        ObtenerDatos();
      

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

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

        HttpClient paci = new HttpClient();
        var paciente = (PacienteModel)(sender as MenuItem).CommandParameter;
        bool result = await DisplayAlert("Eliminar", "¿Estás seguro de que deseas eliminar al paciente " + paciente.NombreCompleto + "?", "Sí", "Cancelar");

        if (result)
        {
            string url = "http://" + ip + "/APPS/Back/Controlador/controlador.php?DeletePaciente=true";

            var parametros = new Dictionary<string, string>
            {
                { "id",paciente.Id.ToString() }
            };

            var content = new FormUrlEncodedContent(parametros);
            var response = await paci.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var respuesta = await response.Content.ReadAsStringAsync();
                if (respuesta == "1")
                {
                    await DisplayAlert("Eliminacion", "Registro Eliminado", "ok");

                    ObtenerDatos();
                }
                Console.WriteLine(respuesta);
            }
            else
            {
                Console.WriteLine("Error en la solicitud. Código de estado: " + response.StatusCode);
            }
        }
    }

    private void listaPaciente_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        PacienteModel paciente = e.SelectedItem as PacienteModel;
        var app = (App)Application.Current;
        
        app.CambiarShell("Paciente", paciente);
    }

    private void btnUpdate_Clicked(object sender, EventArgs e)
    {
        var paciente = (PacienteModel)(sender as MenuItem).CommandParameter;
        Navigation.PushAsync(new VFormPaciente(paciente));
    }
}