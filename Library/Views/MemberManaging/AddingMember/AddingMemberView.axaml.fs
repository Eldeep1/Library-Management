namespace Library.Views
open System.Diagnostics
open Avalonia.Controls
open Avalonia.Markup.Xaml
open Library.ViewModels
open Library.Models
open Avalonia.Interactivity

type AddMemberView() as this =
    inherit Window() 
    do
        this.InitializeComponent()
        this.DataContext <- AddMemberViewModel()

    member private this.InitializeComponent() =
        AvaloniaXamlLoader.Load(this)

    member this.OnAddButtonClick(sender: obj, e: RoutedEventArgs) =
        let userName = this.FindControl<TextBox>("userName").Text
        //just add a user to the database

        this.Close()