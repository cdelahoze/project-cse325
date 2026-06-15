# Fitness Tracker - CSE325

A comprehensive web application designed to track fitness, workouts, nutrition, and personal wellness goals. This project is built using ASP.NET Core Blazor Web App with .NET 8.0 and EF Core with SQLite.

## Team Members

* Cristian Moisés De La Hoz Escorcia
* Jhefersson Enrique Linares Castillo
* Mario Alberto Astonitas Acuna
* Lilian Marcela Vargas de Reyes
* Lucky George Olumah
* Alex Koje Okhitoya

## Overview

The Fitness Tracker application helps users monitor their health journey by providing tools to log daily workouts, record nutritional intake, set fitness goals, and visualize overall progress. The system seeds a SQLite database with initial food and nutritional values from the USDA Foundation Foods dataset to allow precise nutrition logging.

## Description & Features

* **User Profile**: Customize personal details, target metrics, and track body stats.
* **Workout Tracker**: Log workouts, exercises, durations, and intensities to monitor activity history.
* **Nutrition Diary**: Add meals and track daily caloric and macronutrient intake (carbohydrates, proteins, fats).
* **Food Register**: Look up and manage a database of food items pre-populated from USDA Foundation Food data.
* **Goals Management**: Set targets for weight, daily caloric intake, and weekly active minutes.
* **Progress Tracking**: View history and metrics to see improvements over time.

## Prerequisites

Before running the application, make sure you have the following installed:

* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [Git](https://git-scm.com/)
* [Docker](https://www.docker.com/) (Optional, for containerized execution)

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/cdelahoze/project-cse325.git
   cd project-cse325
   ```

2. Restore the project dependencies:
   ```bash
   dotnet restore
   ```

## Running the Application

### Running Locally

To run the application locally in development mode:

```bash
dotnet run
```

Alternatively, you can run it with hot-reload enabled:

```bash
dotnet watch
```

### Running with Docker

To build and run the application container locally:

1. Build the Docker image:
   ```bash
   docker build -t fitness-tracker-cse325 .
   ```

2. Run the Docker container:
   ```bash
   docker run -p 8080:8080 fitness-tracker-cse325
   ```

Once running, navigate to `http://localhost:8080`.

## Deployment

The project is configured for easy deployment to cloud services such as Render.com using the included `render.yaml` and `Dockerfile`. It automatically detects the `PORT` environment variable assigned by the host environment.