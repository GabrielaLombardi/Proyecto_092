Public Class Form5
    Dim Fecha As String

    Sub Limpiar()
        TextBox1.Clear()
        TextBox1.Focus()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (TextBox1.Text = "") Then
            MsgBox("Favor ingresar la Fecha que deseas", vbQuestion, "Ups! Algo pasó")
            Exit Sub
        Else
            Fecha = UCase(TextBox1.Text)
        End If

        ComboBox1.Items.Add(Fecha)
        MsgBox("Salida Creada", vbInformation, "")
        Limpiar()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ComboBox1.Items.Remove(ComboBox1.SelectedItem)
        MsgBox("Salida Eliminada", vbInformation, "")
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
End Class