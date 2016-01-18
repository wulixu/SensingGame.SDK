Imports Visifire.Charts
Imports System.Windows.Input
Imports System.Windows.Media

Public Class Form1

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        CreateChart()

    End Sub

    ''' <summary>
    ''' Function to create a chart
    ''' </summary>
    Public Sub CreateChart()
        ' Create a new instance of Chart
        chart = New Chart()

        ' Create a new instance of Title
        Dim title As New Title()

        ' Set title property
        title.Text = "Visifire Sample with Events"

        ' Attach event to Title
        AddHandler title.MouseLeftButtonDown, AddressOf title_MouseLeftButtonDown

        ' Add title to Titles collection
        chart.Titles.Add(title)

        ' Create a new instance of DataSeries
        Dim dataSeries As New DataSeries()

        ' Set DataSeries property
        dataSeries.RenderAs = RenderAs.Column

        ' Create a DataPoint
        Dim dataPoint As DataPoint

        For i As Integer = 0 To 4
            ' Create a new instance of DataPoint
            dataPoint = New DataPoint()

            ' Set YValue for a DataPoint
            dataPoint.YValue = rand.[Next](10, 100)

            ' Attach event to DataPoint
            AddHandler dataPoint.MouseLeftButtonUp, AddressOf dataPoint_MouseLeftButtonUp

            ' Add dataPoint to DataPoints collection
            dataSeries.DataPoints.Add(dataPoint)
        Next

        ' Add dataSeries to Series collection
        chart.Series.Add(dataSeries)

        ' Add chart to LayoutRoot
        ElementHost1.Child = chart

    End Sub

    ''' <summary>
    ''' Event handler for MouseLeftButtonDown event
    ''' </summary>
    ''' <param name="sender">Title</param>
    ''' <param name="e">MouseButtonEventArgs</param>
    Private Sub title_MouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        TryCast(sender, Title).FontColor = New SolidColorBrush(Colors.Red)
    End Sub

    ''' <summary>
    ''' Event handler for MouseLeftButtonUp event
    ''' </summary>
    ''' <param name="sender">DataPoint</param>
    ''' <param name="e">MouseButtonEventArgs</param>
    Private Sub dataPoint_MouseLeftButtonUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        ' Update YValue property of the DataPoint
        TryCast(sender, DataPoint).YValue = rand.[Next](10, 100)
    End Sub

    Private chart As Chart
    ' Chart object
    Private rand As New Random(DateTime.Now.Millisecond)
    ' Create a random class variable

End Class
