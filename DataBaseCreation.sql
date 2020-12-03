--Create DATABASE MusicManager;
--DROP TABLE Favourites;
--DROP TABLE Ratings;
--DROP TABLE Tabs;
--DROP TABLE Users;

CREATE TABLE Users	
(
	UserId int IDENTITY(1, 1) PRIMARY KEY,
	UserName varchar(16) NOT NULL,
	Password varchar(16) NOT NULL
);

CREATE TABLE Tabs 
(
	TabId int IDENTITY(1, 1) PRIMARY KEY,
	TabName varchar(48) NOT NULL, 
	BandName varchar(48) NOT NULL, 
	Instrument varchar(16) NOT NULL, 
	TabUrl varchar(200) NOT NULL,
	TabCreator int NOT NULL,
	FOREIGN KEY (TabCreator) REFERENCES Users(UserId)
);

CREATE TABLE Favourites 
(
	TabId int NOT NULL,
	UserId int NOT NULL,
	FOREIGN KEY (TabId) REFERENCES Tabs(TabId),
	FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE Ratings
(
	UserId int NOT NULL,
	TabId int NOT NULL,
	Rating int NOT NULL,
	FOREIGN KEY (TabId) REFERENCES Tabs(TabId),
	FOREIGN KEY (UserId) REFERENCES Users(UserId)
);