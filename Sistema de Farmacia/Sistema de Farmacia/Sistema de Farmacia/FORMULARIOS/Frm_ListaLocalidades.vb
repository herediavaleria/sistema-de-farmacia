Public Class Frm_ListaLocalidades
    Dim datacontext As New DataFarmacia

    Private Sub btn_modificar_Click(sender As System.Object, e As System.EventArgs) Handles btn_modificar.Click

        If dgv_lista_localidad.SelectedRows.Count > 0 Then
            Frm_Nueva_Localidad.txt_id_localidad.Text = dgv_lista_localidad.Item("id_localidad", dgv_lista_localidad.SelectedRows(0).Index).Value
            Frm_Nueva_Localidad.txt_nombre_localidad.Text = dgv_lista_localidad.Item("nombre_localidad", dgv_lista_localidad.SelectedRows(0).Index).Value
        End If

        Frm_Nueva_Localidad.btn_guardar.Enabled = False
        Frm_Nueva_Localidad.Show()
    End Sub

    Private Sub btn_eliminar_Click(sender As System.Object, e As System.EventArgs) Handles btn_eliminar.Click
        Dim eliminar = (From C In datacontext.Localidad Where C.id_localidad = CInt(dgv_lista_localidad.Item("id_localidad", dgv_lista_localidad.SelectedRows(0).Index).Value)).ToList()(0)

        Select Case MsgBox("Se eliminará la localidad seleccionada, desea continuar?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Eliminar localidad")
            Case MsgBoxResult.Yes
                datacontext.Localidad.DeleteOnSubmit(eliminar)
                datacontext.SubmitChanges()
                MsgBox("La localidad ha sido eliminada")
                Me.cargargrilla()
        End Select
    End Sub

    Private Sub Frm_ListaLocalidades_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        armargrilla()
        cargargrilla()
    End Sub

    Private Sub armargrilla()
        dgv_lista_localidad.Enabled = True
        dgv_lista_localidad.AutoGenerateColumns = False
        dgv_lista_localidad.Columns.Clear()

        dgv_lista_localidad.Columns.Add("id_localidad", "Id_Localidad")
        dgv_lista_localidad.Columns.Add("nombre_localidad", "Nombre")
       
        dgv_lista_localidad.Columns(0).DataPropertyName = "id_localidad"
        dgv_lista_localidad.Columns(0).Visible = False
        dgv_lista_localidad.Columns(1).DataPropertyName = "nombre_localidad"
        dgv_lista_localidad.Columns(1).Width = 250
        dgv_lista_localidad.ClearSelection()
    End Sub

    Public Sub cargargrilla()
        Dim consultaLocaildad = From U In datacontext.Localidad Select U.id_localidad, U.nombre_localidad
        dgv_lista_localidad.DataSource = consultaLocaildad
    End Sub


    Private Sub dgv_lista_localidad_CellDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_lista_localidad.CellDoubleClick
        Frm_NuevoProveedor.txt_id_localidad.Text = dgv_lista_localidad.SelectedCells.Item(0).Value
        Frm_NuevoProveedor.txt_nombre_localidad.Text = dgv_lista_localidad.SelectedCells(1).Value
        Me.Close()
    End Sub
End Class