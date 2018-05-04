Imports MySql.Data.MySqlClient

Public Class Form2
    Dim Nombre As String
    Dim Ciudad As String
    Dim Pais As String
    Dim idAeropuerto As Int32

    Private adaptador As New MySqlDataAdapter

    Private Sub cargarAeropuerto()
        ComboBox1.Items.Clear()
        ConexionBD.conectar()

        Try
            adaptador.SelectCommand = New MySqlCommand("select nombre from aeropuerto", ConexionBD.getConexion())
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
        TextBox3.Clear()
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

    Private Sub consultar(consulta As String)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (TextBox1.Text = "") Then
            MsgBox("Favor ingresar el nombre del Aeropuerto", vbQuestion, "Ups! Algo pasó")
            Exit Sub
        Else
            Nombre = UCase(TextBox1.Text)
        End If
        If (TextBox2.Text = "") Then
            MsgBox("Favor ingresar la ciudad del Aeropuerto", vbQuestion, "Ups! Algo pasó")
            Exit Sub
        Else
            Ciudad = UCase(TextBox2.Text)
        End If
        If (TextBox3.Text = "") Then
            MsgBox("Favor ingresar el país del Aeropuerto", vbQuestion, "Ups! Algo pasó")
            Exit Sub
        Else
            Pais = UCase(TextBox3.Text)
        End If

        Dim sentencia = "insert into aeropuerto (nombre, ciudad, pais) values ('" + Nombre + "','" + Ciudad + "','" + Pais + "')"

        If (ejecutarSentencia(sentencia)) Then
            MsgBox("Aeropuerto Creado", vbInformation, "")
        Else
            MsgBox("Ocurrio un error")
        End If
        Limpiar()
        cargarAeropuerto()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim sentencia = "delete from aeropuerto where id = " + idAeropuerto.ToString()

        If (ejecutarSentencia(sentencia)) Then
            MsgBox("Aeropuerto Eliminado", vbInformation, "")
        Else
            MsgBox("Ocurrio un error")
        End If
        Limpiar()
        cargarAeropuerto()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If MsgBox("¿Está seguro que desea limpiar todos los elementos?", vbQuestion + vbYesNo, "Advertencia!") = vbYes Then
            ComboBox1.Items.Clear()
            Limpiar()
            cargarAeropuerto()
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
        cargarAeropuerto()
    End Sub

    Private Sub cargarDatos(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ConexionBD.conectar()

        Try
            adaptador.SelectCommand = New MySqlCommand("select * from aeropuerto Where nombre = '" + ComboBox1.SelectedItem + "'", ConexionBD.getConexion())
            Dim result As MySqlDataReader = adaptador.SelectCommand.ExecuteReader()
            While (result.Read())
                idAeropuerto = result(0)
                TextBox1.Text = result(1)
                TextBox2.Text = result(2)
                TextBox3.Text = result(3)
                Exit While
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        ConexionBD.desconectar()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (TextBox1.Text = "") Then
            MsgBox("Favor ingresar el nombre del Aeropuerto", vbQuestion, "Ups! Algo pasó")
            Exit Sub
        Else
            Nombre = UCase(TextBox1.Text)
        End If
        If (TextBox2.Text = "") Then
            MsgBox("Favor ingresar la ciudad del Aeropuerto", vbQuestion, "Ups! Algo pasó")
            Exit Sub
        Else
            Ciudad = UCase(TextBox2.Text)
        End If
        If (TextBox3.Text = "") Then
            MsgBox("Favor ingresar el país del Aeropuerto", vbQuestion, "Ups! Algo pasó")
            Exit Sub
        Else
            Pais = UCase(TextBox3.Text)
        End If

        Dim sentencia = "update aeropuerto set nombre = '" + Nombre + "', ciudad = '" + Ciudad + "', pais = '" + Pais + "' where id = " + idAeropuerto.ToString()

        If (ejecutarSentencia(sentencia)) Then
            MsgBox("Aeropuerto actualizado", vbInformation, "")
        Else
            MsgBox("Ocurrio un error")
        End If
        Limpiar()
        cargarAeropuerto()
    End Sub
End Class