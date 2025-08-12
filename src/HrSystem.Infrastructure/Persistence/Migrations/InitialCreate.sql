CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250811125133_InitialCreate') THEN
    CREATE TABLE org_types (
        "Id" uuid NOT NULL,
        "Name" character varying(200) NOT NULL,
        "CreatedDate" timestamp with time zone NOT NULL,
        "UpdatedDate" timestamp with time zone,
        CONSTRAINT "PK_org_types" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250811125133_InitialCreate') THEN
    CREATE TABLE employees (
        "Id" uuid NOT NULL,
        "FirstName" character varying(200) NOT NULL,
        "LastName" character varying(200) NOT NULL,
        "Email" character varying(320) NOT NULL,
        "HireDate" date NOT NULL,
        "OrgUnitId" uuid NOT NULL,
        "Status" integer NOT NULL,
        "CreatedDate" timestamp with time zone NOT NULL,
        "UpdatedDate" timestamp with time zone,
        CONSTRAINT "PK_employees" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250811125133_InitialCreate') THEN
    CREATE TABLE leave_requests (
        "Id" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        start_date date NOT NULL,
        end_date date NOT NULL,
        "Reason" character varying(1000) NOT NULL,
        "Status" integer NOT NULL,
        "CreatedDate" timestamp with time zone NOT NULL,
        "UpdatedDate" timestamp with time zone,
        CONSTRAINT "PK_leave_requests" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_leave_requests_employees_EmployeeId" FOREIGN KEY ("EmployeeId") REFERENCES employees ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250811125133_InitialCreate') THEN
    CREATE TABLE org_units (
        "Id" uuid NOT NULL,
        "Name" character varying(200) NOT NULL,
        "OrgTypeId" uuid NOT NULL,
        "ParentId" uuid,
        "ManagerId" uuid,
        "CreatedDate" timestamp with time zone NOT NULL,
        "UpdatedDate" timestamp with time zone,
        CONSTRAINT "PK_org_units" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_org_units_employees_ManagerId" FOREIGN KEY ("ManagerId") REFERENCES employees ("Id") ON DELETE SET NULL,
        CONSTRAINT "FK_org_units_org_types_OrgTypeId" FOREIGN KEY ("OrgTypeId") REFERENCES org_types ("Id") ON DELETE RESTRICT,
        CONSTRAINT "FK_org_units_org_units_ParentId" FOREIGN KEY ("ParentId") REFERENCES org_units ("Id") ON DELETE RESTRICT
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250811125133_InitialCreate') THEN
    CREATE UNIQUE INDEX "IX_employees_Email" ON employees ("Email");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250811125133_InitialCreate') THEN
    CREATE INDEX "IX_employees_OrgUnitId" ON employees ("OrgUnitId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250811125133_InitialCreate') THEN
    CREATE INDEX "IX_leave_requests_EmployeeId_CreatedDate" ON leave_requests ("EmployeeId", "CreatedDate");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250811125133_InitialCreate') THEN
    CREATE INDEX "IX_leave_requests_EmployeeId_Status" ON leave_requests ("EmployeeId", "Status");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250811125133_InitialCreate') THEN
    CREATE UNIQUE INDEX "IX_org_types_Name" ON org_types ("Name");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250811125133_InitialCreate') THEN
    CREATE INDEX "IX_org_units_ManagerId" ON org_units ("ManagerId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250811125133_InitialCreate') THEN
    CREATE UNIQUE INDEX "IX_org_units_Name_OrgTypeId" ON org_units ("Name", "OrgTypeId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250811125133_InitialCreate') THEN
    CREATE INDEX "IX_org_units_OrgTypeId" ON org_units ("OrgTypeId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250811125133_InitialCreate') THEN
    CREATE INDEX "IX_org_units_ParentId" ON org_units ("ParentId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250811125133_InitialCreate') THEN
    ALTER TABLE employees ADD CONSTRAINT "FK_employees_org_units_OrgUnitId" FOREIGN KEY ("OrgUnitId") REFERENCES org_units ("Id") ON DELETE RESTRICT;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250811125133_InitialCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250811125133_InitialCreate', '9.0.8');
    END IF;
END $EF$;
COMMIT;

