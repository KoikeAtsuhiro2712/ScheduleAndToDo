using ScheduleAndToDo.Models;
using ScheduleAndToDo.Repository;
using System;
using System.Threading.Tasks;

namespace ScheduleAndToDo.Test;

internal class Program
{
    [STAThread] // WPFでは必須
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== UserRepository テスト開始 ===");

        var repo = new UserRepository();

        // 1. ユーザー追加
        var user = new AppUser
        {
            AppUserId="testUser2",
            Password = "test1234",
            Name="Test Taro",
            Hash = "xyz789",
            Address = "Tokyo"
        };

        await repo.AddUserAsync(user);
        Console.WriteLine("ユーザーを追加しました。");

        // 2. 登録済みユーザーを全件取得
        var users = await repo.GetAllUsersAsync();

        Console.WriteLine("=== 現在のユーザー一覧 ===");
        foreach (var u in users)
        {
            Console.WriteLine($"ID: {u.AppUserId}, Password: {u.Password}, Address: {u.Address}");
        }

        Console.WriteLine("=== テスト完了 ===");
        Console.ReadLine();
    }
}
