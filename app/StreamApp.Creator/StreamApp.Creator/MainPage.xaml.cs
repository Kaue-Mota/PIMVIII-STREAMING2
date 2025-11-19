using System;

namespace StreamApp.Creator;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCadastrarConteudoClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CadastrarConteudoPage));
    }

    private async void OnMeusConteudosClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MeusConteudosPage));
    }

    private async void OnMinhasPlaylistsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MinhasPlaylistsPage));
    }

    private async void OnEstatisticasClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(EstatisticasPage));
    }
}
