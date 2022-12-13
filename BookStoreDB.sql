create database BookStoreDB
use BookStoreDB

---Table for User--
create table Users (
	UserId int identity (1,1) primary key,
	FullName varchar(100) not null,
	EmailId varchar(100) not null,
	Password varchar(100) not null,
	MobileNumber varchar(100) not null
);

--select table--
select * from Users

--Stored procedures for user--

--Register--
create procedure spRegister(
	@FullName varchar(100),
	@EmailId varchar(100),
	@Password varchar(100),
	@MobileNumber varchar(100)
	)
as
begin
		insert into Users
		values(@FullName,@EmailId,@Password,@MobileNumber);
end

--Login--
create procedure spLogin(
	@EmailId varchar(100),
	@Password varchar(100)
	)
as
begin
	select * from Users where EmailId=@EmailId and Password=@Password;
end

--Forget Password--
create or alter procedure spForgetPassword
@EmailId varchar(50)
as
begin
	select * from Users where EmailId=@EmailId
end

--Reset Password--
create or alter procedure spResetPassword
@EmailId varchar(50),
@Password varchar(100)
as
begin
	update Users set Password=@Password where  EmailId=@EmailId
END

--Admin table--
create table Admin(
	AdminId int identity (1,1) primary key,
	FullName varchar(100) not null,
	EmailId varchar(100) not null,
	Password varchar(100) not null,
	MobileNumber bigint not null
);

select * from Admin

--inserting admin details--
insert into Admin 
values('Admin','admin@gmail.com','Admin@123',1234567890);

--admin login--
create procedure spAdminLogin(
	@EmailId varchar(100),
	@Password varchar(100)
	)
as
begin
	select * from Admin where EmailId=@EmailId and Password = @Password;
end

--Book table--
Create Table Book
(
	BookId int identity(1,1) primary key,
	BookName varchar(100) not null,
	AuthorName varchar(100) not null,
	Rating float,
	ReviewerCount int,
	DiscountPrice int not null,
	OriginalPrice int not null,
	BookDetail varchar(max) not null,
	BookImage varchar(max) not null,
	BookQuantity int not null 
)
select * from Book;

--Add Book--
create or alter procedure spAddBook
@BookName varchar(100), 
@AuthorName varchar(100), 
@Price bigint, 
@Description varchar(1000), 
@Rating varchar(100)
as
begin
insert into Book values (@BookName, @AuthorName, @Price, @Description, @Rating )
End

--Get All Books--
create or alter procedure spGetAllBooks
as
begin
select *from Book
End
--Get book by id--
create or alter procedure spGetBookbyId
@BookId bigint
as
begin
select *from Book where BookId=@BookId
End
--Update book --
create or alter procedure spUpdateBook
@BookId bigint,
@BookName varchar(100), 
@AuthorName varchar(100),
@Price bigint,
@Description varchar(1000), 
@Rating varchar(100)
as
begin
UPDATE Book set
BookName=@BookName, AuthorName=@AuthorName, Price=@Price, Description=@Description, Rating=@Rating
where BookId=@BookId
End

--Delete book --
create or alter procedure spDeleteBook
@BookId bigint
as
begin
DELETE from Book where BookId=@BookId
End

----wishlist table---
create table Wishlist(
	WishlistId int identity (1,1) primary key,
	UserId int not null foreign key (UserId) references Users(UserId),
	BookId bigint not null foreign key (BookId) references Book(BookId)
	)

--select table--
select * from Wishlist;

--stored procedure for wishlist--
--add to wishlist--
create procedure spAddToWishlist(
	@BookId bigint,
	@UserId int
	)
as
begin
	select * from Wishlist where BookId=@BookId and UserId=@UserId
	begin
		insert into Wishlist
		values(@UserId,@BookId);
	end
end

--remove from wishlist--
create procedure spRemoveFromWishlist(
	@WishlistId int
	)
as
begin
	delete from Wishlist where WishlistId = @WishlistId;
end

--get wishlist item--
create procedure spGetAllWishlistItem(
	@UserId int
	)
as
begin
	select wish.WishlistId,wish.BookId,wish.UserId,
		books.BookName,books.AuthorName,books.Price,books.Description,books.Rating		
		from WishList wish inner join Book books
		on wish.BookId = books.BookId
		where wish.UserId = @UserId;
end

create table Cart(
	CartId int identity(1,1) primary key,
	BookInCart int default 1,
	UserId int not null foreign key (UserId) references Users(UserId),
	BookId bigint not null foreign key (BookId) references Book(BookId)
)

--select table--
select * from Cart


--Add to cart--
create procedure spAddToCart(
	@BookId bigint,
	@BookInCart int,
	@UserId int
	)
as
begin
	if(not exists(select * from Cart where BookId=@BookId and UserId=@UserId))
	begin
		insert into Cart(BookId,UserId)
		values(@BookId,@UserId);
	end
end

/*
SELECT UserId FROM Users WHERE EmailId = 3
*/

select * from Users
select * from Book
select * from Wishlist
select * from Cart