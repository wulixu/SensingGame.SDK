Imports Visifire.Charts

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
        Dim chart As New Chart()
        chart.Width = 500
        chart.Height = 300
        chart.AnimationEnabled = False

        ' Create a new instance of Title
        Dim title As New Title()

        ' Set title property
        title.Text = "Visifire Sample Chart"

        ' Add title to Titles collection
        chart.Titles.Add(title)

        ' Create a new instance of DataSeries
        Dim dataSeries As New DataSeries()

        ' Set DataSeries property
        dataSeries.RenderAs = RenderAs.Column

        ' Create a DataPoint
        Dim dataPoint As DataPoint

        For i As Integer = 0 To 9
            ' Create a new instance of DataPoint
            dataPoint = New DataPoint()

            ' Set YValue for a DataPoint
            dataPoint.YValue = rand.[Next](-100, 100)

            ' Add dataPoint to DataPoints collection.
            dataSeries.DataPoints.Add(dataPoint)
        Next

        ' Add dataSeries to Series collection.
        chart.Series.Add(dataSeries)

        ' Add chart to LayoutRoot
        ElementHost1.Child = chart

    End Sub

    Private rand As New Random(DateTime.Now.Millisecond)
    ' Create a random class variable

End Class
