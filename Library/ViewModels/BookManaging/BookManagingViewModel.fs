namespace Library.ViewModels

open System.Collections.ObjectModel

type BookManagingViewModel() =

    let buttons = ObservableCollection<string>() // Holds the labels for the buttons
    do
        // Populate buttons based on x
        for i in 1..3 do
            buttons.Add($"Button {i}")
        buttons.Add("last button ?")
    member this.Buttons = buttons
    member this.Testing = "This could be your second page content"