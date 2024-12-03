
namespace Library.Views

open Avalonia.Controls
open Avalonia.Markup.Xaml
open Library.ViewModels
open System.Diagnostics
open Avalonia.Interactivity
open Library.Models

type BookManagingView() as this = 
    inherit UserControl()
    

    do 
        this.InitializeComponent()
        this.DataContext<-BookManagingViewModel()
        let searchTextBox = this.FindControl<TextBox>("searchTextBox")
        searchTextBox.TextChanged.Add(this.HandleSearchTextChanged)

    member private this.InitializeComponent() =
        AvaloniaXamlLoader.Load(this)


        // Attach the TextChanged event with the correct handler
    member this.HandleSearchTextChanged(args: TextChangedEventArgs) =
        let searchTextBox = args.Source :?> TextBox
        let currentText = searchTextBox.Text
        Debug.WriteLine(sprintf "Search text changed to: %s" currentText)
        

    member this.EditBook (sender: obj) (e: RoutedEventArgs) =
        let button = sender :?> Button
        let book = button.Tag :?> Book

        let window = Library.Views.EditingBookView(book)
        window.Show() 

    member this.AddBook (sender: obj) (e: RoutedEventArgs) =
        let button = sender :?> Button

        let window = Library.Views.AddBookView()
        window.Show() 

    // Do something with code
    member this.RefreshView(sender: obj, e: RoutedEventArgs) =
        let newDataContext = BookManagingViewModel() // Reinitialize the ViewModel or fetch updated data
        this.DataContext <- newDataContext        
        
