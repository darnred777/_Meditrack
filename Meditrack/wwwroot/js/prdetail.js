var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/prtransaction/getallprdetails?status=' +status },
        "columns": [
            { data: 'prDtlID', "width": "5%" },
            { data: 'prHdrID', "width": "5%" },
            { data: 'purchaseRequisitionHeader.supplierName', "width": "10%" },
            { data: 'purchaseRequisitionHeader.locationAddress', "width": "10%" },
            { data: 'purchaseRequisitionHeader.statusDescription', "width": "10%" },
            { data: 'purchaseRequisitionHeader.prDate', "width": "10%" },
            { data: 'productName', "width": "10%" },
            { data: 'unitPrice', "width": "10%" },
            { data: 'unitOfMeasurement', "width": "10%" },
            { data: 'quantityInOrder', "width": "10%" }, 
            { data: 'subtotal', "width": "10%" },
            {
                data: 'prDtlID',
                "render": function (data) {                                       
                    return `  
                        <div class="w-75 btn-group" role="group">
                            <a href="/admin/prtransaction/viewprdetails?prdId=${data}" class="btn btn-primary max-2"><i class="bi bi-pencil-square"></i>View</a>                              
                            <button type="button" class="btn btn-success max-2" onclick="approvePR(${data})"><i class="bi bi-check-square"></i>Approve</button>
                            </div>                   
                        `                                                                                                          
                },  
                "width": "20%"
            }
        ]  
    });
}

function approvePR(prdId) {
    $.ajax({
        url: '/Admin/PRTransaction/ApprovePR?prdId=' + prdId,
        type: 'POST',
        success: function (response) {
            // Handle success, such as refreshing the data table or showing a success message
            console.log("Purchase requisition approved successfully!");
        },
        error: function (xhr, textStatus, errorThrown) {
            // Handle error, such as displaying an error message
            console.error("Error approving purchase requisition:", errorThrown);
        }
    });
}


