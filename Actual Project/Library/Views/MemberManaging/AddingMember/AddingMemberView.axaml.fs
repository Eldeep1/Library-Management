namespace Library.Views
open System.Diagnostics
open Avalonia.Controls
open Avalonia.Markup.Xaml
open Library.ViewModels
open Library.Services
open Avalonia.Interactivity
open System
open System.Collections.Generic

type AddMemberView() as this =
    inherit Window() 
    do
        this.InitializeComponent()
        this.DataContext <- AddMemberViewModel()

    member private this.InitializeComponent() =
        AvaloniaXamlLoader.Load(this)

    member this.OnAddButtonClick(sender: obj, e: RoutedEventArgs) =

        let userName = this.FindControl<TextBox>("userName").Text
        let userEmail = this.FindControl<TextBox>("userEmail").Text
        let userPhone = this.FindControl<TextBox>("userPhone").Text

        // Check if any of the fields are null or empty
        if String.IsNullOrWhiteSpace(userName) || String.IsNullOrWhiteSpace(userEmail) || String.IsNullOrWhiteSpace(userPhone) then
            // Do nothing if any field is null or empty
            ()
        //just add a user to the database

        else
            let values = Dictionary<string, obj>()
            values.Add("Name", userName)
            values.Add("Email", userEmail)
            values.Add("Phone", userPhone)

            let success = DatabaseConnection.Instance.Insert("Member", values)
            if success then
                
                this.Close()
            else
                // Handle the case where the insert was not successful
                Debug.WriteLine(sprintf "Failed to add the Member to the database.")



