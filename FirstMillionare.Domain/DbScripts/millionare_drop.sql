USE FirstMillionare;

GO

IF EXISTS(SELECT * FROM sysobjects WHERE name='tblAnswers' and type='U')
BEGIN
DROP TABLE tblAnswers;
END;

GO

IF EXISTS(SELECT * FROM sysobjects WHERE name='tblOptions' and type='U')
BEGIN
DROP TABLE tblOptions;
END;

GO

IF EXISTS(select * from sysobjects where name='tblQuestions' and type='U')
Begin
DROP TABLE tblQuestions;
END;