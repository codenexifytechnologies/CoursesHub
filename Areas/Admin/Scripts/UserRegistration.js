$(document).ready(function () {
    if (window.CHLoader) { CHLoader.show('Loading\u2026'); }
    $.ajax({
        url: '/Admin/AdminLogin/GetUserRegistrationDetails',
        method: 'GET',
        dataType: 'json',
        success: function (datas) {
            if (datas.datas.length > 0) {
                $("#example1").DataTable({
                    data: datas.datas,
                    columns: [
                        { data: "Name" },
                        { data: "EmailId" },
                        { data: "mobileNo" },
                        { data: "RoleName" },
                        /* { data: "isActive" },*/
                        {
                            data: "isActive",
                            render: function (data, type) {
                                if (type === "display") {
                                    return data === "Active"
                                        ? '<span class="badge badge-soft-success">Active</span>'
                                        : '<span class="badge badge-soft-danger">In-Active</span>';
                                }
                                return data;
                            }
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var DeleteLink;
                                if (data.isActive == "Active") {
                                    DeleteLink = '<a href="/Admin/AdminLogin/ActionLoginDetails?Id=' + data.userId + '&Operation=' + data.isActive + '" onclick="return DeleteConfirmed(event);"><i style="color:red" class="fa fa-user-times"  title="Activate/Deactivate"></i></a>';
                                }
                                else {
                                    DeleteLink = '<a href="/Admin/AdminLogin/ActionLoginDetails?Id=' + data.userId + '&Operation=' + data.isActive + '" onclick="return DeleteConfirmed(event);"><i style="color:green" class="fa fa-user-check"  title="Activate/Deactivate"></i></a>';
                                }
                                var EditLink = '<a href="/Admin/AdminLogin/EditLoginDetails?UserId=' + data.userId + '"><i class="fa fa-user-edit" title="Edit"></i></a>';
                                return EditLink + '&nbsp;&nbsp;&nbsp;' + DeleteLink;
                            }
                        }
                    ],
                    //"rowCallback": function(row, data, index) {
                    //    if (data.isActive == "InActive") {
                    //        $(row).css('background-color', 'lightcoral');
                    //    }
                    //},
                    "destroy": true,
                    "responsive": true,
                    "lengthChange": false,
                    "autoWidth": false,
                //    "buttons": [{
                //        extend: 'copy',
                //        className: 'btn btn-sm btn-dark rounded-pill px-3 mr-1 shadow-sm',
                //        exportOptions: { columns: ":visible" }
                //    },
                //{
                //    extend: 'csv',
                //    className: 'btn btn-sm btn-dark rounded-pill px-3 mr-1 shadow-sm',
                //    exportOptions: { columns: ":visible" }
                //},
                //{
                //    extend: 'excel',
                //    className: 'btn btn-sm btn-dark rounded-pill px-3 mr-1 shadow-sm',
                //    exportOptions: { columns: ":visible" }
                //},
                //{
                //    extend: 'pdf',
                //    className: 'btn btn-sm btn-dark rounded-pill px-3 mr-1 shadow-sm',
                //    exportOptions: { columns: ":visible" }
                //},
                //{
                //    extend: 'print',
                //    className: 'btn btn-sm btn-dark rounded-pill px-3 mr-1 shadow-sm',
                //    exportOptions: { columns: ":visible" }
                //}, ],
                }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
            }
            else {
                var table = new DataTable('#example1');
                table.clear().draw();
            }
            if (window.CHLoader) { CHLoader.hide(); }
        },
        error: function (xhr, status, error) {
            console.error('Error fetching data:', error);
            if (window.CHLoader) { CHLoader.hide(); }
        }
    });
});


//function callsweetalert() {
//    let text = "Are you sure ?";
//    if (confirm(text) == true) {
//        return true;
//    } else {
//        return false;
//    }
//}

function DeleteConfirmed(ev) {
    ev.preventDefault();
    var urlToRedirect = ev.currentTarget.getAttribute('href');
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes"
    }).then((result) => {
        if (result.isConfirmed) {
            location.href = urlToRedirect;
            return true;
        }
        else {
            return false;
        }
    });
}