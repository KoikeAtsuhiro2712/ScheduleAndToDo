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
        //チェックボックスに変化が起きたときに、実行される
        WeakReferenceMessenger.Default.Register<TodoCompletedMessage>(this, (r, m) =>
        {
            var item = m.Value;
            //チェックがつけられた場合
            if (item.IsCompleted)
            {
                //該当のスケジュールを未完了タスク領域から取り除く
                ToDoPendingScheduleItem.Remove(item);
                if (!ToDoCompletingScheduleItem.Contains(item))
                {
                    ToDoCompletingScheduleItem.Add(item);
                }
            }
            else 
            {
                //該当のスケジュールを完了タスク領域から取り除く
                ToDoCompletingScheduleItem.Remove(item);
                if (!ToDoPendingScheduleItem.Contains(item))
                {
                    ToDoPendingScheduleItem.Add(item);
                }
            }
        });
        LoadToDos();
    }
    //追加ボタンが押された際に実行される
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
    //削除ボタンが押された際に実行される
    [RelayCommand]
    private void DeleteToDoItem(TodoItem? item)
    {
        ToDoPendingScheduleItem.Remove(item);
        ToDoCompletingScheduleItem.Remove(item);
    }
    //戻るボタンが押された際に実行される
    [RelayCommand]
    private void OpenMain()
    {
        SaveToDos();
        WeakReferenceMessenger.Default.Send(new NavigationMessage("Main"));
    }
    //ToDoリストデータを保持させるために実行
    private void SaveToDos()
    {
        var json=JsonSerializer.Serialize(ToDoPendingScheduleItem);
        File.WriteAllText(SaveFilePath, json);   
    }
    //ToDoリストデータをLoadingする。
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
