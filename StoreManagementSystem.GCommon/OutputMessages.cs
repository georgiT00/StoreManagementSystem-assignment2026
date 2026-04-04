namespace StoreManagementSystem.GCommon
{
    public static class OutputMessages
    {
        public static class Product
        {
            public const string ProductAddedMsg = "Product - {0} added successfully.";
            public const string ProductAddedToCartMsg = "Product - {0} added to Cart successfully.";
            public const string ProductEditedMsg = "Product - {0} edited successfully.";
            public const string ProductDeletedMsg = "Product - {0} deleted successfully.";

            public const string ProductNotFoundMsg = "Product with id {0} does not exist. Try again.";
            public const string ProductCategoryNotFoundMsg = "Selected Category does not exist.";
            public const string ProductSupplierNotFoundMsg = "Selected Supplier does not exist.";
        }

        public static class Cart
        {
            public const string ProductAlreadyInCartMsg = "This product is already in your cart.";
            public const string ProductNotInCartMsg = "This product is not in your cart.";
            public const string ProductRemoveFromCartErrorMsg = "An error occurred while trying to remove the product from your cart. Try again.";
            public const string PlaceOrderErrorMsg = "An error occurred while trying to place your order. Try again.";
        }

        public static class Role
        {
            public const string RoleAddErrorMsg = "An error occurred while trying to add the role - {0}.";
        }

        public static class AdminUser
        {
            public const string AdminUserNameNotFoundMsg = "Admin username not found in app settings.";

            public const string AdminUserPasswordNotMatchMsg = "Incorrect password for Admin user.";

            public const string AdminUserAddErrorMsg = "An error occurred while trying to add the admin user with username - {0}.";
        }
    }
}
