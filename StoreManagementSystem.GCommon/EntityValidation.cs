namespace StoreManagementSystem.GCommon
{
    public static class EntityValidation
    {
        //Category
        public const int CategoryDescriptionMaxLength = 200;
        public const int CategoryNameMaxLength = 30;

        //Supplier
        public const int SupplierNameMaxLength = 100;

        //User
        public const int UserFirstNameMinLength = 3;
        public const int UserLastNameMinLength = 3;
        public const int UserFirstNameMaxLength = 100;
        public const int UserLastNameMaxLength = 100;

        //ProductItem
        public const int ItemNameMaxLength = 150;
        public const string ItemPriceType = "DECIMAL(5,2)";

        //Order
        public const string TotalAmountType = "DECIMAL(7,2)";

        //OrderItem
        public const string UnitPriceType = "DECIMAL(5,2)";

        //Payment
        public const string AmountType = "DECIMAL(7,2)";
    }
}
