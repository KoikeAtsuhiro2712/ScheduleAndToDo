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
    private ObservableCollection<TodoItem> toDoPendingItems = new();
    [ObservableProperty]
    private ObservableCollection<TodoItem> toDoCompletingItems = new();
    [ObservableProperty]
    private string newTitle;
    public ToDoWindowViewModel()
    {
        WeakReferenceMessenger.Default.Register<TodoItemMessage>(this, (r, m) =>
        {
            var item = m.Value;
            if (item.IsCompleted)
            {
                ToDoPendingItems.Remove(item);
                if (!ToDoCompletingItems.Contains(item))
                {
                    ToDoCompletingItems.Add(item);
                }
            }
            else 
            {
                ToDoCompletingItems.Remove(item);
                if (!ToDoPendingItems.Contains(item))
                {
                    ToDoPendingItems.Add(item);
                }
            }
        });
        LoadToDos();
    }
    [RelayCommand]
    private void AddItem()
    {
        if (!string.IsNullOrWhiteSpace(NewTitle))
        {
            ToDoPendingItems.Add(new TodoItem { Title=NewTitle, IsCompleted=false});
            NewTitle = string.Empty;
            SaveToDos();
        }
    }
    [RelayCommand]
    private void DeleteToDoItem(TodoItem? item)
    {
        ToDoPendingItems.Remove(item); 
        ToDoCompletingItems.Remove(item);
        SaveToDos();
    }
    [RelayCommand]
    private void OpenMain()
    {
        SaveToDos();
        WeakReferenceMessenger.Default.Send(new NavigationMessage("Main"));
    }
    private void SaveToDos()
    {
        var allItems = ToDoPendingItems.Concat(ToDoCompletingItems).ToList();
        var json=JsonSerializer.Serialize(allItems);
        File.WriteAllText(SaveFilePath,json);
    }
    private void LoadToDos()
    {
        if (File.Exists(SaveFilePath))
        { 
            var json=File.ReadAllText(SaveFilePath);
            if (!string.IsNullOrEmpty(json))
            {
                var items = JsonSerializer.Deserialize<List<TodoItem>>(json);
                if (items is not null)
                {
                    ToDoPendingItems.Clear();
                    ToDoCompletingItems.Clear();
                    foreach (var item in items)
                    {
                        if (item.IsCompleted)
                        { 
                            ToDoCompletingItems.Add(item);
                        }
                        else
                        {
                            ToDoPendingItems.Add(item);
                        }
                    }
                }
            }
        }
    }
}
