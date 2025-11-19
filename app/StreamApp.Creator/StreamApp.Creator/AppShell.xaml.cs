using StreamApp.Creator;

namespace StreamApp.Creator;
public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(CadastrarConteudoPage), typeof(CadastrarConteudoPage));
        Routing.RegisterRoute(nameof(MeusConteudosPage), typeof(MeusConteudosPage));
        Routing.RegisterRoute(nameof(MinhasPlaylistsPage), typeof(MinhasPlaylistsPage));
        Routing.RegisterRoute(nameof(EstatisticasPage), typeof(EstatisticasPage));
    }
}
