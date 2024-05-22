using ProyectoFinal.Models;

namespace ProyectoFinal.Views.Control;

public partial class VListaPre : ContentPage
{
    public PacienteModel Paciente { get; set; }
    public VListaPre()
	{
		InitializeComponent();
        Paciente = App.Paciente;
        


        PacienteModel paciente1 = new PacienteModel { Id = 1, Nombre = "Juan Perez" };
        PacienteModel paciente2 = new PacienteModel { Id = 2, Nombre = "Maria Lopez" };

        List<TriajeModel> triajes = new List<TriajeModel>
        {
            new TriajeModel
            {
                Id = 1,
                NivelInsu = "Alta",
                PresionArt = 120,
                Fecha = DateTime.Now,
                PacienteId = 1,
                Paciente = paciente1
            },
            new TriajeModel
            {
                Id = 2,
                NivelInsu = "Media",
                PresionArt = 130,
                Fecha = DateTime.Now,
                PacienteId = 2,
                Paciente = paciente2
            }
        };
        listaControl.ItemsSource = triajes;

    }

}