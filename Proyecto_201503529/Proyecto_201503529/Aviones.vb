Imports MySql.Data.MySqlClient
Public Class Form3
    Dim Nombre As String
    Dim Capacidad As Integer
    Dim idAvion As Int32

    Private adaptador As New MySqlDataAdapter

    Private Sub cargarAvion()
        ComboBox1.Items.Clear()
        ConexionBD.conectar()

        Try
            adaptador.SelectCommand = New MySqlCommand("select nombre from avion", ConexionBD.getConexion())
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
        TextBox2.Clear()
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
            MsgBox("Favor ingresar el Nombre del nuevo Avión", vbQuestion, "Ups! Algo pasó")
            Exit Sub
        Else
            Nombre = UCase(TextBox1.Text)
        End If
        If (TextBox2.Text = "") Then
            MsgBox("Favor ingresar la capacidad del Avión", vbQuestion, "Ups! Algo pasó")
            Exit Sub
        ElseIf (IsNumeric(TextBox2.Text)) Then
            Capacidad = TextBox2.Text
        End If

        Dim sentencia = "insert into avion (nombre, capacidad) values ('" + Nombre + "','" + Capacidad.ToString() + "')"

        If (ejecutarSentencia(sentencia)) Then
            MsgBox("Avion Creado", vbInformation, "")
        Else
            MsgBox("Ocurrio un error")
        End If
        Limpiar()
        cargarAvion()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim sentencia = "delete from avion where id = " + idAvion.ToString()

        If (ejecutarSentencia(sentencia)) Then
            MsgBox("Avion Eliminado", vbInformation, "")
        Else
            MsgBox("Ocurrio un error")
        End If
        Limpiar()
        cargarAvion()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If MsgBox("¿Está seguro que desea limpiar todos los elementos?", vbQuestion + vbYesNo, "Advertencia!") = vbYes Then
            ComboBox1.Items.Clear()
            Limpiar()
            cargarAvion()
            Exit Sub
        Else
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub carga(sender As Object, e As EventArgs) Handles MyBase.Load
        cargarAvion()
    End Sub

    Private Sub cargarDatos(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ConexionBD.conectar()

        Try
            adaptador.SelectCommand = New MySqlCommand("select * from avion Where nombre = '" + ComboBox1.SelectedItem + "'", ConexionBD.getConexion())
            Dim result As MySqlDataReader = adaptador.SelectCommand.ExecuteReader()
            While (result.Read())
                idAvion = result(0)
                TextBox1.Text = result(1)
                TextBox2.Text = result(2)
                Exit While
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        ConexionBD.desconectar()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (TextBox1.Text = "") Then
            MsgBox("Favor ingresar el Nombre del nuevo Avión", vbQuestion, "Ups! Algo pasó")
            Exit Sub
        Else
            Nombre = UCase(TextBox1.Text)
        End If
        If (TextBox2.Text = "") Then
            MsgBox("Favor ingresar la capacidad del Avión", vbQuestion, "Ups! Algo pasó")
            Exit Sub
        ElseIf (IsNumeric(TextBox2.Text)) Then
            Capacidad = TextBox2.Text
        End If

        Dim sentencia = "update avion set nombre = '" + Nombre + "', capacidad = '" + Capacidad.ToString() + "' where id = " + idAvion.ToString()

        If (ejecutarSentencia(sentencia)) Then
            MsgBox("AVión actualizado", vbInformation, "")
        Else
            MsgBox("Ocurrio un error")
        End If
        Limpiar()
        cargarAvion()
    End Sub
End Class