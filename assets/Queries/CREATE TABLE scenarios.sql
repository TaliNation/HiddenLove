CREATE TABLE scenarios
(
    id SERIAL PRIMARY KEY,
    eventdate DATE NOT NULL,
    id_user INTEGER NOT NULL,
    id_scenariotemplate INTEGER NOT NULL,
    FOREIGN KEY (id_user)
      REFERENCES users (id),
    FOREIGN KEY (id_scenariotemplate)
      REFERENCES scenariotemplates (id)
)