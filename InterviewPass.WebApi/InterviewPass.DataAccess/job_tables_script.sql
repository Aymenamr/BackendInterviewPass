CREATE TABLE "Job" (
	"Id"	TEXT,
	"Title"	TEXT,
	"ShortDescription"	TEXT,
	"ImagePath"	TEXT,
	"EmploymentTypeId"	TEXT,
	"experience"	INTEGER,
	"WorkingSchedule"	TEXT,
	"Role"	TEXT,
	"Salary"	REAL,
	PRIMARY KEY("Id"),
	FOREIGN KEY("SkillId") REFERENCES "Skill"("Id"),
	FOREIGN KEY("EmploymentTypeId") REFERENCES "EmploymentType"("Id")
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