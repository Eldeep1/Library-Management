namespace Library.Models
open ReactiveUI
open System.Reactive
open System.Diagnostics

type Member(id: int, name: string, email: string, phone: string) =
    member val ID = id with get
    member val Name = name with get, set
    member val Email = email with get, set
    member val Phone = phone with get, set
