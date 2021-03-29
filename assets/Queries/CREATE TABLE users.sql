CREATE TABLE users
(
    id SERIAL PRIMARY KEY,
    emailaddress VARCHAR(255) NOT NULL UNIQUE,
    username VARCHAR(255) NOT NULL,
    fullusername VARCHAR(255) NOT NULL UNIQUE,
    passwordhash VARCHAR(255) NOT NULL,
    id_privilege INTEGER,
    FOREIGN KEY (id_privilege)
      REFERENCES privileges (id)
);