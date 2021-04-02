/*==============================================================*/
/* Table: Offers                                                */
/*==============================================================*/
CREATE TABLE Offers
(
    Id INT IDENTITY NOT NULL PRIMARY KEY,
    Designation VARCHAR(255) NOT NULL UNIQUE
);
go

/*==============================================================*/
/* Table: Privileges                                            */
/*==============================================================*/
CREATE TABLE Privileges
(
    Id INT IDENTITY NOT NULL PRIMARY KEY,
    Designation VARCHAR(255) NOT NULL UNIQUE
);
go

/*==============================================================*/
/* Table: ScenarioTemplates                                     */
/*==============================================================*/
CREATE TABLE ScenarioTemplates
(
    Id INT IDENTITY NOT NULL PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Description VARCHAR(3000),
    Image VARCHAR(255)
);

/*==============================================================*/
/* Table: StepTemplates                                         */
/*==============================================================*/
CREATE TABLE StepTemplates
(
    Id INT IDENTITY NOT NULL PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Description VARCHAR(3000)
)

/*==============================================================*/
/* Table: Users                                                 */
/*==============================================================*/
CREATE TABLE Users
(
    Id INT IDENTITY NOT NULL PRIMARY KEY,
    EmailAddress VARCHAR(255) NOT NULL UNIQUE,
    SecondaryEmailAddress VARCHAR(255) NOT NULL UNIQUE,
    Username VARCHAR(255) NOT NULL,
    FullUsername VARCHAR(255) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Id_privilege INTEGER,
    FOREIGN KEY (Id_privilege)
		REFERENCES Privileges (id)
);

/*==============================================================*/
/* Table: Scenarios                                             */
/*==============================================================*/
CREATE TABLE Scenarios
(
    Id INT IDENTITY NOT NULL PRIMARY KEY,
    EventDate DATE NOT NULL,
    Id_user INTEGER NOT NULL,
    Id_scenariotemplate INTEGER NOT NULL,
    FOREIGN KEY (Id_user)
		REFERENCES Users (id),
    FOREIGN KEY (Id_scenariotemplate)
		REFERENCES ScenarioTemplates (id)
);

/*==============================================================*/
/* Table: ScenarioTemplates_StepTemplates                       */
/*==============================================================*/
CREATE TABLE ScenarioTemplates_StepTemplates
(
    Id INT IDENTITY NOT NULL PRIMARY KEY,
    Id_scenariotemplate INTEGER NOT NULL,
    Id_steptemplate INTEGER NOT NULL,
    StartDate DATETIME,
    EndDate DATETIME,
    FOREIGN KEY (id_scenariotemplate)
		  REFERENCES ScenarioTemplates (id),
    FOREIGN KEY (id_steptemplate)
		  REFERENCES StepTemplates (id)
);

/*==============================================================*/
/* Table: Subscriptions                                         */
/*==============================================================*/
CREATE TABLE Subscriptions 
(
    Id INT IDENTITY NOT NULL PRIMARY KEY,
    Designation VARCHAR(255),
    StartDate DATETIME,
    EndDate DATETIME,
    CheckoutPrice INTEGER,
    Id_billingdetails INTEGER,
    Id_user INTEGER NOT NULL,
    Id_offer INTEGER,
    FOREIGN KEY (id_offer)
		REFERENCES Offers (id),
    FOREIGN KEY (id_user)
		REFERENCES Users (id)
)