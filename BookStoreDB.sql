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


create or alter procedure spForgetPassword
@EmailId varchar(50)
as
begin
	select * from Users where EmailId=@EmailId
end

create or alter procedure spResetPassword
@EmailId varchar(50),
@Password varchar(100)
as
begin
	update Users set Password=@Password where  EmailId=@EmailId
END