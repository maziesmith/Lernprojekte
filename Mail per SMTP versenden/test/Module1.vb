Imports System.Net.Mail

Module Module1
    Sub Main()
        MailVersenden()
    End Sub



    Sub MailVersenden()
        Dim MyEmail As New MailMessage

        Try
            MyEmail.From = New MailAddress("fatturazione@atzwanger.net")
            MyEmail.To.Add("rupobk@gmail.com")
            MyEmail.To.Add("rupert.obkircher@hotmail.com")
            MyEmail.CC.Add("rupert.obkircher@atzwanger.net")
            MyEmail.Subject = "betreff"
            MyEmail.Body = "dies ist der mailtext - zeile 1" & vbCrLf & "zeile2" & vbCrLf & vbCrLf & "Hochachtungsvoll" & vbCrLf & "bla bla"
            'MyEmail.Attachments.Add(New Attachment("c:\temp\test.xml"))

            Dim smtp As New SmtpClient("smtp.office365.com")
            smtp.Port = 587
            smtp.EnableSsl = True
            smtp.Credentials = New System.Net.NetworkCredential("fatturazione@atzwanger.net", "1Fatt638sez")

            smtp.Send(MyEmail)
        Catch ex As Exception
            'Fehlermeldung anzeigen!!!!!!!!!!!!!!!!!!!!!
            MsgBox("Fehler: " & ex.Message.ToString)
        End Try
    End Sub


End Module
