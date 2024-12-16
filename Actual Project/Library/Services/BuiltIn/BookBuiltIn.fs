namespace Library.Functions

module BookBuiltIn =

    open System.Collections.ObjectModel
    open Library.Models
    open System.Collections.Generic
    open System

    let extract<'T> (key: string) (row: Dictionary<string, obj>) (defaultValue: 'T) : 'T =
        if row.ContainsKey(key) && row.[key] <> null && not (row.[key] = box System.DBNull.Value) then
            row.[key] :?> 'T
        else
            defaultValue



    let getBooksData (results: List<Dictionary<string, obj>>) : ObservableCollection<Book> =       
        let books =
            results
            |> Seq.fold (fun acc row ->
                let id = extract "ID" row 0
                let name = extract "Name" row ""
                let author = extract "Author" row ""
                let genre = extract "Genre" row ""  // Changed to lowercase to match record field
                let available = extract "Available" row ""  // Changed to lowercase to match record field
                let book = { 
                    Id = id          // Changed to match record field name
                    Name = name
                    Author = author
                    Genre = genre
                    Available = available
                    Borrowing = false  // Added this field as it's required by the record
                }
            
                book :: acc
            ) []
            |> List.rev
        ObservableCollection<Book>(books)


        
    let contains (substring: string) (str: string) : bool =
        let rec loop i j =
            if j = substring.Length then true
            elif i = str.Length then false
            elif str.[i] = substring.[j] then loop (i + 1) (j + 1)
            else loop (i + 1) 0
        loop 0 0

    let searchForBooks (allBooks: seq<Book>) (books: ObservableCollection<Book>) (search: string) =
        let filteredBooks =
            if String.IsNullOrWhiteSpace(search) then
                allBooks
            else
                let searchLower = search.ToLowerInvariant()
                allBooks
                |> Shared.filter' (fun book -> 
                    contains searchLower (book.Name.ToLowerInvariant()) ||
                    contains searchLower (book.Author.ToLowerInvariant()) ||
                    contains searchLower (book.Genre.ToLowerInvariant()))
                |> Seq.distinctBy (fun book -> book.Id)
        
        Shared.clear' books
        Shared.addItems books (Shared.toList' filteredBooks)