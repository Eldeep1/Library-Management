namespace Library.Functions

open System.Collections.Generic
open Library.Models
open System.Diagnostics
open System.Collections.ObjectModel
open System
open System.Data


module UserBuiltIn =

    let extract<'T> (key: string) (row: Dictionary<string, obj>) (defaultValue: 'T) : 'T =
        if row.ContainsKey(key) && row.[key] <> null && not (row.[key] = box System.DBNull.Value) then
            row.[key] :?> 'T
        else
            defaultValue



    let getMemberData (results: List<Dictionary<string, obj>>) : ObservableCollection<Member> =       

        
        let members =
            results
            |> Seq.fold (fun acc row ->
                let id = extract "ID" row 0
                let name = extract "Name" row ""
                let email = extract "Email" row ""
                let phone = extract "Phone" row ""
                let user = { ID = id; Name = name; Email = email; Phone = phone }
                user :: acc
            ) []
            |> List.rev

        ObservableCollection<Member>(members)

    
    let searchForMember (users: seq<Member>) (newText: string) =
        let members = users |> Seq.toList

        match System.Int32.TryParse(newText) with
        | (true, id) ->
            match members |> List.tryFind (fun m -> m.ID = id) with
            | Some user -> Some (user.Name, user.Email, user.Phone)
            | None -> None
        | _ -> None