using ProyectoFinal.Models;

namespace ProyectoFinal.Views.Control;

public partial class VListaPre : ContentPage
{
    public PacienteModel Paciente { get; set; }
    public VListaPre()
	{
		InitializeComponent();
        Paciente = App.Paciente;
    }
}