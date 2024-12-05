namespace Library.Views
open System.Diagnostics
open Avalonia.Controls
open Avalonia.Markup.Xaml
open Library.ViewModels
open Library.Models
open Avalonia.Interactivity
open System
open System.Collections.Generic
open Library.Services

type EditingMemberView(user:Member) as this =
    inherit Window() // Inherit from Window instead of UserControl
    do
        this.InitializeComponent()
        this.DataContext <- EditingMemberViewModel(user)
        //this.Closed.Add(fun _ ->MemberManagingViewModel.GetMembersData(MemberManagingViewModel.Users))
    member private this.InitializeComponent() =
        AvaloniaXamlLoader.Load(this)



    member this.OnEditButtonClick(sender: obj, e: RoutedEventArgs) =


        let userName = this.FindControl<TextBox>("userName").Text
        let userPhone = this.FindControl<TextBox>("userPhone").Text
        let userEmail = this.FindControl<TextBox>("userEmail").Text


        if String.IsNullOrWhiteSpace(userName) || String.IsNullOrWhiteSpace(userPhone) || String.IsNullOrWhiteSpace(userEmail) then
            // Do nothing if any field is null or empty
            ()
        else
            let values = Dictionary<string, obj>()
            values.Add("Name", userName)
            values.Add("Email", userEmail)
            values.Add("Phone", userPhone)
            let conditions = Dictionary<string, obj>()
            conditions.Add("ID", user.ID)

            let success = DatabaseConnection.Instance.Update("Member", values, conditions)
            if success then
                    // Optionally, provide feedback to the user
                    this.Close()
                    Debug.WriteLine(sprintf "Member details updated successfully.")

            else
                    // Handle the case where the update was not successful
                    Debug.WriteLine(sprintf "Failed to update the Member details.")



