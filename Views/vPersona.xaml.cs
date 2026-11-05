using dquinonezS5.Models;

namespace dquinonezS5.Views;

public partial class vPersona : ContentPage
{
	public vPersona()
	{
		InitializeComponent();
	}

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        App._personaRepo.AddNewPerson(txtNombre.Text);
        lblMessage.Text = App._personaRepo.Message;


    }

    private void btnMostrar_Clicked(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        List<Persona> people = App._personaRepo.GetAllPersonas();
        listaPersonas.ItemsSource = people;


    }

    private async void btnEditar_Clicked(object sender, EventArgs e)
    {
        // Identificar el botón que se presionó
        Button botonPresionado = (Button)sender;

        // Extraer el objeto Persona vinculado a esa fila
        Persona personaSeleccionada = (Persona)botonPresionado.BindingContext;

        if (personaSeleccionada != null)
        {
            // Mostrar un cuadro de diálogo para pedir el nuevo nombre, mostrando el nombre actual como sugerencia
            string nuevoNombre = await DisplayPromptAsync("Editar Persona",
                                                          "Ingresa el nuevo nombre:",
                                                          initialValue: personaSeleccionada.Name);

            // Verificar que el usuario no haya cancelado y que haya ingresado texto
            if (!string.IsNullOrWhiteSpace(nuevoNombre))
            {
                // Llamar al repositorio para actualizar en la base de datos
                App._personaRepo.UpdatePerson(personaSeleccionada.Id, nuevoNombre);

                // Mostrar el mensaje del repositorio
                lblMessage.Text = App._personaRepo.Message;

                // Refrescar la lista de la pantalla llamando al botón mostrar
                btnMostrar_Clicked(sender, e);
            }
        }
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        // Identificar el botón que se presionó
        Button botonPresionado = (Button)sender;

        // Extraer el objeto Persona vinculado a esa fila
        Persona persona = (Persona)botonPresionado.BindingContext;

        if (persona != null)
        {
            // Llamar al repositorio enviando el Id de la persona
            App._personaRepo.DeletePerson(persona.Id);
            lblMessage.Text = App._personaRepo.Message;

            // Refrescar la lista de la pantalla
            btnMostrar_Clicked(sender, e);
        }
    }
}