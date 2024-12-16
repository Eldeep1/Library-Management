namespace Library.ViewModels


open System.Collections.ObjectModel
open Library.Models 
open System.Collections.Generic
open Library.Services
open Library.Functions


type MemberManagingViewModel() as this =

    let users = ObservableCollection<Member>()
    
    do
        this.Initialize()

    member this.Initialize() =
        this.GetMembersData()
        


    member this.GetMembersData() =

        let results = DatabaseConnection.Instance.Select("Member", None) 
        let updatedUsers = UserBuiltIn.getMemberData results
        Shared.clear' users
        Shared.iter' users.Add (List.ofSeq updatedUsers)

     member this.Users = users

    member this.DeleteMember(user: Member) =
        let conditions = Dictionary<string, obj>()
        conditions.Add("ID", user.ID)

        let success = DatabaseConnection.Instance.Delete("Member", conditions)
        if success then

            Shared.removeItem users user



