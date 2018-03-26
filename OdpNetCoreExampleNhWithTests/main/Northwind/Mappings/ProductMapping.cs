using NHibernate.Mapping.ByCode.Conformist;
using Northwind.Models;

namespace Northwind.Mappings
{

    //NHIBERNATE MAPPING
    internal class ProductsMapping : ClassMapping<Product>
    {
        public ProductsMapping()
        {
            Schema("NORTHWIND");
            Table("PRODUCTS");
            Lazy(false);
            Id(prop => prop.Id, map =>
            {
                map.Column("PRODUCT_ID");
                //map.Generator(Generators.Sequence, gmap => gmap.Params(new {sequence = "PRODUCT_ID_SEQ"}));
            });
            Property(prop => prop.ProductName, map => map.Column("PRODUCT_NAME"));
            Property(prop => prop.SupplierId, map => map.Column("SUPPLIER_ID"));
            Property(prop => prop.CategoryId, map => map.Column("CATEGORY_ID"));
            Property(prop => prop.QuantityPerUnit, map => map.Column("QUANTITY_PER_UNIT"));
            Property(prop => prop.UnitPrice, map => map.Column("UNIT_PRICE"));
            Property(prop => prop.UnitsInStock, map => map.Column("UNITS_IN_STOCK"));
            Property(prop => prop.UnitsOnOrder, map => map.Column("UNITS_ON_ORDER"));
            Property(prop => prop.ReorderLevel, map => map.Column("REORDER_LEVEL"));
            Property(prop => prop.Discontinued, map => map.Column("DISCONTINUED"));
        }
    }
}
