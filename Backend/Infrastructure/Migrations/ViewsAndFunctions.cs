using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Migrations
{
    public class ViewsAndFunctions
    {
        private readonly string connectionString;

        public ViewsAndFunctions(IConfiguration config)
        {
            this.connectionString = config.GetConnectionString("DefaultConnection");
        }

        public void CreateViewsAndFunctions()
        {
            var imageByProductIdFunctionSql = @"CREATE OR ALTER Function GetImageByProductId (@productId INT)
                                            RETURNS TABLE
                                            AS
                                            RETURN (SELECT i.Id, i.PublicId
                                            FROM Product AS p 
                                            JOIN Image AS i on p.Id = i.ProductId
                                            WHERE p.Id= @productId)";

            var indexProductsViewSql = @"CREATE OR ALTER VIEW IndexProducts AS
                                        SELECT p.Id, p.Name, p.Description, c.Name AS Category, p.Price, p.SaleQty, i.PublicId AS Image
                                        FROM 
                                        Product AS p 
                                        JOIN Category AS c ON p.CategoryId = c.Id 
                                        LEFT JOIN Image AS i ON i.ProductId = p.Id
                                        WHERE p.SaleQty > 0";

            var inventoryProductViewSql = @"CREATE OR ALTER VIEW InventoryProducts AS
                                            SELECT p.Id, p.Code, p.Name, p.Price, p.Description, c.Name AS Category, p.CategoryId, p.SaleQty, p.CombinedQty, i.PublicId AS Image, l.Name AS Location, p.LocationId
                                            FROM
                                            [Product] AS p
                                            JOIN [Category] AS c ON p.CategoryId = c.Id
                                            JOIN [Location] AS l ON p.LocationId = l.Id
                                            LEFT JOIN [Image] AS i ON i.ProductId = p.Id";


            var pendingOrdersViewSql = @"CREATE OR ALTER VIEW PendingOrders AS
                                            SELECT Id, ProductCode, Qty, Price, OrderedBy, Date
                                            FROM [Order]
                                            WHERE Status = 0";

            var myOrdersFunctionSql = @"CREATE OR ALTER Function GetMyOrdersViewSql (@email VARCHAR(50))
                                        RETURNS TABLE
                                        AS
                                        RETURN (SELECT Id, ProductName, Qty, Price, Date, Status
		                                FROM [Order]
		                                WHERE OrderedBy = @email)";

            var productPendingOrdersFunctionSql = @"CREATE OR ALTER Function GetProductPendingOrdersCount (@productId INT)
                                                    RETURNS INT
                                                    AS
                                                    BEGIN
	                                                    RETURN (SELECT COUNT(*) FROM [Order]
	                                                    WHERE Status = 0 AND ProductId = @productId)
                                                    END";

            List<string> viewsAndFunctions = new List<string>() { imageByProductIdFunctionSql, indexProductsViewSql, inventoryProductViewSql, pendingOrdersViewSql, myOrdersFunctionSql, productPendingOrdersFunctionSql };

            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            connectionStringBuilder.TrustServerCertificate = true;
            using var connection = new SqlConnection(connectionStringBuilder.ConnectionString);
            foreach (var view in viewsAndFunctions)
            {
                connection.Execute(view);
            }
        }
    }
}
