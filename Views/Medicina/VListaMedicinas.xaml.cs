namespace ProyectoFinal.Views.Medicina;


public class Medicina
{
    public string Name { get; set; }
};
public partial class VListaMedicinas : ContentPage
{
    List<Medicina> listaMedicina = new List<Medicina>();

    public VListaMedicinas()
	{
		InitializeComponent();
        listaMedicina.Add(new Medicina{ Name = "Medicina 1" });
        listaMedicina.Add(new Medicina{ Name = "Medicina 2" });
        listaMedicina.Add(new Medicina{ Name = "Medicina 3" });

        ListarMedicina.ItemsSource = listaMedicina;
    }

    private void btnNewMedicina_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new VFormMedicina());
    }
}