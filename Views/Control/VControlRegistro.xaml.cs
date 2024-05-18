using Newtonsoft.Json;
using ProyectoFinal.Models;
using ProyectoFinal.Views.Paciente;
using System.Collections.ObjectModel;
using System.Net;

namespace ProyectoFinal.Views.Control;

public partial class VControlRegistro : ContentPage
{
    List<PacienteModel> listaPacientes = new List<PacienteModel>();
    private const string url = "http://192.168.100.19/APPS/Back/Controlador/controlador.php?ListaPaciente=true";
    private readonly HttpClient paciente = new HttpClient();
    private ObservableCollection<PacienteModel> est;
    public VControlRegistro()
	{
		InitializeComponent();
        ObtenerDatos();

    }

    public async void ObtenerDatos()
    {
        var content = await paciente.GetStringAsync(url);
        List<PacienteModel> mostra = JsonConvert.DeserializeObject<List<PacienteModel>>(content);
        est = new ObservableCollection<PacienteModel>(mostra);
        pkPaciente.ItemsSource = est;
    }

    private async  void btnGuardar_Clicked(object sender, EventArgs e)
    {
        string url = "http://192.168.100.12/APPS/Back/Controlador/controlador.php?AddTriaje=true";
        var id_paciente = pkPaciente.SelectedIndex;
        if (id_paciente != -1)
        {
            var pacie = est[id_paciente];
            var insulina = txtInsulina.Text;
            var presion = txtArterial.Text;
            DisplayAlert("alert", pacie.Id.ToString(), "ok");
            var parametros = new Dictionary<string, string>
            {
                { "paciente",pacie.Id.ToString() },
                { "insulina", insulina},
                { "presion", presion },
            };

            var content = new FormUrlEncodedContent(parametros);
            var response = await paciente.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var respuesta = await response.Content.ReadAsStringAsync();
                if (respuesta == "1")
                {
                    await DisplayAlert("Alerta", "Datos registrados", "OK");
                    await Navigation.PushAsync(new VListPacientes());
                }
                Console.WriteLine(respuesta);
            }
            else
            {
                Console.WriteLine("Error en la solicitud. Código de estado: " + response.StatusCode);
            }

        }
        else
        {
            DisplayAlert("alert", "Seleccione paciente", "ok");
        }

    }
}