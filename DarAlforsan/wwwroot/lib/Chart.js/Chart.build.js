Chart.defaults.global.defaultFontFamily = "hb";
//Chart.defaults.global.defaultFontSize = "12";
//Chart.defaults.global.defaultFont = "hb";
//START:BAR CHART
var chartJS = {
    chartType:
    {
        line: 'line',
        bar: 'bar',
        stackedBar: 'stackedbBar',
        horizontalBar: 'horizontalBar',
        pie: 'pie',
        doughnut: 'doughnut',
        semiDoughnut: 'semiDoughnut'
    },

    initBuildChart: function initBuildChart(title, type, canvasID, dataUrl) {

        $.ajax({
            type: "GET",
            url: dataUrl,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                chartJS.buildChart(title, type, canvasID, data);
            },
            error: function () {
                console.log("ERROR");
            }
        });
    },
    buildChart: function buildChart(title, type, canvasID, chartData) {

        Chart.helpers.each(Chart.instances, function (instance) {
            if (instance.chart.canvas.id == canvasID) {
                instance.destroy();
            }
        })

        var ctx = document.getElementById(canvasID).getContext('2d');

        //for line and bar chart
        var axes = null;
        //for doughnut
        var rotation = -Math.PI / 2;
        var circumference = Math.PI * 2;
        var stack = false;
        //if stacked bar
        if (type == chartJS.chartType.stackedBar) {
            type = chartJS.chartType.bar;
            stack = true;
        }
        //if semi circle doughnut
        if (type == chartJS.chartType.semiDoughnut) {
            type = chartJS.chartType.doughnut;
            rotation = - Math.PI;
            circumference = Math.PI;
        }
        if (type == chartJS.chartType.bar || type == chartJS.chartType.line || type == chartJS.chartType.horizontalBar) {
            axes = {
                xAxes: [{
                    stacked: stack,
                    ticks: {
                        //fontFamily: 'tahoma',
                        fontColor: 'Gray',
                        fontStyle: 'Bold',
                        beginAtZero: true,
                        fontSize: 12,
                        autoSkip: false
                    },

                    //barThickness: 50,
                    maxBarThickness: 30
                    //barPercentage: 0.4,
                }],
                yAxes: [{
                    stacked: stack,
                    ticks: {
                        //fontFamily: 'tahoma',
                        fontColor: 'Gray',
                        fontStyle: 'Bold',
                        beginAtZero: true,
                        precision: 0
                    },
                    gridLines: {
                        //lineWidth: 1,
                        //weight:2
                    }
                }]

            }
        }

        var chart = new Chart(ctx, {
            type: type,
            data: chartData,
            options: {
                responsive: true,
                maintainAspectRatio: false,
                rotation: rotation,
                circumference: circumference,
                //Chart Title
                title: {
                    display: true,
                    text: title,
                    //fontFamily: 'tahoma',
                    fontStyle: 'Bold',
                    fontSize: '13'
                },
                //Chart Legend
                legend: {
                    display: true,
                    labels: {

                        //fontColor: 'Orange'
                    }
                },
                scales: axes
            }
        });

    }
}
//END:BAR CHART