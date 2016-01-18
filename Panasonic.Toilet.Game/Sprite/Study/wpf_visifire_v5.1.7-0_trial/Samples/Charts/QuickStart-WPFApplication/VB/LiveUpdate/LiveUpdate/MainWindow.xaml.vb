Imports Visifire.Charts
Class MainWindow

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

        ' Set the chart width and height
        chart.Width = 500
        chart.Height = 300

        ' Create a new instance of Title
        Dim title As New Title()

        ' Set title property
        title.Text = "Visifire Sample With LiveUpdate"

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
            dataPoint.YValue = rand.[Next](-100, 100)

            ' Add dataPoint to DataPoints collection
            dataSeries.DataPoints.Add(dataPoint)
        Next

        ' Add dataSeries to Series collection.
        chart.Series.Add(dataSeries)

        ' Attach a Loaded event to chart in order to attach a timer's Tick event
        AddHandler chart.Loaded, AddressOf chart_Loaded

        ' Add chart to Chart Grid
        ChartGrid.Children.Add(chart)
    End Sub

    ''' <summary>
    ''' Event handler for loaded event of chart
    ''' </summary>
    ''' <param name="sender">Chart</param>
    ''' <param name="e">RoutedEventArgs</param>
    Private Sub chart_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        AddHandler timer.Tick, AddressOf timer_Tick
        timer.Interval = New TimeSpan(0, 0, 0, 0, 1500)
    End Sub

    ''' <summary>
    ''' Event handler for Tick event of Timer
    ''' </summary>
    ''' <param name="sender"> System.Windows.Threading.DispatcherTimer</param>
    ''' <param name="e">EventArgs</param>
    Private Sub timer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        For i As Int32 = 0 To 4
            ' Update YValue property of the DataPoint
            ' Changing the dataPoint YValue at runtime
            chart.Series(0).DataPoints(i).YValue = rand.[Next](-80, 100)
        Next
    End Sub

    ''' <summary>
    ''' Event handler for Click event of UpdateButton
    ''' </summary>
    ''' <param name="sender">Button</param>
    ''' <param name="e">RoutedEventArgs</param>
    Private Sub UpdateButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        ' timer starts
        timer.Start()
    End Sub

    ''' <summary>
    ''' Event handler for Click event of UpdateStopButton
    ''' </summary>
    ''' <param name="sender">Button</param>
    ''' <param name="e">RoutedEventArgs</param>
    Private Sub UpdateStopButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        ' timer stops
        timer.[Stop]()
    End Sub

    Private chart As Chart
    ' Chart object
    Private rand As New Random(DateTime.Now.Millisecond)
    ' Create a random class variable
    ' Create a new instance of timer object
    Private timer As New System.Windows.Threading.DispatcherTimer()

End Class
