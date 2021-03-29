CREATE TABLE subscriptions 
(
    id SERIAL PRIMARY KEY,
    designation VARCHAR(255),
    startdate TIMESTAMP,
    enddate TIMESTAMP,
    checkoutprice INTEGER,
    id_billingdetails INTEGER,
    id_user INTEGER NOT NULL,
    id_offer INTEGER,
    -- FOREIGN KEY (id_billingdetails)
    --   REFERENCES billingdetails (id)
    FOREIGN KEY (id_offer)
      REFERENCES offers (id)
    FOREIGN KEY (id_user)
      REFERENCES users (id)
)