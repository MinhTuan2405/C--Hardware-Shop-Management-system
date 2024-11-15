﻿Imports MySql.Data.MySqlClient
Module selects
    Public con As MySqlConnection = mysqldb()
    'procedure of your autoappend and autosugest
    Public Sub autocompletetxt(ByVal sql As String, ByVal txt As TextBox)
        Try
            dt = New DataTable
            'OPENING THE CONNECTION
            con.Open()
            'HOLDS THE DATA TO BE EXECUTED
            With cmd
                .Connection = con
                .CommandText = sql
            End With
            'FILLING THE DATA IN THE DATATABLE
            da.SelectCommand = cmd
            da.Fill(dt)
            'SET A VARIABLE AS A ROW OF DATA IN THE DATATABLE
            Dim r As DataRow
            'CLEARING THE AUTOCOMPLETE SOURCE OF THE TEXTBOX
            txt.AutoCompleteCustomSource.Clear()
            'LOOPING THE ROW OF DATA IN THE DATATABLE
            For Each r In dt.Rows
                'ADDING THE DATA IN THE AUTO COMPLETE SOURCE OF THE TEXTBOX
                txt.AutoCompleteCustomSource.Add(r.Item(0).ToString)
            Next
            ''''''''''''''''''''''''
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'CLOSING THE CONNECTION
        con.Close()
        da.Dispose()
    End Sub
    Public Sub auto_sugestAll(ByVal sql As String, ByVal txt As Object)
        With cmd
            .Connection = con
            .CommandText = sql
        End With
        dt = New DataTable
        da = New MySqlDataAdapter(sql, con)
        da.Fill(dt)
        Dim r As DataRow
        txt.AutoCompleteCustomSource.Clear()
        For Each r In dt.Rows
            txt.AutoCompleteCustomSource.Add(r.Item(0).ToString)
            txt.AutoCompleteCustomSource.Add(r.Item(1).ToString)
            txt.AutoCompleteCustomSource.Add(r.Item(2).ToString)
        Next
    End Sub
    Public Sub fillcbo(ByVal sql As String, ByVal cbo As ComboBox)
        Try
            dt = New DataTable
            'OPENING THE CONNECTION
            con.Open()
            'HOLDS THE DATA TO BE EXECUTED
            With cmd
                .Connection = con
                .CommandText = sql
            End With
            'FILLING THE DATA IN THE DATATABLE
            da.SelectCommand = cmd
            da.Fill(dt)
            With cbo
                .DataSource = dt
                .DisplayMember = "DESCRIPTION" 
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'CLOSING THE CONNECTION
        con.Close()
        da.Dispose()
    End Sub
    Public Sub autoreceipt(ByVal desc As String, ByVal txt As Object)
        Try
            sql = "SELECT concat(`STRT`, `END`) FROM `tblautonumber` WHERE `DESCRIPTION`= '" & desc & "'"
            reloadtxt(sql)
            txt.text = dt.Rows(0).Item(0)
        Catch ex As Exception
            ' MsgBox(ex.Message)
        End Try

    End Sub
    Public Sub autoreceiptupdate(ByVal desc As String)
        Try
            sql = "UPDATE `tblautonumber` SET `END`=`END`+`INCREMENT` WHERE `DESCRIPTION`='" & desc & "'"
            createNoMsg(sql)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Sub loadautonumber(ByVal id As String, ByVal txt As TextBox)
        Try
            sql = "SELECT concat(`STRT`, `END`) FROM `tblautonumber` WHERE `ID`= '" & id & "'"
            reloadtxt(sql)
            txt.Text = dt.Rows(0).Item(0)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Sub loadautonumbercateg(ByVal desc As String, ByVal txt As TextBox)
        Try
            sql = "SELECT concat(`STRT`, `END`) FROM `tblautonumber` WHERE `DESCRIPTION`= '" & desc & "'"
            reloadtxt(sql)
            txt.Text = dt.Rows(0).Item(0)
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub
    Public Sub updateautonumbercateg(ByVal desc As String)
        Try
            sql = "UPDATE `tblautonumber` SET `END`=`END`+`INCREMENT` WHERE `DESCRIPTION`='" & desc & "'"
            createNoMsg(sql)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub updateautonumber(ByVal id As Integer)
        Try
            sql = "UPDATE `tblautonumber` SET `END`=`END`+`INCREMENT` WHERE `ID`=" & id
            createNoMsg(sql)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub variableautonum(ByVal id As String, ByVal var As String)
        Try
            sql = "SELECT concat(`STRT`, `END`) FROM `tblautonumber` WHERE `ID`= '" & id & "'"
            reloadtxt(sql)
            var = dt.Rows(0).Item(0)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


#Region "Report"
    Public Sub reports(ByVal sql As String, ByVal rptname As String, ByVal crystalRpt As Object)
        Try
            con.Open()

            Dim reportname As String
            With cmd
                .Connection = con
                .CommandText = sql
            End With
            ds = New DataSet
            da = New MySqlDataAdapter(sql, con)
            da.Fill(ds)
            reportname = rptname
            Dim reportdoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim strReportPath As String
            strReportPath = Application.StartupPath & "\reports\" & reportname & ".rpt"
            With reportdoc
                .Load(strReportPath)
                .SetDataSource(ds.Tables(0))
            End With
            With crystalRpt
                .ShowRefreshButton = False
                .ShowCloseButton = False
                .ShowGroupTreeButton = False
                .ReportSource = reportdoc
            End With
        Catch ex As Exception
            MsgBox(ex.Message & "This content cannot be access please contact the administrator.", MsgBoxStyle.Exclamation)
        End Try
        con.Close()
        da.Dispose()
    End Sub
#End Region

End Module
