using ProyectoFinal.Models;

namespace ProyectoFinal.Views.Medicina;



public partial class VListaMedicinas : ContentPage
{
    List<MedicinaModel> listaMedicina = new List<MedicinaModel>();

    public VListaMedicinas()
	{
		InitializeComponent();
        listaMedicina.Add(new MedicinaModel
        {
            Nombre = "medicina1",
            Descripcion = "Descripción de la medicina1",
            Dosificacion = "Dosificación de la medicina",
            Presentacion = "Presentación de la medicina",
            Indicaciones = "Indicaciones de la medicina"
        });

        ListarMedicina.ItemsSource = listaMedicina;
    }

    private void btnNewMedicina_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new VFormMedicina());
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var medicina = swipeItem.BindingContext as MedicinaModel;
        bool result = await DisplayAlert("Eliminar", "¿Estás seguro de que deseas eliminar la Medicina" + medicina.Nombre+ "?", "Sí", "Cancelar");

        if (result)
        {
            DisplayAlert("Ok", "Eliminado", "ok");
        }

    }
}