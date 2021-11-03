Create database WPF_FCM 
Go

Use WPF_FCM
Go


-- Tài khoản -- 
Create Table Users
(
	Id int identity (1,1) primary key,
	Username nvarchar(100),
	Password nvarchar(max),
	DisplayName nvarchar(100),												-- Tên người dùng
	RoleLevel int															-- Level người dùng ( 1 - Admin, 2 - staff)
)
Go
insert into Users(Username,Password,DisplayName,RoleLevel)
values ('admin','cdd96d3cc73d1dbdaffa03cc6cd7339b','a',1)
Go

-- Giải đấu -- 
Create Table Tournaments
(
	Id int identity (1,1) primary key,
	Honors nvarchar(100),													-- Nhà tài trợ
	DisplayName nvarchar(100),												-- Tên giải đấu
	Time Datetime,
	Status int,
	Logo Image,
	countTeam int
)
Go

-- Đội bóng -- 
Create Table Teams
(
	Id int identity (1,1) primary key,
	IdTournaments int,
	DisplayName nvarchar(100),
	Coach nvarchar(100),													-- Tên huấn luyện viên
	Stadium nvarchar(100),	
	Nation nvarchar(100),																							-- Quốc gia
	Logo Image,

	foreign key (IdTournaments) references Tournaments(Id)
)
Go

-- Cầu thủ -- 
Create Table Players
(
	Id int identity (1,1) primary key,
	IdTeams int,
	DisplayName nvarchar(100),												-- Tên cầu thủ
	UniformNumber int,														-- Số áo
	Birthday Datetime,
	Position nvarchar(100),													-- Vị trí thi đấu
	Nationality nvarchar(100),
	Note nvarchar(max),
	Imagee Image,

	foreign key (IdTeams) references Teams(Id)
)
Go

-- Trận đấu --
Create Table Matchs
(
	Id int identity (1,1) primary key,
	IdTournaments int,
	IdTeam01 int,
	IdTeam02 int,
	Round int,
	Time Datetime,
	Stadium nvarchar(100),

	foreign key (IdTournaments) references Tournaments(Id),
	foreign key (IdTeam01) references Teams(Id),
	foreign key (IdTeam02) references Teams(Id)
)
Go

-- Đội hình/thành viên đưuọc đăng ký thi đấu -
-- Cái này mình đang mặc định là cả Team có bao nhiêu cầu thủ đem hết vào trận, tuy nhiên cái này sai về logic, 
-- hơn nữa mình thêm cái này để mình phát triển tính năng "Chuyển nhượng" sẽ không xảy ra bug chỗ này 
Create Table Lineups
(
	IdMatchs int,
	IdPlayers int,
	IdTeams int,

	foreign key (IdMatchs) references Matchs(Id),
	foreign key (IdPlayers) references Players(Id),
	foreign key (IdTeams) references Teams(Id)
)
Go

-- Loại bàn thắng -- 
Create Table TypeOfGoals
(
	Id int identity (1,1) primary key,
	DisplayName nvarchar(100)
)
Go

-- Bàn thắng -- 
Create Table Match_Goals
(
	IdMatchs int,
	IdPlayers int,
	IdTeams int,
	IdTypeOfGoals int,
	Time Datetime,

	foreign key (IdMatchs) references Matchs(Id),
	foreign key (IdPlayers) references Players(Id),
	foreign key (IdTeams) references Teams(Id),
	foreign key (IdTypeOfGoals) references TypeOfGoals(Id)
)
Go

-- Thẻ phạt -- 
Create Table Penalty
(
	IdMatchs int,
	IdPlayers int,
	IdTeams int,
	Time Datetime,
	TypeOfCard nvarchar(100),

	foreign key (IdMatchs) references Matchs(Id),
	foreign key (IdPlayers) references Players(Id),
	foreign key (IdTeams) references Teams(Id)
)
Go

-- Thay người -- 
Create Table Subtitutions
(
	IdMatchs int,
	IdPlayerIn int,
	IdPlayerOut int,
	IdTeams int,
	Time Datetime,

	foreign key (IdMatchs) references Matchs(Id),
	foreign key (IdPlayerIn) references Players(Id),
	foreign key (IdPlayerOut) references Players(Id),
	foreign key (IdTeams) references Teams(Id)
)
Go

-- Quy định -- 
Create Table Settings
(
	IdTournaments int,
	NumberOfTeams int,															-- Số lượng đội bóng tham gia
	MinPlayerOfTeams int,
	MaxPlayerOfTeams int,
	MinAge int,
	MaxAge int,
	MaxNumberForeignPlayers int,
	Score_win int,
	Score_draw int,
	Score_lose int,

	foreign key (IdTournaments) references Tournaments(Id),
)
Go

