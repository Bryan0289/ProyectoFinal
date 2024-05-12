using ProyectoFinal.Models;

namespace ProyectoFinal.Views.PacienteMedicina;

public partial class VListaRecetas : ContentPage
{

    List<Recordatorio> listarRecordatorio
        = new List<Recordatorio>();
    public VListaRecetas()
	{
		InitializeComponent();
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

        ListarRecordatorio.ItemsSource = listarRecordatorio;

    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {


    }
}