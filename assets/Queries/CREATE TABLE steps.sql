CREATE TABLE steps
(
    id SERIAL PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    description VARCHAR(3000),
    orderindex INTEGER NOT NULL,
    startdate TIMESTAMP,
    enddate TIMESTAMP,
    id_scenario INTEGER NOT NULL,
    id_steptemplate INTEGER NOT NULL
    FOREIGN KEY (id_scenario)
      REFERENCES subscriptions (id)
    FOREIGN KEY (id_privilege)
      REFERENCES privileges (id)
)