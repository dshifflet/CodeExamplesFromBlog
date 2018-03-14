using LinqToDB.Mapping;

namespace Northwind.Models
{
    [Table("PRODUCTS", Schema = "NORTHWIND")]
    public class Product
    {
        [PrimaryKey, Identity]
        [Column(Name = "PRODUCT_ID"), NotNull]
        public int Id { get; set; }
        
        [Column(Name = "PRODUCT_NAME"), NotNull]
        public string ProductName { get; set; }

        [Column(Name = "SUPPLIER_ID"), NotNull]
        public int SupplierId { get; set; }

        [Column(Name = "CATEGORY_ID"), NotNull]
        public int CategoryId { get; set; }

        [Column(Name = "QUANTITY_PER_UNIT"), Nullable]
        public string QuantityPerUnit { get; set; }

        [Column(Name = "UNIT_PRICE"), NotNull]
        public double UnitPrice { get; set; }

        [Column(Name = "UNITS_IN_STOCK"), NotNull]
        public int UnitsInStock { get; set; }

        [Column(Name = "UNITS_ON_ORDER"), NotNull]
        public int UnitsOnOrder { get; set; }

        [Column(Name = "REORDER_LEVEL"), NotNull]
        public int ReorderLevel { get; set; }

        [Column(Name = "DISCONTINUED"), NotNull]
        public string Discontinued { get; set; }
    }

}
