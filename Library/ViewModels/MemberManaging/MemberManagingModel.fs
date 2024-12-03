namespace Library.ViewModels

open ReactiveUI
open System.Reactive
open System.Diagnostics
open System.Collections.ObjectModel
open Library.Models 

type MemberManagingViewModel() as this =

    let users = ObservableCollection<Member>() 

    do
        this.Initialize()

    member this.Initialize() =
        this.GetMembersData()
    //member this.DoTheThing = ReactiveCommand.Create<string>(fun parameter ->
    //    Debug.WriteLine(sprintf "Parameter: %s" parameter)
    //)
    member this.tester(meow)=
        Debug.WriteLine(sprintf "Parameter: %s" meow)

        
    // Method to add members manually
    member this.GetMembersData() =
        let members = [
            Member(1, "Ali", "ali@example.com", "1234567890")
            Member(3, "Omar", "omar@example.com", "0987654321")
            Member(4, "Omar", "omar@example.com", "0987654321")
            Member(5, "Omar", "omar@example.com", "0987654321")
            Member(6, "Omar", "omar@example.com", "0987654321")
            Member(7, "Omar", "omar@example.com", "0987654321")
            Member(8, "Omar", "omar@example.com", "0987654321")
        ]

        for object in members do
            users.Add(object)


    member this.Users = users
