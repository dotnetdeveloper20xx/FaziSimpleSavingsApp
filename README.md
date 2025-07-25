# FaziSimpleSavings – Your Personal Savings Assistant

## What FaziSimpleSavings Will Do for You

**FaziSimpleSavings** is designed to help you take control of your savings, set clear financial goals, and track your progress with ease. Whether you're saving for an emergency fund, a vacation, or a new gadget, our app makes it simple to plan, track, and achieve your financial goals.

## How It Works – Step by Step

### 1. Create Your Account
- **Easy Sign-Up:** Simply create an account by entering your details, and you'll be ready to start managing your savings.
- **Secure Login:** Log in with your username and password, and rest assured that your data is kept secure with industry-standard encryption.

### 2. Set Your Savings Goals
- **Create New Goals:** Whether it’s an emergency fund, a travel fund, or something else, you can set multiple savings goals.
- **Define Target Amounts:** Set a target for each goal, so you know exactly how much you need to save.

### 3. Track Your Progress
- **Progress Dashboard:** On your dashboard, you’ll see a clear, easy-to-read overview of each of your savings goals, including how much you’ve saved and how far you are from your target.
- **Transaction History:** You can track every deposit made towards your goals with a simple history of your transactions.

### 4. Make Deposits
- **One-Time Deposits:** You can deposit money directly into your savings goals whenever you want.
- **Recurring Deposits:** Set up automatic weekly or monthly deposits to keep your savings plan on track without having to think about it.

### 5. Stay Motivated
- **Visual Milestones:** See your savings grow in real-time with progress bars and percentage indicators. The closer you get to your goal, the more motivated you'll feel to reach it.
- **Notifications:** Get friendly reminders about upcoming deposits or when you hit new milestones, ensuring you never miss an opportunity to save.

### 6. Achieve Your Goals
- Once you hit your savings target, you’ll be notified, and you can choose to withdraw or roll your savings into a new goal.

## Why You Should Use FaziSimpleSavings

- **Easy to Use:** The app is designed to be intuitive and straightforward, making savings less complicated.
- **Flexibility:** Whether you prefer one-time deposits or recurring payments, FaziSimpleSavings offers the flexibility to meet your unique financial situation.
- **Secure:** Your data and financial information are always kept safe, using the latest encryption and security measures.
- **Accessible Anytime, Anywhere:** Whether you're at home or on the go, you can access your savings goals through a responsive web app.
- **Stay on Track:** FaziSimpleSavings keeps you motivated with progress tracking and notifications, ensuring you're always aware of your savings status.

# FaziSimpleSavings – Architect's Overview

## Project Overview

**FaziSimpleSavings** is a personal savings tracking app that helps users set savings goals, track their progress, and make deposits. The app is simple, secure, and designed to help you manage your savings effectively. Built with modern technologies, it’s a great way to showcase how a full-stack web application can be developed and deployed.

The app is user-friendly, secure, and scalable, so it can grow as your needs grow. This is a perfect example of how to build a modern, cloud-based application using a variety of technologies.

## Development Goals

### 1. **Scalable & Cloud-Based**
   We want the app to handle lots of users without slowing down. That's why we’ve built it to run on **Azure** and use **CosmosDB** for fast, scalable data storage. 

### 2. **Simple & Secure**
   Security is important! Users' data (like savings goals and transactions) is kept safe with **JWT** authentication and strong encryption.

### 3. **User-Centered Design**
   The goal is to make the app easy for anyone to use. The interface is clean, intuitive, and works seamlessly across both desktop and mobile devices, thanks to **Angular**.

### 4. **Maintainable & Modular Code**
   The code is written in a clean, modular way, making it easy for developers to maintain, fix bugs, or add new features in the future.

### 5. **Data Integrity**
   Whether it's tracking goals or storing transactions, we make sure everything is consistent and reliable by using **SQL Server** and **CosmosDB** for data storage.

### 6. **Cloud-Native Architecture**
   The app is designed to run on **Azure**, taking full advantage of cloud services like **App Services** for hosting, and **CosmosDB** for scalable NoSQL storage.

## Key Technologies

### **Backend (API & Server-side)**

- **ASP.NET Core**: The backend is built with **ASP.NET Core**, which is a high-performance framework for building web APIs. It ensures the app runs smoothly and is easy to maintain.
  
- **C#**: The app's logic is implemented in **C#**, offering strong typing and great performance for backend development.

- **SQL Server**: For structured, transactional data (like user accounts and transactions), we use **SQL Server** to make sure everything stays accurate.

- **CosmosDB**: For scalable, flexible storage of data like savings goals and progress tracking, we use **CosmosDB**. It’s great for handling large amounts of data with low latency.

- **Azure App Services**: The backend is hosted on **Azure App Services**, which provides easy deployment, scaling, and management of our app.

### **Frontend (Client-side)**

- **Angular**: The frontend is built with **Angular**, a powerful framework for building responsive web apps. It's fast, scalable, and great for creating dynamic user interfaces.

- **TypeScript**: We use **TypeScript** for better maintainability and error prevention in the Angular app.

- **HTML/CSS**: Standard web technologies that ensure the app looks great and works well on any device.

### **Authentication & Security**

- **JWT (JSON Web Tokens)**: User authentication is handled with **JWT**, allowing for secure login and protecting sensitive endpoints.

- **OAuth2 (optional)**: If you want to allow users to log in with their Google or Facebook accounts, **OAuth2** can be easily integrated.

### **Deployment & Hosting**

- **Azure DevOps**: We use **Azure DevOps** to automate our build and deployment process. This ensures fast, reliable updates to the app with minimal downtime.

- **Docker (optional)**: If needed, the app can be containerized using **Docker** for consistent deployment across different environments.

### **Development Tools**

- **Visual Studio**: The primary tool for building the ASP.NET Core backend.
  
- **Visual Studio Code**: Lightweight code editor used for developing the Angular frontend.

- **Postman**: Useful for testing and documenting the API endpoints.

- **Git & GitHub**: Version control and collaboration are done through **Git** and **GitHub**, ensuring smooth teamwork and code sharing.

### **Testing & Quality Assurance**

- **Unit Testing**: We write **unit tests** for the backend to ensure our business logic works as expected.

- **Integration Testing**: We test the communication between the backend and database to ensure everything is working together.

- **End-to-End Testing**: The frontend is tested with **end-to-end tests** to ensure the user interface works smoothly.

---

## Conclusion

**FaziSimpleSavings** is built with modern, scalable, and secure technologies, ensuring that it can handle a growing user base while providing a seamless experience. With a user-friendly interface and a cloud-native backend, the app is perfect for managing savings goals and tracking progress.

This project not only demonstrates technical skills in backend, frontend, and cloud architecture but also provides a useful tool for personal savings management.

Whether you're saving for an emergency fund or a vacation, **FaziSimpleSavings** makes managing your savings goals easy, secure, and reliable.

# FaziSimpleSavings – Lead Developer’s Project Overview & Breakdown (Clean Architecture & Web API)

## Project Overview

**FaziSimpleSavings** is a personal savings tracking application that allows users to create, track, and achieve their savings goals. The app follows **Clean Architecture** principles for organizing the codebase and implements a **Web API** backend that can be easily consumed by any frontend framework, such as **Angular**, **React**, or **Vue**.

The app is built using modern technologies, including **ASP.NET Core**, **SQL Server**, **CosmosDB**, and **Azure**, ensuring scalability, maintainability, and security. As the lead developer, my responsibility is to ensure that development tasks follow the principles of **Clean Architecture** and ensure the backend API can be consumed by any frontend.

---

## **Backend Development Breakdown (ASP.NET Core & C# with Clean Architecture)**

### 1. **Setup Project Infrastructure**
   - **Task:** Set up a new **ASP.NET Core** Web API project following **Clean Architecture** principles.
   - **Subtasks:**
     - Create the solution and projects: **Core**, **Application**, **Infrastructure**, and **WebAPI**.
     - **Core**: Contains the domain models, interfaces, and business rules.
     - **Application**: Contains application logic, commands, queries, and services.
     - **Infrastructure**: Handles data access (SQL Server, CosmosDB), file storage, and external service integrations.
     - **WebAPI**: The entry point (API controllers) that exposes the application to the frontend.

### 2. **Define Core Models & Entities**
   - **Task:** Design domain models (Entities) in the **Core** project.
   - **Subtasks:**
     - Define **User**, **SavingsGoal**, and **Transaction** models with business rules and validation.
     - These models will be used by other layers and should be free of dependencies from external libraries (e.g., Entity Framework).

### 3. **Implement Application Layer (Business Logic)**
   - **Task:** Implement **Commands** and **Queries** to handle business logic in the **Application** layer.
   - **Subtasks:**
     - Use **CQRS (Command Query Responsibility Segregation)** to separate data modification (Commands) and data retrieval (Queries).
     - Implement **MediatR** to handle command/queries and ensure clean separation of concerns.
     - Example commands: CreateGoalCommand, DepositMoneyCommand.
     - Example queries: GetGoalByIdQuery, GetAllGoalsQuery.

### 4. **Database Access & Persistence (Infrastructure Layer)**
   - **Task:** Implement data access logic for **SQL Server** and **CosmosDB** in the **Infrastructure** layer.
   - **Subtasks:**
     - Implement **Entity Framework Core** in the **Infrastructure** layer for relational data (user accounts, transaction history).
     - Implement **CosmosDB** repository for savings goals and progress tracking (NoSQL).
     - Implement repository interfaces in the **Core** project, and implement them in **Infrastructure**.

### 5. **API Implementation (WebAPI Layer)**
   - **Task:** Implement the Web API using controllers that interact with the **Application** layer.
   - **Subtasks:**
     - Create **API Controllers** (e.g., **GoalController**, **UserController**) in the **WebAPI** project.
     - Each controller should only handle HTTP requests and delegate business logic to the **Application** layer via **MediatR** commands/queries.
     - Example endpoint: `POST /api/goals` for creating a new savings goal.
   
### 6. **User Authentication & Authorization (Security)**
   - **Task:** Implement **JWT-based authentication** in the **WebAPI**.
   - **Subtasks:**
     - Implement user registration and login endpoints in **UserController**.
     - Use **JWT tokens** for secure API access and implement authorization middleware to protect routes.
     - Ensure users can only access their own goals and data through role-based authorization.

### 7. **Implement Deposit Functionality**
   - **Task:** Implement deposit functionality with both one-time and recurring deposits in the **Application** layer.
   - **Subtasks:**
     - Use **Command** and **Handler** in **Application** to process deposits.
     - Implement the business logic to update the goal progress and transaction history.
     - Implement necessary validation in the **Application** layer (e.g., deposit amount cannot exceed available balance).

### 8. **Set up Background Tasks (Optional for Recurring Deposits)**
   - **Task:** Use **Azure Functions** or **Hangfire** for recurring deposits.
   - **Subtasks:**
     - Set up background processing to handle recurring deposits on a schedule.
     - Implement functions that run periodically to deposit amounts into goals automatically.

### 9. **Testing & Quality Assurance**
   - **Task:** Implement unit and integration tests in the **Core** and **Application** layers.
   - **Subtasks:**
     - Use **xUnit** for unit tests.
     - Mock dependencies like the database using **Moq** or **NSubstitute**.
     - Write integration tests to ensure the Web API and database interactions work as expected.

### 10. **Deployment to Azure**
   - **Task:** Deploy the backend API to **Azure App Services**.
   - **Subtasks:**
     - Set up **CI/CD pipeline** using **Azure DevOps** or **GitHub Actions** for automated deployments.
     - Ensure proper connection between the backend API, **SQL Server**, and **CosmosDB** in Azure.

---

## **Frontend Development Breakdown (Angular)**

### 1. **Setup Angular Project**
   - **Task:** Set up an Angular project to consume the Web API.
   - **Subtasks:**
     - Create the Angular app and install necessary dependencies (e.g., **HttpClient**, **Angular Material**).
     - Set up environment configurations for API URLs, including staging and production settings.

### 2. **Implement Authentication**
   - **Task:** Create authentication service and implement login and registration UI.
   - **Subtasks:**
     - Implement **AuthService** to handle login, registration, and token management.
     - Use **JWT** tokens for secure access to API endpoints.
     - Store JWT in **localStorage** and attach it to requests using **HttpInterceptor**.

### 3. **Create UI for Managing Savings Goals**
   - **Task:** Develop the dashboard and UI for creating and viewing savings goals.
   - **Subtasks:**
     - Implement **DashboardComponent** to display a list of savings goals.
     - Implement forms for creating and editing goals in **GoalComponent**.
     - Integrate with the **GoalService** to fetch data from the Web API.

### 4. **Implement Deposit UI and Functionality**
   - **Task:** Build UI components for making one-time and recurring deposits.
   - **Subtasks:**
     - Implement **DepositComponent** for making deposits into savings goals.
     - Display progress bars showing how close users are to reaching their goals.
     - Make calls to the backend API to create deposit transactions and update goal progress.

### 5. **Responsive Design & Mobile-First Approach**
   - **Task:** Ensure the app works well on mobile devices.
   - **Subtasks:**
     - Use **CSS Flexbox** and **Media Queries** for a responsive layout.
     - Test the app on different screen sizes (mobile, tablet, desktop).

### 6. **API Integration & Error Handling**
   - **Task:** Integrate frontend with the backend API and handle errors gracefully.
   - **Subtasks:**
     - Implement **ErrorInterceptor** to handle API errors (e.g., unauthorized access, bad requests).
     - Display appropriate messages to users based on the error response from the API.

### 7. **Testing & Quality Assurance**
   - **Task:** Write unit and end-to-end (E2E) tests for frontend components.
   - **Subtasks:**
     - Use **Jasmine** and **Karma** for unit testing Angular services and components.
     - Set up **Protractor** or **Cypress** for E2E testing of the full user flow.

### 8. **Deployment to Azure**
   - **Task:** Deploy the Angular frontend to **Azure Static Web Apps**.
   - **Subtasks:**
     - Optimize Angular for production build.
     - Set up **CI/CD pipeline** for deploying the frontend to Azure.

---

## Final Thoughts

By following **Clean Architecture** principles, the backend of **FaziSimpleSavings** is well-structured, maintainable, and scalable. The **Web API** design ensures that it can be easily consumed by any frontend technology (such as Angular, React, or Vue), making the app flexible for future changes in the frontend stack.

As the lead developer, I will oversee both backend and frontend development, ensuring smooth coordination, clean code practices, and seamless integration. The application will be fully testable, deployable, and capable of evolving to meet future requirements.



# **FaziSimpleSavings - Domain Entities Overview**

## **1. User**
   - **Relationship**: The **User** is at the core of the application. A **User** can have multiple **SavingsGoals**, **Transactions**, **RecurringDeposits**, and **Notifications**.
   - **Need**: This entity is essential for authenticating and identifying the person interacting with the app. Users manage their savings goals, track progress, and interact with the system.
   - **Part in the Project**:
     - **Authentication & Authorization**: The **User** entity ties into the user registration and login processes.
     - **Personalization**: Each user has their own set of goals, transactions, and settings.
     - **Profile Management**: The **User** entity stores the basic details and can be updated by the user themselves (name, email, etc.).

---

## **2. SavingsGoal**
   - **Relationship**: A **SavingsGoal** belongs to a **User** (via `UserId`) and can have multiple **Transactions** and **RecurringDeposits** associated with it. A goal can be linked to a **GoalCategory** for better organization.
   - **Need**: This is the primary entity that users interact with. Users create savings goals (e.g., Emergency Fund, Vacation Fund), track progress, and make deposits toward them.
   - **Part in the Project**:
     - **Goal Creation & Tracking**: Each user can create multiple savings goals. This entity helps in tracking how much they’ve saved, how close they are to their target, and whether they have achieved their goal.
     - **Financial Planning**: It helps users break down their savings strategy into concrete, actionable steps (e.g., monthly deposits for an emergency fund).
     - **Business Logic**: Contains the logic for handling goal progress, deposits, and achieving goals (methods like `AddDeposit` and `IsGoalAchieved`).

---

## **3. Transaction**
   - **Relationship**: **Transactions** are linked to both a **User** and a **SavingsGoal**. Each **Transaction** records the deposit made by a user towards a goal. It has properties like `Amount` and `TransactionDate`.
   - **Need**: This entity tracks the movement of money into savings goals. Every time a user deposits money towards a goal, a **Transaction** is created.
   - **Part in the Project**:
     - **Deposit History**: **Transactions** are crucial for maintaining the history of all deposits made towards a goal.
     - **Financial Reporting**: By tracking the amounts deposited over time, users can view their deposit history and evaluate their progress.

---

## **4. RecurringDeposit**
   - **Relationship**: A **RecurringDeposit** is associated with a **User** and a **SavingsGoal**. It schedules automatic deposits, tied to specific intervals (e.g., monthly).
   - **Need**: This entity is important for users who want to automate their savings process. Instead of manually depositing money, a **RecurringDeposit** ensures that money is deposited into a goal automatically at specified intervals.
   - **Part in the Project**:
     - **Automated Savings**: Facilitates recurring deposits (e.g., monthly) to ensure consistency in saving for a specific goal.
     - **Background Processing**: Works with background services (like **Azure Functions**) to automate deposits, triggered based on the frequency and the next due date.
     - **User Convenience**: Provides a hands-off savings approach that doesn't require the user to manually initiate every deposit.

---

## **5. GoalCategory**
   - **Relationship**: A **GoalCategory** belongs to a **User** and can be linked to multiple **SavingsGoals**. It helps organize savings goals based on their purpose.
   - **Need**: Users may have multiple goals for different purposes (e.g., “Vacation”, “Emergency Fund”). **GoalCategory** helps to group and organize goals under specific categories.
   - **Part in the Project**:
     - **Organizing Goals**: Helps users categorize and visualize their savings plans based on the goal type (e.g., an "Emergency Fund" category).
     - **User Customization**: Allows users to customize how they track their savings goals by giving them the ability to group them by category.

---

## **6. Notification**
   - **Relationship**: A **Notification** is associated with a **User**. It stores messages related to deposit reminders, goal milestones, or other relevant updates.
   - **Need**: Users need to be informed about their progress and reminders about deposits. **Notifications** keep users engaged and remind them about their next steps.
   - **Part in the Project**:
     - **User Engagement**: Notifications ensure users don’t miss important events (e.g., recurring deposit reminders, goal completion alerts).
     - **Reminders**: Sends reminders for pending deposits or milestones, encouraging users to keep saving and meet their financial goals.
     - **Interaction**: Users can interact with these notifications (e.g., marking them as read or dismissing them).

---

## **7. UserSettings**
   - **Relationship**: A **UserSettings** entity is tied to a specific **User**. It stores the user’s preferences, such as currency and notification settings.
   - **Need**: Users should have the ability to customize how they interact with the app, such as setting their preferred currency, enabling/disabling email notifications, etc.
   - **Part in the Project**:
     - **Personalization**: Provides customization for the user experience (e.g., currency formatting, notification preferences).
     - **User Preferences**: Ensures that the app adapts to individual user needs, making it more tailored and user-friendly.

---

## **Entity Relationship Diagram (ERD)**

1. **User** is central to the application and has a **one-to-many relationship** with **SavingsGoals**, **Transactions**, **RecurringDeposits**, **Notifications**, and **UserSettings**.
2. **SavingsGoal** can have a **one-to-many relationship** with **Transactions** and **RecurringDeposits**, and is associated with a **GoalCategory**.
3. **Transaction** belongs to a specific **User** and a specific **SavingsGoal**, recording all deposits made by the user to achieve a specific goal.
4. **RecurringDeposit** belongs to a specific **User** and **SavingsGoal**, ensuring recurring deposits are automatically processed.
5. **Notification** is linked to a **User** and alerts them about key events like upcoming deposits or goal completions.
6. **GoalCategory** belongs to a **User** and categorizes **SavingsGoals** into groups (e.g., "Vacation Fund").

---

## **Summary**

- **User** is the central entity that interacts with multiple aspects of the app, including savings goals, transactions, and notifications.
- **SavingsGoal** is where the user’s financial planning takes shape, allowing them to track the progress of their savings towards a specific target.
- **Transaction** tracks the actual flow of money into savings goals.
- **RecurringDeposit** automates regular savings towards a goal, reducing manual intervention.
- **GoalCategory** helps organize and categorize goals, making the user experience more structured and personalized.
- **Notification** ensures users are reminded of key events in their financial journey, keeping them engaged.
- **UserSettings** allows users to customize their app experience, adding a personal touch to their interaction.

These entities are essential for ensuring that the system handles all required functionalities of goal creation, transaction management, savings automation, categorization, and user interaction in a scalable and maintainable way.

---


