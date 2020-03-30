Public Class Form1

    'Add these to your form class
    Private MouseIsDown As Boolean = False
    Private MouseIsDownLoc As Point = Nothing

    'This is the MouseMove event of your panel
    Private Sub HeaderPanel_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles HeaderPanel.MouseMove
        If e.Button = MouseButtons.Left Then
            If MouseIsDown = False Then
                MouseIsDown = True
                MouseIsDownLoc = New Point(e.X, e.Y)
            End If

            Me.Location = New Point(Me.Location.X + e.X - MouseIsDownLoc.X, Me.Location.Y + e.Y - MouseIsDownLoc.Y)
        End If
    End Sub

    'And the MouseUp event of your panel
    Private Sub HeaderPanel_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles HeaderPanel.MouseUp
        MouseIsDown = False
    End Sub

    Private Sub ExitBtn_Click(sender As Object, e As EventArgs) Handles ExitBtn.Click
        Application.Exit()
    End Sub

    Private Sub MinimizeBtn_Click(sender As Object, e As EventArgs) Handles MinimizeBtn.Click
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
    End Sub

    Private Sub SelectVideoPath1_Click(sender As Object, e As EventArgs) Handles SelectVideoPath1.Click
        OpenFileDialogVideo.FileName = ""
        If (OpenFileDialogVideo.ShowDialog() = DialogResult.OK) Then
            VideoPath.Text = OpenFileDialogVideo.FileName
        End If
        OutpuPath.Text = "" & VideoPath.Text & ""
        SubtitlePath.Text = "" & VideoPath.Text & ""
    End Sub

    Private Sub SelectOutput1_Click(sender As Object, e As EventArgs) Handles SelectOutput1.Click
        If (SaveFileDialogSavePath.ShowDialog() = DialogResult.OK) Then
            OutpuPath.Text = SaveFileDialogSavePath.FileName
        End If
    End Sub

    Private Sub SelectSubPath_Click(sender As Object, e As EventArgs) Handles SelectSubPath.Click
        OpenFileDialogSub.FileName = ""
        OpenFileDialogSub.Filter = "Supported Subtitles|*.ass;*.srt;*.ssa"
        If (OpenFileDialogSub.ShowDialog() = DialogResult.OK) Then
            SubtitlePath.Text = OpenFileDialogSub.FileName
        End If
    End Sub

    Private Sub SelectSavePath_Click(sender As Object, e As EventArgs) Handles SelectSavePath.Click
        If (SaveFileDialogSavePath.ShowDialog() = DialogResult.OK) Then
            SavePath.Text = SaveFileDialogSavePath.FileName
        End If
    End Sub

    Private Sub Start_Click(sender As Object, e As EventArgs) Handles Start.Click
        If VideoPath.Text = "" Then
            MessageBox.Show("YOU NEED TO ADD THE VIDEO PATH!")
        ElseIf OutpuPath.Text = "" Then
            MessageBox.Show("YOU NEED TO ADD THE OUTPUT PATH!")
        Else
            SubtitlePath.Text = SubtitlePath.Text.Replace("\", "\\\\")
            SubtitlePath.Text = SubtitlePath.Text.Replace(":", "\\:")
            Shell("cmd.exe /k" + "\ffmpeg\bin\ffmpeg.exe -i " + VideoPath.Text + " " + APBtn.Text + " -acodec copy -map_metadata -1 -vf " + "subtitles=" + SubtitlePath.Text + " " + OutpuPath.Text + " -y")
            OutpuPath.Clear()
            SubtitlePath.Clear()
            VideoPath.Clear()
        End If
    End Sub

    Private Sub DownloadBtn_Click(sender As Object, e As EventArgs) Handles DownloadBtn.Click
        If VideoUrl.Text = "" Then
            MessageBox.Show("YOU NEED TO ADD THE VIDEO URL!")
        ElseIf SavePath.Text = "" Then
            MessageBox.Show("YOU NEED TO ADD THE OUTPUT PATH!")
        Else
            Shell("cmd.exe /k" + "ffmpeg -i " + VideoUrl.Text + " -c copy " + SavePath.Text)
        End If
    End Sub

    Private Sub iSubBurner_Click(sender As Object, e As EventArgs) Handles iSubBurner.Click
        SubBurnerPanel.Visible = "true"
        DownloaderPanel.Visible = "false"
        RecordPanel.Visible = "false"
        SplitterPanel.Visible = "false"
    End Sub

    Private Sub iDownloader_Click(sender As Object, e As EventArgs) Handles iDownloader.Click
        SubBurnerPanel.Visible = "false"
        DownloaderPanel.Visible = "true"
        RecordPanel.Visible = "false"
        SplitterPanel.Visible = "false"
    End Sub

    Private Sub iRecorder_Click(sender As Object, e As EventArgs) Handles iRecorder.Click
        SubBurnerPanel.Visible = "false"
        DownloaderPanel.Visible = "false"
        RecordPanel.Visible = "true"
        SplitterPanel.Visible = "false"
    End Sub

    Private Sub iSplitter_Click(sender As Object, e As EventArgs) Handles iSplitter.Click
        SubBurnerPanel.Visible = "false"
        DownloaderPanel.Visible = "false"
        RecordPanel.Visible = "false"
        SplitterPanel.Visible = "true"
    End Sub

    Private Sub RecordBtn_Click(sender As Object, e As EventArgs) Handles RecordBtn.Click
        If FPS.Text = "" Then
            MessageBox.Show("YOU NEED TO ADD FPS YOU WANT!")
        ElseIf SaveRecPath.Text = "" Then
            MessageBox.Show("YOU NEED TO ADD THE SAVING PATH!")
        ElseIf Res.Text = "" Then
            Shell("cmd.exe /k" + "\ffmpeg\bin\ffmpeg.exe -f gdigrab -r " + FPS.Text + " " + APBtn2.Text + " -i desktop -acodec copy " + SaveRecPath.Text + " -y")
        ElseIf ShowRegYes.Checked Then
            Shell("cmd.exe /k" + "\ffmpeg\bin\ffmpeg.exe -f gdigrab -r " + FPS.Text + " -offset_x 10 -offset_y 20 -video_size " + Res.Text + " -show_region 1 " + APBtn2.Text + " -i desktop -acodec copy " + SaveRecPath.Text + " -y")
        Else
            Shell("cmd.exe /k" + "\ffmpeg\bin\ffmpeg.exe -f gdigrab -r " + FPS.Text + " -offset_x 10 -offset_y 20 -video_size " + Res.Text + " " + APBtn2.Text + " -i desktop -acodec copy " + SaveRecPath.Text + " -y")
        End If
    End Sub

    Private Sub SelectRecPath_Click(sender As Object, e As EventArgs) Handles SelectRecPath.Click
        If (SaveFileDialogRec.ShowDialog() = DialogResult.OK) Then
            SaveRecPath.Text = SaveFileDialogRec.FileName
        End If
    End Sub

    Private Sub StartSplitting_Click(sender As Object, e As EventArgs) Handles StartSplitting.Click
        If SplitterTarget.Text = "" Then
            MessageBox.Show("YOU NEED TO ADD THE VIDEO PATH!")
        ElseIf SplitOutputPath.Text = "" Then
            MessageBox.Show("YOU NEED TO ADD THE OUTPUT PATH!")
        ElseIf SegmentTime.Text = "" Then
            MessageBox.Show("YOU MUST CHOOSE THE SEGMENT TIME!")
        Else
            Shell("cmd.exe /k" + "\ffmpeg\bin\ffmpeg.exe -i " + SplitterTarget.Text + " -acodec copy -f segment -segment_time " + SegmentTime.Text + " -vcodec copy -reset_timestamps 1 -map 0 " + SplitOutputPath.Text + "_%03d" + SplitOutputExtension.Text + " -y ")
        End If
    End Sub

    Private Sub SelectSplit_Click(sender As Object, e As EventArgs) Handles SelectSplit.Click
        OpenFileDialogSplit.FileName = ""
        If (OpenFileDialogSplit.ShowDialog() = DialogResult.OK) Then
            SplitterTarget.Text = OpenFileDialogSplit.FileName
        End If
    End Sub

    Private Sub OutputSplit_Click(sender As Object, e As EventArgs) Handles OutputSplit.Click
        If (SaveFileDialogSplit.ShowDialog() = DialogResult.OK) Then
            SplitOutputPath.Text = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(SaveFileDialogSplit.FileName), System.IO.Path.GetFileNameWithoutExtension(SaveFileDialogSplit.FileName))
        End If
    End Sub

    Private Sub SubTools_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SubBurnerPanel.Visible = "true"
        DownloaderPanel.Visible = "false"
        RecordPanel.Visible = "false"
        SplitterPanel.Visible = "false"
    End Sub

    Private Sub Donate1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Donate1.Click
        Process.Start("https://www.in4sser.com/p/donate.html")
    End Sub

    Private Sub LogoBox_Click(sender As Object, e As EventArgs) Handles LogoBox.Click
        OpenFileDialogLogo.Title = "Please select a file"
        OpenFileDialogLogo.InitialDirectory = "c:"
        OpenFileDialogLogo.ShowDialog()
        LogoBox.ImageLocation = OpenFileDialogLogo.FileName.ToString
        LogoBox.Visible = True
    End Sub
End Class
