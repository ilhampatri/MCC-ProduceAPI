// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.



// Write your JavaScript code.


$.ajax({
    /*url: "https://localhost:44362/API/employees/register"*/
    url: "/employees/register",
    /*url: "/employees/getall"*/
    /*url: "/employees/getregistered",*/

}).done((result) => {
    console.log("ini register")
    console.log(result);
})



$(document).ready(function () {
    var tableEmp = $('#tableEmployee').DataTable({
        ajax: {
            /*'url': "https://localhost:44362/API/employees/register",*/
            url: "/employees/getregistered",
            'dataType': 'json',
            'dataSrc': ''
        },
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copy',
                exportOptions: { columns: [0, 1, 2, 3, 4] }
            },
            {
                extend: 'csv',
                exportOptions: { columns: [0, 1, 2, 3, 4] }
            },
            {
                extend: 'excel',
                exportOptions: { columns: [0, 1, 2, 3, 4] }
            },
            {
                extend: 'pdfHtml5',
                exportOptions: { columns: [0, 1, 2, 3, 4] }
            },
            {
                extend: 'print',
                exportOptions: { columns: [0, 1, 2, 3, 4] }
            },
        ],
        columns: [
            {
                data: 'nik'
            },
            {
                data: 'fullName'
            },
            {
                data: 'gender',
                render: function (data, type, row) {
                    if (data == 0) {
                        return "Male";
                    } else if (data == 1) {
                        return "Female";
                    } else {
                        return "Undefined";
                    }
                }
            },
            {
                data: 'email',
            },
            {
                data: 'university'
            },
            {
                data: 'salary',
                render: function (data, type, row) {
                    return "Rp " + data;
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    //console.log(data);
                    /*console.log(data.nik);*/
                    return `<button class="btn btn-primary" data-toggle="modal" data-target="#detailEmployee" onclick="getdetailsRegister(\`${data.nik}\`)"><i class="bi bi-info-circle-fill"></i></button>
                            <button class="btn btn-secondary" data-toggle="modal" data-target="#editEmployee" onclick="getDetails(\`${data.nik}\`)"><i class="bi bi-pencil-fill"></i></button>
                            <button class="btn btn-danger" onclick="deleteEmployee(\`${data.nik}\`)"><i class="bi bi-trash-fill"></i></button>
                            `;
                } 
            }
        ]
    });
});


//dijalankan ketika fungsi submit dijalankan
$("#form-regist").submit(function (event) {
    $("#form-regist").validate({
        //validasi dengan rules
        rules: {
            //atribut2
            FirstName: {
                required: true,
                minlength: 2
            },
            LastName: {
                required: true,
                minlength: 2
            },
            email: { email: true, required: true },
            phone: {
                required: true,
                minlength: 10
            },
            gender: { required: true },
            birthdate: { required: true },
            password: {
                required: true,
                minlength: 8
            },
            salary: { required: true },
            university: { required: true },
            degree: { required: true },
            gpa: {
                required: true,
                number: true,
                range: [0, 4]
            }
        },
        submitHandler: function (form) {
            registerData(); // requestPOST
        }
    });
    event.preventDefault();
});

function registerData() {
    var obj = Object();
    obj.FirstName = $('#fname').val();
    obj.LastName = $('#lname').val();
    obj.Gender = parseInt($('#gender').val());
    obj.Birthdate = $('#birthdate').val();
    obj.Phone = $('#phone').val();
    obj.Email = $('#email').val();
    obj.Password = $('#password').val();
    obj.Salary = parseInt($('#salary').val());
    obj.UniversityId = parseInt($('#university').val());
    obj.Degree = $('#degree').val();
    obj.GPA = parseFloat($('#GPA').val());
    console.log(obj);

    $.ajax({
        /*url: "https://localhost:44362/API/employees/register",*/
        url: "/employees/register",
        type: "POST",
        //dataType: 'json',
        //data: JSON.stringify(obj),
        data: obj,
    }).done((result) => {
        
        Swal.fire(
            'Good job!',
            'You clicked the button!',
            'success'
        );
        $('#registerEmployee').modal('hide');
        $('#tableEmployee').DataTable().ajax.reload();
        /*tableEmp.ajax.reload();*/
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
        });
    });
}


function deleteEmployee(nik) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                //url: "https://localhost:44362/API/Employees/" + nik,
                url: "/Employees/Delete/"+ nik,

                type: "DELETE",
                //headers: {
                //    'Accept': 'application/json',
                //    'Content-Type': 'application/json'
                //},
                //dataType: "json",
            }).done((result) => {
                console.log("success");
                Swal.fire({
                    title: 'Deleted!',
                    text: 'Data successfuly deleted',
                    icon: 'success',
                    confirmButtonText: 'Cool'
                })
                $('#tableEmployee').DataTable().ajax.reload();
                /*tableEmp.ajax.reload();*/
            }).fail((error) => {
                Swal.fire({
                    title: 'Error!',
                    text: 'Do you want to continue',
                    icon: 'error',
                    confirmButtonText: 'Cool'
                })
            })
        }
    })
}

//function deleteEmployee(nik) {
//    $.ajax({
//        url: "https://localhost:44362/API/employees/register/"+ nik,
//        type: "DELETE",
//        dataType: 'json',
//        headers: {
//            'Accept': 'application/json',
//            'Content-Type': 'application/json'
//        },
//    }).done((result) => {
//        Swal.fire({
//            title : 'Deleted!',
//            text : 'Data Successfully deleted',
//            icon: 'success'
//        });
//        /*$('#deleteEmployee').modal('hide');*/
//        tableEmp.ajax.reload();
//    }).fail((error) => {
//        Swal.fire({
//            icon: 'error',
//            title: 'Oops...',
//            text: 'Something went wrong!',
//        });
//    });
//}


//dijalankan ketika fungsi submit dijalankan
$("#form-edit").submit(function (event) {
    $("#form-edit").validate({
        //validasi dengan rules
        rules: {
            //atribut2
            editFirstName: {
                required: true,
                minlength: 2
            },
            editLastName: {
                required: true,
                minlength: 2
            },
            editemail: { email: true, required: true },
            editphone: {
                required: true,
                minlength: 10
            },
            degree: { required: true },
            birthdate: { required: true },
            editsalary: { required: true },
        },
        submitHandler: function (form) {
            editData(); // requestPOST
        }
    });
    event.preventDefault();
});

function getDetails(nik) {
    $.ajax({
        /*url: "https://localhost:44362/API/employees/register"*/
        url: "/employees/getregistered/"+nik
        /*url : /employees/getall*/
    }).done((result) => {
        /*let selectedObj;*/
        console.log(result);

        //Object.entries(result.result).forEach(([key, val]) => {
        //    if (val.nik == nik) {
        //        selectedObj = val;
        //    }
        //});
        console.log("selectedOBj");
        document.getElementById("editnik").value = result.nik;
        document.getElementById("editfname").value = result.firstName;
        document.getElementById("editlname").value = result.lastName;
        document.getElementById("editphone").value = result.phone;
        document.getElementById("editemail").value = result.email;
        document.getElementById("editsalary").value = result.salary;
        //document.getElementById("edituniversity").value = selectedObj.university;
        //document.getElementById("editdegree").value = selectedObj.degree;
        //document.getElementById("editgpa").value = selectedObj.gpa;


    }).fail((error) => {
        console.log(error);
    });
};

function editData(nik) {

    var obj = Object();
    console.log("ini obj")
    console.log(obj);
    obj.NIK = $('#editnik').val();;
    obj.FirstName = $('#editfname').val();
    obj.LastName = $('#editlname').val();
    obj.Gender = parseInt($('#editgender').val());
    obj.Birthdate = $('#editbirthdate').val();
    obj.Phone = $('#editphone').val();
    obj.Email = $('#editemail').val();
    obj.Salary = parseInt($('#editsalary').val());
    //obj.University = $('#edituniversity').val();
    //obj.Degree = $('#editdegree').val();
    //obj.GPA = $('#editgpa').val();
    

    $.ajax({
        url: "https://localhost:44362/API/employees/",
        type: "PUT",
        dataType: 'json',
        data: JSON.stringify(obj),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    }).done((result) => {
        Swal.fire(
            'Good job!',
            'You clicked the button!',
            'success'
        );
        $('#editEmployee').modal('hide');
        $('#tableEmployee').DataTable().ajax.reload();
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
        });
    });
}


function getdetailsRegister(nik) {
    $.ajax({
        /*url: "https://localhost:44362/API/employees/register",*/
        //url: "/employees/getregistered",
        url: "/employees/getregistered/"+nik,
    }).done((result) => {
          let selectedObj;

        //console.log(result);
        //var text = "";
        //Object.entries(result).forEach(([key, val]) => {
        //    if (val.nik == nik) {
        //        selectedObj = val;
        //    }
        //});

        text = `<div class = "text-center">
                    <table class= "table bg-light table-hover text-info text-center">
                        <tr>
                            <td>Name</td>
                            <td>:</td>
                            <td>${result.fullName}</td>
                        </tr>
                        <tr>
                            <td>Birth Date</td>
                            <td>:</td>
                            <td>${result.birthdate}</td>
                        </tr>
                        <tr>
                            <td>Salary</td>
                            <td>:</td>
                            <td>Rp. ${result.salary}</td>
                        </tr>
                        <tr>
                            <td>E-mail</td>
                            <td>:</td>
                            <td>${result.email}</td>
                        </tr>
                        <tr>
                            <td>Phone</td>
                            <td>:</td>
                            <td>${result.phone}</td>
                        </tr>
                        <tr>
                            <td>University</td>
                            <td>:</td>
                            <td>${result.university}</td>
                        </tr
                        <tr>
                            <td>GPA</td>
                            <td>:</td>
                            <td>${result.gpa}</td>
                        </tr>
                         <tr>
                            <td>Degree</td>
                            <td>:</td>
                            <td>${result.degree}</td>
                        </tr>
                    </table>
                    </div>`

        //console.log("selectedOBj");
        //console.log(selectedObj.nik);
        
        //document.getElementById("detailfullname").html(selectedObj);
        //document.getElementById("detailbirthdate").value = selectedObj.birthdate;
        //document.getElementById("detailsalary").value = selectedObj.phone;
        //document.getElementById("detailemail").value = selectedObj.email;
        //document.getElementById("detailgender").value = selectedObj.gender;
        //document.getElementById("detaildegree").value = selectedObj.degree;
        //document.getElementById("detailgpa").value = selectedObj.gpa;
        //document.getElementById("detailuniversity").value = selectedObj.university;
        $('.detailEmployee').html(text).css('background-color', 'white');
    }).fail((error) => {
    }).fail((error) => {
        console.log(error);
    });
};
//chartGender();
//function chartGender() {
//    male = 0;
//    female = 0;
//    jQuery.ajax({
//        url: 'https://localhost:44362/api/Employees/register',
//        success: function (result) {
//            $.each(result.result, function (key, val) {
//                if (val.gender == "Female") {
//                    female++;
//                }
//                else {
//                    male++;
//                }
//            });
//            var options = {
//                chart: {
//                    type: 'donut',
//                    size: '100%',
//                    toolbar: {
//                        show: true,
//                        offsetX: 0,
//                        offsetY: 0,
//                        tools: {
//                            download: true,
//                            selection: true,
//                            zoom: true,
//                            zoomin: true,
//                            zoomout: true,
//                            pan: true,
//                            reset: true | '<img src="/static/icons/reset.png" width="20">',
//                            customIcons: []
//                        },
//                        export: {
//                            csv: {
//                                filename: undefined,
//                                columnDelimiter: ',',
//                                headerCategory: 'category',
//                                headerValue: 'value',
//                                dateFormatter(timestamp) {
//                                    return new Date(timestamp).toDateString()
//                                }
//                            },
//                            svg: {
//                                filename: undefined,
//                            },
//                            png: {
//                                filename: undefined,
//                            }
//                        },
//                        autoSelected: 'zoom'
//                    },
//                },
//                dataLabels: {
//                    enabled: true
//                },
//                title: {
//                    text: 'Perbandingan Gender',
//                    align: 'left',
//                    margin: 10,
//                    offsetX: 0,
//                    offsetY: 0,
//                    floating: false,
//                    style: {
//                        fontSize: '14px',
//                        fontWeight: 'bold',
//                        fontFamily: undefined,
//                        color: 'fb0081'
//                    },
//                },
//                series: [male, female],
//                labels: ['Male', 'Female'],
//                noData: {
//                    text: 'Loading...'
//                }
//            }
//            var chart = new ApexCharts(document.querySelector("#chartGender"), options);
//            chart.render();
//        },
//        async: false
//    })
//}

//chartSalary();
//function chartSalary() {
//    upper = 0;
//    mid = 0;
//    jQuery.ajax({
//        url: 'https://localhost:44332/api/Employees/GetRegisteredData',
//        success: function (result) {
//            $.each(result.data, function (key, val) {
//                if (val.salary > 8000000) {
//                    upper++;
//                }
//                else {
//                    mid++;
//                }
//            });
//            var options = {
//                chart: {
//                    type: 'donut',
//                    size: '50%',
//                    toolbar: {
//                        show: true,
//                        offsetX: 0,
//                        offsetY: 0,
//                        tools: {
//                            download: true,
//                            selection: true,
//                            zoom: true,
//                            zoomin: true,
//                            zoomout: true,
//                            pan: true,
//                            reset: true | '<img src="/static/icons/reset.png" width="20">',
//                            customIcons: []
//                        },
//                        export: {
//                            csv: {
//                                filename: undefined,
//                                columnDelimiter: ',',
//                                headerCategory: 'category',
//                                headerValue: 'value',
//                                dateFormatter(timestamp) {
//                                    return new Date(timestamp).toDateString()
//                                }
//                            },
//                            svg: {
//                                filename: undefined,
//                            },
//                            png: {
//                                filename: undefined,
//                            }
//                        },
//                        autoSelected: 'zoom'
//                    },
//                },
//                dataLabels: {
//                    enabled: true
//                },
//                title: {
//                    text: 'Salary Rate',
//                    align: 'left',
//                    margin: 10,
//                    offsetX: 0,
//                    offsetY: 0,
//                    floating: false,
//                    style: {
//                        fontSize: '14px',
//                        fontWeight: 'bold',
//                        fontFamily: undefined,
//                        color: '#263238'
//                    },
//                },
//                series: [upper, mid],
//                labels: ['Gaji Di Atas 8jt', 'Gaji Di Bawah 8jt'],
//                noData: {
//                    text: 'Loading...'
//                }
//            }
//            var chart = new ApexCharts(document.querySelector("#chartSalary"), options);
//            chart.render();
//        },
//        async: false
//    })
//}

