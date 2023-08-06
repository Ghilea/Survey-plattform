$(document).ready(function () {

    let chartType = 'bar';
    window.myCharts = {};
    let chartIndex = 0;

    //recivers and retrivers
    const ctx = $('#chart-amount');

    new Chart(ctx, {
        type: 'doughnut',
        data: {
            datasets: [{
                data: [amountRecivers - amountRetrivers, amountRetrivers],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                },
            }
        }
    });

    function createTrendChart(canvasId, labels, data) {
        const ctx = document.getElementById('questionTrendChart-' + canvasId);

        const trendChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [{
                    data: data,
                    borderWidth: 1,
                    borderColor: 'white',
                    backgroundColor: 'white',
                }]
            },
            options: {
                plugins: {
                    legend: {
                        display: false
                    }
                },
                responsive: true,
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });

        return trendChart;
    }

    function createChart(canvasId, chartType, labels, data) {
        const ctx = document.getElementById(canvasId);

        const chart = new Chart(ctx, {
            type: chartType,
            data: {
                labels: labels,
                datasets: [{
                    data: data,
                    borderWidth: 1,
                    borderColor: chartType === 'bar' ? 'green' : undefined,
                    backgroundColor: chartType === 'bar' ? 'green' : undefined,
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        display: false
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });

        return chart;
    }

    $.each(questions, function (index, questionStat) {

        //question
        const labels = Object.getOwnPropertyNames(questionStat.answers);
        const data = Object.values(questionStat.answers);

        const chartId = 'chart-' + chartIndex;
        window.myCharts[chartId] = createChart(chartId, chartType, labels, data);

        //trend
        const trendData = questions[chartIndex].trendData;

        const trendSortLabels = trendData.map(item => new Date(item.date).toLocaleString('default', { month: 'short', day: '2-digit', year: '2-digit' }));
        const trendSortData = trendData.map(item => item.value);

        if (trendSortData[0] > 0) {
            createTrendChart(chartIndex, trendSortLabels, trendSortData);
        }

        chartIndex++;
    });

    // Event handler for chart type change
    $('#chartType').on('change', function () {
        chartType = $(this).val();
        $.each(window.myCharts, function (chartId, chart) {
            chart.destroy();
            const labels = Object.getOwnPropertyNames(questions[chartId.substring(6)].answers);
            const data = Object.values(questions[chartId.substring(6)].answers);
            window.myCharts[chartId] = createChart(chartId, chartType, labels, data);
        });
    });
});