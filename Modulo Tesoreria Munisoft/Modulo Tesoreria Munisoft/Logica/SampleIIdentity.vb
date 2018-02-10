Imports System.Security.Principal
Imports System.Data
Imports MySql.Data
Imports MySql.Data.Types
Imports MySql.Data.MySqlClient
Public Class SampleIIdentity
    Implements System.Security.Principal.IIdentity
    Private nameValue As String
    Private authenticatedValue As Boolean
    Private roleValue As ApplicationServices.BuiltInRole
    Private _adaptador As New MySqlDataAdapter
    Private _dtsIdentificacion As New DataSet

    Public ReadOnly Property AuthenticationType As String Implements IIdentity.AuthenticationType
        Get
            Return "Custom Authentication"
        End Get
    End Property

    Public ReadOnly Property IsAuthenticated As Boolean Implements IIdentity.IsAuthenticated
        Get
            Return authenticatedValue
        End Get
    End Property

    Public ReadOnly Property Name As String Implements IIdentity.Name
        Get
            Return nameValue
        End Get
    End Property
    Public ReadOnly Property Role() As ApplicationServices.BuiltInRole
        Get
            Return roleValue
        End Get
    End Property
    Public Sub New(ByVal name As String, ByVal password As String)
        ' The name is not case sensitive, but the password is.
        If IsValidNameAndPassword(name, password) Then
            nameValue = name
            authenticatedValue = True
            roleValue = ApplicationServices.BuiltInRole.Administrator
        Else
            nameValue = ""
            authenticatedValue = False
            roleValue = ApplicationServices.BuiltInRole.Guest
        End If
    End Sub
    Private Function IsValidNameAndPassword(ByVal username As String, ByVal password As String) As Boolean
        Dim passwordold As String = GetPassOld(username)
        Dim passwordnew As String = GetPassNew(password)
        If GetUserValid(username) Then
            Return passwordnew = passwordold
        End If
        Return "direrente" = "dif"
        ''************************************************

        '' Look up the stored hashed password and salt for the username.
        'Dim storedHashedPW As String = GetHashedPassword(username)
        'Dim salt As String = GetSalt(username)

        ''Create the salted hash.
        'Dim rawSalted As String = salt & Trim(password)
        'Dim saltedPwBytes() As Byte = System.Text.Encoding.Unicode.GetBytes(rawSalted)
        'Dim sha1 As New System.Security.Cryptography.SHA1CryptoServiceProvider
        'Dim hashedPwBytes() As Byte = sha1.ComputeHash(saltedPwBytes)
        'Dim hashedPw As String = Convert.ToBase64String(hashedPwBytes)

        '' Compare the hashed password with the stored password.
        'Return hashedPw = storedHashedPW

    End Function
    Private Function GetPassOld(ByVal username As String) As String
        Dim totalRegistros As Integer
        Dim valuePass As String
        Try
            conex_Global()
            _adaptador.SelectCommand = New MySqlCommand("select * from user where usuario=@usuario", _conexion)
            _adaptador.SelectCommand.Parameters.Add("@usuario", MySqlDbType.String).Value = Trim(username).ToLower
            _adaptador.Fill(_dtsIdentificacion)
            totalRegistros = _dtsIdentificacion.Tables(0).Rows.Count
            If totalRegistros > 0 Then
                For Each row In _dtsIdentificacion.Tables(0).Rows
                    valuePass = row("password")
                    Return valuePass
                Next
            Else
                Return ""

            End If

        Catch ex As MySqlException
            MessageBox.Show(ex.Message)

        Finally
            cerrar()
        End Try

    End Function
    Private Function GetPassNew(ByVal username As String) As String

        Return Trim(username).ToLower

    End Function
    Private Function GetUserValid(ByVal username As String) As Boolean
        Dim estado As Boolean = False
        Dim totalRegistros As Integer
        Try
            conex_Global()
            _adaptador.SelectCommand = New MySqlCommand("select * from user where usuario=@usuario", _conexion)
            _adaptador.SelectCommand.Parameters.Add("@usuario", MySqlDbType.String).Value = Trim(username).ToLower
            _adaptador.Fill(_dtsIdentificacion)
            totalRegistros = _dtsIdentificacion.Tables(0).Rows.Count
            If totalRegistros > 0 Then
                estado = True
            Else
                estado = False

            End If

        Catch ex As MySqlException
            MessageBox.Show(ex.Message)

        Finally
            cerrar()
        End Try
        Return estado

    End Function

    Private Function GetHashedPassword(ByVal username As String) As String
        ' Code that gets the user's hashed password goes here.
        ' This example uses a hard-coded hashed passcode.
        ' In general, the hashed passcode should be stored 
        ' outside of the application.
        If Trim(username).ToLower = "testuser" Then
            Return "ZFFzgfsGjgtmExzWBRmZI5S4w6o="
        Else
            Return ""
        End If
    End Function

    Private Function GetSalt(ByVal username As String) As String
        ' Code that gets the user's salt goes here.
        ' This example uses a hard-coded salt.
        ' In general, the salt should be stored 
        ' outside of the application.
        If Trim(username).ToLower = "testuser" Then
            Return "Should be a different random value for each user"
        Else
            Return ""
        End If
    End Function
End Class

