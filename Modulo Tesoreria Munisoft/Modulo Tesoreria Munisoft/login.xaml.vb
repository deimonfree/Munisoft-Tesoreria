Public Class login
    Private Sub Border_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        DragMove()
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub
    Private Sub Sesion_Click(sender As Object, e As RoutedEventArgs)
        Dim dashboard = New MainWindow
        dashboard.Show()
        Me.Close()
    End Sub
End Class
