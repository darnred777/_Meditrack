var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/prtransaction/getall?status=' +status },
        "columns": [
            { data: 'prHdrID', "width": "10%" },
            { data: 'supplier.supplierName', "width": "10%" },
            { data: 'location.locationAddress', "width": "10%" },
            { data: 'status.statusDescription', "width": "10%" },
            { data: 'totalAmount', "width": "10%" },
            { data: 'prDate', "width": "10%" },
            {
                data: 'prHdrID',
                "render": function (data) {                                       
                        return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/admin/prtransaction/prdetails?prId=${data}" class="btn btn-primary max-2"><i class="bi bi-pencil-square"></i></a>                              
                        </div>                      
                        `                                                                                                          
                },  
                "width": "20%"
            }
        ]
    });
}

