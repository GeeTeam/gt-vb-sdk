Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Text
Imports System.Security.Cryptography
Imports System.Net.Sockets
Imports System.Net
Imports System.IO

Public Class Geetestlib
    Public privateKey As String
    Public captchaID As String
    Public challenge As String
    Public productType As String
    Public popupBtnID As String
    Private version As String = "2015.04.01"
    Public Sub New(ByVal key As String)
        privateKey = privateKey
    End Sub

    Public Sub New(ByVal key As String, ByVal captcha As String)
        privateKey = key
        captchaID = captcha
    End Sub

    Public Function geetest_validate(ByVal challenge As String, ByVal validate As String, ByVal seccode As String)
        Dim host As String
        Dim path As String
        Dim port As Integer
        geetest_validate = False
        host = "http://api.geetest.com"
        path = "/validate.php"
        port = 80
        If (validate.Length > 0 And geetest_checkResultByPrivate(challenge, validate)) Then
            Dim query As String = "seccode=" + seccode
            Dim response As String = ""
            Try
                response = geetests_postValidate(host, path, query, port)
            Catch ex As Exception
                Print(ex.ToString())
            End Try
            If (response.Equals(md5Encode(seccode))) Then
                geetest_validate = True
            End If
        End If
    End Function

    Public Function get_apiurl()
        Dim url As String = String.Format("http://api.geetest.com/get.php?gt={0}&challenge={1}", captchaID, challenge)
        If productType <> vbNullString Then
            url = url + "&product=" + productType
            If productType = "popup" Then
                url = url + "&popupbtnid=" + popupBtnID
            End If
        End If
        get_apiurl = url
    End Function

    Public Function register()
        register = False
        Dim host As String = "http://api.geetest.com"
        Dim path As String = "/register.php"
        Dim url As String = host + path + "?gt=" + captchaID
        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
        Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
        Dim myresponseStream As Stream = response.GetResponseStream()
        Dim myStreamReader As New StreamReader(myresponseStream, Encoding.GetEncoding("utf-8"))
        Dim retString As String = myStreamReader.ReadToEnd()
        myStreamReader.Close()
        myresponseStream.Close()
        If retString.Length = 32 Then
            challenge = retString
            register = True
        End If
    End Function

    Private Function geetest_checkResultByPrivate(ByVal origin As String, ByVal validate As String)
        Dim encodeStr As String = md5Encode(privateKey + "geetest" + origin)
        geetest_checkResultByPrivate = validate.Equals(encodeStr)
    End Function

    Private Function geetests_postValidate(ByVal host As String, ByVal path As String, ByVal data As String, ByVal port As Integer)
        Dim url As String = host + path
        Dim sdkInfo As String = "&sdk=" + version + "&sdklang=vb"
        data = data + sdkInfo
        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"

        request.ContentLength = Encoding.UTF8.GetByteCount(data)

        Dim myRequestStream As Stream = request.GetRequestStream()
        Dim requestBytes() As Byte = Encoding.ASCII.GetBytes(data)
        myRequestStream.Write(requestBytes, 0, requestBytes.Length)
        myRequestStream.Close()

        Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
        Dim myresponseStream As Stream = response.GetResponseStream()
        Dim myStreamReader As New StreamReader(myresponseStream, Encoding.GetEncoding("utf-8"))
        Dim retString As String = myStreamReader.ReadToEnd()
        myStreamReader.Close()
        myresponseStream.Close()
        geetests_postValidate = retString
    End Function

    Private Function fixEncoding(ByVal str As String)
        Dim utf8 As New UTF8Encoding()
        Dim encodeBytes() As Byte = utf8.GetBytes(str)
        Dim decodeString As String
        decodeString = utf8.GetString(encodeBytes)
        fixEncoding = decodeString
    End Function

    Public Function md5Encode(ByVal plainText As String)
        Dim md5 = New MD5CryptoServiceProvider()
        Dim t2 As String
        t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(plainText)))
        t2 = t2.Replace("-", "")
        t2 = t2.ToLower()
        md5Encode = t2
    End Function

End Class
