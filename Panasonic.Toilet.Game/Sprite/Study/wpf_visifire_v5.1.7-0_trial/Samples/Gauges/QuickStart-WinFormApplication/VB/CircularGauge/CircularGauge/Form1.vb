Imports Visifire.Gauges

Public Class Form1

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        CreateGauge()

    End Sub

    Private Sub CreateGauge()
        ' Create a gauge
        Dim gauge As New Gauge()
        gauge.Width = 300
        gauge.Height = 300

        ' Create a Needle Indicator
        Dim indicator As New NeedleIndicator()
        indicator.Value = 20

        ' Add indicator to Indicators collection of gauge
        gauge.Indicators.Add(indicator)

        ' Add gauge to the LayoutRoot for display
        ElementHost1.Child = gauge

    End Sub

End Class
