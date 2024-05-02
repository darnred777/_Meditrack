var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/viewer/prtransaction/getallpodetails?status=' +status },
        "columns": [
            {
                data: null,
                render: function (data, type, row) {
                    return row.purchaseOrderHeader.applicationUserFname + ' ' + row.purchaseOrderHeader.applicationUserLname;
                },
                width: "20%"
            },
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
                    // Convert the data (assumed to be in ISO format) to a Date object
                    const date = new Date(data);

                    // Extract year, month, and day components
                    const year = date.getFullYear();
                    const month = String(date.getMonth() + 1).padStart(2, '0'); // Month is zero-indexed
                    const day = String(date.getDate()).padStart(2, '0');

                    // Construct the formatted date string in yyyy/mm/dd format
                    const formattedDate = `${year}-${month}-${day}`;

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
                data: 'poHdrID', // Assuming prHdrID is accessible in your data
                "render": function (data) {
                    return `
                        
                    `
                },
                "width": "20%"

            }
            
        ]  
    });
}