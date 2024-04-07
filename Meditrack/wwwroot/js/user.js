var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/user/getall' },
        "columns": [
            { data: 'firstName', "width": "10%" },
            { data: 'lastName', "width": "10%" },
            { data: 'email', "width": "10%" },
            { data: 'location.locationAddress', "width": "10%" },
            { data: 'birthDate', "width": "10%" },
            { data: 'registrationDate', "width": "10%" },
            { data: 'lastLoginTime_Date', "width": "10%" }, 
            { data: 'role', "width": "10%" },
            { data: 'isActive', "width": "10%" },
            {
                data: { id:"id", lockoutEnd:"lockoutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();

                    if (lockout > today) {
                        return `
                        <div class="text-center">
                            <a onclick=LockUnlock('${data.id}') class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="bi bi-unlock-fill"></i>Activate
                            </a>    
                            <a href="/admin/user/RoleManagement?userId=${data.id}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="bi bi-pencil-fill"></i>Edit
                            </a>
                        </div>
                    `
                    } else {
                        return `
                        <div class="text-center">
                            <a onclick=LockUnlock('${data.id}') class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                                <i class="bi bi-lock-fill"></i>Deactivate
                            </a>
                            <a href="/admin/user/RoleManagement?userId=${data.id}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="bi bi-pencil-fill"></i>Edit
                            </a>
                        </div>
                    `
                    }                                                                      
                },
                "width": "50%"
            }
        ]
    });
}

function LockUnlock(id) {
    $.ajax({
        type: "POST",   
        url: '/Admin/User/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                //success(data.message);
                dataTable.ajax.reload();
            }
        }
    });
}