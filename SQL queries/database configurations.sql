Drop DATABASE IF EXISTS Library;

CREATE DATABASE  Library;

USE Library;

CREATE TABLE IF NOT EXISTS Member (
    ID INT AUTO_INCREMENT,
    Name text,
    Email text ,
    Phone text,
    PRIMARY KEY (ID)
);

CREATE TABLE IF NOT EXISTS Book (
    ID INT AUTO_INCREMENT,
    Name text,
    Author text,
    Genre text,
    Available text,
    PRIMARY KEY (ID)
);

CREATE TABLE IF NOT EXISTS BorrowedBooks (
    ID INT AUTO_INCREMENT,
    BookID INT,
    UserID INT,
    BookName text,
    UserName text,
    Date text,
    Returned text,
    PRIMARY KEY (ID)

);

INSERT INTO Member (Name, Email, Phone) VALUES
('depo', 'depo@example.com', '123456789'),
('Ali', 'Ali@example.com', '987654321'),
('m4m4', 'm4m4@example.com', '05557000');

INSERT INTO Book (Name, Author, Genre, Available) VALUES
('Attack on Titan', 'Hajime Isayama', 'Action', 'Available'),
('Kaguya-sama: Love is War', 'Aka Akasaka', 'Romantic Comedy', 'Unavailable'),
('Your Lie in April', 'Naoshi Arakawa', 'Drama', 'Unavailable'),
('My Hero Academia', 'Kohei Horikoshi', 'Superhero', 'Unavailable'),
('One Piece', 'Eiichiro Oda', 'Adventure', 'Available'),
('Naruto', 'Masashi Kishimoto', 'Action', 'Available'),
('Hunter x Hunter', 'Yoshihiro Togashi', 'Adventure', 'Unavailable'),
('Fullmetal Alchemist: Brotherhood', 'Hiromu Arakawa', 'Fantasy', 'Available'),
('Death Note', 'Tsugumi Ohba', 'Thriller', 'Unavailable'),
('Demon Slayer: Kimetsu no Yaiba', 'Koyoharu Gotouge', 'Action', 'Available'),
('Sword Art Online', 'Reki Kawahara', 'Fantasy', 'Unavailable'),
('Tokyo Ghoul', 'Sui Ishida', 'Horror', 'Unavailable'),
('Jujutsu Kaisen', 'Gege Akutami', 'Action', 'Unavailable'),
('No Game No Life', 'Yuu Kamiya', 'Fantasy', 'Unavailable'),
('A Silent Voice', 'Yoshitoki Oima', 'Drama', 'Available'),
('Dragon Ball Z', 'Akira Toriyama', 'Action', 'Available');
INSERT INTO BorrowedBooks (BookID, UserID, BookName, UserName, Date, Returned) VALUES
(1, 1, 'Attack on Titan', 'Ali', '2024-12-01', 'Borrowed'),
(2, 2, 'Kaguya-sama: Love is War', 'depo', '2024-11-20','Borrowed'),
(3, 3, 'Your Lie in April', 'm4m4', '2024-11-25', 'Returned');

use Library;
select * from Book;
select * from BorrowedBooks;



