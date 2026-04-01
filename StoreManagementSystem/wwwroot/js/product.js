$(document).ready(function () {
    $("#product-details").on("click", function (e) {
        e.preventDefault();
        e.stopPropagation();

        const id = $(this).attr("data-product-id");

        $.ajax({
            url: `/api/ProductApi/${id}`,
            type: 'GET',
            success: function (data) {
                $('#productDetailsModalLabel').text(data.productName);
                $('#productCategory').text(data.categoryName);
                $('#productSupplier').text(data.supplierName || 'N/A');
                $('#productQuantity').text(data.quantity);
                $('#productPrice').text(`$${data.price}`);

                $('#editProductLink').attr('href', `/Product/Edit/${data.productId}`);
                $('#deleteProductLink').attr('href', `/Product/Delete/${data.productId}`);

                $('#productDetailsModal').modal('show');
            },
            error: function () {
                alert('Failed to fetch product details. Please try again.');
            }
        });
    });
});