CREATE TABLE scenariotemplates_steptemplates
(
    id_scenariotemplate INTEGER NOT NULL,
    id_steptemplate INTEGER NOT NULL,
    orderindex INTEGER,
    startdate TIME,
    enddate TIME,
    PRIMARY KEY(id_scenariotemplate, id_steptemplate),
    FOREIGN KEY (id_scenariotemplate)
      REFERENCES scenariotemplates (id),
    FOREIGN KEY (id_steptemplate)
      REFERENCES steptemplates (id)
)