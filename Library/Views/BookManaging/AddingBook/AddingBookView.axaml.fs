namespace Library.Views
open System.Diagnostics
open Avalonia.Controls
open Avalonia.Markup.Xaml
open Library.ViewModels
open Library.Models
open Avalonia.Interactivity

type AddBookView() as this =
    inherit Window() 
    do
        this.InitializeComponent()
        this.DataContext <- AddBookViewModel()

    member private this.InitializeComponent() =
        AvaloniaXamlLoader.Load(this)

    member this.OnAddButtonClick(sender: obj, e: RoutedEventArgs) =
        let bookName = this.FindControl<TextBox>("bookName").Text
        //just add a user to the database

        this.Close()