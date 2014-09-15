Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub




    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim geetest As New Geetestlib("55e100f0bd6cc6279d47b80b3ccedbf7")
        Dim result As Boolean
        Dim challenge As String = Request.Params("geetest_challenge")
        Dim validata As String = Request.Params("geetest_validate")
        Dim seccode As String = Request.Params("geetest_seccode")
        result = geetest.geetest_validate(challenge, validata, seccode)
        If (result) Then
            Response.Write("ok")
        Else
            Response.Write("error")
        End If


    End Sub
End Class