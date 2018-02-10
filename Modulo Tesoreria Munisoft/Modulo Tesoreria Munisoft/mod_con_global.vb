Imports MySql.Data
Imports MySql.Data.Types
Imports MySql.Data.MySqlClient
Module mod_con_global
    Public _cadena As String
    Public _conexion As New MySqlConnection

    'conexion global
    Public Function conex_Global() As Boolean
        Dim estado As Boolean = True
        Try
            '_cadena = ("server=192.168.1.250;user id=deimonfree;password=s1st3m4s;database=munisystem")
            _cadena = ("server=localhost;user id=root;password=root;database=munisystem")
            '_cadena = ("server=localhost;user id=root;password=root;database=pruebas")
            _conexion = New MySqlConnection(_cadena)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            estado = False

        End Try
        Return estado
    End Function

    'CERRAR CONEXION GLOBAL
    Public Sub cerrar()
        _conexion.Close()
    End Sub
End Module