namespace Library.Views
open System.Diagnostics
open Avalonia.Controls
open Avalonia.Markup.Xaml
open Library.ViewModels
open Library.Models
open Avalonia.Interactivity

type EditingBookView(book:Book) as this =
    inherit Window() // Inherit from Window instead of UserControl

    do
        this.InitializeComponent()
        this.DataContext <- EditingBookViewModel(book)
        //this.Closed.Add(fun _ ->MemberManagingViewModel.GetMembersData(MemberManagingViewModel.Users))
    member private this.InitializeComponent() =
        AvaloniaXamlLoader.Load(this)

    
    member this.OnEditButtonClick(sender: obj, e: RoutedEventArgs) =
        book.Name<-"hema"

        let bookName = this.FindControl<TextBox>("bookName").Text
        //update database, then update user
        //updating the database:

        //updating the user:
        book.Name<-bookName
        let comboBox = this.FindControl<ComboBox>("bookAvailable")

        let selectedItem = comboBox.SelectedItem :?> ComboBoxItem
        book.Available <- selectedItem.Content :?> string

        book.Title<-this.FindControl<TextBox>("bookTitle").Text
        Debug.WriteLine(sprintf "يا مسهل الحال يارب %s"book.Available)

        // code to save the new changes to the database
        this.Close()