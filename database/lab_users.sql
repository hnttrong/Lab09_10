CREATE DATABASE IF NOT EXISTS software_engineering_labs;
USE software_engineering_labs;

CREATE TABLE IF NOT EXISTS users (
    id INT PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(50) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Demonstration data for the lab login cases only.
-- Production systems must store salted password hashes instead of plain text values.
INSERT INTO users (username, password_hash) VALUES
    ('user1', 'password1'),
    ('user2', 'password2'),
    ('user3', 'password3')
ON DUPLICATE KEY UPDATE username = VALUES(username);
