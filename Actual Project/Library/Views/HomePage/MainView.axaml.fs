namespace Library.Views

open Avalonia.Controls
open Avalonia.Markup.Xaml
open Avalonia.Interactivity

type MainView () as this = 
    inherit UserControl ()

    do 
        this.InitializeComponent()

    member private this.InitializeComponent() =
        AvaloniaXamlLoader.Load(this)


    //member this.OnNavigateButtonClick(sender: obj, e: RoutedEventArgs) =
    //    let testView = TestView()
    //    //testView.Show()
    //    this.FindControl<ContentControl>("ContentArea").Content <- testView

    member this.OnBookManagingButtonClick(sender: obj, e: RoutedEventArgs) =
        let bookManagingView = BookManagingView() :> UserControl
        this.FindControl<ContentControl>("ContentArea").Content <- bookManagingView

    member this.OnMemberManagingButtonClick(sender:obj, e:RoutedEventArgs)=
        let memberManagingView= MemberManagingView() :> UserControl
        this.FindControl<ContentControl>("ContentArea").Content <- memberManagingView

    member this.OnBorrowingReturningButtonClick(sender: obj, e: RoutedEventArgs) =
       let borrowingReturningView = BorrowingReturningView() :> UserControl
       this.FindControl<ContentControl>("ContentArea").Content <- borrowingReturningView

    member this.OnReportsButtonClick(sender: obj, e: RoutedEventArgs) =
       let reportView = ReportsView() :> UserControl
       this.FindControl<ContentControl>("ContentArea").Content <- reportView
