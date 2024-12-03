namespace Library.ViewModels

open System.Collections.ObjectModel
open System.ComponentModel
open System.Diagnostics
open System.Runtime.CompilerServices
open Library.Models

type BookManagingViewModel() as this =
    let mutable searchText = ""
    let books = ObservableCollection<Book>()
    let propertyChanged = Event<PropertyChangedEventHandler, PropertyChangedEventArgs>()

    do
        this.Initialize()

    /// Initializes the books collection.
    member this.Initialize() =
        this.GetBooksData()
    member this.EditingBook(book: Book) =
        Debug.WriteLine(sprintf "Parameter: %s  " book.Name)
    member this.DeleteBook(book: Book) =
         let deleted = books.Remove(book)
         Debug.WriteLine(sprintf "deleted ? %b" deleted)


    /// Mock function to fetch initial books.
    member this.GetBooksData() =
        let initialBooks = [
            Book(1, "test1", "ali@example.com", "1234567890", "culture", "Available")
            Book(2, "test9", "omar@example.com", "0987654321", "culture", "Unavailable")
        ]

        books.Clear()
        for book in initialBooks do
            books.Add(book)

    /// Updates the books collection based on the search text.
    member this.SearchForBooks(search: string) =
        let filteredBooks = [
            Book(1, "test99", "ali@example.com", "1234567890", "culture", "Available")
            Book(2, "test9000", "omar@example.com", "0987654321", "culture", "Unavailable")
            Book(3, "test222", "omar@example.com", "0987654321", "culture", "Available")
        ]
        books.Clear()
        for book in filteredBooks do
            books.Add(book)

    /// Property for accessing the books collection.
    member this.Book = books

    /// Property for binding the search text.
    member this.SearchText
        with get () = searchText
        and set value =
            if searchText <> value then
                searchText <- value
                this.OnPropertyChanged("SearchText")
                this.OnSearchTextChanged(value)

    /// Handles search text changes and updates the books collection.
    member this.OnSearchTextChanged(newText: string) =
        Debug.WriteLine(sprintf "Search text changed to: %s" newText)
        this.SearchForBooks(newText) // Update books based on new search text.

    /// INotifyPropertyChanged implementation.
    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member _.PropertyChanged = propertyChanged.Publish

    /// Trigger the PropertyChanged event.
    member this.OnPropertyChanged(propertyName: string) =
        propertyChanged.Trigger(this, PropertyChangedEventArgs(propertyName))
