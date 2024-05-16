using Newtonsoft.Json;
using ProyectoFinal.Models;
using System.Collections.ObjectModel;

namespace ProyectoFinal.Views.Medicina;



public partial class VListaMedicinas : ContentPage
{
    List<MedicinaModel> listaMedicina = new List<MedicinaModel>();
    private const string url = "http://192.168.100.12/APPS/Back/Controlador/controlador.php?ListaMedicina=true";
    private readonly HttpClient medicina = new HttpClient();
    private ObservableCollection<MedicinaModel> est;


    public VListaMedicinas()
	{
		InitializeComponent();
        ObtenerDatos();
            /*
        listaMedicina.Add(new MedicinaModel
        {
            Nombre = "medicina1",
            Descripcion = "Descripción de la medicina1",
            Dosificacion = "Dosificación de la medicina",
            Presentacion = "Presentación de la medicina",
            Indicaciones = "Indicaciones de la medicina"
        });

        ListarMedicina.ItemsSource = listaMedicina; */
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

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        HttpClient med = new HttpClient();
        Button btnEliminar = (Button)sender;
        int id = (int)btnEliminar.CommandParameter;
        var swipeItem = sender as SwipeItem;
        var medicina = swipeItem.BindingContext as MedicinaModel;
        bool result = await DisplayAlert("Eliminar", "¿Estás seguro de que deseas eliminar la Medicina" + medicina.Nombre+ "?", "Sí", "Cancelar");

        if (result)
        {
            string url = "http://192.168.100.12/APPS/Back/Controlador/controlador.php?DeleteMedicina=true";
           
                var parametros = new Dictionary<string, string>
                {
                    { "id",id.ToString() }
                };

                var content = new FormUrlEncodedContent(parametros);
                var response = await med.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var respuesta = await response.Content.ReadAsStringAsync();
                    if (respuesta == "1")
                    {
                    await            DisplayAlert("Ok", "Eliminado", "ok");

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