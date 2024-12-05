namespace Library.ViewModels

open ReactiveUI
open Library.Models
open System.Reactive
open System.Diagnostics
open System.Collections.ObjectModel
open System.ComponentModel
open System.Diagnostics
open System.Runtime.CompilerServices
open Library.Models
open System

type ReportsViewModel() as this =
    inherit ReactiveObject()
    let availableBooksList = ObservableCollection<Book>()
    let booksTransactionHistory = ObservableCollection<BorrowedBooks>()  
    let userBooksTransactionHistory = ObservableCollection<BorrowedBooks>()  
    let propertyChanged = Event<PropertyChangedEventHandler, PropertyChangedEventArgs>()


    let mutable searchID=""
    let mutable availableBooks = true
    let mutable borrowHistory = false

    do
        this.Initialize()

    member this.Initialize() =
        this.GetBooksData()
        this.GetBorrowData()
    member this.GetBorrowData() =
        let borrowedBooks = [
            BorrowedBooks(1,1, 1, "learn by hard way", "Eldeep", "12/4/2024 6:10 pm", "Borrowed")
            BorrowedBooks(1,1, 1, "is that useful?", "Eldeep", "12/4/2024 6:10 pm", "Returned")
            BorrowedBooks(1,1, 1, "is that useful?", "Eldeep", "12/4/2024 6:10 pm", "Returned")
            BorrowedBooks(1,1, 1, "is that useful?", "Eldeep", "12/4/2024 6:10 pm", "Returned")


        ]
        for book in borrowedBooks do
            booksTransactionHistory.Add(book)

    member this.GetBooksData() =
        let availableBooks = [
                    Book(1, "test1", "1234567890", "culture", "Available")
                    Book(2, "test9", "0987654321", "culture", "Unavailable")

                ]

        availableBooksList.Clear()
        for book in availableBooks do
            availableBooksList.Add(book)



     //searching in borrowed books list about the member id = written id and then show the results

     //1- search for the member using the ID
     //2- show the member name, book id, borrowDate, borrow state
    member this.SearchForBorrowHistory(WrittenID: string) =
        match System.Int32.TryParse(WrittenID) with
        | (true, userID) ->
            // Filter books for the valid user ID
            let filteredBooks = 
                booksTransactionHistory 
                |> Seq.filter (fun book -> book.UserID = userID)
                |> Seq.toList

            // Update the observable collection
            userBooksTransactionHistory.Clear()
            filteredBooks |> List.iter (fun book -> userBooksTransactionHistory.Add(book))
        | (false, _) ->
            // Clear the list if the input is invalid or empty
            userBooksTransactionHistory.Clear()

    member this.UserBooksTransactionHistory=userBooksTransactionHistory
    member this.AvailableBooksList=availableBooksList

    member this.AvailableBooks
        with get() = availableBooks
        and set(value) =
            this.RaiseAndSetIfChanged(&availableBooks, value) |> ignore

    member this.BorrowHistory
        with get() = borrowHistory
        and set(value) =
            this.RaiseAndSetIfChanged(&borrowHistory, value) |> ignore


    member this.ShowHistory()=
        this.BorrowHistory<-true
        Debug.WriteLine(sprintf "we have clicked on showing history with borrowhistory =%b and available books = %b" this.BorrowHistory this.AvailableBooks)

        this.AvailableBooks<-false
        Debug.WriteLine(sprintf "we have clicked on showing history with borrowhistory =%b and available books = %b" this.BorrowHistory this.AvailableBooks)
        Debug.WriteLine(sprintf "-----------------------------------------------------------------------------------")



    member this.ShowAvailable()=
        this.BorrowHistory<-false
        this.AvailableBooks<-true

    member this.SearchText
        with get () = searchID
        and set value =
            if searchID <> value then
                this.RaiseAndSetIfChanged(&searchID, value) |> ignore
                this.SearchForBorrowHistory(value)

    //member this.SearchText
    //    with get () = searchID
    //    and set value =
    //        if searchID <> value then
    //            searchID <- value
    //            this.OnPropertyChanged("SearchID")
    //            this.SearchForBorrowHistory(value)

    ///// INotifyPropertyChanged implementation.
    //interface INotifyPropertyChanged with
    //    [<CLIEvent>]
    //    member _.PropertyChanged = propertyChanged.Publish

    ///// Trigger the PropertyChanged event.
    //member this.OnPropertyChanged(propertyName: string) =
    //    propertyChanged.Trigger(this, PropertyChangedEventArgs(propertyName))
