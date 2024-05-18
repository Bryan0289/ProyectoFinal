using ProyectoFinal.Models;

namespace ProyectoFinal
{
    public partial class App : Application
    {
        public static PacienteModel Paciente{ get; set; }
        public App()
        {
            InitializeComponent();

            CambiarShell();
        }
        public async void CambiarShell(string parametro = null, PacienteModel paciente = null)
        {
            if (parametro != null)
            {
                Paciente = paciente;
                Application.Current.MainPage = new NavigationPage(new Views.AppShell.AppShellPaciente());
            }
            else {
                Paciente = null;
                Application.Current.MainPage = new Views.AppShell.AppShell();
            }
        }
    }
}
