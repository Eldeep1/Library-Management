namespace Library.Models
open ReactiveUI
open System.Reactive
open System.Diagnostics

type Book(id: int, name: string, title: string, author: string, genre: string, available: string) =
    member val ID = id with get
    member val Name = name with get, set
    member val Title = title with get, set
    member val Author = author with get, set
    member val Genre = genre with get, set
    member val Available = available with get, set

