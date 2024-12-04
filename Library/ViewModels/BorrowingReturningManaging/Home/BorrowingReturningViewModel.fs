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
type BorrowingReturningViewModel() as this =
    inherit ReactiveObject()
    let mutable userName = ""
    let borrowedBooksList = ObservableCollection<BorrowedBooks>()
    let returnedBooksList = ObservableCollection<BorrowedBooks>()
    let mutable borrowedList = true
    let mutable returnedList = false

    do
        this.Initialize()

    member this.Initialize() =
        this.GetBooksData()

    member this.GetBooksData() =
        let initialBooks = [
            BorrowedBooks(1, 1, "learn by hard way", "Eldeep", "12/4/2024 6:10 pm", false)
            BorrowedBooks(1, 1, "is that useful?", "Eldeep", "12/4/2024 6:10 pm", true)

        ]
        for book in initialBooks do
            if book.Returned then returnedBooksList.Add(book)  else borrowedBooksList.Add(book)


    member this.ReturnBook(book:BorrowedBooks)=
        book.Returned=true
        borrowedBooksList.Remove(book)
        returnedBooksList.Add(book)
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
