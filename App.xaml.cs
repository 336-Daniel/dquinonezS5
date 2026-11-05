using Microsoft.Extensions.DependencyInjection;

namespace dquinonezS5
{
    public partial class App : Application
    {
        public static Repositories.PersonaRepository _personaRepo { get; set; }
        public App(Repositories.PersonaRepository personaRepository)
        {
            InitializeComponent();
            _personaRepo = personaRepository;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new Views.vPersona());
        }
    }
}