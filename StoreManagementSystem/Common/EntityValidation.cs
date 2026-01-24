namespace StoreManagementSystem.Common
{
    public static class EntityValidation
    {
        //Category
        public const int CategoryDescriptionMaxLength = 200;

        //Customer
        public const int CustomerFirstNameMaxLength = 100;
        public const int CustomerLastNameMaxLength = 100;
        public const string CustomerEmailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        public const string CustomerPhoneNumberRegex = @"^\+?[1-9]\d{1,14}$";

        //Item
        public const int ItemNameMaxLength = 150;
        public const string ItemPriceType = "DECIMAL(5,2)";
    }
}
