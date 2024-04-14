var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/inventoryofficer/prtransaction/getallpodetails?status=' +status },
        "columns": [
            { data: 'poDtlID', "width": "5%" },
            { data: 'poHdrID', "width": "5%" },
            { data: 'purchaseOrderHeader.supplierName', "width": "10%" },
            { data: 'purchaseOrderHeader.locationAddress', "width": "10%" },
            { data: 'purchaseOrderHeader.statusDescription', "width": "10%" },
            { data: 'purchaseOrderHeader.totalAmount', "width": "10%" },
            { data: 'purchaseOrderHeader.poDate', "width": "10%" },
            { data: 'productName', "width": "10%" },
            { data: 'unitPrice', "width": "10%" },
            { data: 'unitOfMeasurement', "width": "10%" },
            { data: 'quantityInOrder', "width": "10%" }, 
            { data: 'subtotal', "width": "10%" },
            {
                data: 'poHdrID', // Assuming prHdrID is accessible in your data
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">                          
                            <button type="button" class="btn btn-success mx-2" onclick="approvePR(${data})"><i class="bi bi-check-square"></i>Send</button>
                            <button type="button" class="btn btn-danger" onclick="cancelPO(${data})">Cancel</button>
                        </div>
                    `
                },
                "width": "20%"

            }
        ]  
    });
}

function cancelPO(poId) {
    if (confirm("Are you sure you want to cancel this Purchase Order?")) {
        fetch(`/InventoryOfficer/PRTransaction/CancelPO?poId=${poId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'X-CSRF-TOKEN': $('input[name="__RequestVerificationToken"]').val()
            }
        })
            .then(response => response.json())
            .then(data => {
                alert('Purchase ordered cancelled successfully.');
                dataTable.ajax.reload(); // Reload DataTable to reflect changes
            })
            
            .catch(error => {
                console.error('Error cancelling the purchase requisition:', error);
                alert('Failed to cancel the purchase requisition.');
            });
    }
}