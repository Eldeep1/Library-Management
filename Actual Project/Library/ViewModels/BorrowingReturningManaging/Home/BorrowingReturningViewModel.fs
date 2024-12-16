namespace Library.ViewModels

open ReactiveUI
open Library.Models
open System.Diagnostics
open System.Collections.ObjectModel
open System.Diagnostics
open Library.Models
open System
open System.Collections.Generic
open Library.Services
open Library.Functions
type BorrowingReturningViewModel() as this =
    inherit ReactiveObject()
    let borrowedBooksList = ObservableCollection<BorrowedBooks>()
    let returnedBooksList = ObservableCollection<BorrowedBooks>()
    let mutable borrowedList = true
    let mutable returnedList = false

    do
        this.Initialize()

    member this.Initialize() =
        this.GetBorrowData()
        
    member this.GetBorrowData() =
        let results = DatabaseConnection.Instance.Select("BorrowedBooks", None)
        let books = BorrowedBooksBuiltIn.getBorrowData results

        let borrowedBooks = 
            books 
            |> Shared.filter' (fun book -> book.Returned.Equals("Borrowed"))
            |> Shared.toList'

        let returnedBooks = 
            books 
            |> Shared.filter' (fun book -> not (book.Returned.Equals("Borrowed")))
            |> Shared.toList'

        Shared.addItems borrowedBooksList borrowedBooks
        Shared.addItems returnedBooksList returnedBooks


    member this.ReturnBook(book:BorrowedBooks)=

        let values = Dictionary<string, obj>()
        values.Add("Returned", "Returned")
        values.Add("Date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))

        let conditions = Dictionary<string, obj>()
        conditions.Add("ID", book.ID)

        let success = DatabaseConnection.Instance.Update("BorrowedBooks", values, conditions)
        if success then

            let updatedBook = BorrowedBooksBuiltIn.updateBookReturnStatus book
            Shared.removeItem borrowedBooksList book
            Shared.addItems returnedBooksList [updatedBook]

            Debug.WriteLine(sprintf "Member details updated successfully.")

        else
                // Handle the case where the update was not successful
                Debug.WriteLine(sprintf "Failed to update the Member details.")




    member this.BorrowedBooksList=borrowedBooksList
    member this.ReturnedBooksList=returnedBooksList


    member this.BorrowedList
        with get() = borrowedList
        and set(value) =
            this.RaiseAndSetIfChanged(&borrowedList, value) |> ignore

    member this.ReturnedList
        with get() = returnedList
        and set(value) =
            this.RaiseAndSetIfChanged(&returnedList, value) |> ignore


    member this.ShowBorrowed()=
        this.BorrowedList<-true
        this.ReturnedList<-false

    member this.ShowReturned()=
        this.BorrowedList<-false
        this.ReturnedList<-true
        borrowedList<-false
        Debug.WriteLine(sprintf "do the borrowed items appearing  ? %b" borrowedList)
