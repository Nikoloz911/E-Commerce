using System.Net;
using System.Net.Sockets;
using System.Management;
namespace E_Commerce.Utilities;
internal class SystemInfo
{
    public static void DisplaySystemDetails()
    {
        Console.Clear();
        Console.WriteLine("===================== System Information =====================");
        Console.WriteLine($"Computer Name: {Environment.MachineName}");
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine($"User Name: {Environment.UserName}");
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine($"Operating System: {Environment.OSVersion}");
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine($"Processor: {GetProcessorInfo()}");
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine($"Installed RAM: {GetRAMInfo()}");
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine($"IP Address(es): {GetIPAddresses()}");
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine($"Disk Information: {GetDiskInfo()}");
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine($"BIOS Version: {GetBIOSInfo()}");
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine($"System Uptime: {GetUptime()}");
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine($"Installed .NET Framework: {Environment.Version}");
        Console.WriteLine("--------------------------------------------------------");
    }

    private static string GetProcessorInfo()
    {
        var searcher = new ManagementObjectSearcher("SELECT Name FROM Win32_Processor");
        foreach (var item in searcher.Get())
        {
            return item["Name"]?.ToString() ?? "Unknown Processor";
        }
        return "Unknown Processor";
    }

    private static string GetRAMInfo()
    {
        var searcher = new ManagementObjectSearcher("SELECT Capacity FROM Win32_PhysicalMemory");
        ulong totalRAM = 0;
        foreach (var item in searcher.Get())
        {
            totalRAM += Convert.ToUInt64(item["Capacity"]);
        }
        return $"{totalRAM / (1024 * 1024 * 1024)} GB";
    }
    private static string GetDiskInfo()
    {
        var searcher = new ManagementObjectSearcher("SELECT Size, FreeSpace FROM Win32_LogicalDisk WHERE DriveType=3");
        foreach (var disk in searcher.Get())
        {
            ulong totalSize = Convert.ToUInt64(disk["Size"]);
            ulong freeSpace = Convert.ToUInt64(disk["FreeSpace"]);
            return $"Total Size: {totalSize / (1024 * 1024 * 1024)} GB, Free Space: {freeSpace / (1024 * 1024 * 1024)} GB";
        }
        return "No Disk Info Available";
    }
    private static string GetIPAddresses()
    {
        var addresses = Dns.GetHostAddresses(Dns.GetHostName())
            .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork)
            .Select(ip => ip.ToString());
        return string.Join(", ", addresses);
    }
    private static string GetBIOSInfo()
    {
        var searcher = new ManagementObjectSearcher("SELECT SMBIOSBIOSVersion FROM Win32_BIOS");
        foreach (var bios in searcher.Get())
        {
            return bios["SMBIOSBIOSVersion"]?.ToString() ?? "Unknown BIOS Version";
        }
        return "Unknown BIOS Version";
    }
    private static string GetUptime()
    {
        TimeSpan uptime = TimeSpan.FromMilliseconds(Environment.TickCount64);
        return $"{uptime.Days} days, {uptime.Hours} hours, {uptime.Minutes} minutes";
    }
}
