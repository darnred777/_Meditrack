var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/inventoryofficer/monitoring/getmonitoring' },
        "columns": [
            { data: 'product.productName', "width": "10%" },
            { data: 'supplier.supplierName', "width": "10%" },
            { data: 'supplier.officeAddress', "width": "10%" },
            { data: 'quantityReceived', "width": "10%" },
            { data: 'quantityExpected', "width": "10%" },
            { data: 'quantityLacking', "width": "10%" },
            {
                data: 'receivedDate',
                render: function (data) {
                    // Convert the data (assumed to be in ISO format) to a Date object
                    const date = new Date(data);

                    // Extract year, month, and day components
                    const year = date.getFullYear();
                    const month = String(date.getMonth() + 1).padStart(2, '0'); // Month is zero-indexed
                    const day = String(date.getDate()).padStart(2, '0');

                    // Construct the formatted date string in yyyy-mm-dd format
                    const formattedDate = `${year}-${month}-${day}`;

                    return formattedDate;
                },
                "width": "15%"
            },
            {
                data: 'monitoringID', // Assuming productID is accessible in your data
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/inventoryofficer/monitoring/upsertmonitoring?monitoringID=${data}" class="btn btn-info mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
                            <a href="/inventoryofficer/monitoring/deletemonitoring?monitoringID=${data}" class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i>Delete</a>
                        </div>
                    `
                },
                "width": "20%"
            }
        ]
    });
}
