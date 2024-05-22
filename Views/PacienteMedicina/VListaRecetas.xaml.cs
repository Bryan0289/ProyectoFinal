using ProyectoFinal.Models;

namespace ProyectoFinal.Views.PacienteMedicina;

public partial class VListaRecetas : ContentPage
{

    public PacienteModel Paciente { get; set; }
    List<Recordatorio> listarRecordatorio
        = new List<Recordatorio>();
    public VListaRecetas()
	{
		InitializeComponent();
        Paciente = App.Paciente;
        listarRecordatorio.Add(new Recordatorio
        {
            Id = 1,
            IdPaciente = 101,
            Paciente = new PacienteModel { Id = 101, Nombre = "Juan", Apellido = "Pérez" },
            IdMedicamento = 201,
            Medicina = new MedicinaModel { Id = 201, Nombre = "Ibuprofeno" },
            FechaInicio = DateTime.Today,
            Frecuencia = TimeSpan.FromHours(8),
            DuracionDias = 7, 
            Activo = true
        });
        listarRecordatorio.Add(new Recordatorio
        {
            Id = 2,
            IdPaciente = 102,
            Paciente = new PacienteModel { Id = 102, Nombre = "María", Apellido = "Gómez" },
            IdMedicamento = 202,
            Medicina = new MedicinaModel { Id = 202, Nombre = "Paracetamol" },
            FechaInicio = DateTime.Today.AddDays(-1),  // Fecha de inicio de ayer
            Frecuencia = TimeSpan.FromHours(12),
            DuracionDias = 5,
            Activo = true
        });

        // Agregar el tercer ejemplo
        listarRecordatorio.Add(new Recordatorio
        {
            Id = 3,
            IdPaciente = 103,
            Paciente = new PacienteModel { Id = 103, Nombre = "Carlos", Apellido = "López" },
            IdMedicamento = 203,
            Medicina = new MedicinaModel { Id = 203, Nombre = "Amoxicilina" },
            FechaInicio = DateTime.Today.AddDays(-2),  // Fecha de inicio de hace dos días
            Frecuencia = TimeSpan.FromHours(6),
            DuracionDias = 10,
            Activo = false  // Este recordatorio está inactivo
        });

        listaReceta.ItemsSource = listarRecordatorio;

    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        var paciente = (PacienteModel)(sender as MenuItem).CommandParameter;
        bool result = await DisplayAlert("Eliminar", "¿Estás seguro de que deseas eliminar la receta" + paciente.NombreCompleto + "?", "Sí", "Cancelar");

        if (result)
        {
            DisplayAlert("Ok", "Eliminado", "ok");
        }

    }
}