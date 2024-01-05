USE [master]
GO

IF DB_ID('BOOKSHOP') IS NOT NULL
DROP DATABASE BOOKSHOP

/****** Object:  Database [BOOKSHOP]    Script Date: 1/4/2024 7:21:21 PM ******/
CREATE DATABASE [BOOKSHOP]
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
/****** Object:  Table [dbo].[Category]    Script Date: 1/4/2024 7:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [varchar](10) NOT NULL,
	[CategoryName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 1/4/2024 7:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[ID] [varchar](10) NOT NULL,
	[CustomerName] [nvarchar](100) NULL,
	[TelephoneNumber] [nchar](10) NULL,
	[Address] [nvarchar](200) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 1/4/2024 7:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [varchar](10) NOT NULL,
	[OrderDate] [date] NULL,
	[CustomerID] [varchar](10) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 1/4/2024 7:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderID] [varchar](10) NOT NULL,
	[ProductID] [varchar](10) NOT NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 1/4/2024 7:21:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [varchar](10) NOT NULL,
	[ProductName] [nvarchar](100) NULL,
	[Author] [nvarchar](100) NULL,
	[PublishYear] [int] NULL,
	[Publisher] [nvarchar](100) NULL,
	[CostPrice] [int] NULL,
	[SellingPrice] [int] NULL,
	[CategoryID] [varchar](10) NULL,
	[Quantity] [int] NULL,
	[ImagePath] [nvarchar](100) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
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

USE BOOKSHOP
GO

INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'PTBT', N'Phát triển bản thân')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'VHCM', N'Văn học cách mạng')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'VHHT', N'Văn học hiện thực')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'VHNN', N'Văn học nước ngoài')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'VHTCM', N'Văn học thời kỳ chống Mỹ')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'VHTM', N'Văn học tuổi mới lớn')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'VHTN', N'Văn học thiếu nhi')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (N'VHVN', N'Văn học Việt Nam')
GO

INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES (N'C1', N'Customer 1', N'1111111111', N'Address 1')
INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES (N'C2', N'Customer 2', N'2222222222', N'Address 2')
INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES (N'C3', N'Customer 3', N'3333333333', N'Address 3')
INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES (N'C4', N'Customer 4', N'4444444444', N'Address 4')
INSERT [dbo].[Customer] ([ID], [CustomerName], [TelephoneNumber], [Address]) VALUES (N'C5', N'Customer 5', N'5555555555', N'Address 5')
GO


INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'SACH001', N'Đắc nhân tâm', N'Dale Carnegie', 1936, N'NXB Trẻ', 100000, 150000, N'PTBT', 100, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'SACH002', N'Nhà giả kim', N'Paulo Coelho', 1988, N'NXB Văn học', 150000, 200000, N'VHNN', 50, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'SACH003', N'Harry Potter và hòn đá phù thủy', N'J.K. Rowling', 1997, N'NXB Trẻ', 250000, 300000, N'VHTN', 150, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'SACH004', N'Người Hobbit', N'J.R.R. Tolkien', 1937, N'NXB Trẻ', 300000, 350000, N'VHTN', 100, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'SACH005', N'Truyện Kiều', N'Nguyễn Du', 1820, N'NXB Văn học', 200000, 250000, N'VHVN', 50, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'SACH006', N'Lục Vân Tiên', N'Nguyễn Đình Chiểu', 1805, N'NXB Văn học', 150000, 200000, N'VHVN', 100, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'SACH007', N'Chinh phụ ngâm', N'Đặng Trần Côn', 1746, N'NXB Văn học', 100000, 150000, N'VHVN', 50, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'SACH008', N'Tắt đèn', N'Ngô Tất Tố', 1938, N'NXB Văn học', 200000, 250000, N'VHHT', 100, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'SACH009', N'Sống mòn', N'Nam Cao', 1942, N'NXB Văn học', 150000, 200000, N'VHHT', 50, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'SACH011', N'Rừng xà nu', N'Nguyễn Trung Thành', 1965, N'NXB Văn học', 200000, 250000, N'VHCM', 100, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'SACH012', N'Đất nước', N'Nguyễn Khoa Điềm', 1971, N'NXB Văn học', 150000, 200000, N'VHTCM', 50, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'SACH013', N'Mắt biếc', N'Nguyễn Nhật Ánh', 1990, N'NXB Trẻ', 100000, 150000, N'VHTM', 100, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'SACH014', N'Cô gái đến từ hôm qua', N'Nguyễn Nhật Ánh', 2000, N'NXB Trẻ', 200000, 250000, N'VHTM', 150, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'SACH015', N'Hoa cỏ may', N'Nguyễn Nhật Ánh', 1995, N'NXB Trẻ', 250000, 300000, N'VHTM', 100, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Author], [PublishYear], [Publisher], [CostPrice], [SellingPrice], [CategoryID], [Quantity], [ImagePath]) VALUES (N'SACH016', N'Tôi thấy hoa vàng trên cỏ xanh', N'Nguyễn Nhật Ánh', 2000, N'NXB Trẻ', 300000, 350000, N'VHTM', 150, NULL)
GO

INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O1', CAST(N'2023-01-01' AS Date), N'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O13', CAST(N'2023-01-15' AS Date), N'C2')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O14', CAST(N'2023-02-28' AS Date), N'C4')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O15', CAST(N'2023-03-05' AS Date), N'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O16', CAST(N'2023-04-18' AS Date), N'C3')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O17', CAST(N'2023-05-22' AS Date), N'C5')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O18', CAST(N'2023-06-10' AS Date), N'C2')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O2', CAST(N'2023-02-15' AS Date), N'C2')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O3', CAST(N'2023-03-10' AS Date), N'C3')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O4', CAST(N'2023-04-22' AS Date), N'C4')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O5', CAST(N'2023-05-05' AS Date), N'C5')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O6', CAST(N'2023-06-13' AS Date), N'C1')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O7', CAST(N'2023-07-19' AS Date), N'C3')
INSERT [dbo].[Order] ([ID], [OrderDate], [CustomerID]) VALUES (N'O8', CAST(N'2023-08-28' AS Date), N'C2')
GO

INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O1', N'SACH001', 2)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O1', N'SACH002', 1)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O13', N'SACH003', 2)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O13', N'SACH005', 1)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O14', N'SACH001', 3)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O14', N'SACH006', 2)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O15', N'SACH004', 1)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O15', N'SACH009', 2)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O16', N'SACH002', 2)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O16', N'SACH007', 1)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O17', N'SACH005', 3)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O17', N'SACH008', 2)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O18', N'SACH001', 1)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O18', N'SACH003', 2)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O2', N'SACH003', 3)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O3', N'SACH004', 2)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O3', N'SACH005', 1)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O3', N'SACH006', 1)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O4', N'SACH007', 3)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O5', N'SACH008', 2)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O5', N'SACH009', 1)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O6', N'SACH001', 1)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O6', N'SACH005', 2)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O7', N'SACH003', 1)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O7', N'SACH006', 2)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O7', N'SACH008', 1)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity]) VALUES (N'O8', N'SACH002', 3)
GO
