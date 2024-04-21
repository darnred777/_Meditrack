var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/inventoryofficer/prtransaction/getallprdetails?status=' +status },
        "columns": [
            { data: 'prDtlID', "width": "5%" },
            { data: 'prHdrID', "width": "5%" },
            { data: 'purchaseRequisitionHeader.supplierName', "width": "10%" },
            { data: 'purchaseRequisitionHeader.locationAddress', "width": "10%" },
            { data: 'purchaseRequisitionHeader.statusDescription', "width": "10%" },
            {
                data: 'purchaseRequisitionHeader.totalAmount', "width": "10%",
                "render": function (data) {
                    return data ? parseFloat(data).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) : '0.00';
                }
            },
            {
                data: 'purchaseRequisitionHeader.prDate',
                render: function (data) {
                    return new Date(data).toLocaleString();
                },
                width: "10%"
            },
            { data: 'productName', "width": "10%" },
            {
                data: 'unitPrice', "width": "10%",
                "render": function (data) {
                    return data ? parseFloat(data).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) : '0.00';
                }
            },
            { data: 'unitOfMeasurement', "width": "10%" },
            { data: 'quantityInOrder', "width": "10%" }, 
            {
                data: 'subtotal', "width": "10%",
                "render": function (data) {
                    return data ? parseFloat(data).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) : '0.00';
                }
            },
            {
                data: 'prDtlID', // Assuming prHdrID is accessible in your data
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/inventoryofficer/prtransaction/editpr?prdId=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a>                                       
                        </div>
                    `
                },
                "width": "20%"

            }
        ]  
    });
}