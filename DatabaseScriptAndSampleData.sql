USE [master]
GO
/****** Object:  Database [BOOKSHOP]    Script Date: 11/16/2023 2:49:49 PM ******/
-- Create a new database called 'BOOKSHOP'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
	SELECT [name]
		FROM sys.databases
		WHERE [name] = N'BOOKSHOP'
)
CREATE DATABASE BOOKSHOP
 CONTAINMENT = NONE
GO

ALTER DATABASE [BOOKSHOP] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BOOKSHOP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BOOKSHOP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BOOKSHOP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BOOKSHOP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BOOKSHOP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BOOKSHOP] SET ARITHABORT OFF 
GO
ALTER DATABASE [BOOKSHOP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BOOKSHOP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BOOKSHOP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BOOKSHOP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BOOKSHOP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BOOKSHOP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BOOKSHOP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BOOKSHOP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BOOKSHOP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BOOKSHOP] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BOOKSHOP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BOOKSHOP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BOOKSHOP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BOOKSHOP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BOOKSHOP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BOOKSHOP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BOOKSHOP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BOOKSHOP] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BOOKSHOP] SET  MULTI_USER 
GO
ALTER DATABASE [BOOKSHOP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BOOKSHOP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BOOKSHOP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BOOKSHOP] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BOOKSHOP] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BOOKSHOP] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BOOKSHOP] SET QUERY_STORE = ON
GO
ALTER DATABASE [BOOKSHOP] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BOOKSHOP]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 11/16/2023 2:49:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [nchar](10) NOT NULL,
	[CategoryName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 11/16/2023 2:49:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[ID] [nchar](10) NOT NULL,
	[CustomerName] [nvarchar](100) NULL,
	[TelephoneNumber] [nchar](10) NULL,
	[Address] [nvarchar](200) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 11/16/2023 2:49:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [nchar](10) NOT NULL,
	[OrderDate] [date] NULL,
	[CustomerID] [nchar](10) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 11/16/2023 2:49:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderID] [nchar](10) NOT NULL,
	[ProductID] [nchar](10) NOT NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 11/16/2023 2:49:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [nchar](10) NOT NULL,
	[ProductName] [nvarchar](100) NULL,
	[Author] [nvarchar](100) NULL,
	[PublishYear] [int] NULL,
	[Publisher] [nvarchar](100) NULL,
	[CostPrice] [int] NULL,
	[SellingPrice] [int] NULL,
	[CategoryID] [nchar](10) NULL,
	[Quantity] [int] NULL,
	[ImagePath] [nvarchar](100) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'Cat1      ', N'Category 1')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'Cat10     ', N'Category 10')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'Cat2      ', N'Category 2')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'Cat3      ', N'Category 3')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'Cat4      ', N'Category 4')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'Cat5      ', N'Category 5')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'Cat6      ', N'Category 6')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'Cat7      ', N'Category 7')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'Cat8      ', N'Category 8')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'Cat9      ', N'Category 9')
GO
INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES (N'C1        ', N'Customer 1', N'1111111111', N'Address 1')
INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES (N'C2        ', N'Customer 2', N'2222222222', N'Address 2')
INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES (N'C3        ', N'Customer 3', N'3333333333', N'Address 3')
INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES (N'C4        ', N'Customer 4', N'4444444444', N'Address 4')
INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES (N'C5        ', N'Customer 5', N'5555555555', N'Address 5')
GO
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O1        ', CAST(N'2023-01-01' AS Date), N'C1        ')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O2        ', CAST(N'2023-01-02' AS Date), N'C2        ')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O3        ', CAST(N'2023-01-03' AS Date), N'C3        ')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O4        ', CAST(N'2023-01-04' AS Date), N'C4        ')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O5        ', CAST(N'2023-01-05' AS Date), N'C5        ')
GO
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O1        ', N'P1        ', 10)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O2        ', N'P2        ', 20)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O3        ', N'P3        ', 30)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O4        ', N'P4        ', 40)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O5        ', N'P5        ', 50)
GO
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P1        ', N'Product 1', N'Author 1', 2020, N'Publisher 1', 100, 150, N'Cat1      ', 50, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P10       ', N'Product 10', N'Author 10', 2029, N'Publisher 10', 1000, 1050, N'Cat10     ', 140, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P2        ', N'Product 2', N'Author 2', 2021, N'Publisher 2', 200, 250, N'Cat2      ', 60, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P23       ', N'Book 23', N'Author 23', 2021, N'Publisher 23', 100, 200, N'Cat1      ', 10, N'')
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P3        ', N'Product 3', N'Author 3', 2022, N'Publisher 3', 300, 350, N'Cat3      ', 70, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P4        ', N'Product 4', N'Author 4', 2023, N'Publisher 4', 400, 450, N'Cat4      ', 80, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P5        ', N'Product 5', N'Author 5', 2024, N'Publisher 5', 500, 550, N'Cat5      ', 90, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P6        ', N'Product 6', N'Author 6', 2025, N'Publisher 6', 600, 650, N'Cat6      ', 100, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P7        ', N'Product 7', N'Author 7', 2026, N'Publisher 7', 700, 750, N'Cat7      ', 110, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P8        ', N'Product 8', N'Author 8', 2027, N'Publisher 8', 800, 850, N'Cat8      ', 120, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'P9        ', N'Product 9', N'Author 9', 2028, N'Publisher 9', 900, 950, N'Cat9      ', 130, NULL)
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
USE [master]
GO
ALTER DATABASE [BOOKSHOP] SET  READ_WRITE 
GO
