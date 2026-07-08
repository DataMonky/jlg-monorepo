# Johns Lyng Group - Technical Assessment (Todo App)

A full-stack Todo list application featuring an Angular frontend and a .NET Web API backend. 

## 📌 Key Assumptions
- **Personal Use Only**: Built for local, single-user operation; no login or identity management is configured.
- **In-Memory Storage**: Data is managed entirely in-memory on the backend. No database server configuration or schema migrations are required.

## 📋 Prerequisites & Tooling
Ensure you have these baseline system versions installed before compiling:
- **.NET 10 SDK** (Required to compile the backend solution)
- **Node.js** (v22.22.0 or higher required for Angular 22)
- **Angular 22 CLI** (Installed globally via `npm install -g @angular/cli`)

## 📁 Project Structure
```text
jlg-monorepo/
├── backend/            # .NET Web API Backend Project
└── todoApp/            # Angular Frontend Project
```

---

## 🚀 How to Run the Project

### 1. Run the Backend API
Open your terminal from the root folder, navigate to the backend directory, and start the API:

```bash
cd backend/todoApi
dotnet run
```
*The API will restore dependencies, compile, and listen on its configured local port.*

### 2. Run the Frontend App
Open a second terminal window from the root folder, navigate to the frontend directory, install dependencies, and start the development server:

```bash
cd todoApp
npm install
npm start
```
*Once running, the application will be accessible at **`http://localhost:4200`**.*

---

## 🧪 Running Tests
To execute the automated test suites:

```bash
# Backend Tests
dotnet test

# Frontend Tests
cd todoApp && npm test
```
