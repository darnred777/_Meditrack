var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getproduct' },
        "columns": [
            { data: 'productCategory.categoryName', "width": "10%" },
            { data: 'productName', "width": "10%" },
            { data: 'sku', "width": "10%" },
            { data: 'brand', "width": "10%" },
            { data: 'productDescription', "width": "10%" },
            { data: 'unitPrice', "width": "10%" },
            { data: 'unitOfMeasurement', "width": "5%" },
            { data: 'quantityInStock', "width": "10%" },
            {
                data: 'expirationDate',
                render: function (data) {
                    // Convert the data (assumed to be in ISO format) to a Date object
                    const date = new Date(data);

                    // Check if the expiration date is in the past
                    if (date < new Date()) {
                        return "Expired";
                    }

                    // Extract year, month, and day components
                    const year = date.getFullYear();
                    const month = String(date.getMonth() + 1).padStart(2, '0'); // Month is zero-indexed
                    const day = String(date.getDate()).padStart(2, '0');

                    // Construct the formatted date string in yyyy-mm-dd format
                    const formattedDate = `${year}-${month}-${day}`;

                    return formattedDate;
                },
                "width": "10%"
            },
            {
                data: 'lastUnitPriceUpdated',
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
                data: 'lastQuantityInStockUpdated',
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
                data: 'productID', // Assuming productID is accessible in your data
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/admin/product/upsertproduct?productID=${data}" class="btn btn-info mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
                            <a href="/admin/product/deleteproduct?productID=${data}" class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i>Delete</a>
                        </div>
                    `
                },
                "width": "20%"
            }
        ]
    });
}
