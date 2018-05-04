Imports MySql.Data.MySqlClient

Public Class Form8
    Dim Fecha As String
    Private idEscala As New Integer
    Private idAeropuerto As New LinkedList(Of Integer)
    Private adaptador As New MySqlDataAdapter

    Private Sub cargarAeropuertos()
        idAeropuerto.Clear()
        ComboBox2.Items.Clear()
        ConexionBD.conectar()

        Try
            adaptador.SelectCommand = New MySqlCommand("select id, nombre from aeropuerto", ConexionBD.getConexion())
            Dim result As MySqlDataReader = adaptador.SelectCommand.ExecuteReader()
            While (result.Read())
                idAeropuerto.AddLast(result(0))
                ComboBox2.Items.Add(result(1))
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        ConexionBD.desconectar()
    End Sub

    Private Sub cargarEscalas()
        ComboBox1.Items.Clear()
        ConexionBD.conectar()

        Try
            adaptador.SelectCommand = New MySqlCommand("select id from escala", ConexionBD.getConexion())
            Dim result As MySqlDataReader = adaptador.SelectCommand.ExecuteReader()
            While (result.Read())
                ComboBox1.Items.Add(result(0))
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        ConexionBD.desconectar()
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

    Sub Limpiar()
        TextBox1.Clear()
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        ComboBox3.SelectedIndex = -1
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (TextBox1.Text = "") Then
            MsgBox("Favor ingresar el nombre del Aeropuerto", vbQuestion, "Ups! Algo pasó")
            Exit Sub
        Else
            Fecha = UCase(TextBox1.Text)
        End If

        If (ComboBox2.SelectedIndex = -1) Then
            MsgBox("Debe seleccionar un aeropuerto para la escala")
            Exit Sub
        End If

        If (ComboBox3.SelectedIndex = -1) Then
            MsgBox("Debe seleccionar un programa para la escala")
            Exit Sub
        End If

        Dim sentencia = "insert into escala(num_escala, aeropuerto_id, programa_vuelo) values (" + Fecha

        MsgBox("Escala Creada", vbInformation, "")
        Limpiar()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ComboBox1.Items.Remove(ComboBox1.SelectedItem)
        MsgBox("Escala Eliminada", vbInformation, "")
        Limpiar()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If MsgBox("¿Está seguro que desea limpiar todos los elementos?", vbQuestion + vbYesNo, "Advertencia!") = vbYes Then
            ComboBox1.Items.Clear()
            Limpiar()
            Exit Sub
        Else
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        Form1.Visible = True
    End Sub

    Private Sub buscar(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ConexionBD.conectar()

        Try
            adaptador.SelectCommand = New MySqlCommand("select * from escala where id = " + ComboBox1.SelectedItem.ToString(), ConexionBD.getConexion())
            Dim result As MySqlDataReader = adaptador.SelectCommand.ExecuteReader()
            While (result.Read())
                idEscala = result(0)
                TextBox1.Text = result(1)
                ComboBox2.SelectedIndex = idAeropuerto.ToList().IndexOf(result(2))
                ComboBox3.SelectedItem = result(3)
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        ConexionBD.desconectar()
    End Sub

    Private Sub carga(sender As Object, e As EventArgs) Handles MyBase.Load
        cargarAeropuertos()
        cargarEscalas()
    End Sub
End Class