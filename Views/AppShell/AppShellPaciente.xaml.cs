using ProyectoFinal.Models;

namespace ProyectoFinal.Views.AppShell;

public partial class AppShellPaciente : Shell
{
    public PacienteModel Paciente { get; set; }
	public AppShellPaciente()
	{
		InitializeComponent();
        Paciente = App.Paciente;
        lblNombre.Text = Paciente.NombreCompleto;
    }
    private void btnSalir_Clicked(object sender, EventArgs e)
    {
        var app = (App)Application.Current;
        app.CambiarShell();

    }
}