var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/user/getall' },
        "columns": [
            { "data": "firstName", "width": "15%" },
            { "data": "lastName", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "location.locationAddress", "width": "15%" },
            { "data": "birthDate", "width": "15%" },
            { "data": "registrationDate", "width": "15%" },
            { "data": "lastLoginTime_Date", "width": "15%" }, 
            { "data": "role", "width": "20%" },
            { "data": "isActive", "width": "20%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/admin/location/upsert?id=${data}" class="btn-primary mx-2"> <i class="bi bi-pencil-sqaure"></i> Edit</a>
                    </div>
                    `                                
                },
                "width": "25%"
            }
        ]
    });
}
