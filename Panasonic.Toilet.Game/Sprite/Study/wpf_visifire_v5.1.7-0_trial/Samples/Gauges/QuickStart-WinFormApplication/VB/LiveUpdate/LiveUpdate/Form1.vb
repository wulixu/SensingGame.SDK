Imports System.Windows
Imports System.Windows.Input
Imports Visifire.Gauges

Public Class Form1

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        CreateGauge()

    End Sub

    ''' <summary>
    ''' Function to create a Visifire Gauge
    ''' </summary>

    Private Sub CreateGauge()
        ' Create a gauge
        gauge = New Gauge()
        gauge.Width = 300
        gauge.Height = 300

        ' Create a Needle Indicator
        Dim indicator As New NeedleIndicator()
        indicator.Value = 20

        ' Add indicator to Indicators collection of gauge
        gauge.Indicators.Add(indicator)

        ' Attach a Loaded event to gauge in order to attach a timer's Tick event
        AddHandler gauge.Loaded, AddressOf gauge_Loaded

        ' Add gauge to the LayoutRoot for display
        ElementHost1.Child = gauge
    End Sub

    ''' <summary>
    ''' Event handler for loaded event of gauge
    ''' </summary>
    ''' <param name="sender">Gauge</param>
    ''' <param name="e">RoutedEventArgs</param>
    Private Sub gauge_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        AddHandler timer.Tick, AddressOf timer_Tick
        timer.Interval = New TimeSpan(0, 0, 0, 0, 1500)
    End Sub

    ''' <summary>
    ''' Event handler for Tick event of Dispatcher Timer
    ''' </summary>
    ''' <param name="sender">System.Windows.Threading.DispatcherTimer</param>
    ''' <param name="e">EventArgs</param>
    Private Sub timer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        ' Update Indicators Value property
        gauge.Indicators(0).Value = rand.[Next](10, 80)
    End Sub

    Private gauge As Gauge
    ' Visifire gauge
    Private rand As New Random(DateTime.Now.Millisecond)
    ' Create a random class variable
    ' Create a timer object
    Private timer As New System.Windows.Threading.DispatcherTimer()

    Private Sub UpdateButton_Click(sender As System.Object, e As System.EventArgs) Handles UpdateButton.Click
        ' timer starts
        timer.Start()
    End Sub

    Private Sub UpdateStopButton_Click(sender As System.Object, e As System.EventArgs) Handles UpdateStopButton.Click
        ' timer stops
        timer.[Stop]()
    End Sub

End Class
