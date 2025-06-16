document.addEventListener("DOMContentLoaded", function () {
    const barCtx = document.getElementById('barChart').getContext('2d');
    const pieCtx = document.getElementById('pieChart').getContext('2d');   
    if (typeof ChartDataLabels !== 'undefined') {
        Chart.register(ChartDataLabels);
    }
   
    //Pie Chart data Variables
    if (!window.caseStatusData) {
        console.error('No monthly case status data found');
        return;
    }

    const lawyerStatusData = window.caseStatusData;
    const labels = lawyerStatusData.map(item => item.Status);
    const data = lawyerStatusData.map(item => item.Count);

    //Monthly Filed and Disposed status
    if (!window.monthlyCaseData) {
        console.error('No monthly case status data found');
        return;
    }

    const monthlyStatus = window.monthlyCaseData;
    const months = monthlyStatus.map(x => x.Month);
    const filedCases = monthlyStatus.map(x => x.Filed);
    const disposedCases = monthlyStatus.map(x => x.Disposed);


    new Chart(barCtx, {
        type: 'bar',
        data: {            
            labels: months,
            datasets: [                
                { label: 'Filed', data: filedCases, backgroundColor: '#007bff' },
                { label: 'Disposed', data: disposedCases, backgroundColor: '#fd7e14' }
            ]
        },
        options: {
            responsive: false, maintainAspectRatio: false, scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });

    new Chart(pieCtx, {
        type: 'pie',
        data: {
            labels: labels,
            datasets: [{ data: data, backgroundColor: ['#007bff', '#20c997', '#fd7e14'] }]
        },
        options: {
            responsive: true,
            plugins: {
                datalabels: {
                    color: '#fff',
                    formatter: (value, ctx) => {
                        return value;
                    },
                    font: {
                        weight: 'bold',
                        size: 14
                    }
                },
                legend: {
                    position: 'bottom'
                }
            }
        },
        plugins: [ChartDataLabels]
        //options: {
        //    responsive: false, maintainAspectRatio: false, scales: {
        //        y: {
        //            beginAtZero: true
        //        }
        //    }
        //}
    });
});