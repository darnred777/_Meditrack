var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/prtransaction/getallprdetails?status=' +status },
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
                data: 'prDtlID', // Assuming prHdrID is accessible in your data
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/admin/prtransaction/editpr?prdId=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a>                                                                              
                            <a href="/admin/prtransaction/viewpr?prdId=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>View</a>                                                                              
                        </div>
                    `
                },
                "width": "20%"

            }
        ]  
    });
}