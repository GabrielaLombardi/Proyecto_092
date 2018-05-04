Imports MySql.Data.MySqlClient

Public Class Form7
    Dim noPasajero As Integer
    Private adaptador As New MySqlDataAdapter
    Private idPrograma As New Integer
    Private idAvion As New LinkedList(Of Integer)
    Private idLinea As New LinkedList(Of Integer)

    Private Sub cargarAviones()
        idAvion.Clear()
        ComboBox2.Items.Clear()
        ConexionBD.conectar()

        Try
            adaptador.SelectCommand = New MySqlCommand("select id, nombre from avion", ConexionBD.getConexion())
            Dim result As MySqlDataReader = adaptador.SelectCommand.ExecuteReader()
            While (result.Read())
                idAvion.AddLast(result(0))
                ComboBox2.Items.Add(result(1))
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        ConexionBD.desconectar()
    End Sub

    Private Sub cargarLineas()
        idLinea.Clear()
        ComboBox3.Items.Clear()
        ConexionBD.conectar()

        Try
            adaptador.SelectCommand = New MySqlCommand("select id, nombre from linea_aerea", ConexionBD.getConexion())
            Dim result As MySqlDataReader = adaptador.SelectCommand.ExecuteReader()
            While (result.Read())
                idLinea.AddLast(result(0))
                ComboBox3.Items.Add(result(1))
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        ConexionBD.desconectar()
    End Sub

    Private Sub cargarProgramas()
        ComboBox1.Items.Clear()
        ConexionBD.conectar()

        Try
            adaptador.SelectCommand = New MySqlCommand("select id from programa_vuelo", ConexionBD.getConexion())
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

    Private Sub consultar(consulta As String)

    End Sub

    Sub Limpiar()
        TextBox2.Clear()
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        ComboBox3.SelectedIndex = -1
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (TextBox2.Text = "") Then
            MsgBox("Favor ingresar la cantidad de pasajeros", vbQuestion, "Ups! Algo pasó")
            Exit Sub
        ElseIf IsNumeric(TextBox2.Text) Then
            noPasajero = Int32.Parse(TextBox2.Text.ToString())
        Else
            MsgBox("Solo ingrese numeros en el campo de pasajeros")
        End If

        If (ComboBox2.SelectedIndex = -1) Then
            MsgBox("Debe seleccionar un aviòn para el programa")
            Exit Sub
        End If

        If (ComboBox3.SelectedIndex = -1) Then
            MsgBox("Debe seleccionar una aerolinea para el programa")
            Exit Sub
        End If

        Dim sentencia = "insert into programa_vuelo(num_pasajeros, linea_aerea_id, avion_id) values (" + noPasajero.ToString() + "," + idLinea.ElementAt(ComboBox3.SelectedIndex).ToString() + "," + idAvion.ElementAt(ComboBox2.SelectedIndex).ToString() + ")"
        If (ejecutarSentencia(sentencia)) Then
            MsgBox("Programa Creado!")
        End If
        Limpiar()
        cargarProgramas()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim sentencia = "delete from programa_vuelo where id =" + idPrograma.ToString()
        If (ejecutarSentencia(sentencia)) Then
            MsgBox("Programa Eliminado", vbInformation, "")
        End If
        Limpiar()
        cargarProgramas()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If MsgBox("¿Está seguro que desea limpiar todos los elementos?", vbQuestion + vbYesNo, "Advertencia!") = vbYes Then
            Limpiar()
            Exit Sub
        Else

        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        Form1.Visible = True
    End Sub

    Private Sub cargar(sender As Object, e As EventArgs) Handles MyBase.Load
        cargarAviones()
        cargarLineas()
        cargarProgramas()
    End Sub

    Private Sub buscar(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ConexionBD.conectar()

        Try
            adaptador.SelectCommand = New MySqlCommand("select * from programa_vuelo where id = " + ComboBox1.SelectedItem.ToString(), ConexionBD.getConexion())
            Dim result As MySqlDataReader = adaptador.SelectCommand.ExecuteReader()
            While (result.Read())
                idPrograma = result(0)
                TextBox2.Text = result(1)
                MessageBox.Show("Avion " + result(3).ToString() + "; Linea " + result(2).ToString())
                ComboBox2.SelectedIndex = idAvion.ToList().IndexOf(result(3))
                ComboBox3.SelectedIndex = idLinea.ToList().IndexOf(result(2))
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        ConexionBD.desconectar()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (TextBox2.Text = "") Then
            MsgBox("Favor ingresar la cantidad de pasajeros", vbQuestion, "Ups! Algo pasó")
            Exit Sub
        ElseIf IsNumeric(TextBox2.Text) Then
            noPasajero = Int32.Parse(TextBox2.Text.ToString())
        Else
            MsgBox("Solo ingrese numeros en el campo de pasajeros")
        End If

        If (ComboBox2.SelectedIndex = -1) Then
            MsgBox("Debe seleccionar un aviòn para el programa")
            Exit Sub
        End If

        If (ComboBox3.SelectedIndex = -1) Then
            MsgBox("Debe seleccionar una aerolinea para el programa")
            Exit Sub
        End If

        Dim sentencia = "update programa_vuelo set num_pasajeros = " + noPasajero.ToString() + ", linea_aerea_id = " + idLinea.ElementAt(ComboBox3.SelectedIndex).ToString() + ", avion_id = " + idAvion.ElementAt(ComboBox2.SelectedIndex).ToString() + " where id = " + idPrograma.ToString()
        If (ejecutarSentencia(sentencia)) Then
            MsgBox("Programa Modificado!")
        End If
        Limpiar()
        cargarProgramas()
    End Sub
End Class