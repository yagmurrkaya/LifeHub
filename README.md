# LifeHub

## 1. Overview

LifeHub is an integrated personal assistant application developed using .NET 9.0 MAUI, specifically designed to provide a seamless experience on Windows (WinUI 3) desktop environments. The application empowers users to take control of their daily lives through four specialized modules:

Dashboard (Stats): Provides a visual summary of habit completion, task progress, and emotional trends using interactive charts and percentage indicators.

Habit Tracker: Allows users to define personal goals, select specific metrics, and track daily progress with automated feedback.

Planner: A comprehensive task management system featuring smart sorting, task timestamps, and automated cleanup of completed items.

Mood Journal: A reflective space where users record their daily emotional states with personal notes to monitor their mental well-being over time.

## 2. Pages and ViewModels

The following list shows the mapping between the main pages and their corresponding
ViewModels:

- **DashboardPage.xaml** → DashboardViewModel.cs
- **StatsPage.xaml** → StatsViewModel.cs

- **HabitListPage.xaml** → HabitListViewModel.cs
- **HabitAddPage.xaml** → HabitAddViewModel.cs
- **HabitEditPage.xaml** → HabitEditViewModel.cs

- **MoodJournalPage.xaml** → MoodJournalViewModel.cs
- **MoodHistoryPage.xaml** → MoodHistoryViewModel.cs

- **PlannerListPage.xaml** → PlannerViewModel.cs
- **PlannerSettingsPage.xaml** → PlannerSettingsViewModel.cs

## 3. Services

LifeHub application uses service classes to encapsulate business logic and manage
application data independently from the UI layer. Each service is injected into
ViewModels using dependency injection.

- **HabitService (IHabitService)**  
  Manages habit-related operations such as adding, removing, updating, and listing
  user habits. The service maintains an in-memory list of habits and initializes the
  application with default habit data when the app starts.

- **MoodService (IMoodService)**  
  Responsible for managing mood journal entries. It stores mood data in an
  ObservableCollection to support real-time UI updates and provides functionality
  for adding, deleting, and clearing mood entries.

- **PlannerService (IPlannerService)**  
  Handles planner task management, including adding and removing daily tasks.
  Tasks are stored in an ObservableCollection to ensure automatic UI synchronization,
  and operations are safely executed on the main thread when necessary.

These services help keep the ViewModels lightweight and ensure a clean separation
between UI logic and application data.
