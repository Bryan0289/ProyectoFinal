using Newtonsoft.Json;
using ProyectoFinal.Models;
using ProyectoFinal.Views.Paciente;
using System;
using System.Collections.ObjectModel;

namespace ProyectoFinal.Views.PacienteMedicina;

public partial class VFormReceta : ContentPage
{
    private string ip;
    private string url;
    private string url2;
    private string url3;
    List<MedicinaModel> listaMedicina = new List<MedicinaModel>();
    List<PacienteModel> listaPacientes = new List<PacienteModel>();
    Config serverip = new Config();

    private readonly HttpClient medicina = new HttpClient();
    private ObservableCollection<PacienteModel> est;
    private ObservableCollection<MedicinaModel> est2;
    public VFormReceta()
	{

        ip = serverip.ipserver;
        url = "http://" + ip + "/APPS/Back/Controlador/controlador.php?ListaPaciente=true";
        url2 = "http://" + ip + "/APPS/Back/Controlador/controlador.php?ListaMedicina=true";
        url3 = "http://" + ip +"/APPS/Back/Controlador/controlador.php?AddDosis=true";
        InitializeComponent();
        ObtenerDatos();
        ObtenerDatosM();


    }
    public async void ObtenerDatos()
    {
        var content = await medicina.GetStringAsync(url);
        List<PacienteModel> mostra = JsonConvert.DeserializeObject<List<PacienteModel>>(content);
        est = new ObservableCollection<PacienteModel>(mostra);
        pkUsuarios.ItemsSource = est;
    }
    public async void ObtenerDatosM()
    {
        var content = await medicina.GetStringAsync(url2);
        List<MedicinaModel> mostra = JsonConvert.DeserializeObject<List<MedicinaModel>>(content);
        est2 = new ObservableCollection<MedicinaModel>(mostra);
        pkMedicina.ItemsSource = est2;
    }

    private async void btnGuardar_Clicked(object sender, EventArgs e)
    {
        var index_pa = pkUsuarios.SelectedIndex;
        var index_me = pkMedicina.SelectedIndex;
        if (index_pa != -1 && index_me!=-1)
        {
            var pa = est[index_pa];
            var me = est2[index_me];
            var hora = txtHoraInicio.Text;
            var intervalo = txtIntervalo.Text;
            var duracion = txtDuracionDias.Text;
            var parametros = new Dictionary<string, string>
            {
                { "paciente",pa.Id.ToString() },
                { "medicina", me.Id.ToString()},
                { "hora", hora },
                { "intervalo", intervalo },
                { "duracion", duracion},
            };

            var content = new FormUrlEncodedContent(parametros);
            var response = await medicina.PostAsync(url3, content);

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