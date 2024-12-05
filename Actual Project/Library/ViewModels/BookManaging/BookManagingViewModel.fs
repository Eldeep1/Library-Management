namespace Library.ViewModels

open System.Collections.ObjectModel
open System.ComponentModel
open System.Diagnostics
open Library.Models
open System
open System.Collections.Generic
open Library.Services
type BookManagingViewModel() as this =
    let mutable searchText = ""
    let books = ObservableCollection<Book>()
    let propertyChanged = Event<PropertyChangedEventHandler, PropertyChangedEventArgs>()
    let allBooks = List<Book>() // Cached full list of books
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

        books.Clear()
        allBooks.Clear()

        for row in results do
            let id = if row.["ID"] = DBNull.Value then 0 else row.["ID"] :?> int
            let name = if row.["Name"] = DBNull.Value then "" else row.["Name"] :?> string
            let author = if row.["Author"] = DBNull.Value then "" else row.["Author"] :?> string
            let genre = if row.["Genre"] = DBNull.Value then "" else row.["Genre"] :?> string
            let available = if row.["Available"] = DBNull.Value then "" else row.["Available"] :?> string
            let book = Book(
                 id,name,author,genre,available
            )
            allBooks.Add(book) // Add to cache

            books.Add(book)

    //member this.EditingBook(book: Book) =
    //    Debug.WriteLine(sprintf "Parameter: %s  " book.Name)
    
    member this.DeleteBook(book: Book) =


            let conditions = Dictionary<string, obj>()
            conditions.Add("ID", book.Id)

            let success = DatabaseConnection.Instance.Delete("Book", conditions)
            if success then
                // Optionally, provide feedback to the user
                books.Remove(book)
                Debug.WriteLine(sprintf "done deleting")

                



    member this.AddBorrowingToMember(book: Book)=
        if String.IsNullOrWhiteSpace(this.MemberName) then
                // Do nothing if any field is null or empty
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
                
        //Debug.WriteLine(sprintf "the member with ID : %s borrowed  %s at %A " this.MemberID book.Name DateTime.Now)


    /// Updates the books collection based on the search text.
    member this.SearchForBooks(search: string) =
        if String.IsNullOrWhiteSpace(search) then
            // If search text is empty, restore the full list from cache
            books.Clear()
            for book in allBooks do
                books.Add(book)
        else
            let searchLower = search.ToLowerInvariant()
            let filteredBooks = 
                allBooks
                |> Seq.filter (fun book -> 
                    book.Name.ToLowerInvariant().Contains(searchLower) ||
                    book.Author.ToLowerInvariant().Contains(searchLower) ||
                    book.Genre.ToLowerInvariant().Contains(searchLower))
                |> Seq.distinctBy (fun book -> book.Id)
                |> Seq.toList

            books.Clear()
            for book in filteredBooks do
                books.Add(book)


    member this.ToggleBorrowing(book: Book) =
        if book.Borrowing then book.Borrowing<-false else book.Borrowing<-true

        Debug.WriteLine(sprintf "value changed to %b" book.Borrowing) 

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


    member this.GetMembersData() =
        let results = DatabaseConnection.Instance.Select("Member",  None)

        users.Clear()

        for row in results do
            let id = if row.["ID"] = DBNull.Value then 0 else row.["ID"] :?> int
            let name = if row.["Name"] = DBNull.Value then "" else row.["Name"] :?> string
            let Email = if row.["Email"] = DBNull.Value then "" else row.["Email"] :?> string
            let Phone = if row.["Phone"] = DBNull.Value then "" else row.["Phone"] :?> string
            let user = Member(
                 id,name,Email,Phone
            )

            users.Add(user)

    member private this.SearchForMember(newText: string) =
        let members = users |> Seq.toList


        match System.Int32.TryParse(newText) with
        | (true, id) ->
            match members |> List.tryFind (fun m -> m.ID = id) with
            | Some user ->
                this.MemberName <- user.Name
                this.MemberEmail <- user.Email
                this.MemberPhone <- user.Phone
            | None ->
                this.MemberName <- ""
                this.MemberEmail <- ""
                this.MemberPhone <- ""
        | _ ->
            this.MemberName <- ""
            this.MemberEmail <- ""
            this.MemberPhone <- ""

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



