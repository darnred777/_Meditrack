// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const toggler = document.querySelector(".btn");
toggler.addEventListener("click", function () {
    document.querySelector("#sidebar").classList.toggle("collapsed");
});

//Admin Side
$(document).ready(function () {
    $('#supplierDropdown').change(function () {
        var supplierId = $(this).val();
        fetchLocationAddress(supplierId);
    });

    function fetchLocationAddress(supplierId) {
        if (!supplierId) {
            $('#location').val(''); // Clear the location field if no supplier is selected
            return;
        }

        $.ajax({
            url: '/Admin/PRTransaction/GetSupplierLocation',
            type: 'GET',
            data: { supplierId: supplierId },
            success: function (data) {
                // Set the LocationID value to a hidden input for submission
                $('#locationId').val(data);

                // Fetch and display the LocationAddress for the selected supplier
                fetchLocation(data); // Assuming this fetches LocationAddress based on LocationID
            },
            error: function (xhr, status, error) {
                console.error("Error fetching location:", error);
            }
        });
    }

    function fetchLocation(locationId) {
        // Make an AJAX request to retrieve the LocationAddress based on LocationID
        $.ajax({
            url: '/Admin/PRTransaction/GetLocationAddress',
            type: 'GET',
            data: { locationId: locationId },
            success: function (address) {
                // Display the LocationAddress in the visible input field
                $('#location').val(address);
            },
            error: function (xhr, status, error) {
                console.error("Error fetching location address:", error);
            }
        });
    }
});

$(document).ready(function () {
    $('#supplierDropdown').change(function () {
        var supplierId = $(this).val();
        fetchLocationAddress(supplierId);
    });

    function fetchLocationAddress(supplierId) {
        if (!supplierId) {
            $('#location').val(''); // Clear the location field if no supplier is selected
            return;
        }

        $.ajax({
            url: '/Admin/Monitoring/GetSupplierLocation',
            type: 'GET',
            data: { supplierId: supplierId },
            success: function (data) {
                // Set the LocationID value to a hidden input for submission
                $('#locationId').val(data);

                // Fetch and display the LocationAddress for the selected supplier
                fetchLocation(data); // Assuming this fetches LocationAddress based on LocationID
            },
            error: function (xhr, status, error) {
                console.error("Error fetching location:", error);
            }
        });
    }

    function fetchLocation(locationId) {
        // Make an AJAX request to retrieve the LocationAddress based on LocationID
        $.ajax({
            url: '/Admin/Monitoring/GetLocationAddress',
            type: 'GET',
            data: { locationId: locationId },
            success: function (address) {
                // Display the LocationAddress in the visible input field
                $('#location').val(address);
            },
            error: function (xhr, status, error) {
                console.error("Error fetching location address:", error);
            }
        });
    }
});



//Admin side
document.addEventListener("DOMContentLoaded", function () {
    const productDropdown = document.getElementById('productDropdown');
    const unitPriceInput = document.getElementById('unitPrice');

    productDropdown.addEventListener('change', function () {
        const productId = this.value;
        fetchUnitPrice(productId);
    });

    function fetchUnitPrice(productId) {
        if (!productId) {
            unitPriceInput.value = ''; // Clear the input if no product is selected
            return;
        }

        $.ajax({
            url: '/Admin/PRTransaction/GetUnitPrice',
            type: 'GET',
            data: { productId: productId },
            success: function (data) {
                unitPriceInput.value = data; // directly use the data
            },
            error: function (xhr, status, error) {
                console.error("Error fetching unit price:", error);
                //alert('Could not fetch the unit price for the selected product.');
            }
        });
    }
});

//Admin side
document.addEventListener("DOMContentLoaded", function () {
    const productDropdown = document.getElementById('productDropdown');
    const unitOfMeasurementInput = document.getElementById('unitOfMeasurement');

    productDropdown.addEventListener('change', function () {
        const productId = this.value;
        fetchUnitOfMeasurement(productId);
    });

    function fetchUnitOfMeasurement(productId) {
        if (!productId) {
            unitOfMeasurementInput.value = ''; // Clear the input if no product is selected
            return;
        }

        $.ajax({
            url: '/Admin/PRTransaction/GetUnitOfMeasurement',
            type: 'GET',
            data: { productId: productId },
            success: function (data) {
                unitOfMeasurementInput.value = data; // directly use the data
            },
            error: function (xhr, status, error) {
                console.error("Error fetching unit of measurement:", error);
                //alert('Could not fetch the unit price for the selected product.');
            }
        });
    }
});


//Inventory Officer Side
$(document).ready(function () {
    $('#supplierDropdown').change(function () {
        var supplierId = $(this).val();
        fetchLocationAddress(supplierId);
    });

    function fetchLocationAddress(supplierId) {
        if (!supplierId) {
            $('#location').val(''); // Clear the location field if no supplier is selected
            return;
        }

        $.ajax({
            url: '/InventoryOfficer/PRTransaction/GetSupplierLocation',
            type: 'GET',
            data: { supplierId: supplierId },
            success: function (data) {
                // Set the LocationID value to a hidden input for submission
                $('#locationId').val(data);

                // Fetch and display the LocationAddress for the selected supplier
                fetchLocation(data); // Assuming this fetches LocationAddress based on LocationID
            },
            error: function (xhr, status, error) {
                console.error("Error fetching location:", error);
            }
        });
    }

    function fetchLocation(locationId) {
        // Make an AJAX request to retrieve the LocationAddress based on LocationID
        $.ajax({
            url: '/InventoryOfficer/PRTransaction/GetLocationAddress',
            type: 'GET',
            data: { locationId: locationId },
            success: function (address) {
                // Display the LocationAddress in the visible input field
                $('#location').val(address);
            },
            error: function (xhr, status, error) {
                console.error("Error fetching location address:", error);
            }
        });
    }
});

$(document).ready(function () {
    $('#supplierDropdown').change(function () {
        var supplierId = $(this).val();
        fetchLocationAddress(supplierId);
    });

    function fetchLocationAddress(supplierId) {
        if (!supplierId) {
            $('#location').val(''); // Clear the location field if no supplier is selected
            return;
        }

        $.ajax({
            url: '/InventoryOfficer/Monitoring/GetSupplierLocation',
            type: 'GET',
            data: { supplierId: supplierId },
            success: function (data) {
                // Set the LocationID value to a hidden input for submission
                $('#locationId').val(data);

                // Fetch and display the LocationAddress for the selected supplier
                fetchLocation(data); // Assuming this fetches LocationAddress based on LocationID
            },
            error: function (xhr, status, error) {
                console.error("Error fetching location:", error);
            }
        });
    }

    function fetchLocation(locationId) {
        // Make an AJAX request to retrieve the LocationAddress based on LocationID
        $.ajax({
            url: '/InventoryOfficer/Monitoring/GetLocationAddress',
            type: 'GET',
            data: { locationId: locationId },
            success: function (address) {
                // Display the LocationAddress in the visible input field
                $('#location').val(address);
            },
            error: function (xhr, status, error) {
                console.error("Error fetching location address:", error);
            }
        });
    }
});

//Inventory Officer side
document.addEventListener("DOMContentLoaded", function () {
    const productDropdown = document.getElementById('productDropdown');
    const unitPriceInput = document.getElementById('unitPrice');

    productDropdown.addEventListener('change', function () {
        const productId = this.value;
        fetchUnitPrice(productId);
    });

    function fetchUnitPrice(productId) {
        if (!productId) {
            unitPriceInput.value = ''; // Clear the input if no product is selected
            return;
        }

        $.ajax({
            url: '/InventoryOfficer/PRTransaction/GetUnitPrice',
            type: 'GET',
            data: { productId: productId },
            success: function (data) {
                unitPriceInput.value = data; // directly use the data
            },
            error: function (xhr, status, error) {
                console.error("Error fetching unit price:", error);
                //alert('Could not fetch the unit price for the selected product.');
            }
        });
    }
});

//Inventory Officer side
document.addEventListener("DOMContentLoaded", function () {
    const productDropdown = document.getElementById('productDropdown');
    const unitOfMeasurementInput = document.getElementById('unitOfMeasurement');

    productDropdown.addEventListener('change', function () {
        const productId = this.value;
        fetchUnitOfMeasurement(productId);
    });

    function fetchUnitOfMeasurement(productId) {
        if (!productId) {
            unitOfMeasurementInput.value = ''; // Clear the input if no product is selected
            return;
        }

        $.ajax({
            url: '/InventoryOfficer/PRTransaction/GetUnitOfMeasurement',
            type: 'GET',
            data: { productId: productId },
            success: function (data) {
                unitOfMeasurementInput.value = data; // directly use the data
            },
            error: function (xhr, status, error) {
                console.error("Error fetching unit of measurement:", error);
                //alert('Could not fetch the unit price for the selected product.');
            }
        });
    }
});

// Function to handle form submission and validate quantity change
document.querySelector('form').addEventListener('submit', function (event) {
    var quantityChangeInput = document.getElementById('quantityChange');
    var quantityChange = parseInt(quantityChangeInput.value);

    // Check if quantity change is negative
    if (quantityChange < 0) {
        event.preventDefault(); // Prevent form submission
        var validationMessage = document.querySelector('#quantityChange + .text-danger');
        validationMessage.textContent = 'Quantity change cannot be negative.';
        return;
    }

    // Reset validation message if no error
    var validationMessage = document.querySelector('#quantityChange + .text-danger');
    validationMessage.textContent = '';
});

// Attach event listeners to the operation dropdown and quantityChange input
document.getElementById("operation").addEventListener("change", validateQuantityChange);
document.getElementById("quantityChange").addEventListener("input", validateQuantityChange);

function validateQuantityChange() {
    var operation = document.getElementById("operation").value;
    var quantityChange = parseInt(document.getElementById("quantityChange").value);
    var currentQuantityInStock = parseInt("@Model.Product.QuantityInStock"); // Retrieve current QuantityInStock from Razor model

    if (operation === "Withdraw" && (quantityChange > currentQuantityInStock || quantityChange < 0)) {
        // Display error message if withdrawing more than available QuantityInStock or entering a negative quantity
        document.querySelector("span[data-valmsg-for='QuantityChange']").innerHTML = "Invalid quantity for withdrawal";
        document.querySelector("span[data-valmsg-for='QuantityChange']").style.display = "block";
        document.getElementById("quantityChange").value = ""; // Clear the input value
    } else {
        // Hide error message if valid or operation is "Deposit"
        document.querySelector("span[data-valmsg-for='QuantityChange']").innerHTML = "";
        document.querySelector("span[data-valmsg-for='QuantityChange']").style.display = "none";
    }
}


// JavaScript to update UnitPrice based on UnitOfMeasurement selection
$(document).ready(function () {
    $('#Product_UnitOfMeasurement').change(function () {
        var selectedUnit = $(this).val();
        var unitPrices = {
            PC: 700,
            BOX: 600,
            PACKAGE: 500,
            VIAL: 400,
            TUBE: 300,
            MILILITER: 200
        };
        $('#unitPrice').val(unitPrices[selectedUnit]);
    });
});

// JavaScript to update UnitPrice based on UnitOfMeasurement selection
$(document).ready(function () {
    $('#unitOfMeasurement').change(function () {
        var selectedPrice = $(this).find(':selected').data('price');
        $('#unitPrice').val(selectedPrice);
    });
});