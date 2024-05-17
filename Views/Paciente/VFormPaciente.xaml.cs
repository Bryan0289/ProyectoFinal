namespace ProyectoFinal.Views.Paciente;

public partial class VFormPaciente : ContentPage
{
	public VFormPaciente()
	{
		InitializeComponent();
	}

    private async void btnTomarF_Clicked(object sender, EventArgs e)
    {
        var foto = await MediaPicker.CapturePhotoAsync();

        if (foto != null)
        {
            var memoriaStream = await foto.OpenReadAsync();
            pacienteImg.Source = ImageSource.FromStream(() => memoriaStream);
        }
    }

    private async void btnSeleccionarF_Clicked(object sender, EventArgs e)
    {
        var foto = await MediaPicker.PickPhotoAsync();

        if (foto != null)
        {
            var memoriaStream = await foto.OpenReadAsync();
            pacienteImg.Source = ImageSource.FromStream(() => memoriaStream);
        }
    }
}