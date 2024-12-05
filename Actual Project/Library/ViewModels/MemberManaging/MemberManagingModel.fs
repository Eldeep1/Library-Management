namespace Library.ViewModels


open System.Diagnostics
open System.Collections.ObjectModel
open Library.Models 
open System
open System.Collections.Generic
open Library.Services

type MemberManagingViewModel() as this =

    let users = ObservableCollection<Member>() 
    do
        this.Initialize()

    member this.Initialize() =
        this.GetMembersData()


    member this.GetMembersData() =
        let results = DatabaseConnection.Instance.Select("Member",  None)

        users.Clear()

        for row in results do
            let id = if row.["ID"] = DBNull.Value then 0 else row.["ID"] :?> int
            let name = if row.["Name"] = DBNull.Value then "" else row.["Name"] :?> string
            let Email = if row.["Email"] = DBNull.Value then "" else row.["Email"] :?> string
            let Phone = if row.["Phone"] = DBNull.Value then "" else row.["Phone"] :?> string
            let user = Member(
                 id,name,Email,Phone
            )

            users.Add(user)

     member this.Users = users

    member this.DeleteMember(user: Member) =

            let conditions = Dictionary<string, obj>()
            conditions.Add("ID", user.ID)

            let success = DatabaseConnection.Instance.Delete("Member", conditions)
            if success then
                // Optionally, provide feedback to the user
                users.Remove(user)
                Debug.WriteLine(sprintf "done deleting")

                
