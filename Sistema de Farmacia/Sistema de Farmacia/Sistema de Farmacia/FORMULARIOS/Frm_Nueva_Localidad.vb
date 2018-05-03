Public Class Frm_Nueva_Localidad

    Dim datacontext As New DataFarmacia

    Private Sub Frm_Nuevo_Pais_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label1.Visible = False
        txt_id_localidad.Visible = False
    End Sub

    Private Sub btn_guardar_Click(sender As System.Object, e As System.EventArgs) Handles btn_guardar.Click
        Try
            Dim buscapais = (From localidad In datacontext.Localidad Select localidad.nombre_localidad Where nombre_localidad = txt_nombre_localidad.Text.ToUpper).Any
            If buscapais = True Then
                MsgBox("La localidad ingresada ya existe")
                txt_nombre_localidad.Clear()
                Exit Sub
            End If
            If txt_nombre_localidad.Text.Length = 0 Then
                MsgBox("Debe completar todos los campos requeridos")
                Exit Sub
            End If
            Dim local = New Localidad
            local.nombre_localidad = txt_nombre_localidad.Text
            datacontext.Localidad.InsertOnSubmit(local)
            datacontext.SubmitChanges()
            MsgBox("La localidad se ha creado correctamente", vbInformation)
            txt_nombre_localidad.Clear()
        Catch ex As Exception
            MsgBox("La localidad NO fue creada")
            txt_nombre_localidad.Clear()
        End Try
    End Sub

    Private Sub btn_actualizar_Click(sender As System.Object, e As System.EventArgs) Handles btn_actualizar.Click
        If txt_nombre_localidad.Text.Length = 0 Then
            MsgBox("Debe completar todos los campos requeridos")
            Exit Sub
        End If
        Try
            Dim ActualizarLocalidad = (From P In datacontext.Localidad Where P.id_localidad = (txt_id_localidad.Text.ToUpper)).ToList()(0)
            ActualizarLocalidad.nombre_localidad = txt_nombre_localidad.Text
            datacontext.SubmitChanges()
            MsgBox("Los datos se han modificado correctamente")
            txt_nombre_localidad.Clear()
            Me.Close()
        Catch ex As Exception
            MsgBox("Los datos no se han modificado! intente nuevamente", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Modificar Localidad")
            txt_nombre_localidad.Clear()
            Me.Close()
        End Try
    End Sub

    Private Sub btn_cancelar_Click(sender As System.Object, e As System.EventArgs) Handles btn_cancelar.Click
        Me.Close()
        Me.Dispose()
    End Sub
End Class