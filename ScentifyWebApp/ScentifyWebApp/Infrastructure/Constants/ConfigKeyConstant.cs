namespace ScentifyWebApp.Infrastructure.Constants
{
    public static class ConfigKeyConstant
    {
        #region DateTime Configuration

        public const string DATE_TIME_FULL_FORMAT = "ddd MMM dd yyyy - HH:mm:ss";

        #endregion


        public const string SQL_CONNECTION_STRING = "SQLConnectionString";
        public const string ADMIN_ACCOUNT_KEY = "AdminAccount";
        public const string STAFF_ACCOUNT_KEY = "StaffAccount";
        public const string TIME_ZONE_KEY = "Timezone";

        #region Message Notify

        public const string INF_MSG_LOGIN_SUCCESS = "Have successfully logged in!";
        public const string INF_MSG_CREATE_PRODUCT_SUCCESS = "Have successfully CREATED product!";
        public const string INF_MSG_UPDATED_PRODUCT_SUCCESS = "Have successfully UPDATED product!";
        public const string INF_MSG_DELETE_PRODUCT_SUCCESS = "Have successfully DELETED product!";
        public const string INF_MSG_CREATE_CATEGORY_SUCCESS = "Have successfully CREATED category!";
        public const string INF_MSG_CREATE_ADDON_SUCCESS = "Have successfully CREATED addon!";
        public const string INF_MSG_CREATE_STAFF_SUCCESS = "Have successfully CREATED staff!";
        public const string INF_MSG_CREATE_INVOICE_SUCCESS = "Have successfully CREATED invoice!";
        public const string INF_MSG_UPDATED_CATEGORY_SUCCESS = "Have successfully UPDATED category!";
        public const string INF_MSG_UPDATED_STAFF_SUCCESS = "Have successfully UPDATED staff!";
        public const string INF_MSG_UPDATED_ADDON_SUCCESS = "Have successfully UPDATED addon!";
        public const string INF_MSG_DELETED_CATEGORY_SUCCESS = "Have successfully DELETED category!";
        public const string INF_MSG_DELETED_ADDON_SUCCESS = "Have successfully DELETED addon!";
        public const string INF_MSG_DELETED_STAFF_SUCCESS = "Have successfully DELETED staff!";
        public const string INF_MSG_ADDED_CART_ITEM_SUCCESS = "Have successfully ADDED cart item!";
        public const string INF_MSG_UPDATED_CART_ITEM_SUCCESS = "Have successfully UPDATED cart item!";
        public const string INF_MSG_DELETED_CART_ITEM_SUCCESS = "Have successfully DELETED cart item!";
        public const string INF_MSG_DELETED_ALL_CART_ITEM_SUCCESS = "Have successfully DELETED ALL cart item!";
        public const string INF_MSG_PAYMENT_SUCCESS = "Bill payment successful!";
        public const string INF_MSG_FINISH_INVOICE_SUCCESS = "Finish invoice successful!";
        public const string INF_MSG_CANCEL_INVOICE_SUCCESS = "Cancel invoice successful!";
        public const string INF_MSG_PAY_LATER_INVOICE_SUCCESS = "Paylater invoice successful!";
        public const string INF_MSG_CHECK_IN_SUCCESS = "Check-in successful!";
        public const string INF_MSG_START_BREAK_TIME_SUCCESS = "Start break time successful!";
        public const string INF_MSG_END_BREAK_TIME_SUCCESS = "End break time successful!";
        public const string INF_MSG_CHECK_OUT_SUCCESS = "Check-out successful!";
        public const string INF_MSG_EXPORT_INVOICES_SUCCESS = "Export list invoices from {0} to {1} successful! {2}";
        public const string INF_MSG_DELETED_SELECTED_INVOICES_SUCCESS = "Deleted selected invoices successful!";
        public const string INF_MSG_DELETED_SELECTED_WAGES_SUCCESS = "Deleted selected wages successful!";
        public const string INF_MSG_EXPORT_INVOICES_NOT_FOUND = "Invoices from {0} to {1} not found";

        #endregion

        #region Error Message

        public const string ERR_MSG_SYS = "(Error)The system is failing... you can retry later. Sorry for this inconvenience";
        public const string ERR_MSG_LOGIN_FAILED = "Error User login failed...";
        public const string ERR_MSG_ADDED_CART_ITEM_FAILED = "Error Added cart item failed!";
        public const string ERR_MSG_UPDATED_CART_ITEM_FAILED = "Error Updated cart item failed!";
        public const string ERR_MSG_DELETED_CART_ITEM_FAILED = "Error Deleted cart item failed!";
        public const string ERR_MSG_DELETED_SELECTED_INVOICES_FAILED = "Error Deleted selected invoices failed!";
        public const string ERR_MSG_DELETED_SELECTED_WAGES_FAILED = "Error Deleted selected wages failed!";
        public const string ERR_MSG_THE_CART_IS_EMPTY = "Error - The cart is empty!";
        public const string ERR_MSG_LIST_ID_INVALID = "Error List Id is invalid.";
        public const string ERR_MSG_CATEGORY_INVALID = "Error Category is invalid.";
        public const string ERR_MSG_PRICE_CART_INVALID = "Error Price item is invalid."; // For product - Pls don't change msg
        public const string ERR_MSG_PRICE_INVALID = "Error Price of {0} {1} is invalid."; // For product - Pls don't change msg
        public const string ERR_MSG_COST_INVALID = "Error Cost of {0} {1} is invalid."; // For product - Pls don't change msg
        public const string ERR_MSG_STATUS_INVALID = "Error Status is invalid.";
        public const string ERR_MSG_PRODUCT_ID_INVALID = "Error Product Id is invalid.";
        public const string ERR_MSG_CATEGORY_ID_INVALID = "Error Category Id is invalid.";
        public const string ERR_MSG_ADDON_ID_INVALID = "Error Addon Id is invalid.";
        public const string ERR_MSG_CART_ITEMS_INVALID = "Error Cart Items is invalid.";
        public const string ERR_MSG_ORDER_CODE_INVALID = "Error Order Code is invalid.";
        public const string ERR_MSG_INVOICE_ID_INVALID = "Error Invoice Id is invalid.";
        public const string ERR_MSG_PAY_DATE_INVALID = "Error Pay Date is invalid.";
        public const string ERR_MSG_CART_ORDER_ID_INVALID = "Error Cart Order Id is invalid.";
        public const string ERR_MSG_CART_SHIPPING_INVALID = "Error Cart Shipping is invalid.";
        public const string ERR_MSG_CART_DISCOUNT_INVALID = "Error Cart Discount is invalid.";
        public const string ERR_MSG_CART_OTHER_COSTS_INVALID = "Error Cart Other Costs is invalid.";
        public const string ERR_MSG_FROM_DATE_INVALID = "Error From Date is invalid.";
        public const string ERR_MSG_TO_DATE_INVALID = "Error To Date is invalid.";
        public const string ERR_MSG_FROM_DATE_LATER_TO_DATE = "Error From Date was later for To Date.";
        public const string ERR_MSG_PRODUCT_NOT_FOUND = "Error Product is not found.";
        public const string ERR_MSG_CATEGORY_NOT_FOUND = "Error Category is not found.";
        public const string ERR_MSG_INVOICE_NOT_FOUND = "Error Invoice is not found.";
        public const string ERR_MSG_ADDON_NOT_FOUND = "Error Addon is not found.";
        public const string ERR_MSG_AVAILABLE_FULFILLMENTS_NOT_FOUND = "Error Available Fulfillments variable is not found.";
        public const string ERR_MSG_SOLD_BYS_NOT_FOUND = "Error Sold By variable is not found.";
        public const string ERR_MSG_AVAILABLE_STORES_NOT_FOUND = "Error Available Stores variable is not found.";
        public const string ERR_MSG_EXIST_PRODUCTS_OF_CATEGORY = "Error DELETED category {0}, due to exist products of this!";
        public const string ERR_MSG_EXIST_STAFF = "Error - Staff is not exist.";
        public const string ERR_MSG_EXIST_CHECK_IN = "Warning - You are already checked in.";
        public const string ERR_MSG_CHECK_OUT_BEFORE_CHECK_IN = "Warning - You need to check in before check out.";
        public const string ERR_MSG_CHECK_OUT_TIME_INVALID = "Error - The time you entered is invalid. Please input the date time again.";
        public const string ERR_MSG_TAP_4_TIME = "Warning - You are already checked out (tap 4 times/day).";
        public const string ERR_MSG_AT_LEAST_ONE_VARIANT = "Error - Please choose at least one variant.";
        public const string ERR_MSG_MISSING_VARIABLE = "Error Variable Inputs is missing!";
        #endregion
    }
}
