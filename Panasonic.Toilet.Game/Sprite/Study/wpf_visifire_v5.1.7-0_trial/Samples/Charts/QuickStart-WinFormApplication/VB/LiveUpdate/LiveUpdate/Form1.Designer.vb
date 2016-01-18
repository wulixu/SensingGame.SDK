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
        Me.StartUpdateButton = New System.Windows.Forms.Button()
        Me.StopUpdateButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ElementHost1
        '
        Me.ElementHost1.Location = New System.Drawing.Point(0, 1)
        Me.ElementHost1.Name = "ElementHost1"
        Me.ElementHost1.Size = New System.Drawing.Size(571, 276)
        Me.ElementHost1.TabIndex = 0
        Me.ElementHost1.Text = "ElementHost1"
        Me.ElementHost1.Child = Nothing
        '
        'StartUpdateButton
        '
        Me.StartUpdateButton.Location = New System.Drawing.Point(44, 283)
        Me.StartUpdateButton.Name = "StartUpdateButton"
        Me.StartUpdateButton.Size = New System.Drawing.Size(153, 23)
        Me.StartUpdateButton.TabIndex = 1
        Me.StartUpdateButton.Text = "Start Update"
        Me.StartUpdateButton.UseVisualStyleBackColor = True
        '
        'StopUpdateButton
        '
        Me.StopUpdateButton.Location = New System.Drawing.Point(355, 284)
        Me.StopUpdateButton.Name = "StopUpdateButton"
        Me.StopUpdateButton.Size = New System.Drawing.Size(155, 23)
        Me.StopUpdateButton.TabIndex = 2
        Me.StopUpdateButton.Text = "Stop Update"
        Me.StopUpdateButton.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(571, 309)
        Me.Controls.Add(Me.StopUpdateButton)
        Me.Controls.Add(Me.StartUpdateButton)
        Me.Controls.Add(Me.ElementHost1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ElementHost1 As System.Windows.Forms.Integration.ElementHost
    Friend WithEvents StartUpdateButton As System.Windows.Forms.Button
    Friend WithEvents StopUpdateButton As System.Windows.Forms.Button

End Class
