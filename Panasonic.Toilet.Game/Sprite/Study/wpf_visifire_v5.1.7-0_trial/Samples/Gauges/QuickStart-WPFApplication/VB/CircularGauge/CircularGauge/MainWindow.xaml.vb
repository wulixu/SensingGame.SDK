Imports Visifire.Gauges

Class MainWindow

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
        LayoutRoot.Children.Add(gauge)
    End Sub

End Class
