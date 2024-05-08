using ProyectoFinal.Views.AppShell;

namespace ProyectoFinal.Views.Paciente;

public class Paciente
{
    public string Name { get; set; }
};

public partial class VListPacientes : ContentPage
{
    List<Paciente> listaPacientes = new List<Paciente>();

    
public VListPacientes()
	{
		InitializeComponent();
    listaPacientes.Add(new Paciente { Name = "Paciente 1" });
        listaPacientes.Add(new Paciente { Name = "Paciente 2" });
listaPacientes.Add(new Paciente { Name = "Paciente 3" });

        ListarPacientes.ItemsSource = listaPacientes;

    }

    private void onChangeItem(object sender, SelectionChangedEventArgs e)
    {
        
        var app = (App)Application.Current;
        app.CambiarShell("Paciente");

    }

    private async void btnNewPaciente_Clicked(object sender, EventArgs e)
    {
        
        Navigation.PushAsync(new VFormPaciente());

    }
}