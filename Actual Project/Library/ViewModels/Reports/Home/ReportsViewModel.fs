namespace Library.ViewModels

open ReactiveUI
open Library.Models
open System.Diagnostics
open System.Collections.ObjectModel
open System
open Library.Services

type ReportsViewModel() as this =
    inherit ReactiveObject()
    let availableBooksList = ObservableCollection<Book>()
    let booksTransactionHistory = ObservableCollection<BorrowedBooks>()  
    let userBooksTransactionHistory = ObservableCollection<BorrowedBooks>()  


    let mutable searchID=""
    let mutable availableBooks = true
    let mutable borrowHistory = false

    do
        this.Initialize()

    member this.Initialize() =
        this.GetBooksData()
        this.GetBorrowData()
    
    member this.GetBorrowData() =
        let results = DatabaseConnection.Instance.Select("BorrowedBooks",  None)


        for row in results do
            let ID = if row.["ID"] = DBNull.Value then 0 else row.["ID"] :?> int

            let BookID = if row.["BookID"] = DBNull.Value then 0 else row.["BookID"] :?> int
            let UserID = if row.["UserID"] = DBNull.Value then 0 else row.["UserID"] :?> int
            let BookName = if row.["BookName"] = DBNull.Value then "" else row.["BookName"] :?> string
            let UserName = if row.["UserName"] = DBNull.Value then "" else row.["UserName"] :?> string
            let Returned = if row.["Returned"] = DBNull.Value then "" else row.["Returned"] :?> string
            //let Date = if row.["Date"] = DBNull.Value then DateTime.MinValue else row.["Date"] :?> DateTime
            let Date = if row.["Date"] = DBNull.Value then "" else row.["Date"] :?> string


            let book = BorrowedBooks(
                 ID,BookID,UserID,BookName,UserName,Date,Returned
            )
            booksTransactionHistory.Add(book)


  
    member this.GetBooksData() =
        let results = DatabaseConnection.Instance.Select("Book",  None)



        for row in results do
            let id = if row.["ID"] = DBNull.Value then 0 else row.["ID"] :?> int
            let name = if row.["Name"] = DBNull.Value then "" else row.["Name"] :?> string
            let author = if row.["Author"] = DBNull.Value then "" else row.["Author"] :?> string
            let genre = if row.["Genre"] = DBNull.Value then "" else row.["Genre"] :?> string
            let available = if row.["Available"] = DBNull.Value then "" else row.["Available"] :?> string
            let book = Book(
                 id,name,author,genre,available
            )
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
