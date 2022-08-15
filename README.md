# Library_Management_System
A library management system is software that is designed using **ASP.NET MVC** to manage all the functions of a library . It helps librarian to maintain the database of new books and the books that are borrowed by members along with their due dates. It also helps a user to borrow and return a book online.
### Features
> For Admin
 * Can *Approve* or *Reject* the request for any book.
 * Can perform *CRUD* operations on Account, Book, Author, Publisher tables.
 * Can View all the past *Records* of each user.
> For User
* Can views all the books and *Request* any book.
* Can view all the issued books and *Return* them.
* Can view all of his past *Records*.

> Landing Page


![Screenshot (7)](https://user-images.githubusercontent.com/53620724/184606936-f524a1be-d95e-4d59-9430-db584acfeeb7.png)

> Login Page

![Screenshot (9)](https://user-images.githubusercontent.com/53620724/184606994-59cb4ad1-1e4d-4d44-85ed-01fda64bed85.png)

> View Books Page



![Screenshot (17)](https://user-images.githubusercontent.com/53620724/184607087-6f6f5f11-0802-4086-9be7-32fe83c573f4.png)


> Issue Records


![Screenshot (19)](https://user-images.githubusercontent.com/53620724/184607202-fb4b5afb-b3b1-4173-b1da-2962c5848c8d.png)

>User History

![Screenshot (18)](https://user-images.githubusercontent.com/53620724/184607273-f72cd552-cbd7-4a63-acb3-5b48e8a00676.png)

>Book Request Aproval Page

![Screenshot (21)](https://user-images.githubusercontent.com/53620724/184607398-e600c6a5-4f52-4c0f-afb8-7369fb8de05a.png)


## To get Started
### Softwares
* Microsoft Visual Studio Community 2022 (64-bit) Version **17.3.0**
* Microsoft SQL Server Management Studio Version **18.2.1**
* .NET Framework Version **5.0.17**

### Packages
> Inside your Visual Stdio Navigate to 
**Tools** > **NuGet Package Manager** > **Manage NuGet Packages for Solution**
and install these packages

* Microsoft.EntityFrameworkCore Version **5.0.17**
* Microsoft.EntityFrameworkCore.Design Version **5.0.17**
* Microsoft.EntityFrameworkCore.Tools Version **5.0.17**
* Microsoft.EntityFrameworkCore.SqlServer Version **5.0.17**

### Commands
> Inside your Visual Stdio Navigate to 
**Tools** > **NuGet Package Manager** > **Package Manager Console**
and enter these commands
```sh
Add-Migration <Migration Name>
```
```sh
Update-Database
```
Make sure to update [appsettings.json](https://github.com/KDI-pulkit/Library_Management_System/blob/master/LibraryManagementSystem/appsettings.json) with the **Database Name** you want to give.
```sh
"DBConnection": "Server=localhost;Database=<Database Name>;Trusted_Connection=True;"
```

> Schema Diagram

![Screenshot (24)](https://user-images.githubusercontent.com/53620724/184608059-9bca6bcc-e062-4307-8d50-cb30a7e53e6f.png)


