var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/prtransaction/getallpodetails?status=' +status },
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
                            <button type="button" class="btn btn-danger" onclick="cancelPO(${data})" ${status === 'Cancelled' ? 'disabled' : ''}>Cancel</button>
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
        fetch(`/Admin/PRTransaction/CancelPO?poId=${poId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'X-CSRF-TOKEN': $('input[name="__RequestVerificationToken"]').val()
            }
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                if (data.success) {
                    alert(data.message);
                    dataTable.ajax.reload();
                } else {
                    throw new Error(data.message);
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert(error.message || 'Something went wrong, please try again.');
            });
    }
}