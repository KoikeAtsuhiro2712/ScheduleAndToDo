using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ScheduleAndToDo.Models;
using ScheduleAndToDo.Views.Message;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace ScheduleAndToDo.ViewModels;

public　partial class ToDoWindowViewModel : ObservableObject
{
    private const string SaveFilePath = "todos.json";
    [ObservableProperty]
    private ObservableCollection<TodoItem> toDoItems = new();
    [ObservableProperty]
    private string newTitle;
    public ToDoWindowViewModel()
    {
        LoadToDos();
    }
    [RelayCommand]
    private void AddItem()
    {
        if (!string.IsNullOrWhiteSpace(NewTitle))
        {
            ToDoItems.Add(new TodoItem { Title = NewTitle });
            NewTitle = string.Empty;
            SaveToDos();
        }
    }
    [RelayCommand]
    private void DeleteToDoItem(TodoItem? item)
    {
        if (item is not null && ToDoItems.Contains(item))
        {
            ToDoItems.Remove(item);
            SaveToDos();
        }
    }
    [RelayCommand]
    private void OpenMain()
    {
        SaveToDos();
        WeakReferenceMessenger.Default.Send(new NavigationMessage("Main"));
    }
    private void SaveToDos()
    {
        var json=JsonSerializer.Serialize(ToDoItems);
        File.WriteAllText(SaveFilePath, json);   
    }
    private void LoadToDos()
    {
        if (File.Exists(SaveFilePath))
        { 
            var json=File.ReadAllText(SaveFilePath);
            if (!string.IsNullOrEmpty(json))
            { 
                var items= JsonSerializer.Deserialize<ObservableCollection<TodoItem>>(json);
                if (items is not null)
                {
                    ToDoItems = items;
                }
            }
        }
    }
}
