var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblCompany').DataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "streetAddress", "width": "15%" },
            { "data": "city", "width": "10%" },
            { "data": "state", "width": "10%" },
            { "data": "phoneNumber", "width": "10%" },
            {
                "data": "isAuthorizedCompany",
                "reder": function (data) {
                    if (data) {
                        return `<input type="checkbox" disabled checked/>`
                    }
                    else {
                        return `<input type="checkbox" disabled />`
                    }
                },
                "width": "10%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Admin/Company/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                           <i class="fas fa-edit"></i>
                        </a>
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Delete('/Admin/Company/Delete/${data}')>
                           <i class="fas fa-trash-alt"></i>
                        </a>
                        </div>`;
                }, "width": "60%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}