namespace E_Commerce.Utilities;
internal class Dotnet
{
    public static void ConsoleLine()
    {
        int length = 47;
        Console.WriteLine(new string('-', length));
    }
    public static void ColorfulWrite(string text, ConsoleColor color)
    {
        ConsoleColor originalColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ForegroundColor = originalColor;
    }
    public static void InitializeDotnet()
    {   
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                           _/\\__");
            Console.WriteLine("                   --  -==/    \\\\");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("             ___   ___");
            Console.ResetColor();
            Console.WriteLine("    |.    \\|\\\\");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("            | __| | __|");
            Console.ResetColor();
            Console.WriteLine("   |  )   \\\\\\\\");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("            | _|  | _|");
            Console.ResetColor();
            Console.WriteLine("    \\_/ |  //|\\\\");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("            |___| |_|");
            Console.ResetColor();
            Console.WriteLine("        /   \\\\\\/\\\\");
            Console.WriteLine();
            Console.WriteLine();
            ColorfulWrite("Entity ", ConsoleColor.DarkRed);
            ColorfulWrite("Framework ", ConsoleColor.Red);
            ColorfulWrite("Core ", ConsoleColor.Yellow);
            ColorfulWrite(".NET ", ConsoleColor.Cyan);
            ColorfulWrite("Command-line ", ConsoleColor.Blue);
            ColorfulWrite("Tools ", ConsoleColor.Magenta);
            ColorfulWrite("8.", ConsoleColor.DarkBlue);
            ColorfulWrite("0.", ConsoleColor.DarkCyan);
            ColorfulWrite("10. ", ConsoleColor.DarkYellow);
            Console.WriteLine();
            Console.WriteLine("");
            ConsoleLine();
            Console.WriteLine("");       
    }
}
