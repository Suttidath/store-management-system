using System;
using System.Data.SqlClient;

namespace StoreManagementSystem
{
    /// <summary>
    /// Central database helper for the Store Management System.
    /// Holds the single connection string and creates the schema on first run.
    /// </summary>
    public static class Database
    {
        private const string MasterConnectionString =
            @"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30";

        // Single source of truth for the connection string. Uses a named LocalDB
        // database (not a file path) so the app runs on any machine with LocalDB
        // installed, with no per-user file paths to set up.
        public const string ConnectionString =
            @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=StoreManagementDB;Integrated Security=True;Connect Timeout=30";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// Creates the database and tables used by the store system if they do
        /// not exist yet. Safe to call on every start-up.
        /// </summary>
        public static void EnsureSchema()
        {
            using (SqlConnection conn = new SqlConnection(MasterConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "IF DB_ID('StoreManagementDB') IS NULL CREATE DATABASE StoreManagementDB;", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            const string script = @"
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'users')
CREATE TABLE users (
    id            INT PRIMARY KEY IDENTITY(1,1),
    username      VARCHAR(MAX) NULL,
    password      VARCHAR(MAX) NULL,
    date_register DATE NULL
);

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'categories')
CREATE TABLE categories (
    id           INT PRIMARY KEY IDENTITY(1,1),
    name         NVARCHAR(200) NOT NULL,
    description  NVARCHAR(MAX) NULL,
    date_created DATETIME NOT NULL DEFAULT GETDATE()
);

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'products')
CREATE TABLE products (
    id           INT PRIMARY KEY IDENTITY(1,1),
    product_code NVARCHAR(100) NULL,
    name         NVARCHAR(200) NOT NULL,
    category_id  INT NULL,
    brand        NVARCHAR(200) NULL,
    description  NVARCHAR(MAX) NULL,
    quantity     INT NOT NULL DEFAULT 0,
    date_created DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_products_categories FOREIGN KEY (category_id)
        REFERENCES categories(id)
);

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'stock_movements')
CREATE TABLE stock_movements (
    id            INT PRIMARY KEY IDENTITY(1,1),
    product_id    INT NOT NULL,
    movement_type NVARCHAR(10) NOT NULL,   -- 'IN' or 'OUT'
    quantity      INT NOT NULL,
    department    NVARCHAR(200) NULL,      -- which department used it
    used_for      NVARCHAR(MAX) NULL,      -- where / what it was used for
    note          NVARCHAR(MAX) NULL,
    moved_by      NVARCHAR(200) NULL,
    date_moved    DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_movements_products FOREIGN KEY (product_id)
        REFERENCES products(id)
);";

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(script, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    /// <summary>
    /// Holds information about the currently logged-in user for the session.
    /// </summary>
    public static class Session
    {
        public static string CurrentUser = "User";
    }
}
