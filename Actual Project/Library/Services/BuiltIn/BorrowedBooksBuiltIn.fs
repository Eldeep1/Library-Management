namespace Library.Functions


module BorrowedBooksBuiltIn =

    open System.Collections.ObjectModel
    open Library.Models
    open System.Collections.Generic
    open System

    let extract<'T> (key: string) (row: Dictionary<string, obj>) (defaultValue: 'T) : 'T =
        if row.ContainsKey(key) && row.[key] <> null && not (row.[key] = box System.DBNull.Value) then
            row.[key] :?> 'T
        else
            defaultValue



    let getBorrowData (results: List<Dictionary<string, obj>>) : ObservableCollection<BorrowedBooks> =     
        let books =
            results
            |> Seq.fold (fun acc row ->
                let id = extract "ID" row 0
                let bookID = extract "BookID" row 0
                let userID = extract "UserID" row 0
                let bookName = extract "BookName" row ""
                let userName = extract "UserName" row ""
                let returned = extract "Returned" row ""
                let date = extract "Date" row ""
                let book = { 
                    ID = id
                    BookID = bookID
                    UserID = userID
                    BookName = bookName
                    UserName = userName
                    Date = date
                    Returned = returned 
                }
            
                book :: acc
            ) []
            |> List.rev
        ObservableCollection<BorrowedBooks>(books)

    let updateBookReturnStatus (book: BorrowedBooks) : BorrowedBooks =
        { book with 
            Returned = "Returned"
            Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm") }