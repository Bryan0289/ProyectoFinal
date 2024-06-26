using Newtonsoft.Json;
using ProyectoFinal.Models;
using ProyectoFinal.Views.Paciente;
using System.Collections.ObjectModel;
using System.Net;

namespace ProyectoFinal.Views.Control;

public partial class VControlRegistro : ContentPage
{
    List<PacienteModel> listaPacientes = new List<PacienteModel>();
    private readonly HttpClient paciente = new HttpClient();
    private ObservableCollection<PacienteModel> est;

    private string ip;
    private string url;
    Config serverip = new Config();
    public VControlRegistro()
	{

        ip = serverip.ipserver;
        url = "http://"+ip+"/APPS/Back/Controlador/controlador.php?ListaPaciente=true";
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
        if (string.IsNullOrWhiteSpace(txtInsulina.Text) ||
            string.IsNullOrWhiteSpace(txtArterial.Text) ||
            string.IsNullOrWhiteSpace(txtAltura.Text) ||
            string.IsNullOrWhiteSpace(txtPeso.Text))
        {
            await DisplayAlert("Error", "Por favor, rellena todos los campos.", "OK");
            return;
        }
        create();

    }
    private async void create()
    {
        string url = "http://" + ip + "/APPS/Back/Controlador/controlador.php?AddTriaje=true";
        var id_paciente = pkPaciente.SelectedIndex;
        if (id_paciente != -1)
        {
            var pacie = est[id_paciente];
            var insulina = txtInsulina.Text;
            var presion = txtArterial.Text;
            var altura = txtAltura.Text;
            var peso = txtPeso.Text;

            var parametros = new Dictionary<string, string>
            {
                { "paciente",pacie.Id.ToString() },
                { "insulina", insulina},
                { "presion", presion },
                { "altura", altura },
                { "peso",peso }
            };

            var content = new FormUrlEncodedContent(parametros);
            var response = await paciente.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var respuesta = await response.Content.ReadAsStringAsync();
                if (respuesta == "1")
                {
                    await DisplayAlert("Guardado", "Datos registrados", "OK");
                    Reset();
                }
                Console.WriteLine(respuesta);
            }
            else
            {
                Console.WriteLine("Error en la solicitud. C�digo de estado: " + response.StatusCode);
            }

        }
        else
        {
            DisplayAlert("alert", "Seleccione paciente", "ok");
        }
    }
    public void Reset()
    {
        pkPaciente.SelectedIndex = -1;
        txtInsulina.Text = string.Empty;
        txtArterial.Text = string.Empty;
        txtAltura.Text = string.Empty;
        txtPeso.Text = string.Empty;
    }
}