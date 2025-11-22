PRAGMA foreign_keys = 0;

CREATE TABLE sqlitestudio_temp_table0 AS SELECT *
                                           FROM Results;

DROP TABLE Results;

CREATE TABLE Results (
    Id                 STRING   PRIMARY KEY,
    Status             STRING,
    ExamId             STRING   REFERENCES Exam (Id),
    Score              DOUBLE,
    UserId             STRING   REFERENCES UserJobSeeker (Id),
    DeadLine           DATETIME,
    CandidateSucceeded BOOLEAN,
    StartDate          DATETIME
);

INSERT INTO Results (
                        Id,
                        Status,
                        ExamId,
                        Score,
                        UserId,
                        DeadLine,
                        CandidateSucceeded
                    )
                    SELECT Id,
                           Status,
                           ExamId,
                           Score,
                           UserId,
                           DeadLine,
                           CandidateSucceeded
                      FROM sqlitestudio_temp_table0;

DROP TABLE sqlitestudio_temp_table0;

PRAGMA foreign_keys = 1;
