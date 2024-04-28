var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/inventoryofficer/prtransaction/getallpodetails?status=' +status },
        "columns": [
            { data: 'purchaseOrderHeader.applicationUserEmail', "width": "5%" },
            { data: 'purchaseOrderHeader.supplierName', "width": "10%" },
            { data: 'purchaseOrderHeader.locationAddress', "width": "10%" },
            { data: 'purchaseOrderHeader.statusDescription', "width": "10%" },
            {
                data: 'purchaseOrderHeader.totalAmount', "width": "10%",
                "render": function (data) {
                    return data ? parseFloat(data).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) : '0.00';
                }
            },
            {
                data: 'purchaseOrderHeader.poDate',
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
                data: 'poHdrID', // Assuming prHdrID is accessible in your data
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">                          
                            <button type="button" class="btn btn-success mx-2" onclick="sendPO(${data})"><i class="bi bi-check-square"></i>Send</button>
                            <button type="button" class="btn btn-danger" onclick="cancelPO(${data})" ${status === 'Cancelled' ? 'disabled' : ''}>Cancel</button>
                        </div>
                    `
                },
                "width": "20%"

            }
        ]  
    });
}

function sendPO(poHdrID) {
    if (confirm("Are you sure you want to send this Purchase Order?")) {
        fetch('/InventoryOfficer/PRTransaction/SendPurchaseOrderEmail?poHdrID=' + poHdrID, {
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

function cancelPO(poId) {
    if (confirm("Are you sure you want to cancel this Purchase Order?")) {
        fetch(`/InventoryOfficer/PRTransaction/CancelPO?poId=${poId}`, {
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