namespace ProyectoFinal
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            CambiarShell();
        }
        public async void CambiarShell(string parametro = null)
        {
            if (parametro != null)
            {

                //Application.Current.MainPage = new Views.AppShell.AppShellPaciente();
                Application.Current.MainPage = new NavigationPage(new Views.AppShell.AppShellPaciente());
            }
            else { 
                
                Application.Current.MainPage = new Views.AppShell.AppShell();

            }

        }


    }
}
