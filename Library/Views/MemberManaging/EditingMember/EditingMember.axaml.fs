namespace Library.Views
open System.Diagnostics
open Avalonia.Controls
open Avalonia.Markup.Xaml
open Library.ViewModels
open Library.Models
open Avalonia.Interactivity
type EditingMemberView(user:Member) as this =
    inherit Window() // Inherit from Window instead of UserControl
    do
        this.InitializeComponent()
        this.DataContext <- EditingMemberViewModel(user)
        //this.Closed.Add(fun _ ->MemberManagingViewModel.GetMembersData(MemberManagingViewModel.Users))
    member private this.InitializeComponent() =
        AvaloniaXamlLoader.Load(this)

    member this.OnCloseButtonClick(sender: obj, e: RoutedEventArgs) =
        user.Name<-"hema"
        let userName = this.FindControl<TextBox>("userName").Text
        //update database, then update user
        //updating the database:

        //updating the user:
        user.Name<-userName
        user.Phone<-this.FindControl<TextBox>("userPhone").Text
        user.Email<-this.FindControl<TextBox>("userEmail").Text

        // code to save the new changes to the database
        this.Close()