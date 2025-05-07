CREATE TABLE "JobSkill" (
	"Id"	TEXT,
	"JobId"	TEXT,
	"SkillId"	TEXT,
	PRIMARY KEY("Id"),
	FOREIGN KEY("JobId") REFERENCES "Job"("Id"),
	FOREIGN KEY("SkillId") REFERENCES "Skill"("Id")
);