
namespace Library.Views

open Avalonia.Controls
open Avalonia.Markup.Xaml
open Library.ViewModels

type MemberManagingView() as this = 
    inherit UserControl()

    do 
        this.InitializeComponent()
        this.DataContext<-MemberManagingViewModel()

    member private this.InitializeComponent() =
        AvaloniaXamlLoader.Load(this)