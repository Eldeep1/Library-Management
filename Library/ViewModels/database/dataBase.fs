namespace Library.ViewModels

open MySql.Data.MySqlClient
open System.Collections.Generic
open System
open System.Diagnostics

type DatabaseConnection private () =
    static let instance = lazy (new DatabaseConnection())
    let connectionString = "Server=localhost;Port=3306;Database=Library;Uid=root;Pwd=1234;"
    let connection = new MySqlConnection(connectionString)

    do
        connection.Open()

    member this.Connection = connection

    static member Instance = instance.Value
    member this.Select(tableName: string, conditions: Dictionary<string, obj> option) =
        let whereClause = 
            match conditions with
            | Some conds -> 
                conds.Keys 
                |> Seq.map (fun key -> $"{key} = @cond_{key}") 
                |> String.concat " AND "
            | None -> ""

        let commandText = 
            if whereClause = "" then
                $"SELECT * FROM {tableName}"
            else
                $"SELECT * FROM {tableName} WHERE {whereClause}"

        let command = new MySqlCommand(commandText, connection)

        match conditions with
        | Some conds ->
            for kvp in conds do
                command.Parameters.AddWithValue("@cond_" + kvp.Key, kvp.Value) |> ignore
        | None -> ()

        let results = new List<Dictionary<string, obj>>()
        try
            let reader = command.ExecuteReader()
            while reader.Read() do
                let row = new Dictionary<string, obj>()
                for i in 0 .. reader.FieldCount - 1 do
                    let columnName = reader.GetName(i)
                    row.Add(columnName, reader.[i])
                results.Add(row)
            reader.Close()
        finally
            // Do not close the connection here
            ()
        results

    member this.Insert(tableName: string, values: Dictionary<string, obj>) =
            let columns = String.Join(", ", values.Keys)
            let parameters = String.Join(", ", values.Keys |> Seq.map (fun key -> "@" + key))
            let commandText = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})"
            let command = new MySqlCommand(commandText, connection)

            for kvp in values do
                command.Parameters.AddWithValue("@" + kvp.Key, kvp.Value) |> ignore

            try
                command.ExecuteNonQuery() |> ignore
                true // Return true if the insert was successful
            with
            | ex -> 
                // Handle any exceptions that occur during the database operation
                Debug.WriteLine(sprintf $"An error occurred: {ex.Message}")

                false // Return false if the insert failed


    member this.Update(tableName: string, values: Dictionary<string, obj>, conditions: Dictionary<string, obj>) =
        let setClause = 
            values.Keys 
            |> Seq.map (fun key -> $"{key} = @{key}") 
            |> String.concat ", "

        let whereClause = 
            conditions.Keys 
            |> Seq.map (fun key -> $"{key} = @cond_{key}") 
            |> String.concat " AND "

        let commandText = $"UPDATE {tableName} SET {setClause} WHERE {whereClause}"
        let command = new MySqlCommand(commandText, connection)

        for kvp in values do
            command.Parameters.AddWithValue("@" + kvp.Key, kvp.Value) |> ignore

        for kvp in conditions do
            command.Parameters.AddWithValue("@cond_" + kvp.Key, kvp.Value) |> ignore

        try
            command.ExecuteNonQuery() |> ignore
            true // Return true if the update was successful
        with
        | ex -> 
            // Handle any exceptions that occur during the database operation
            Debug.WriteLine(sprintf $"An error occurred: {ex.Message}")
            false // Return false if the update failed

    member this.Delete(tableName: string, conditions: Dictionary<string, obj>) =
        let whereClause = 
            conditions.Keys 
            |> Seq.map (fun key -> $"{key} = @cond_{key}") 
            |> String.concat " AND "

        let commandText = $"DELETE FROM {tableName} WHERE {whereClause}"
        let command = new MySqlCommand(commandText, connection)

        for kvp in conditions do
            command.Parameters.AddWithValue("@cond_" + kvp.Key, kvp.Value) |> ignore

        try
            command.ExecuteNonQuery() |> ignore
            true // Return true if the delete was successful
        with
        | ex -> 
            // Handle any exceptions that occur during the database operation
            Debug.WriteLine(sprintf $"An error occurred: {ex.Message}")

            false // Return false if the delete failed