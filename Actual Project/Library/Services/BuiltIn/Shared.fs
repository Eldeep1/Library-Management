namespace Library.Functions

open System.Collections.ObjectModel

module Shared=

    let rec toList' (source: seq<'a>) : 'a list =
        let enumerator = source.GetEnumerator()
        
        let rec collect acc =
            if enumerator.MoveNext() then
                collect (enumerator.Current :: acc)
            else
                List.rev acc
                
        collect []

    
    let rec clear' (collection: System.Collections.Generic.ICollection<'a>) : unit =
        if collection.Count > 0 then
            let first = collection |> Seq.head
            collection.Remove(first) |> ignore
            clear' collection


    let rec filter' (f: 'a -> bool) (source: seq<'a>) : seq<'a> =
        let enumerator = source.GetEnumerator()

        let rec collect () =
            seq {
                if enumerator.MoveNext() then
                    let current = enumerator.Current
                    if f current then
                        yield current
                    yield! collect ()
            }
        
        collect ()
        
    
    let rec iter' (f: 'a -> unit) (source: 'a list) : unit =
        match source with
        | [] -> ()  
        | head :: tail ->
            f head     
            iter' f tail  

    let addItems (collection: ObservableCollection<'T>) (items: 'T list) =
        items |> List.iter (fun item -> collection.Add(item))


    let removeItem (collection: ObservableCollection<'T>) (item: 'T) =
                collection.Remove(item) |> ignore
