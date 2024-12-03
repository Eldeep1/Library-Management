namespace Library.Views

open Avalonia.Controls
open Avalonia.Markup.Xaml
open Library.ViewModels

type BookManagingView() as this = 
    inherit UserControl()

    do 
        this.InitializeComponent()
        this.DataContext <- BookManagingViewModel() // Set the DataContext to the ViewModel

    member private this.InitializeComponent() =
        AvaloniaXamlLoader.Load(this)