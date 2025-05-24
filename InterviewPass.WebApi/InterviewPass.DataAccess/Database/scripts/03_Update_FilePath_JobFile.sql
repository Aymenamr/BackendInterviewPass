PRAGMA foreign_keys = 0;

CREATE TABLE sqlitestudio_temp_table AS SELECT *
                                          FROM JobFile;

DROP TABLE JobFile;

CREATE TABLE JobFile (
    Id       TEXT,
    JobId    TEXT,
    File     TEXT,
    FileName TEXT,
    PRIMARY KEY (
        Id
    ),
    FOREIGN KEY (
        JobId
    )
    REFERENCES Job (Id) 
);

INSERT INTO JobFile (
                        Id,
                        JobId,
                        File,
                        FileName
                    )
                    SELECT Id,
                           JobId,
                           Filej,
                           FileName
                      FROM sqlitestudio_temp_table;

DROP TABLE sqlitestudio_temp_table;

PRAGMA foreign_keys = 1;
