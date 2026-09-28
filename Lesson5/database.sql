CREATE TABLE IF NOT EXISTS product (
    id INTEGER PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    price NUMERIC(15,2) NOT NULL
);

INSERT INTO product (id, name, price) VALUES
(1, 'Laptop', 15000000),
(2, 'Mouse', 300000),
(3, 'Keyboard', 500000)
ON CONFLICT (id) DO NOTHING;
