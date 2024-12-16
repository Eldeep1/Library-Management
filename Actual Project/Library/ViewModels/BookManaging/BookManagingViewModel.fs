namespace Library.ViewModels

open System.Collections.ObjectModel
open System.ComponentModel
open System.Diagnostics
open Library.Models
open System
open System.Collections.Generic
open Library.Services
open Library.Functions

type BookManagingViewModel() as this =
    let mutable searchText = ""
    let books = ObservableCollection<Book>()
    let propertyChanged = Event<PropertyChangedEventHandler, PropertyChangedEventArgs>()
    let allBooks = List<Book>() 
    let mutable memberID = ""
    let mutable memberName = ""
    let mutable memberEmail = ""
    let mutable memberPhone = ""
    let users = ObservableCollection<Member>() 

        
    do
        this.Initialize()

    member this.Initialize() =
        this.GetBooksData()
        this.GetMembersData()

    member this.GetBooksData() =
        let results = DatabaseConnection.Instance.Select("Book",  None)
        let updatedBooks = BookBuiltIn.getBooksData results

        Shared.clear' books
        Shared.clear' allBooks
        Shared.iter' books.Add (List.ofSeq updatedBooks)
        Shared.iter' allBooks.Add (List.ofSeq updatedBooks)

    member this.DeleteBook(book: Book) =

            let conditions = Dictionary<string, obj>()
            conditions.Add("ID", book.Id)

            let success = DatabaseConnection.Instance.Delete("Book", conditions)
            if success then
                Shared.removeItem books book

                


    member this.GetMembersData() =

        let results = DatabaseConnection.Instance.Select("Member", None) 
        let updatedUsers = UserBuiltIn.getMemberData results
        Shared.clear' users
        Shared.iter' users.Add (List.ofSeq updatedUsers)

     member this.Users = users


    member this.AddBorrowingToMember(book: Book)=
        if String.IsNullOrWhiteSpace(this.MemberName) then
                ()
            else
                let values = Dictionary<string, obj>()
                values.Add("BookID", book.Id)
                values.Add("UserID", this.MemberID)
                values.Add("BookName", book.Name)
                values.Add("UserName", this.MemberName)
                values.Add("Date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
                values.Add("Returned", "Borrowed")


                let success = DatabaseConnection.Instance.Insert("BorrowedBooks", values)
                if success then
                    this.ToggleBorrowing(book)
                


    member this.SearchForBooks(search: string) =
        BookBuiltIn.searchForBooks allBooks books search


    member this.ToggleBorrowing(book: Book) =
        let updatedBook = { book with Borrowing = not book.Borrowing }
        Debug.WriteLine(sprintf "value changed to %b" updatedBook.Borrowing)
        

   //don't know how to change it to immutable, as it's required for a better gui
    member this.SearchForMember(newText: string) =
            match UserBuiltIn.searchForMember users newText with
            | Some (name, email, phone) ->
                this.MemberName <- name
                this.MemberEmail <- email
                this.MemberPhone <- phone
            | None ->
                this.MemberName <- ""
                this.MemberEmail <- ""
                this.MemberPhone <- ""

    member this.SearchText
        with get () = searchText
        and set value =
            if searchText <> value then
                searchText <- value
                this.OnPropertyChanged("SearchText")
                this.SearchForBooks(value)


    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member _.PropertyChanged = propertyChanged.Publish

    /// Trigger the PropertyChanged event.
    member this.OnPropertyChanged(propertyName: string) =
        propertyChanged.Trigger(this, PropertyChangedEventArgs(propertyName))



     
    member this.Book = books

    member this.MemberID
        with get() = memberID
        and set(value) =
            memberID <- value
            this.OnPropertyChanged("MemberID")
            this.SearchForMember(value)

    member this.MemberName
        with get() = memberName
        and set(value) =
            memberName <- value
            this.OnPropertyChanged("MemberName")

    member this.MemberEmail
        with get() = memberEmail
        and set(value) =
            memberEmail <- value
            this.OnPropertyChanged("MemberEmail")

    member this.MemberPhone
        with get() = memberPhone
        and set(value) =
            memberPhone <- value
            this.OnPropertyChanged("MemberPhone")



