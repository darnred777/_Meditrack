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
            { data: 'purchaseRequisitionHeader.supplier.supplierName', "width": "10%" },
            { data: 'purchaseRequisitionHeader.location.locationAddress', "width": "10%" },
            { data: 'purchaseRequisitionHeader.status.statusDescription', "width": "10%" },
            { data: 'purchaseRequisitionHeader.totalAmount', "width": "10%" },
            { data: 'purchaseRequisitionHeader.prDate', "width": "10%" },
            { data: 'product.productName', "width": "10%" },
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
                        </div>                      
                        `                                                                                                          
                },  
                "width": "20%"
            }
        ]
    });
}

