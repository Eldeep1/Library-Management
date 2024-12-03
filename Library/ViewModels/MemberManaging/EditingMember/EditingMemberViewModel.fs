namespace Library.ViewModels

open ReactiveUI
open System.Reactive
open System.Diagnostics
open System.Collections.ObjectModel
open Library.Models 

namespace Library.ViewModels

open ReactiveUI
open Library.Models

type EditingMemberViewModel(user: Member) as this =
    inherit ReactiveObject()


    do
        this.Initialize()

    member this.Initialize() =
        this.GetMembersData()

    member this.GetMembersData() =
        printf "lol"
        

    member this.User =user




