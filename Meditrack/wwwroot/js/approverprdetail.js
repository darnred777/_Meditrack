var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/approver/prtransaction/getallprdetails?status=' +status },
        "columns": [
            { data: 'prDtlID', "width": "5%" },
            { data: 'prHdrID', "width": "5%" },
            { data: 'purchaseRequisitionHeader.supplierName', "width": "10%" },
            { data: 'purchaseRequisitionHeader.locationAddress', "width": "10%" },
            { data: 'purchaseRequisitionHeader.statusDescription', "width": "10%" },
            { data: 'purchaseRequisitionHeader.totalAmount', "width": "10%" },
            { data: 'purchaseRequisitionHeader.prDate', "width": "10%" },
            { data: 'productName', "width": "10%" },
            { data: 'unitPrice', "width": "10%" },
            { data: 'unitOfMeasurement', "width": "10%" },
            { data: 'quantityInOrder', "width": "10%" }, 
            { data: 'subtotal', "width": "10%" },
            {
                data: 'prHdrID', // Assuming prHdrID is accessible in your data
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">                          
                                <button type="button" class="btn btn-success mx-2" onclick="approvePR(${data})"><i class="bi bi-check-square"></i> Approve</button>
                                <button type="button" class="btn btn-danger mx-2" onclick="cancelPR(${data})"><i class="bi bi-x-square"></i> Cancel</button>
                            </div>`
                },
                "width": "25%"

            }
        ]  
    });
}

function approvePR(prdId) {
    $.ajax({
        url: '/Approver/PRTransaction/ApprovePR?prdId=' + prdId,
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


function cancelPR(prId) {
    if (!confirm('Are you sure you want to cancel this purchase requisition?')) return;

    fetch(`/Approver/PRTransaction/CancelPR?prdId=${prId}`, {
        method: 'POST',
        headers: {
            // Include any necessary headers, like CSRF tokens if needed
            'Content-Type': 'application/json',
            'X-CSRF-TOKEN': $('input[name="__RequestVerificationToken"]').val()
        }
    })
        .then(response => response.json())
        .then(data => {
            alert('Purchase requisition cancelled successfully.');
            // You might want to redirect or refresh the page here
            window.location.reload();
        })
        .catch(error => {
            console.error('Error cancelling the purchase requisition:', error);
            alert('Failed to cancel the purchase requisition.');
        });
}
