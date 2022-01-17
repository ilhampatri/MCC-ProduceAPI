// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



//donat1

//var options = {
//    series: [44, 55, 41, 17, 15],
//    chart: {
//        type: 'donut',
//    }
//};

//var chart = new ApexCharts(document.querySelector("#donutChart"), options);
//chart.render();

//$.ajax({
//    url: "https://localhost:44362/API/Employees/CountEmployeeBySalary"
//}).done((result) => {
//    console.log(result);

//    const salary = [];
//    const valEmp2 = [];
//    $.each(result.result, function (key, val) {
//        if (val.salary != 0) {
//            salary.push(val.salary);
//            valEmp2.push(val.value);
//        }
//    });

//    var optionsSal = {
//        series: [{
//            name: 'Employee',
//            data: generateSalary(salary, valEmp2)
//        }],
//        chart: {
//            height: 350,
//            type: 'bar'
//        },
//        plotOptions: {
//            bar: {
//                columnWidth: '40%'
//            }
//        },
//        colors: ['#00E396'],
//        dataLabels: {
//            enabled: false
//        },
//        legend: {
//            show: true,
//            showForSingleSeries: true,
//            customLegendItems: ['Employees'],
//            markers: {
//                fillColors: ['#00E396', '#775DD0']
//            }
//        }
//    };

//    var chart2 = new ApexCharts(document.querySelector("#salaryChart"), optionsSal);
//    chart2.render();
//}).fail((error) => {
//    console.log(error);
//});

//function generateSalary(salary, valEmp2) {
//    var values = [salary, valEmp2];
//    var i = 0;
//    var series = [];
//    while (i < salary.length) {
//        /*series.push([x, values[s][i]]);*/
//        series.push([
//            values[0][i],
//            values[1][i]
//        ]);
//        i++;
//    }
//    return series;
//}



var gender_chart;
var university_chart;
var salary_chart;
$(document).ready(function () {
    //$.ajax({
    //    url: 'https://localhost:44362/Api/Employees/SalaryStat'
    //}).done((result) => {
    //    console.log(result.data[0]);

    //    var salary_chart_options = {
    //        chart: {
    //            type: 'donut'
    //        },
    //        series: [result.data[0].count, result.data[1].count],
    //        labels: ['Male', 'Female']
    //    }

    //    salary_chart = new ApexCharts(document.querySelector("#gender-chart"), salary_chart_options);
    //    salary_chart.render();

    //}).fail((error) => {
    //    console.log(error);
    //    salary_chart.html = error;
    //})





    $.ajax({
        url: 'https://localhost:44362/Api/Employees/GenderStat'
    }).done((result) => {
        console.log(result.data[0]);

        var gender_chart_options = {
            chart: {
                type: 'donut'
            },
            series: [result.data[0].count, result.data[1].count],
            labels: ['Male', 'Female']
        }

        gender_chart = new ApexCharts(document.querySelector("#gender-chart"), gender_chart_options);
        gender_chart.render();

    }).fail((error) => {
        console.log(error);
        gender_chart.html = error;
    })

    $.ajax({
        url: 'https://localhost:44362/Api/Universities/UniversityStat'
    }).done((result) => {
        let dataCount = new Array();
        let dataUniversityName = new Array();
        console.log("INI Univ")
        console.log(result);
        $.each(result.data, function (key, val) {
            console.log(val);
            dataCount.push(val.count);
            dataUniversityName.push(val.universityName);
        });
        console.log(dataCount);
        console.log(dataUniversityName);
        var university_chart_options = {
            chart: {
                type: 'bar'
            },
            series: [{
                name: 'count',
                data: dataCount
            }],
            xaxis: {
                categories: dataUniversityName
            }
        }
        university_chart = new ApexCharts(document.querySelector("#university-chart"), university_chart_options);

        university_chart.render();

    }).fail((error) => {
        console.log(error);
        university_chart.html = error;
    })
});


































//var gender_chart;
//$(document).ready(function () {
//    $.ajax({
//        url: 'https://localhost:44367/Api/Employees/GenderStat'
//    }).done((result) => {
//        console.log(result.data[0]);

//        var gender_chart_options = {
//            chart: {
//                type: 'donut'
//            },
//            series: [result.data[0].count, result.data[1].count],
//            labels: ['Male', 'Female']
//        }

//        gender_chart = new ApexCharts(document.querySelector("#gender-chart"), gender_chart_options);
//        gender_chart.render();

//    }).fail((error) => {
//        console.log(error);
//        gender_chart.html = error;
//    })
//});


