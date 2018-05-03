Public Class Frm_ListaUsuarios

    Dim datacontext As New DataFarmacia

    Private Sub btn_modificar_Click(sender As System.Object, e As System.EventArgs) Handles btn_modificar.Click

        If dgv_lista_usuarios.SelectedRows.Count > 0 Then
            frm_nuevo_usuario.txt_id_usuario.Text = dgv_lista_usuarios.Item("id_usuario", dgv_lista_usuarios.SelectedRows(0).Index).Value
            frm_nuevo_usuario.cbo_perfil_usuario.Text = dgv_lista_usuarios.Item("perfil_usuario", dgv_lista_usuarios.SelectedRows(0).Index).Value
            frm_nuevo_usuario.txt_nombre_usuario.Text = dgv_lista_usuarios.Item("nombre_usuario", dgv_lista_usuarios.SelectedRows(0).Index).Value
            frm_nuevo_usuario.txt_contraseña_usuario.Text = dgv_lista_usuarios.Item("contraseña_usuario", dgv_lista_usuarios.SelectedRows(0).Index).Value
        End If
        frm_nuevo_usuario.btn_guardar.Enabled = False
        frm_nuevo_usuario.Show()
    End Sub

    Private Sub Frm_ListaUsuarios_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        armargrilla()
        cargargrilla()
    End Sub

    Private Sub armargrilla()
        dgv_lista_usuarios.Enabled = True
        dgv_lista_usuarios.AutoGenerateColumns = False
        dgv_lista_usuarios.Columns.Clear()

        dgv_lista_usuarios.Columns.Add("id_usuario", "Id_Usuario")
        dgv_lista_usuarios.Columns.Add("nombre_usuario", " Nombre")
        dgv_lista_usuarios.Columns.Add("contraseña_usuario", "Contraseña")
        dgv_lista_usuarios.Columns.Add("perfil_usuario", "Perfil")

        dgv_lista_usuarios.Columns(0).DataPropertyName = "id_usuario"
        dgv_lista_usuarios.Columns(0).Visible = False
        dgv_lista_usuarios.Columns(1).DataPropertyName = "nombre_usuario"
        dgv_lista_usuarios.Columns(2).DataPropertyName = "contraseña_usuario"
        dgv_lista_usuarios.Columns(3).DataPropertyName = "perfil_usuario"

        dgv_lista_usuarios.ClearSelection()
    End Sub

    Public Sub cargargrilla()
        Dim consultausu = From U In datacontext.Usuario Select U.id_usuario, U.nombre_usuario, U.contraseña_usuario, U.perfil_usuario
        dgv_lista_usuarios.DataSource = consultausu
    End Sub

    Private Sub btn_eliminar_Click(sender As System.Object, e As System.EventArgs) Handles btn_eliminar.Click
        Dim eliminar = (From C In datacontext.Usuario Where C.id_usuario = CInt(dgv_lista_usuarios.Item("id_usuario", dgv_lista_usuarios.SelectedRows(0).Index).Value)).ToList()(0)

        Select Case MsgBox("Se eliminará el usuario seleccionado, desea continuar?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Eliminar usuario")
            Case MsgBoxResult.Yes
                datacontext.Usuario.DeleteOnSubmit(eliminar)
                datacontext.SubmitChanges()
                MsgBox("El usuario ha sido eliminado")
                Me.cargargrilla()
        End Select
    End Sub

    Private Sub btn_cancelar_Click(sender As System.Object, e As System.EventArgs) Handles btn_cancelar.Click
        Me.Close()
        Me.Dispose()
    End Sub
End Class