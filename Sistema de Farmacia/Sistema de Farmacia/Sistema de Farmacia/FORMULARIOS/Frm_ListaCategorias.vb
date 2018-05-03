Public Class Frm_ListaCategorias
    Dim datacontext As New DataFarmacia

    Private Sub btn_modificar_Click(sender As System.Object, e As System.EventArgs) Handles btn_modificar.Click

        If dgv_lista_categoria.SelectedRows.Count > 0 Then
            Frm_NuevaCategoria.txt_id_categoria.Text = dgv_lista_categoria.Item("id_categoria", dgv_lista_categoria.SelectedRows(0).Index).Value
            Frm_NuevaCategoria.txt_nombre_categoria.Text = dgv_lista_categoria.Item("nombre_categoria", dgv_lista_categoria.SelectedRows(0).Index).Value
        End If
        Frm_NuevaCategoria.btn_guardar.Enabled = False
        Frm_NuevaCategoria.Show()
    End Sub

    Private Sub btn_eliminar_Click(sender As System.Object, e As System.EventArgs) Handles btn_eliminar.Click
        Dim eliminar = (From C In datacontext.Categoria Where C.id_categoria = CInt(dgv_lista_categoria.Item("id_categoria", dgv_lista_categoria.SelectedRows(0).Index).Value)).ToList()(0)

        Select Case MsgBox("Se eliminará la categoria seleccionada, desea continuar?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Eliminar categoria")
            Case MsgBoxResult.Yes
                datacontext.Categoria.DeleteOnSubmit(eliminar)
                datacontext.SubmitChanges()
                MsgBox("La categoria ha sido eliminada")
                Me.cargargrilla()
        End Select
    End Sub

    Private Sub Frm_ListaCategorias_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        armargrilla()
        cargargrilla()
    End Sub

    Private Sub armargrilla()
        dgv_lista_categoria.Enabled = True
        dgv_lista_categoria.AutoGenerateColumns = False
        dgv_lista_categoria.Columns.Clear()

        dgv_lista_categoria.Columns.Add("id_categoria", "Id_Categoria")
        dgv_lista_categoria.Columns.Add("nombre_categoria", "Nombre")

        dgv_lista_categoria.Columns(0).DataPropertyName = "id_categoria"
        dgv_lista_categoria.Columns(0).Visible = False
        dgv_lista_categoria.Columns(1).DataPropertyName = "nombre_categoria"
        dgv_lista_categoria.Columns(1).Width = 250
        dgv_lista_categoria.ClearSelection()
    End Sub

    Public Sub cargargrilla()
        Dim consultaCategoria = From U In datacontext.Categoria Select U.id_categoria, U.nombre_categoria
        dgv_lista_categoria.DataSource = consultaCategoria
    End Sub

    Private Sub btn_cancelar_Click(sender As System.Object, e As System.EventArgs) Handles btn_cancelar.Click
        Me.Close()
        Me.Dispose()
    End Sub
End Class