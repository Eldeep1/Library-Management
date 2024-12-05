namespace Library.Views

open Avalonia.Controls
open Avalonia.Markup.Xaml
open Library.ViewModels
//open Avalonia.Interactivity

type ReportsView() as this = 
    inherit UserControl()

    do 
        this.InitializeComponent()
        this.DataContext<-ReportsViewModel()

    member private this.InitializeComponent() =
        AvaloniaXamlLoader.Load(this)

   