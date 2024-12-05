namespace Library.Views

open Avalonia.Controls
open Avalonia.Markup.Xaml
open Library.ViewModels
//open Avalonia.Interactivity

type BorrowingReturningView() as this = 
    inherit UserControl()

    do 
        this.InitializeComponent()
        this.DataContext<-BorrowingReturningViewModel()

    member private this.InitializeComponent() =
        AvaloniaXamlLoader.Load(this)

     //member this.OnBorrowingButtonClick(sender:obj, e:RoutedEventArgs)=
     //   let BorrowingView= BorrowingView() :> UserControl
     //   this.FindControl<ContentControl>("ContentArea").Content <- BorrowingView