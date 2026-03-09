# Library System – Console Application (TDD)

Ett **konsolbaserat bibliotekssystem** skrivet i **C# (.NET)** som hanterar böcker, medlemmar och utlåningar. Systemet erbjuder sökning, statistik och enkel menyhantering i konsolen.

Projektet är utvecklat med **Test-Driven Development (TDD)** och följer objektorienterade principer som **inkapsling**, **komposition** och **polymorfism**.

---

## 📚 Features

### Huvudfunktioner
- ✅ **Hantera böcker** - Lägg till, sök och visa böcker
- ✅ **Hantera medlemmar** - Registrera medlemmar med email och medlemsdatum
- ✅ **Låna och returnera böcker** - Komplett lånehantering med förfallodatum
- ✅ **Förseningsavgifter** - Automatisk beräkning ($5 per dag)
- ✅ **Sök- och sorteringsfunktioner** - Case-insensitive sökning
- ✅ **Statistik** - Totalt antal böcker, utlånade böcker, mest aktiv medlem
- ✅ **Robust felhantering** - Validering och tydliga felmeddelanden

---

## 🎯 Objektorienterade Principer

### Inkapsling
- **Book** - Properties med `private set`, ISBN är `readonly`
- **Member** - Inkapslade listor över lån, email och medlemskap
- **Loan** - Beräknade properties (`IsOverdue`, `IsReturned`)

### Komposition
`Library` använder tre specialiserade service-klasser:
- **BookCatalog** - Hanterar alla böcker och sökning
- **MemberRegistry** - Hanterar alla medlemmar
- **LoanManager** - Hanterar utlåning och returer

### Interface & Polymorfism
- **ISearchable** - Implementerat av `Book`
- Enhetlig sökfunktionalitet genom interface

### Algoritmer
- **Sökfunktion** - LINQ `Where` med case-insensitive sökning
- **Sortering** - LINQ `OrderBy` för alfabetisk sortering
- **Statistik** - LINQ `GroupBy`, `Count`, `Sum` för analys

---


### Köra programmet

1. Öppna lösningen i Visual Studio eller via terminal
2. Kör testerna:

```powershell
dotnet test
```

3. Starta konsolapplikationen:

```powershell
dotnet run --project LibrarySystem
```

---

## Overview

## Features

### Core Domain

* **Book**
  Represents a library book with title, author, and ISBN.

* **Member**
  Represents a library member with an ID and name.

* **Loan**
  Represents a book loan with a member and loan date.

---

### Library Functionality

* Total number of books
* Number of borrowed books
* Most active member (member with the most loans)

---

### Catalog & Search

* Search books by title, author, or ISBN
* Sorting books alphabetically by title
* Polymorphism via `ISearchable` interface

---

### Console Application

A simple console program (`Program.cs`) demonstrates how the system can be used and outputs basic statistics.

---

## Technologies Used

* **C# (.NET)**
* **xUnit** for unit testing
* **LINQ** for filtering, grouping, and sorting
* **Git** for version control

---


## Testing & TDD Approach

This project strictly follows **Test-Driven Development**:

1. **RED** – Write a failing test
2. **REFACTOR** – Improve code 
3. **GREEN** – Write minimal code to pass the test
   
### Testöversikt

**19 enhetstester** med xUnit som täcker alla krav och mer:

| Testkategori | Antal tester | Täcker |
|--------------|--------------|--------|
| **Book-klassen** | 4 tester | Constructor, properties, GetInfo(), IsAvailable |
| **Loan-klassen** | 6 tester | IsOverdue, CalculateLateFee, IsReturned, Theory-tester |
| **Member-klassen** | 3 tester | BorrowedBooks, AddLoan, HasOverdueBooks |
| **Sökning (ISearchable)** | 3 tester | Matches(), case-insensitive, edge cases |
| **Statistik** | 3 tester | TotalBooks, BorrowedBooks, MostActiveMember |
| **Totalt** | **19 tester** | **100% av kraven** |

### Testtekniker som används
- ✅ **[Fact]** - Enskilda testfall med specifika förutsättningar
- ✅ **[Theory]** med **[InlineData]** - Parametriserade tester med flera datavärden
- ✅ **AAA-mönstret** - Arrange, Act, Assert för tydlig teststruktur
- ✅ **Edge cases** - Testar gränsfall som tomma listor, null-värden, försenade lån
- ✅ **Negativa tester** - Verifierar felhantering och validering
  
Each feature is backed by unit tests, and the Git history clearly shows the progression from failing tests to working implementations.

Run all tests with:

```powershell
dotnet test
```


## How to Run

1. Clone the repository
2. Navigate to the solution folder
3. Run the application:

```powershell
dotnet run
```
---

## Author

Developed as part of a programming assignment focusing on clean code, testing, and maintainable design.

---

## Notes

This project intentionally keeps the console UI minimal to focus on **logic, structure, and testability** rather than presentation.
