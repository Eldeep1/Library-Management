namespace Library.Models

open System.ComponentModel
open System

[<CLIMutable>]
type Book = {
    Id: int
    Name: string
    Author: string
    Genre: string
    Available: string
    Borrowing: bool
}
