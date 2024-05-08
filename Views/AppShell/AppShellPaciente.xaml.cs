namespace ProyectoFinal.Views.AppShell;

public partial class AppShellPaciente : Shell
{
	public AppShellPaciente()
	{
		InitializeComponent();
	}
    private void btnSalir_Clicked(object sender, EventArgs e)
    {
        var app = (App)Application.Current;
        app.CambiarShell();

    }
}