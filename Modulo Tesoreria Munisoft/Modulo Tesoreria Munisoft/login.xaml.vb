Public Class login
    Private UserNameEnvio As String
    Private Sub Border_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        DragMove()
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub
    Private Sub Sesion_Click(sender As Object, e As RoutedEventArgs)
        'Dim login As New class_login(Me.UsernameTextBox.Text, Me.PasswordTextBox.Text)
        'pbIcon.Visible = False
        'pbLoad.Visible = True
        Dim samplePrincipal As New SampleIPrincipal(Me.txt_Usuario.Text, Me.txt_Pasword.Password)
        Me.txt_Pasword.Password = ""
        If (Not samplePrincipal.Identity.IsAuthenticated) Then
            ' The user is still not validated.
            'pbIcon.Visible = True
            'pbLoad.Visible = False
            MsgBox("El usuario o contraseña son incorrectos")

        Else
            ' Update the current principal.
            My.User.CurrentPrincipal = samplePrincipal
            If My.User.IsAuthenticated Then
                UserNameEnvio = My.User.Name
            Else
                UserNameEnvio = "User"
            End If

            'If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
            '    tsmUserName.Text = "User is an Administrator"
            'Else
            '    Me.Label6.Text = "User is not an Administrator"
            'End If
            Dim formulario As New MainWindow
            'formulario.UserName = UserNameEnvio
            MsgBox("Bienvenido")
            formulario.Show()
            Me.Close()
        End If
    End Sub
End Class
