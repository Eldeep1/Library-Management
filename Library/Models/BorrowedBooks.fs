namespace Library.Models

type BorrowedBooks(id:int, bookID: int, userID: int,bookName: string, userName: string, Date: string, returned: string) =
    member val ID = id with get
    member val BookID = bookID with get
    member val BookName= bookName with get, set
    member val UserID = userID with get, set
    member val UserName = userName with get, set
    member val Date = Date with get, set
    member val Returned = returned with get, set

