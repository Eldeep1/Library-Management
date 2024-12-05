
namespace Library.Models

open System.ComponentModel
open System.Runtime.CompilerServices

type Book(id: int, name: string, author: string, genre: string, available: string) =
    let mutable borrowing = false
    let propertyChanged = Event<PropertyChangedEventHandler, PropertyChangedEventArgs>()

    member val Id = id with get, set
    member val Name = name with get, set
    //member val Title = title with get, set
    member val Author = author with get, set
    member val Genre = genre with get, set
    member val Available = available with get, set

    member this.Borrowing
        with get() = borrowing
        and set(value) =
            if borrowing <> value then
                borrowing <- value
                this.OnPropertyChanged()

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member _.PropertyChanged = propertyChanged.Publish

    member private this.OnPropertyChanged([<CallerMemberName>] ?propertyName: string) =
        let propertyName = defaultArg propertyName ""
        propertyChanged.Trigger(this, PropertyChangedEventArgs(propertyName))
