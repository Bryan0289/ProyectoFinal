using Newtonsoft.Json;
using ProyectoFinal.Models;
using System.Collections.ObjectModel;

namespace ProyectoFinal.Views.Medicina;



public partial class VListaMedicinas : ContentPage
{
    List<MedicinaModel> listaMedicina = new List<MedicinaModel>();
    private readonly HttpClient medicina = new HttpClient();
    private ObservableCollection<MedicinaModel> est;
    Config serverip = new Config();
    private string ip;
    private string url;


    public VListaMedicinas()
    {
        ip = serverip.ipserver;
        url = "http://"+ip+"/APPS/Back/Controlador/controlador.php?ListaMedicina=true";
        InitializeComponent();
        ObtenerDatos();
           
    }

    public async void ObtenerDatos()
    {
        var content = await medicina.GetStringAsync(url);
        List<MedicinaModel> mostra = JsonConvert.DeserializeObject<List<MedicinaModel>>(content);
        est = new ObservableCollection<MedicinaModel>(mostra);
        ListarMedicina.ItemsSource = est;
    }

    private void btnNewMedicina_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new VFormMedicina());
    }
    private async void btn_Delete_Clicked(object sender, EventArgs e)
    {
        HttpClient med = new HttpClient();
        

        var medicina =(MedicinaModel)(sender as MenuItem).CommandParameter;

        bool result = await DisplayAlert("Eliminar", "¿Estás seguro de que deseas eliminar la Medicina" + medicina.Nombre + "?", "Sí", "Cancelar");

        if (result)
        {
            string url = "http://"+ip+"/APPS/Back/Controlador/controlador.php?DeleteMedicina=true";

            var parametros = new Dictionary<string, string>
            {
                { "id",medicina.Id.ToString() }
            };

            var content = new FormUrlEncodedContent(parametros);
            var response = await med.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var respuesta = await response.Content.ReadAsStringAsync();
                if (respuesta == "1")
                {
                    await DisplayAlert("Ok", "Eliminado", "ok");

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
}