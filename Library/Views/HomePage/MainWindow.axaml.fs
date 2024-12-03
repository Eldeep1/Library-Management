namespace Library.Views.HomePage

open Avalonia
open Avalonia.Controls
open Avalonia.Markup.Xaml
open Library.ViewModels
type MainWindow() as this = 
    inherit Window ()

    do 
        this.InitializeComponent()

    member private this.InitializeComponent() =
        this.DataContext <- MainViewModel()

#if DEBUG
        this.AttachDevTools()
#endif
        AvaloniaXamlLoader.Load(this)
