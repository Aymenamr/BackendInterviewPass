CREATE TABLE "Job" (
	"Id"	TEXT,
	"Title"	TEXT,
	"ShortDescription"	TEXT,
	"Image"	BLOB,
	"EmploymentTypeId"	TEXT,
	"experience"	INTEGER,
	"WorkingSchedule"	TEXT,
	"Role"	TEXT,
	"Salary"	REAL,
	FOREIGN KEY("EmploymentTypeId") REFERENCES "EmploymentType"("Id"),
	PRIMARY KEY("Id")
);

CREATE TABLE "EmploymentType" (
	"Id"	INTEGER,
	"Type"	TEXT,
	PRIMARY KEY("Id")
);
CREATE TABLE "Benefits" (
	"Id"	TEXT,
	"Name"	TEXT,
	PRIMARY KEY("Id")
);

CREATE TABLE "JobBenefit" (
	"Id"	TEXT,
	"JobId"	TEXT,
	"BenefitId"	TEXT,
	FOREIGN KEY("JobId") REFERENCES "Job"("Id"),
	FOREIGN KEY("BenefitId") REFERENCES "Benefits"("Id"),
	PRIMARY KEY("Id")
);

CREATE TABLE "JobFile" (
	"Id"	TEXT,
	"JobId"	TEXT,
	"FileName"	TEXT,
	"File"	BLOB,
	FOREIGN KEY("JobId") REFERENCES "Job"("Id"),
	PRIMARY KEY("Id")
);

CREATE INDEX "IX_Job_Title" ON "Job" (
	"Title"
);