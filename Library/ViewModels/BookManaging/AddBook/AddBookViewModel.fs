namespace Library.ViewModels

open ReactiveUI

type AddBookViewModel() as this =
    inherit ReactiveObject()


    do
        this.Initialize()

    member this.Initialize() =
        this.GetBookData()

    member this.GetBookData() =
        printf "will we delete this page ?"
        
   



