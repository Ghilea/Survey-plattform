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
        const ctx = document.getElementById(canvasId);

        const trendChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [{
                    data: data,
                    label: 'Trend for ' + canvasId, // Use the question name as the dataset label
                    borderWidth: 1,
                    borderColor: 'blue',
                    backgroundColor: 'transparent',
                }]
            },
            options: {
                responsive: true,
                scales: {
                    x: {
                        type: 'time', // Use 'time' scale for X-axis
                        time: {
                            unit: 'day', // Show data points per day
                            displayFormats: {
                                day: 'MMM D', // Display date format for day
                            }
                        }
                    },
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
        const labels = Object.getOwnPropertyNames(questionStat.answers);
        const data = Object.values(questionStat.answers);

        const chartId = 'chart-' + chartIndex;
        window.myCharts[chartId] = createChart(chartId, chartType, labels, data);

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

    // Click event for trend buttons
    $('.btn-primary').on('click', function () {
        const questionId = $(this).data('question-id');
        const questionStat = window.myCharts[questionId];

        // Get trend data from the selected question
        const trendData = questionStat.TrendData;

        // Extract labels and data arrays from the trend data
        const labels = trendData.map(item => item.date);
        const data = trendData.map(item => item.value);

        // Create and show the trend chart in the modal
        const trendChart = createTrendChart('questionTrendChart-' + questionId, labels, data);
        $('#questionTrendModal-' + questionId).on('shown.bs.modal', function () {
            trendChart.update(); // Update the chart after the modal is shown
        });
    });
});