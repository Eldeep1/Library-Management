# Library Management System

This project is a **Library Management System** built with an MVVM architecture pattern to efficiently manage books and members of a library. The application allows users to add, edit, delete, and search books and members, along with handling borrowing transactions. It leverages functional programming principles for modularity and maintainability, with some exceptions for GUI updates due to the mutable nature of state changes.

---

## Setup Instructions

> ⚠️ **Note**: These instructions will be moved to the bottom section once all necessary steps regarding the functional and primitive paradigms have been implemented.

Follow these steps to set up and run the project successfully:

1. **Prepare the Database**  
   Run the provided SQL queries to set up the database structure and seed it with initial data.  
   Ensure the MySQL password is set to **1234**, as specified in the `Services` folder configuration.  
   This will save you time troubleshooting connection issues.


2. **Project Configuration**
- When setting up the project in your development environment, ensure that you run Library.Desktop, not Library.
- You can find this option in the "Configure Setup Projects" section of your IDE.
- Failing to do this might result in misconfigurations or build errors.

3. **Verify Database Connection**
- Run the application and confirm it connects to the database without issues.
- Check that all functionalities like adding and searching books/members work as expected.

4. **For Functional Paradigm Developers**

- Functional programming focuses on immutability and avoids side effects. Start by identifying functions in the ViewModel that could be refactored to be pure functions.
- Apply functional paradigms to logic-intensive parts of the application such as AddBooksViewModel, EditBooksViewModel, etc.
- Highlight and avoid mutable state unless necessary for GUI updates.

5. **For all Developers**
- __upload your work to a branch different than main__
- becareful not to change any variable name as its related to the GUI
- try not to use built-in functions as much as you can
- feel free to contact me whenver you face a problem

## Features
- Book Management: Add, edit, delete, and search for books based on name, author, or genre.
- Member Management: Add, edit, delete, and search for library members.
- Borrowing Transactions: Assign books to members with a record of borrowing history.
- Search Functionality: Filter books and members dynamically with optimized in-memory searches.
- MVVM Architecture: Clean separation of concerns with ViewModel-based data binding.
- Functional Paradigm Application: Functional principles applied to enhance code readability and robustness.

## Screenshots
<p float="left">
  <img src="screenshots/1.png?raw=true" width="49%" height="auto" alt="book managing view" />
  <img src="screenshots/4.png?raw=true" width="49%" height="auto" alt="borrowed books history" />
</p>
<p float="left">
  <img src="screenshots/5.png?raw=true" width="49%" height="auto" alt="available books report" />
  <img src="screenshots/6.png?raw=true" width="49%" height="auto" alt="member borrow history" />
</p>

## MVVM Architecture
-  Primary Codebase: The main logic resides inside the ViewModel directory.
- Not Pure MVVM: While the project adheres to the MVVM pattern for the majority of its logic, certain exceptions were necessary:
- - The EditMember, AddMember, EditBooks, and AddBooks pages contain logic directly in the view layer.
This was required to handle the opening of new pages and initializing specific code for those pages.
