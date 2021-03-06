USE [dbforwebproject]
GO
/****** Object:  Table [dbo].[cthd]    Script Date: 12/30/2020 2:29:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cthd](
	[macthd] [int] IDENTITY(1,1) NOT NULL,
	[mahd] [int] NULL,
	[masp] [int] NULL,
	[soluong] [int] NULL,
	[thanhtien] [float] NULL,
	[tensp] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[macthd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[hoadon]    Script Date: 12/30/2020 2:29:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hoadon](
	[mahd] [int] IDENTITY(1,1) NOT NULL,
	[tendangnhap] [varchar](50) NULL,
	[tien] [float] NULL,
	[sdt] [varchar](11) NULL,
	[diachi] [nvarchar](100) NULL,
	[hoten] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[mahd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sanpham]    Script Date: 12/30/2020 2:29:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sanpham](
	[masp] [int] IDENTITY(1,1) NOT NULL,
	[loai] [int] NULL,
	[tensp] [nvarchar](40) NULL,
	[gia] [float] NULL,
	[hinhanh] [nvarchar](40) NULL,
	[mota] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[masp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[taikhoan]    Script Date: 12/30/2020 2:29:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[taikhoan](
	[tendangnhap] [varchar](50) NOT NULL,
	[hoten] [nvarchar](50) NOT NULL,
	[matkhau] [varchar](20) NOT NULL,
	[sdt] [varchar](11) NOT NULL,
	[diachi] [nvarchar](100) NOT NULL,
	[role] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[tendangnhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[sanpham] ADD  DEFAULT (NULL) FOR [loai]
GO
ALTER TABLE [dbo].[sanpham] ADD  DEFAULT (NULL) FOR [tensp]
GO
ALTER TABLE [dbo].[sanpham] ADD  DEFAULT (NULL) FOR [gia]
GO
ALTER TABLE [dbo].[sanpham] ADD  DEFAULT (NULL) FOR [hinhanh]
GO
ALTER TABLE [dbo].[sanpham] ADD  DEFAULT (NULL) FOR [mota]
GO
ALTER TABLE [dbo].[cthd]  WITH CHECK ADD  CONSTRAINT [fk_cthd_hd] FOREIGN KEY([mahd])
REFERENCES [dbo].[hoadon] ([mahd])
GO
ALTER TABLE [dbo].[cthd] CHECK CONSTRAINT [fk_cthd_hd]
GO
/****** Object:  StoredProcedure [dbo].[ao]    Script Date: 12/30/2020 2:29:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[ao]
as
select * from sanpham where loai='3';
GO
/****** Object:  StoredProcedure [dbo].[chanvay]    Script Date: 12/30/2020 2:29:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[chanvay]
as
select * from sanpham where loai='6';
GO
/****** Object:  StoredProcedure [dbo].[chitiet]    Script Date: 12/30/2020 2:29:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[chitiet]
	@masp_ int
as
begin
	select * from sanpham 
	where masp=@masp_
end;
GO
/****** Object:  StoredProcedure [dbo].[dangnhap]    Script Date: 12/30/2020 2:29:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[dangnhap]
	@username varchar(50),
	@password varchar(20)
as
begin
	declare @count int
	declare @res bit
	select @count = count(*) from taikhoan where tendangnhap=@username and matkhau=@password
	if @count>0
		set @res =1
	else
		set @res =0
	select @res
end
GO
/****** Object:  StoredProcedure [dbo].[giay]    Script Date: 12/30/2020 2:29:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[giay]
as
select * from sanpham where loai='2';

GO
/****** Object:  StoredProcedure [dbo].[quan]    Script Date: 12/30/2020 2:29:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[quan]
as
select * from sanpham where loai='4';
GO
/****** Object:  StoredProcedure [dbo].[themsp]    Script Date: 12/30/2020 2:29:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[themsp] 
	@loaisp int,
	@ten nvarchar(40),
	@giasp float,
	@hinhanhsp nvarchar(40),
	@motasp text
as
begin
	insert into sanpham (loai, tensp, gia, hinhanh, mota)
	values(@loaisp, @ten, @giasp, @hinhanhsp, @motasp)
end
GO
/****** Object:  StoredProcedure [dbo].[themtk]    Script Date: 12/30/2020 2:29:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[themtk] 
	@username varchar(50),
	@name nvarchar(50),
	@password varchar(20),
	@phone varchar(11),
	@address nvarchar(100),
	@role int
as
begin
	insert into taikhoan (tendangnhap, hoten, matkhau, sdt, diachi, role)
	values(@username,@name, @password, @phone, @address, @role)
end
GO
/****** Object:  StoredProcedure [dbo].[tui]    Script Date: 12/30/2020 2:29:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[tui]
as
select * from sanpham where loai='1';
GO
/****** Object:  StoredProcedure [dbo].[vay]    Script Date: 12/30/2020 2:29:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[vay]
as
select * from sanpham where loai='7';
GO
/****** Object:  StoredProcedure [dbo].[yem]    Script Date: 12/30/2020 2:29:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[yem]
as
select * from sanpham where loai='5';
GO
