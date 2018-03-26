namespace Northwind.Models
{
    public class Product
    {
        //PRODUCTS
        public int Id { get; set; } //PRODUCT_ID
        public string ProductName { get; set; } //PRODUCT_NAME
        public int SupplierId { get; set; } //SUPPLIER_ID
        public int CategoryId { get; set; } //CATEGORY_ID
        public string QuantityPerUnit { get; set; } //QUANTITY_PER_UNIT
        public double UnitPrice { get; set; } //UNIT_PRICE
        public int UnitsInStock { get; set; } //UNITS_IN_STOCK
        public int UnitsOnOrder { get; set; } //UNITS_ON_ORDER
        public int ReorderLevel { get; set; } //REORDER_LEVEL
        public string Discontinued { get; set; } //DISCONTINUED
    }
}
