using ProyectoFinal.Models;

namespace ProyectoFinal.Views.Paciente;


public partial class VListPacientes : ContentPage
{
    List<PacienteModel> listaPacientes = new List<PacienteModel>();

    
public VListPacientes()
	{
		InitializeComponent();
        listaPacientes.Add(new PacienteModel { Nombre = "Paciente 1",Apellido="Paciente 1", FechaNac=DateTime.Today });
        listaPacientes.Add(new PacienteModel { Nombre = "Paciente 2", Apellido = "Paciente 2", FechaNac = new DateTime() });
        listaPacientes.Add(new PacienteModel { Nombre = "Paciente 2", Apellido = "Paciente 2", FechaNac = new DateTime() });

        ListarPacientes.ItemsSource = listaPacientes;

    }

    private async void btnNewPaciente_Clicked(object sender, EventArgs e)
    { 
        Navigation.PushAsync(new VFormPaciente());
    }

    private void btnVer_Clicked(object sender, EventArgs e)
    {
        var app = (App)Application.Current;
        app.CambiarShell("Paciente");

    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var paciente = swipeItem.BindingContext as PacienteModel;
        bool result = await DisplayAlert("Eliminar", "¿Estás seguro de que deseas eliminar al paciente " + paciente.NombreCompleto + "?", "Sí", "Cancelar");

        if (result)
        {
            DisplayAlert("Ok","Eliminado","ok");
        }

    }
}