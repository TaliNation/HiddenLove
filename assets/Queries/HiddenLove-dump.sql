/*==============================================================*/
/* Table: Offers                                                */
/*==============================================================*/
CREATE TABLE Offers (
  Id INT IDENTITY NOT NULL PRIMARY KEY,
  Designation VARCHAR(255) NOT NULL UNIQUE
);

  /*==============================================================*/
  /* Table: Privileges                                            */
  /*==============================================================*/
  CREATE TABLE Privileges (
    Id INT IDENTITY NOT NULL PRIMARY KEY,
    Designation VARCHAR(255) NOT NULL UNIQUE
  );

  /*==============================================================*/
  /* Table: ScenarioTemplates                                     */
  /*==============================================================*/
  CREATE TABLE ScenarioTemplates (
    Id INT IDENTITY NOT NULL PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Description VARCHAR(3000),
    Image VARCHAR(255)
  );

/*==============================================================*/
/* Table: StepTemplates                                         */
/*==============================================================*/
CREATE TABLE StepTemplates (
  Id INT IDENTITY NOT NULL PRIMARY KEY,
  Title VARCHAR(255) NOT NULL,
  Description VARCHAR(3000)
);

/*==============================================================*/
/* Table: Users                                                 */
/*==============================================================*/
CREATE TABLE Users (
  Id INT IDENTITY NOT NULL PRIMARY KEY,
  EmailAddress VARCHAR(255) NOT NULL UNIQUE,
  SecondaryEmailAddress VARCHAR(255) NOT NULL UNIQUE,
  Username VARCHAR(255) NOT NULL,
  FullUsername VARCHAR(255) NOT NULL UNIQUE,
  PasswordHash VARCHAR(255) NOT NULL,
  Id_privilege INTEGER,
  FOREIGN KEY (Id_privilege) REFERENCES Privileges (id)
);

/*==============================================================*/
/* Table: Scenarios                                             */
/*==============================================================*/
CREATE TABLE Scenarios (
  Id INT IDENTITY NOT NULL PRIMARY KEY,
  EventDate DATE NOT NULL,
  Id_user INTEGER NOT NULL,
  Id_scenariotemplate INTEGER NOT NULL,
  FOREIGN KEY (Id_user) REFERENCES Users (id),
  FOREIGN KEY (Id_scenariotemplate) REFERENCES ScenarioTemplates (id)
);

/*==============================================================*/
/* Table: ScenarioTemplates_StepTemplates                       */
/*==============================================================*/
CREATE TABLE ScenarioTemplates_StepTemplates (
  Id INT IDENTITY NOT NULL PRIMARY KEY,
  Id_scenariotemplate INTEGER NOT NULL,
  Id_steptemplate INTEGER NOT NULL,
  StartDate DATETIME,
  EndDate DATETIME,
  FOREIGN KEY (id_scenariotemplate) REFERENCES ScenarioTemplates (id),
  FOREIGN KEY (id_steptemplate) REFERENCES StepTemplates (id)
);

/*==============================================================*/
/* Table: Subscriptions                                         */
/*==============================================================*/
CREATE TABLE Subscriptions (
  Id INT IDENTITY NOT NULL PRIMARY KEY,
  Designation VARCHAR(255),
  StartDate DATETIME,
  EndDate DATETIME,
  CheckoutPrice INTEGER,
  Id_billingdetails INTEGER,
  Id_user INTEGER NOT NULL,
  Id_offer INTEGER,
  FOREIGN KEY (id_offer) REFERENCES Offers (id),
  FOREIGN KEY (id_user) REFERENCES Users (id)
);

  /*==============================================================*/
  /* Views: UserScenarios                                         */
  /*==============================================================*/
  CREATE VIEW UserScenarios AS
  SELECT
    dbo.Scenarios.*,
    dbo.ScenarioTemplates.Image,
    dbo.ScenarioTemplates_StepTemplates.EndDate,
    dbo.ScenarioTemplates_StepTemplates.StartDate,
    dbo.StepTemplates.Description AS StepDescription,
    dbo.ScenarioTemplates.Description AS ScenarioDescription,
    dbo.ScenarioTemplates.Title AS ScenarioTitle,
    dbo.StepTemplates.Title AS StepTitle
  FROM
    dbo.Scenarios
    INNER JOIN dbo.ScenarioTemplates ON dbo.Scenarios.Id_scenariotemplate = dbo.ScenarioTemplates.Id
    INNER JOIN dbo.ScenarioTemplates_StepTemplates ON dbo.ScenarioTemplates.Id = dbo.ScenarioTemplates_StepTemplates.Id_scenariotemplate
    INNER JOIN dbo.StepTemplates ON dbo.ScenarioTemplates_StepTemplates.Id_steptemplate = dbo.StepTemplates.Id
