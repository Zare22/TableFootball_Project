CREATE DATABASE TableFootball;
GO

USE TableFootball;
GO

CREATE TABLE Leagues (
    IdLeague INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Teams (
    IdTeam INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME NOT NULL
);
GO

CREATE TABLE TeamsLeagues (
    TeamId INT,
    LeagueId INT,
    PRIMARY KEY (TeamId, LeagueId),
    FOREIGN KEY (TeamId) REFERENCES Teams(IdTeam),
    FOREIGN KEY (LeagueId) REFERENCES Leagues(IdLeague)
);
GO

CREATE TABLE Players (
    IdPlayer INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    CONSTRAINT UC_Players_Email UNIQUE (Email)
);
GO

CREATE TABLE PlayerTeams (
    PlayerId INT,
    TeamId INT,
    PRIMARY KEY (PlayerId, TeamId),
    FOREIGN KEY (PlayerId) REFERENCES Players(IdPlayer),
    FOREIGN KEY (TeamId) REFERENCES Teams(IdTeam),
);
GO

CREATE TABLE Games (
    IdGame INT PRIMARY KEY IDENTITY,
    HomeTeamId INT,
    AwayTeamId INT,
    LeagueId INT,
    WinnerId INT,
    FOREIGN KEY (HomeTeamId) REFERENCES Teams(IdTeam),
    FOREIGN KEY (AwayTeamId) REFERENCES Teams(IdTeam),
    FOREIGN KEY (LeagueId) REFERENCES Leagues(IdLeague),
    FOREIGN KEY (WinnerId) REFERENCES Teams(IdTeam)
);
GO

CREATE TABLE GameSets (
    IdGameSet INT PRIMARY KEY IDENTITY,
    GameId INT,
    HomeTeamGoals INT,
    AwayTeamGoals INT,
    FOREIGN KEY (GameId) REFERENCES Games(IdGame)
);
GO

CREATE PROCEDURE sp_CreateLeague
    @Name NVARCHAR(100),
    @IdLeague INT OUTPUT
AS
BEGIN
    INSERT INTO Leagues (Name)
    VALUES (@Name)
    SET @IdLeague = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE sp_CreateTeam
	@Name NVARCHAR(100),
	@CreatedAt DATETIME,
	@IdTeam INT OUTPUT
AS
BEGIN
	INSERT INTO Teams(Name, CreatedAt)
	VALUES (@Name, @CreatedAt)
	SET @IdTeam = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE sp_CreatePlayer
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@Email NVARCHAR(100),
	@CreatedAt DATETIME,
	@IdPlayer INT OUTPUT
AS
BEGIN
	INSERT INTO Players(FirstName, LastName, Email, CreatedAt)
	VALUES (@FirstName, @LastName, @Email, @CreatedAt)
	SET @IdPlayer = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE sp_CreateGame
	@HomeTeamId INT,
	@AwayTeamId INT,
	@LeagueId INT,
	@WinnerId INT,
	@IdGame INT OUTPUT
AS
BEGIN
	INSERT INTO Games(HomeTeamId, AwayTeamId, LeagueId, WinnerId)
	VALUES (@HomeTeamId, @AwayTeamId, @LeagueId, @WinnerId)
	SET @IdGame = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE sp_CreateGameSet
	@HomeTeamGoals INT,
	@AwayTeamGoals INT,
	@IdGame INT,
	@IdGameSet INT OUTPUT
AS
BEGIN
	INSERT INTO GameSets(HomeTeamGoals, AwayTeamGoals, GameId)
	VALUES (@HomeTeamGoals, @AwayTeamGoals, @IdGame)
	SET @IdGameSet = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE sp_AddPlayerToTeam
	@PlayerId INT,
	@TeamId INT
AS
BEGIN
	INSERT INTO PlayerTeams(PlayerId, TeamId)
	VALUES(@PlayerId, @TeamId)
END
GO

CREATE PROCEDURE sp_AddTeamToLeague
	@TeamId INT,
	@LeagueId INT
AS
BEGIN
	INSERT INTO TeamsLeagues(TeamId, LeagueId)
	VALUES(@TeamId, @LeagueId)
END
GO

CREATE PROCEDURE sp_GetAllPlayers
AS
BEGIN
    SELECT IdPlayer, FirstName, LastName, Email, CreatedAt
    FROM Players
END
GO

CREATE PROCEDURE sp_GetAllTeamsWithPlayers
AS
BEGIN
    SELECT t.IdTeam, t.Name, t.CreatedAt, p.IdPlayer, p.FirstName, p.LastName, p.Email, p.CreatedAt
    FROM Teams t
    LEFT JOIN PlayerTeams pt ON t.IdTeam = pt.TeamId
    LEFT JOIN Players p ON pt.PlayerId = p.IdPlayer;
END
GO

CREATE PROCEDURE sp_GetAllLeagues
AS
BEGIN
	SELECT IdLeague, Name
	FROM Leagues
END
GO

CREATE PROCEDURE sp_GetLeaguesAndTeams
AS
BEGIN
    SELECT 
        l.IdLeague, 
        l.Name, 
        t.IdTeam, 
        t.Name, 
        t.CreatedAt
    FROM Leagues l
    LEFT JOIN TeamsLeagues tl ON l.IdLeague = tl.LeagueId
    LEFT JOIN Teams t ON tl.TeamId = t.IdTeam
    ORDER BY l.IdLeague, t.IdTeam;
END
GO

CREATE PROCEDURE sp_GetLeagueTable
    @LeagueId INT
AS
BEGIN
    SELECT
        ROW_NUMBER() OVER (ORDER BY Points DESC, GoalsScored DESC) AS Position,
        TeamName,
        GamesPlayed,
        GoalsScored,
        GoalsConceded,
        Points
    FROM
    (
        SELECT
            Teams.IdTeam AS TeamId,
            Teams.Name AS TeamName,
            COUNT(Games.IdGame) AS GamesPlayed,
            SUM(CASE WHEN Games.WinnerId = Teams.IdTeam THEN 1 ELSE 0 END) AS Points,
            SUM(CASE WHEN Games.HomeTeamId = Teams.IdTeam THEN GameSets.HomeTeamGoals ELSE GameSets.AwayTeamGoals END) AS GoalsScored,
            SUM(CASE WHEN Games.HomeTeamId = Teams.IdTeam THEN GameSets.AwayTeamGoals ELSE GameSets.HomeTeamGoals END) AS GoalsConceded
        FROM
            Teams
        LEFT JOIN Games ON (Teams.IdTeam = Games.HomeTeamId OR Teams.IdTeam = Games.AwayTeamId)
        LEFT JOIN GameSets ON Games.IdGame = GameSets.GameId
        WHERE
            Games.LeagueId = @LeagueId
        GROUP BY
            Teams.IdTeam, Teams.Name
    ) AS LeagueTable
END
GO

CREATE PROCEDURE sp_UpdateTeam
    @IdTeam INT,
    @Name NVARCHAR(100),
    @CreatedAt DATETIME,
	@PlayerOneId INT,
	@PlayerTwoId INT
AS
BEGIN
    UPDATE Teams
    SET Name = @Name,
        CreatedAt = @CreatedAt
    WHERE IdTeam = @IdTeam

	DELETE
	FROM PlayerTeams
	WHERE TeamId = @IdTeam

	INSERT INTO PlayerTeams(PlayerId, TeamId)
	VALUES(@PlayerOneId, @IdTeam)

	INSERT INTO PlayerTeams(PlayerId, TeamId)
	VALUES(@PlayerTwoId, @IdTeam)
END
GO

CREATE TABLE Admin
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(100) NOT NULL,
    PasswordHash VARBINARY(64) NOT NULL,
    Salt VARBINARY(32) NOT NULL,
    CONSTRAINT UC_Admin_Username UNIQUE (Username)
)
GO

CREATE PROCEDURE sp_CreateAdmin
    @Username NVARCHAR(100),
    @Password NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Salt VARBINARY(32) = CRYPT_GEN_RANDOM(32);
    DECLARE @PasswordWithSalt NVARCHAR(132) = @Password + CONVERT(NVARCHAR(100), @Salt);
    DECLARE @PasswordHash VARBINARY(64) = HASHBYTES('SHA2_512', @PasswordWithSalt);

    INSERT INTO Admin (Username, PasswordHash, Salt)
    VALUES (@Username, @PasswordHash, @Salt);
END
GO

EXEC sp_CreateAdmin @Username = 'admin', @Password = 'admin'
GO

CREATE PROCEDURE sp_VerifyAdminLogin
    @Username NVARCHAR(100),
    @Password NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Salt VARBINARY(32);
    DECLARE @PasswordWithSalt NVARCHAR(132);
    DECLARE @PasswordHash VARBINARY(64);

    SELECT @Salt = Salt FROM Admin WHERE Username = @Username;

    IF @Salt IS NOT NULL
    BEGIN
        SET @PasswordWithSalt = @Password + CONVERT(NVARCHAR(100), @Salt);
        SET @PasswordHash = HASHBYTES('SHA2_512', @PasswordWithSalt);

        IF EXISTS (SELECT 1 FROM Admin WHERE Username = @Username AND PasswordHash = @PasswordHash)
        BEGIN
            SELECT 1;
        END
        ELSE
        BEGIN
            SELECT 0;
        END
    END
    ELSE
    BEGIN
        SELECT -1;
    END
END
GO
--MOCK DATA
INSERT INTO Leagues (Name) VALUES ('Premier League');
INSERT INTO Leagues (Name) VALUES ('La Liga');
INSERT INTO Leagues (Name) VALUES ('Bundesliga');
INSERT INTO Teams (Name, CreatedAt) VALUES ('Arsenal', GETDATE());
INSERT INTO Teams (Name, CreatedAt) VALUES ('Man Utd', GETDATE());
INSERT INTO Teams (Name, CreatedAt) VALUES ('Real Madrid', GETDATE());
INSERT INTO Teams (Name, CreatedAt) VALUES ('Barcelona', GETDATE());
INSERT INTO Teams (Name, CreatedAt) VALUES ('Bayern', GETDATE());
INSERT INTO Teams (Name, CreatedAt) VALUES ('Borussia', GETDATE());
INSERT INTO Players (FirstName, LastName, Email, CreatedAt) VALUES ('John', 'Doe', 'john.doe@example.com', GETDATE());
INSERT INTO Players (FirstName, LastName, Email, CreatedAt) VALUES ('Jane', 'Smith', 'jane.smith@example.com', GETDATE());
INSERT INTO Players (FirstName, LastName, Email, CreatedAt) VALUES ('Michael', 'Johnson', 'michael.johnson@example.com', GETDATE());
INSERT INTO Players (FirstName, LastName, Email, CreatedAt) VALUES ('Emily', 'Williams', 'emily.williams@example.com', GETDATE());
INSERT INTO Players (FirstName, LastName, Email, CreatedAt) VALUES ('James', 'Brown', 'james.brown@example.com', GETDATE());
INSERT INTO Players (FirstName, LastName, Email, CreatedAt) VALUES ('Laura', 'Davis', 'laura.davis@example.com', GETDATE());
INSERT INTO Players (FirstName, LastName, Email, CreatedAt) VALUES ('Robert', 'Martinez', 'robert.martinez@example.com', GETDATE());
INSERT INTO Players (FirstName, LastName, Email, CreatedAt) VALUES ('Sarah', 'Rodriguez', 'sarah.rodriguez@example.com', GETDATE());
INSERT INTO Players (FirstName, LastName, Email, CreatedAt) VALUES ('William', 'Lee', 'william.lee@example.com', GETDATE());
INSERT INTO Players (FirstName, LastName, Email, CreatedAt) VALUES ('Jennifer', 'Garcia', 'jennifer.garcia@example.com', GETDATE());
INSERT INTO Players (FirstName, LastName, Email, CreatedAt) VALUES ('Christopher', 'Lopez', 'christopher.lopez@example.com', GETDATE());
INSERT INTO Players (FirstName, LastName, Email, CreatedAt) VALUES ('Mary', 'Martinez', 'mary.martinez@example.com', GETDATE());
INSERT INTO TeamsLeagues (TeamId, LeagueId) VALUES (1, 1);
INSERT INTO TeamsLeagues (TeamId, LeagueId) VALUES (2, 1);
INSERT INTO TeamsLeagues (TeamId, LeagueId) VALUES (3, 2);
INSERT INTO TeamsLeagues (TeamId, LeagueId) VALUES (4, 2);
INSERT INTO TeamsLeagues (TeamId, LeagueId) VALUES (5, 3);
INSERT INTO TeamsLeagues (TeamId, LeagueId) VALUES (6, 3);


INSERT INTO PlayerTeams (PlayerId, TeamId) VALUES (1, 1);
INSERT INTO PlayerTeams (PlayerId, TeamId) VALUES (2, 1); 
INSERT INTO PlayerTeams (PlayerId, TeamId) VALUES (3, 2); 
INSERT INTO PlayerTeams (PlayerId, TeamId) VALUES (4, 2); 
INSERT INTO PlayerTeams (PlayerId, TeamId) VALUES (5, 3); 
INSERT INTO PlayerTeams (PlayerId, TeamId) VALUES (6, 3); 
INSERT INTO PlayerTeams (PlayerId, TeamId) VALUES (7, 4); 
INSERT INTO PlayerTeams (PlayerId, TeamId) VALUES (8, 4); 
INSERT INTO PlayerTeams (PlayerId, TeamId) VALUES (9, 5); 
INSERT INTO PlayerTeams (PlayerId, TeamId) VALUES (10, 5); 
INSERT INTO PlayerTeams (PlayerId, TeamId) VALUES (11, 6); 
INSERT INTO PlayerTeams (PlayerId, TeamId) VALUES (12, 6); 

