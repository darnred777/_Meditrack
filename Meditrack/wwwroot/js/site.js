// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const toggler = document.querySelector(".btn");
toggler.addEventListener("click", function () {
    document.querySelector("#sidebar").classList.toggle("collapsed");
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