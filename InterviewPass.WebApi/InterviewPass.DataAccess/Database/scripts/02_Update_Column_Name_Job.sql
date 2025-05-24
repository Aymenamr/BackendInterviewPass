PRAGMA foreign_keys = 0;

CREATE TABLE sqlitestudio_temp_table AS SELECT *
                                          FROM Job;

DROP TABLE Job;

CREATE TABLE Job (
    Id               TEXT,
    Title            TEXT,
    ShortDescription TEXT,
    Image            TEXT,
    EmploymentTypeId TEXT,
    experience       INTEGER,
    SkillId          TEXT,
    WorkingSchedule  TEXT,
    Role             TEXT,
    Salary           REAL,
    PRIMARY KEY (
        Id
    ),
    FOREIGN KEY (
        SkillId
    )
    REFERENCES Skill (Id),
    FOREIGN KEY (
        EmploymentTypeId
    )
    REFERENCES EmploymentType (Id) 
);

INSERT INTO Job (
                    Id,
                    Title,
                    ShortDescription,
                    Image,
                    EmploymentTypeId,
                    experience,
                    SkillId,
                    WorkingSchedule,
                    Role,
                    Salary
                )
                SELECT Id,
                       Title,
                       ShortDescription,
                       ImagePath,
                       EmploymentTypeId,
                       experience,
                       SkillId,
                       WorkingSchedule,
                       Role,
                       Salary
                  FROM sqlitestudio_temp_table;

DROP TABLE sqlitestudio_temp_table;

CREATE INDEX IX_Job_Title ON Job (
    "Title"
);

PRAGMA foreign_keys = 1;
PRAGMA foreign_keys = 0;

CREATE TABLE sqlitestudio_temp_table AS SELECT *
                                          FROM JobBenefit;

DROP TABLE JobBenefit;

CREATE TABLE JobBenefit (
    Id        TEXT,
    JobId     TEXT REFERENCES Job (Id),
    BenefitId TEXT REFERENCES Benefits (Id),
    FOREIGN KEY (
        JobId
    )
    REFERENCES Job (Id),
    FOREIGN KEY (
        BenefitId
    )
    REFERENCES Benefits (Id),
    PRIMARY KEY (
        Id
    )
);

INSERT INTO JobBenefit (
                           Id,
                           JobId,
                           BenefitId
                       )
                       SELECT Id,
                              JobId,
                              BenefitId
                         FROM sqlitestudio_temp_table;

DROP TABLE sqlitestudio_temp_table;

PRAGMA foreign_keys = 1;
