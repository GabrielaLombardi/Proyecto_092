Imports MySql.Data.MySqlClient
Public Class Form4
    Dim NombreLinea As String
    Dim idLinea As Int32

    Private adaptador As New MySqlDataAdapter
    Private Sub cargarLinea()
        ComboBox1.Items.Clear()
        ConexionBD.conectar()

        Try
            adaptador.SelectCommand = New MySqlCommand("select nombre from linea_aerea", ConexionBD.getConexion())
            Dim result As MySqlDataReader = adaptador.SelectCommand.ExecuteReader()
            While (result.Read())
                ComboBox1.Items.Add(result(0))
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        ConexionBD.desconectar()
    End Sub
    Sub Limpiar()
        TextBox1.Clear()
        TextBox1.Focus()
    End Sub
    Private Function ejecutarSentencia(sent As String) As Boolean
        Dim estado = True
        ConexionBD.conectar()

        Try
            adaptador.InsertCommand = New MySqlCommand(sent, ConexionBD.getConexion())
            adaptador.InsertCommand.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
            estado = False
        End Try

        ConexionBD.desconectar()
        Return estado
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (TextBox1.Text = "") Then
            MsgBox("Favor ingresar el nombre de la Línea", vbQuestion, "Ups! Algo pasó")
            Exit Sub
        Else
            NombreLinea = UCase(TextBox1.Text)
        End If
        Dim sentencia = "insert into linea_aerea (nombre) values ('" + NombreLinea + "')"

        If (ejecutarSentencia(sentencia)) Then
            MsgBox("Línea Aérea Creada", vbInformation, "")
        Else
            MsgBox("Ocurrio un error")
        End If
        Limpiar()
        cargarLinea()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim sentencia = "delete from linea_aerea where id = " + idLinea.ToString()

        If (ejecutarSentencia(sentencia)) Then
            MsgBox("Línea Aérea Eliminada", vbInformation, "")
        Else
            MsgBox("Ocurrio un error")
        End If
        Limpiar()
        cargarLinea()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If MsgBox("¿Está seguro que desea limpiar todos los elementos?", vbQuestion + vbYesNo, "Advertencia!") = vbYes Then
            ComboBox1.Items.Clear()
            Limpiar()
            cargarLinea()
            Exit Sub
        Else
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        Form1.Visible = True
    End Sub

    Private Sub carga(sender As Object, e As EventArgs) Handles MyBase.Load
        cargarLinea()
    End Sub

    Private Sub cargarDatos(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ConexionBD.conectar()

        Try
            adaptador.SelectCommand = New MySqlCommand("select * from linea_aerea Where nombre = '" + ComboBox1.SelectedItem + "'", ConexionBD.getConexion())
            Dim result As MySqlDataReader = adaptador.SelectCommand.ExecuteReader()
            While (result.Read())
                idLinea = result(0)
                TextBox1.Text = result(1)
                Exit While
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        ConexionBD.desconectar()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (TextBox1.Text = "") Then
            MsgBox("Favor ingresar el nombre de la Línea", vbQuestion, "Ups! Algo pasó")
            Exit Sub
        Else
            NombreLinea = UCase(TextBox1.Text)
        End If

        Dim sentencia = "update linea_aerea set nombre = '" + NombreLinea + "' where id = " + idLinea.ToString()

        If (ejecutarSentencia(sentencia)) Then
            MsgBox("Línea Aérea Actualizada", vbInformation, "")
        Else
            MsgBox("Ocurrio un error")
        End If
        Limpiar()
        cargarLinea()
    End Sub
End Class