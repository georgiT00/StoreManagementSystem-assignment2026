$(document).ready(function () {
    $(document).on('click', '#product-details', function (e) {
        e.preventDefault();
        e.stopPropagation();

        const id = $(this).data("product-id");

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
    $(document).on('click', '#add-to-cart', function (e) {
        e.preventDefault();
        e.stopPropagation();

        const productId = $(this).data("product-id");

        $.ajax({
            url: `/Product/AddToCart/${productId}`,
            type: 'POST',
            data: { id: productId },
            success: function () {
                const productName = $(`[data-product-id="${productId}"]`).data("product-name");
                $('#successMsg').text(`Product '${productName}' added to cart successfully!`);
                $('#success').fadeIn();

                setTimeout(function () {
                    $('#success').fadeOut();
                }, 3000);
            },
            error: function () {
                $('#errorMsg').text(`Product does not exist!`)
                $('#error').fadeIn();

                setTimeout(function () {
                    $('#error').fadeOut();
                }, 3000);
            }
        });
    });
});