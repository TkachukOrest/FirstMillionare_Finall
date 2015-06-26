IF NOT EXISTS(select * from sysdatabases where name='FirstMillionare')
BEGIN
	CREATE DATABASE FirstMillionare;
END;

GO

USE FirstMillionare;

GO

IF NOT EXISTS(select * from sysobjects where name='tblQuestions' and type='U')
BEGIN
	CREATE TABLE tblQuestions(
		Id INT NOT NULL IDENTITY(1,1),
		Question NVARCHAR(200) NOT NULL,
		Complexity INT NOT NULL,
		CONSTRAINT PK_tblQuestions_Id PRIMARY KEY(ID)
	);
END;

GO

IF NOT EXISTS(SELECT * FROM sysobjects WHERE name='tblOptions' and type='U')
BEGIN
	CREATE TABLE tblOptions(
		Id INT NOT NULL IDENTITY(1,1),
		QuestionId INT NOT NULL,
		OptionText NVARCHAR(50) NOT NULL,
		CONSTRAINT PK_tblOptions_Id PRIMARY KEY(ID),
		CONSTRAINT FK_tblOptions_QuestionId_tblQuestions_Id FOREIGN KEY(QuestionId) REFERENCES tblQuestions(Id)
	);
END;

GO

IF NOT EXISTS(SELECT * FROM sysobjects WHERE name='tblAnswers' and type='U')
BEGIN
	CREATE TABLE tblAnswers(
	  Id INT NOT NULL IDENTITY(1,1),	  
	  OptionId INT NOT NULL,
	  CONSTRAINT FK_tblAnswers_OptionId_tblOptions_Id FOREIGN KEY(OptionId) REFERENCES tblOptions(Id),	  
	  CONSTRAINT UQ_tblAnswers_OptionId UNIQUE(OptionId)
	);
END;

