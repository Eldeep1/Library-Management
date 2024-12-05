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
            if book.Returned.Equals("Borrowed") then borrowedBooksList.Add(book)  else returnedBooksList.Add(book)


    member this.ReturnBook(book:BorrowedBooks)=

        let values = Dictionary<string, obj>()
        values.Add("Returned", "Returned")
        values.Add("Date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))

        let conditions = Dictionary<string, obj>()
        conditions.Add("ID", book.ID)

        let success = DatabaseConnection.Instance.Update("BorrowedBooks", values, conditions)
        if success then
                // Optionally, provide feedback to the user
                book.Returned.Equals("Returned")
                book.Date<-DateTime.Now.ToString("yyyy-MM-dd HH:mm")
                borrowedBooksList.Remove(book)
                returnedBooksList.Add(book)

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
