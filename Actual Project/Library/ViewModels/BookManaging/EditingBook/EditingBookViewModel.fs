namespace Library.ViewModels

open ReactiveUI
open Library.Models

type EditingBookViewModel(book: Book)  =
    inherit ReactiveObject()
    let  isAvailable = if book.Available.Equals("Available") then 0 else 1

    

    //member this.ToggleButtonContent
    //    with get() = if this.IsAvailable then "Available" else "Unavailable"

    member this.comboBox=isAvailable
    member this.Book =book




