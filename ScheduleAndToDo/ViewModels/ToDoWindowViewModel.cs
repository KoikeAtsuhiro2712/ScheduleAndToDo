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
    [ObservableProperty]
    private DateTime deadlineDate= DateTime.Now;
    private const string SaveFilePath = "todos.json";
    [ObservableProperty]
    private ObservableCollection<TodoItem> toDoPendingScheduleItem = new();
    [ObservableProperty]
    private ObservableCollection<TodoItem> toDoCompletingScheduleItem=new();
    [ObservableProperty]
    private DateTime newDate;
    [ObservableProperty]
    private string newTitle;
    public ToDoWindowViewModel()
    {
        WeakReferenceMessenger.Default.Register<TodoCompletedMessage>(this, (r, m) =>
        {
            var item = m.Value;
            if (item.IsCompleted)
            {
                ToDoPendingScheduleItem.Remove(item);
                if (!ToDoCompletingScheduleItem.Contains(item))
                {
                    ToDoCompletingScheduleItem.Add(item);
                }
            }
            else 
            {
                ToDoCompletingScheduleItem.Remove(item);
                if (!ToDoPendingScheduleItem.Contains(item))
                {
                    ToDoPendingScheduleItem.Add(item);
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
            ToDoPendingScheduleItem.Add(new TodoItem { Content = NewTitle ,Deadlinedate = NewDate });
            NewTitle = string.Empty;
            SaveToDos();
        }
    }
    [RelayCommand]
    private void DeleteToDoItem(TodoItem? item)
    {
        ToDoPendingScheduleItem.Remove(item);
        ToDoCompletingScheduleItem.Remove(item);
    }
    [RelayCommand]
    private void OpenMain()
    {
        SaveToDos();
        WeakReferenceMessenger.Default.Send(new NavigationMessage("Main"));
    }
    private void SaveToDos()
    {
        var json=JsonSerializer.Serialize(ToDoPendingScheduleItem);
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
                    ToDoPendingScheduleItem = items;
                }
            }
        }
    }
}
