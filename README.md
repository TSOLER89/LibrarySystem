Part 2 – Entity Framework & Blazor
Overview

In Part 2 of the project, the library system was extended with:

Entity Framework Core for persistent data storage

Blazor Server for the web user interface

Repository Pattern to separate data access from business logic

Unit tests to verify functionality

The application allows librarians to manage books, members, and loans through a modern web interface.


How to Run the Project
1. Clone the repository
git clone https://github.com/your-username/LibrarySystem.git
2. Navigate to the project folder
cd LibrarySystem
3. Restore dependencies
dotnet restore
4. Run the application
dotnet run --project LibrarySystem.Web

The application will start and open in the browser at:

https://localhost:7065
Database Model

The system uses Entity Framework Core with SQLite.

Three main entities are used:

Book
Id
ISBN
Title
Author
PublishedYear
IsAvailable
Member
Id
Name
Email
MemberSince
Loan
Id
BookId
MemberId
LoanDate
DueDate
ReturnDate
Database Relationships
Member 1 ----- * Loan
Book   1 ----- * Loan

Explanation:

One Member can have many Loans

One Book can appear in many Loans

Each Loan references one Book and one Member

Blazor Web Interface

The Blazor Server application contains the following pages:

Dashboard (/)

Displays system statistics:

Total books

Total members

Active loans

Available vs borrowed books

Overdue loans

Books (/books)

Features:

View all books

Search by title, author, ISBN, or year

Sort by columns

Add new books

Edit book information

Delete books

View detailed book information

Members (/members)

Features:

View all members

Register new members

Edit member information

View member details

Loans (/loans)

Features:

Create a new loan

Return borrowed books

Display active loans

Highlight overdue loans

Unit Testing

Unit tests are implemented using xUnit.

Tests verify:

Book repository operations

Loan creation and return logic

Search functionality

Business rules

Overdue loan detection

The test project contains more than 10 tests, fulfilling the assignment requirements.

Run tests with:
dotnet test


Application Screenshots

Dashboard
<img width="960" height="459" alt="Dashboard" src="https://github.com/user-attachments/assets/b89fcc94-1b57-41ab-9414-c750f0299806" />

Books Page
<img width="953" height="429" alt="Boklista" src="https://github.com/user-attachments/assets/c7727ff1-f526-439e-8bdb-0700fddab9b5" />


Members Page
<img width="948" height="451" alt="Medlemmar" src="https://github.com/user-attachments/assets/575abca6-b4df-4c14-b210-a4bb4cd467ec" />


Loans Page

<img width="942" height="434" alt="lån" src="https://github.com/user-attachments/assets/86c60491-abbd-436a-bccc-c63c92b33e51" />


Technologies Used
.NET 8
ASP.NET Core
Blazor Server
Entity Framework Core
SQLite
xUnit
Bootstrap


Summary

This project demonstrates:

Entity Framework database integration

Blazor component-based UI

Clean project structure

Repository pattern

Dependency injection

Unit testing

Responsive user interface

The system fulfills the requirements for Entity Framework integration, Blazor UI, database persistence, and automated testing.
