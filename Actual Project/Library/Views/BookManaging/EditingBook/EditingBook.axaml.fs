namespace Library.Views
open System.Diagnostics
open Avalonia.Controls
open Avalonia.Markup.Xaml
open Library.ViewModels
open Library.Models
open Avalonia.Interactivity
open System
open System.Collections.Generic

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
        let bookAuthor = this.FindControl<TextBox>("bookAuthor").Text
        let bookGenre = this.FindControl<TextBox>("bookGenre").Text

        let comboBox = this.FindControl<ComboBox>("bookAvailable")

        let selectedItem = comboBox.SelectedItem :?> ComboBoxItem
        let availability = selectedItem.Content :?> string

        if String.IsNullOrWhiteSpace(bookName) || String.IsNullOrWhiteSpace(bookAuthor) || String.IsNullOrWhiteSpace(bookGenre) then
            // Do nothing if any field is null or empty
            ()
        else
            let values = Dictionary<string, obj>()
            values.Add("Name", bookName)
            values.Add("Author", bookAuthor)
            values.Add("Genre", bookGenre)
            values.Add("Available",availability)
            let conditions = Dictionary<string, obj>()
            conditions.Add("ID", book.Id)

            let success = DatabaseConnection.Instance.Update("Book", values, conditions)
            if success then
                    // Optionally, provide feedback to the user
                    this.Close()
                    Debug.WriteLine(sprintf "Book details updated successfully.")

            else
                    // Handle the case where the update was not successful
                    Debug.WriteLine(sprintf "Failed to update the book details.")



