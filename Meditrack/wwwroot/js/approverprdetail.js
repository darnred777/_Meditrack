var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/approver/prtransaction/getallprdetails?status=' +status },
        "columns": [
            { data: 'purchaseRequisitionHeader.applicationUserEmail', "width": "5%" },
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
                    // Convert the data (assumed to be in ISO format) to a Date object
                    const date = new Date(data);

                    // Extract year, month, and day components
                    const year = date.getFullYear();
                    const month = String(date.getMonth() + 1).padStart(2, '0'); // Month is zero-indexed
                    const day = String(date.getDate()).padStart(2, '0');

                    // Construct the formatted date string in yyyy/mm/dd format
                    const formattedDate = `${year}/${month}/${day}`;

                    return formattedDate;
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
                data: 'prHdrID', // Assuming prHdrID is accessible in your data
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">                                                        
                                <button type="button" class="btn btn-success mx-2" onclick="approvePR(${data})" ${status === 'Cancelled' ? 'disabled' : ''}><i class="bi bi-check-square"></i> Approve</button>
                                <button type="button" class="btn btn-danger mx-2" onclick="cancelPR(${data})" ${status === 'Cancelled' ? 'disabled' : ''}><i class="bi bi-x-square"></i> Cancel</button>
                            </div>`
                },
                "width": "25%"

            }
        ]  
    });
}

function cancelPR(prId) {
    if (!confirm('Are you sure you want to cancel this purchase requisition?')) return;

    fetch(`/Approver/PRTransaction/CancelPR?prdId=${prId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'X-CSRF-TOKEN': $('input[name="__RequestVerificationToken"]').val()
        }
    })
        .then(response => response.json())
        .then(data => {
            //alert('Purchase requisition cancelled successfully.');
            dataTable.ajax.reload(); // Reload DataTable to reflect changes
        })
        .catch(error => {
            console.error('Error cancelling the purchase requisition:', error);
            alert('Failed to cancel the purchase requisition.');
        });
}

function approvePR(prdId) {
    $.ajax({
        url: '/Approver/PRTransaction/ApprovePR?prdId=' + prdId,
        type: 'POST',
        success: function (response) {
            //console.log("Purchase requisition approved successfully!");
            dataTable.ajax.reload(); // Reload DataTable to reflect changes
        },
        error: function (xhr, textStatus, errorThrown) {
            console.error("Error approving purchase requisition:", errorThrown);
        }
    });
}

