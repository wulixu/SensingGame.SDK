<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ElementHost1 = New System.Windows.Forms.Integration.ElementHost()
        Me.UpdateButton = New System.Windows.Forms.Button()
        Me.UpdateStopButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ElementHost1
        '
        Me.ElementHost1.Location = New System.Drawing.Point(12, 6)
        Me.ElementHost1.Name = "ElementHost1"
        Me.ElementHost1.Size = New System.Drawing.Size(350, 299)
        Me.ElementHost1.TabIndex = 0
        Me.ElementHost1.Text = "ElementHost1"
        Me.ElementHost1.Child = Nothing
        '
        'UpdateButton
        '
        Me.UpdateButton.Location = New System.Drawing.Point(44, 311)
        Me.UpdateButton.Name = "UpdateButton"
        Me.UpdateButton.Size = New System.Drawing.Size(132, 23)
        Me.UpdateButton.TabIndex = 1
        Me.UpdateButton.Text = "Start Update"
        Me.UpdateButton.UseVisualStyleBackColor = True
        '
        'UpdateStopButton
        '
        Me.UpdateStopButton.Location = New System.Drawing.Point(201, 311)
        Me.UpdateStopButton.Name = "UpdateStopButton"
        Me.UpdateStopButton.Size = New System.Drawing.Size(128, 23)
        Me.UpdateStopButton.TabIndex = 2
        Me.UpdateStopButton.Text = "Stop Update"
        Me.UpdateStopButton.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(374, 342)
        Me.Controls.Add(Me.UpdateStopButton)
        Me.Controls.Add(Me.UpdateButton)
        Me.Controls.Add(Me.ElementHost1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ElementHost1 As System.Windows.Forms.Integration.ElementHost
    Friend WithEvents UpdateButton As System.Windows.Forms.Button
    Friend WithEvents UpdateStopButton As System.Windows.Forms.Button

End Class
