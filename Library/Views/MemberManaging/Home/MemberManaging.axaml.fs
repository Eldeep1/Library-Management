
namespace Library.Views

open Avalonia.Controls
open Avalonia.Markup.Xaml
open Library.ViewModels
open System.Diagnostics
open Avalonia.Interactivity
open Library.Models

type MemberManagingView() as this = 
    inherit UserControl()

    do 
        this.InitializeComponent()
        this.DataContext<-MemberManagingViewModel()

    member private this.InitializeComponent() =
        AvaloniaXamlLoader.Load(this)
        

    member this.EditMember (sender: obj) (e: RoutedEventArgs) =
        let button = sender :?> Button
        let user = button.Tag :?> Member

        let window = Library.Views.EditingMemberView(user)
        window.Show() 

    // Do something with code
    member this.RefreshView(sender: obj, e: RoutedEventArgs) =
        AvaloniaXamlLoader.Load(this)
        
        
