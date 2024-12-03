namespace Library.Views

open Avalonia.Controls
open Avalonia.Markup.Xaml

type BorrowingReturningView() as this = 
    inherit UserControl()

    do 
        this.InitializeComponent()

    member private this.InitializeComponent() =
        AvaloniaXamlLoader.Load(this)