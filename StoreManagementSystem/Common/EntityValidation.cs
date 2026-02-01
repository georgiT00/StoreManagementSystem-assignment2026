namespace StoreManagementSystem.Common
{
    public static class EntityValidation
    {
        //Category
        public const int CategoryDescriptionMaxLength = 200;
        public const int CategoryNameMaxLength = 30;

        //Supplier
        public const int SupplierNameMaxLength = 100;

        //User
        public const int UserFirstNameMaxLength = 100;
        public const int UserLastNameMaxLength = 100;
        public const int UserNameMaxLength = 50;
        public const int UserPasswordMinLength = 6;
        public const int UserPasswordMaxLength = 20;
        public const int UserEmailMaxLength = 150;

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
