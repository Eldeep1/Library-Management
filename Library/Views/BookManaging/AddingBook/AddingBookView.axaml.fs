namespace Library.Views
open System.Diagnostics
open Avalonia.Controls
open Avalonia.Markup.Xaml
open Library.ViewModels
open Avalonia.Interactivity
open System
open System.Collections.Generic

type AddBookView() as this =
    inherit Window() 
    do
        this.InitializeComponent()
        this.DataContext <- AddBookViewModel()

    member private this.InitializeComponent() =
        AvaloniaXamlLoader.Load(this)

    member this.OnAddButtonClick(sender: obj, e: RoutedEventArgs) =

        let bookName = this.FindControl<TextBox>("bookName").Text
        let bookAuthor = this.FindControl<TextBox>("bookAuthor").Text
        let bookGenre = this.FindControl<TextBox>("bookGenre").Text

        // Check if any of the fields are null or empty
        if String.IsNullOrWhiteSpace(bookName) || String.IsNullOrWhiteSpace(bookAuthor) || String.IsNullOrWhiteSpace(bookGenre) then
            // Do nothing if any field is null or empty
            ()
        else
            let values = Dictionary<string, obj>()
            values.Add("Name", bookName)
            values.Add("Author", bookAuthor)
            values.Add("Genre", bookGenre)
            values.Add("Available", "Available")

            let success = DatabaseConnection.Instance.Insert("Book", values)
            if success then
                
                this.Close()
            else
                // Handle the case where the insert was not successful
                Debug.WriteLine(sprintf "Failed to add the book to the database.")

