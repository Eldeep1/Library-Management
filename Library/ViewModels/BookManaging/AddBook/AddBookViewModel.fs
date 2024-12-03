namespace Library.ViewModels

open ReactiveUI
open System.Reactive
open System.Diagnostics
open System.Collections.ObjectModel
open Library.Models 

namespace Library.ViewModels

open ReactiveUI
open Library.Models
open System.Reactive
open System.Diagnostics

type AddBookViewModel() as this =
    inherit ReactiveObject()
    let mutable userName = ""


    do
        this.Initialize()

    member this.Initialize() =
        this.GetBookData()

    member this.GetBookData() =
        printf "will we delete this page ?"
        
   



