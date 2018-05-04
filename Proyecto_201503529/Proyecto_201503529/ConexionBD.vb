Imports MySql.Data
Imports MySql.Data.MySqlClient

Module ConexionBD
    Private con As New MySqlConnection("server=localhost;User Id=root;database=programas_vuelos;Password=MenaLombardi")

    Public Sub conectarBD()
        Try
            con.Open()

            MsgBox("Conexion exitosa!")
            desconectar()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Function getConexion() As MySqlConnection
        Return con
    End Function

    Public Sub conectar()
        Try
            con.Open()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub desconectar()
        Try
            con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Module
