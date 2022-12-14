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
--add books--
create procedure spAddBook(
	@BookName varchar(100),
	@AuthorName varchar(100),
	@Rating float,
	@ReviewerCount int,
	@DiscountPrice int,
	@OriginalPrice int,
	@BookDetail varchar(max),
	@BookImage varchar(max),
	@BookQuantity int
	)
as
begin
	insert into Book
	values(@BookName,@AuthorName,@Rating,@ReviewerCount,@DiscountPrice,@OriginalPrice,@BookDetail,@BookImage,@BookQuantity);
end

--Update book--
create procedure spUpdateBook(
	@BookId int,
	@BookName varchar(100),
	@AuthorName varchar(100),
	@Rating float,
	@ReviewerCount int,
	@DiscountPrice int,
	@OriginalPrice int,
	@BookDetail varchar(max),
	@BookImage varchar(max),
	@BookQuantity int
	)
as 
begin
	update Book set 
	BookName= @BookName,
	AuthorName= @AuthorName,
	Rating = @Rating,
	ReviewerCount= @ReviewerCount,
	DiscountPrice = @DiscountPrice,
	OriginalPrice = @OriginalPrice,
	BookDetail= @BookDetail,
	BookImage = @BookImage,
	BookQuantity = @BookQuantity
	where BookId = @BookId;
end

--Delete book--
create procedure spDeleteBook(
	@BookId int
	)
as
begin
	delete from Book where BookId=@BookId;
end

--Get all books--
create procedure spGetAllBooks
as
begin
	select * from Book;
end

--Get book by id--
create procedure spGetBookById(
	@BookId int
	)
as
begin
	select * from Book where BookId=@BookId;
end

----wishlist table---
create table Wishlist(
	WishlistId int identity (1,1) primary key,
	UserId int not null foreign key (UserId) references Users(UserId),
	BookId int not null foreign key (BookId) references Book(BookId)
	)

--select table--
select * from Wishlist;

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
		books.BookName,books.BookImage,books.AuthorName,books.DiscountPrice,books.OriginalPrice		
		from WishList wish inner join Book books
		on wish.BookId = books.BookId
		where wish.UserId = @UserId;
end

create table Cart(
	CartId int identity(1,1) primary key,
	BookQuantity  int default 1,
	UserId int not null foreign key (UserId) references Users(UserId),
	BookId int not null foreign key (BookId) references Book(BookId)
)

--select table--
select * from Cart

--Add to cart--
create procedure spAddToCart(
	@BookId int,
	@BookQuantity int,
	@UserId int
	)
as
begin
	select * from Cart where BookId=@BookId and UserId=@UserId
	begin
		insert into Cart(BookId,UserId)
		values(@BookId,@UserId);
	end
end


--update cart--
create procedure spUpdateCart(
	@CartId int,
	@BookQuantity int
	)
as
begin
	update Cart set BookQuantity=@BookQuantity where CartId=@CartId;
end

--remove from cart--
create procedure spRemoveFromCart(
	@CartId int
	)
as
begin
	delete from Cart where CartId=@CartId;
end

--get all cart items--
create procedure spGetAllCartItem(
	@UserId int
	)
as
begin
	select cart.CartId,cart.BookId,cart.BookQuantity,cart.UserId,
		books.BookName,books.BookImage,books.AuthorName,books.DiscountPrice,books.OriginalPrice from Cart cart inner join Book books 
		on books.BookId=cart.BookId where cart.UserId = @UserId;
end


--Table for Address type--
create table AddressType(
	TypeId int identity(1,1) primary key,
	AddType varchar(100)
	)

--adding types--
insert into AddressType values('Home');
insert into AddressType values('Work');
insert into AddressType values('Other');

--select table--
select * from AddressType;

--table for Address info--

create table Address(
	AddressId int identity(1,1) primary key,
	Address varchar(max) not null,
	City varchar(100) not null,
	State varchar(100) not null,
	TypeId int not null foreign key (TypeId) references AddressType(TypeId),
	UserId int not null foreign key (UserId) references Users(UserId)
	)

--select table--
select * from Address;

--add address--
create procedure spAddAddress(
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int,
	@UserId int
	)
as
begin
	insert into Address
	values(@Address,@City,@State,@TypeId,@UserId);
end

--update address--
create procedure spUpdateAddress(
	@AddressId int,
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int,
	@UserId int
	)
as
begin
	update Address set
	Address=@Address,City=@City,State=@State,TypeId=@TypeId where UserId=@UserId and AddressId=@AddressId;
end

--update address--
create procedure spDeleteAddress
@AddressId int
as
begin
	delete from Address where AddressId = @AddressId;
end

--order table--
create table Orders(
	OrderId int identity(1,1) primary key,
	OrderQty int not null,
	TotalPrice float not null,
	OrderDate Date not null,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
	BookId INT NOT NULL FOREIGN KEY REFERENCES Book(BookId),
	AddressId int not null FOREIGN KEY REFERENCES Address(AddressId)
	)

--select table--
select * from Orders

create procedure spAddOrder(
	@UserId int,
	@BookId int,
	@AddressId int
	)
as
	declare @TotalPrice int;
	declare @OrderQty int;
begin
	set @TotalPrice = (select DiscountPrice from Book where BookId = @BookId); 
	set @OrderQty = (select BookQuantity from Cart where BookId = @BookId); 
	if(exists(select * from Book where BookId = @BookId))
	begin
		Begin try
			Begin Transaction
				if((select BookQuantity from Book where BookId = @BookId)>= @OrderQty)
				begin
					insert into Orders values(@OrderQty,@TotalPrice*@OrderQty,GETDATE(),@UserId,@BookId,@AddressId);
					update Book set BookQuantity = (BookQuantity - @OrderQty) where BookId = @BookId;
					delete from Cart where BookId = @BookId and UserId = @UserId; 
				end
			commit Transaction
		End try

		Begin Catch
				rollback;
		End Catch
	end
end

/*
SELECT UserId FROM Users WHERE EmailId = 3
*/

select * from Users
select * from Book
select * from Wishlist
select * from Cart
select * from Orders
select * from Address;