Imports Visifire.Charts

Class MainWindow


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Create a new DateTime object
        dt = New List(Of DateTime)()

        ' Populate DateTime collection
        dt.Add(New DateTime(2008, 1, 2))
        dt.Add(New DateTime(2008, 2, 4))
        dt.Add(New DateTime(2008, 3, 2))
        dt.Add(New DateTime(2008, 4, 11))
        dt.Add(New DateTime(2008, 5, 1))
        dt.Add(New DateTime(2008, 6, 10))

        ' Create a new Visifire Chart
        CreateChart()
    End Sub

    ''' <summary>
    ''' Function to create a chart
    ''' </summary>
    Public Sub CreateChart()
        ' Create a new instance of Chart
        Dim chart As New Chart()

        ' Set chart properties
        chart.ScrollingEnabled = False
        chart.View3D = True

        ' Create a new instance of Title
        Dim title As New Title()

        ' Set title property
        title.Text = "Visifire DateTime Axis Chart"

        ' Add title to Titles collection
        chart.Titles.Add(title)

        ' Create a new Axis
        Dim axis As New Axis()

        ' Set axis properties
        axis.IntervalType = IntervalTypes.Days
        axis.Interval = 20

        ' Add axis to AxesX collection
        chart.AxesX.Add(axis)

        For j As Int32 = 0 To 1
            ' Create a new instance of DataSeries
            Dim dataSeries As New DataSeries()

            ' Set DataSeries properties
            dataSeries.RenderAs = RenderAs.Column
            dataSeries.XValueType = ChartValueTypes.DateTime

            ' Create a DataPoint
            Dim dataPoint As DataPoint

            For i As Integer = 0 To 5
                ' Create a new instance of DataPoint
                dataPoint = New DataPoint()

                ' Set XValue for a DataPoint
                dataPoint.XValue = dt(i)

                ' Set YValue for a DataPoint
                dataPoint.YValue = rand.[Next](10, 100)

                ' Add dataPoint to DataPoints collection
                dataSeries.DataPoints.Add(dataPoint)
            Next

            ' Add dataSeries to Series collection.
            chart.Series.Add(dataSeries)
        Next

        ' Add chart to LayoutRoot
        LayoutRoot.Children.Add(chart)
    End Sub

    ''' <summary>
    ''' Create a random class variable
    ''' </summary>
    Private rand As New Random(DateTime.Now.Millisecond)
    Private dt As List(Of DateTime)
End Class
