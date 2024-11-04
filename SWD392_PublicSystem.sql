-- Tạo cơ sở dữ liệu
CREATE DATABASE SWD392_PublicSystem;
GO

-- Sử dụng cơ sở dữ liệu vừa tạo
USE SWD392_PublicSystem;
GO

-- Tạo bảng Department
CREATE TABLE Department (
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100)
);

-- Tạo bảng Role
CREATE TABLE [Role] (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) -- role Công Dân, role Cán Bộ
);

-- Tạo bảng ProcessingAgency
CREATE TABLE ProcessingAgency (
    AgencyID INT PRIMARY KEY IDENTITY(1,1),
    [Level] INT, -- 1: cấp xã, 2: cấp huyện
    [Type] INT, --  
    [District] NVARCHAR(80),
    [Name] NVARCHAR(256)
);

-- Tạo bảng PublicService
CREATE TABLE PublicService (
    ServiceID INT PRIMARY KEY IDENTITY(1,1),
    ServiceName NVARCHAR(200),
    [Description] NVARCHAR(1024),
    ServiceFee DECIMAL(18, 2),
    DateCreated DATE,
    DateDeleted DATE,
    isDeleted INT,-- 0 là false, 1 là true
    [ProcessedBy] INT,
    DepartmentID INT REFERENCES Department(DepartmentID)
);

-- Tạo bảng User
CREATE TABLE [User] (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(30),
    Password VARCHAR(30),
    Email VARCHAR(320),
    RoleID INT REFERENCES [Role](RoleID)
);

-- Tạo bảng Application với các khóa ngoại cần thiết
CREATE TABLE [Application] (
    ApplicationID INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(256),
    [Status] INT, -- 0 là Pending, 1 là Success, 2 là Fail
    [Type] INT, -- 
    PaymentAmount DECIMAL(18, 2),
    [SubmissionDate] DATE,
    Note NVARCHAR(512),
    UserID INT REFERENCES [User](UserID),
    ServiceID INT REFERENCES PublicService(ServiceID),
    AgencyID INT REFERENCES ProcessingAgency(AgencyID)
);


CREATE TABLE [Document](
	DocumentID INT PRIMARY KEY IDENTITY(1,1),
	[Name] nvarchar(256),
	[Path] nvarchar(256),
	[Type] int, -- 
	[CreatedBy] int references [User](UserID),
);

CREATE TABLE ApplicationDocument(
	[ApplicationID] int references [Application](ApplicationID),
	[DocumentID] int references [Document](DocumentID),
	AttachDate date,
	[Description] nvarchar(512)
	primary key ([ApplicationID], [DocumentID])
);

-- Bảng Department
INSERT INTO Department (DepartmentName) VALUES
(N'Sở Y tế'),
(N'Sở Giáo dục và Đào tạo'),
(N'Sở Giao thông Vận tải'),
(N'Sở Tài nguyên và Môi trường');

-- Bảng Role
INSERT INTO [Role] (RoleName) VALUES
(N'Công Dân'),
(N'Cán Bộ');

-- Bảng ProcessingAgency
-- Level: cấp độ: 1: Xã, 2: Quận/Huyện
-- Type: tạm thời bỏ qua
INSERT INTO ProcessingAgency ([Level], [Type], [District], [Name]) VALUES
(2, 1, N'Quận Hoàn Kiếm', N'Ủy ban nhân dân phường Tràng Tiền'),
(2, 1, N'Quận Ba Đình', N'Ủy ban nhân dân quận Ba Đình'),
(2, 2, N'Quận Hai Bà Trưng', N'Ủy ban nhân dân phường Bạch Mai'),
(2, 2, N'Huyện Đông Anh', N'Ủy ban nhân dân huyện Đông Anh'),
(2, 2, N'Huyện Thạch Thất', N'Ủy ban nhân dân huyện Thạch Thất'),
(1, 2, N'Xã Thạch Hòa',N'Ủy ban nhân dân xã Thạch Hòa'),
(1, 2, N'Phường Kim Mã',N'Ủy ban nhân dân phường Kim Mã');

-- Bảng PublicService
-- ProcessedBy: 1: Xã, 2: Quận/Huyện
-- isDeleted: 0: chưa, 1 là đã xóa.
INSERT INTO PublicService (ServiceName, [Description], ServiceFee, DateCreated, DateDeleted, isDeleted, ProcessedBy, DepartmentID) VALUES
(N'Cấp giấy khai sinh', N'Dịch vụ cấp giấy khai sinh', 50000.00, '2023-01-01', NULL, 0, 1, 1),
(N'Cấp giấy chứng nhận kết hôn', N'Dịch vụ cấp giấy chứng nhận kết hôn', 100000.00, '2023-02-01', NULL, 0, 1, 1),
(N'Cấp giấy phép lái xe', N'Dịch vụ cấp giấy phép lái xe', 300000.00, '2023-03-01', NULL, 0, 2, 3),
(N'Cấp giấy chứng nhận quyền sử dụng đất', N'Dịch vụ cấp giấy chứng nhận quyền sử dụng đất', 500000.00, '2023-04-01', NULL,1, 2, 4);

-- Bảng User
INSERT INTO [User] (Username, Password, Email, RoleID) VALUES
(N'congdan1', 'password1', 'congdan1@hanoi.gov.vn', 1),
(N'congdan2', 'password2', 'congdan2@hanoi.gov.vn', 1),
(N'canbo1', 'password3', 'canbo1@hanoi.gov.vn', 2),
(N'canbo2', 'password4', 'canbo2@hanoi.gov.vn', 2);

-- Bảng Application
-- status: 0: pending, 1: success, 2: reject
INSERT INTO [Application] ([Name], [Status], [Type], PaymentAmount, SubmissionDate, Note, UserID, ServiceID, AgencyID) VALUES
(N'Đăng ký giấy khai sinh', 0, 1, 50000.00, '2023-06-15', N'Yêu cầu xử lý nhanh', 1, 1, 1),
(N'Đăng ký kết hôn', 1, 1, 100000.00, '2023-07-20', N'Xử lý tiêu chuẩn', 2, 2, 2),
(N'Đăng ký giấy phép lái xe', 2, 2, 300000.00, '2023-08-10', N'Yêu cầu dịch vụ nhanh', 1, 3, 3),
(N'Đăng ký giấy chứng nhận đất đai', 0, 2, 500000.00, '2023-09-05', N'Xử lý tiêu chuẩn', 1, 4, 4);

-- Bảng Document
INSERT INTO [Document] ([Name], [Path], [Type], [CreatedBy]) VALUES
(N'Giấy tờ tùy thân', N'/documents/giay_to_tuy_than.pdf', 1, 1),
(N'Sổ hộ khẩu', N'/documents/so_ho_khau.pdf', 1, 2),
(N'Giấy khám sức khỏe', N'/documents/giay_kham_suc_khoe.pdf', 2, 3),
(N'Hồ sơ đất đai', N'/documents/ho_so_dat_dai.pdf', 3, 4);

-- Bảng ApplicationDocument
INSERT INTO ApplicationDocument (ApplicationID, DocumentID, AttachDate, [Description]) VALUES
(1, 1, '2023-06-16', N'Hồ sơ đăng ký giấy khai sinh'),
(2, 2, '2023-07-21', N'Hồ sơ đăng ký kết hôn'),
(3, 3, '2023-08-11', N'Hồ sơ đăng ký giấy phép lái xe'),
(4, 4, '2023-09-06', N'Hồ sơ đăng ký giấy chứng nhận đất đai');
