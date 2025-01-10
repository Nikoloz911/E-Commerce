using NAudio.Wave;

namespace E_Commerce.Utilities
{
    internal class Music
    {
        public static void ConsoleLine()
        {
            int length = 47;
            Console.WriteLine(new string('-', length));
        }
        /// funqcua aketebs ferebs wrilinesbistvis /// funqcua aketebs ferebs wrilinesbistvis
        public static void ColorfulWriteLine(string text, ConsoleColor color)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = originalColor;
        }
        /// funqcua aketebs ferebs wrilinesbistvis /// funqcua aketebs ferebs wrilinesbistvis
        /// funqcia ucvlis fers mwvaned /// funqcia ucvlis fers mwvaned 
        public static void SetGreenColor(Action action)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            action();
            Console.ResetColor();
        }
        /// funqcia ucvlis fers mwvaned /// funqcia ucvlis fers mwvaned 
        /// funqcia ucvlis fers yvitlad /// funqcia ucvlis fers yvitlad
       public static void SetYellowColor(Action action)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            action();
            Console.ResetColor();
        }
        /// funqcia ucvlis fers yvitlad /// funqcia ucvlis fers yvitlad
        public static void PlayMusic()
        {
            string musicFolder = Path.Combine(AppContext.BaseDirectory, "Music");
            var musicLibrary = LoadMusicLibrary(musicFolder);

            if (musicLibrary.Count == 0)
            {
                Console.WriteLine($"No music files found in the '{musicFolder}' folder.");
                return;
            }
            while (true)
            {
                ShowMenu();
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    PlayFromLibrary(musicLibrary);
                }
                else if (choice == "2")
                {
                    StartColorSlideshow();
                }
                else if (choice == "3")
                {
                    Console.Clear();
                    break;
                }
                else
                {       
                }
            }
        }
        private static void ShowMenu()
        {
            Console.Clear();
            SetGreenColor(() =>
            {
                ConsoleLine();
            });
            ColorfulWriteLine("          Music Player          ", ConsoleColor.Magenta);
            SetGreenColor(() => { ConsoleLine(); });
            ColorfulWriteLine("1. Play a song", ConsoleColor.Magenta);
            ColorfulWriteLine("2. Start Color Slideshow", ConsoleColor.Yellow);
            ColorfulWriteLine("3. Exit", ConsoleColor.DarkRed);
            SetGreenColor(() =>
            {
                ConsoleLine();
            });
        }
        private static Dictionary<int, string> LoadMusicLibrary(string folder)
        {
            var library = new Dictionary<int, string>();
            if (!Directory.Exists(folder))
            {
                ColorfulWriteLine($"Music folder not found at: {folder}", ConsoleColor.DarkRed);
                return library;
            }
            string[] musicFiles = Directory.GetFiles(folder, "*.mp3");
            for (int i = 0; i < musicFiles.Length; i++)
            {
                library.Add(i + 1, musicFiles[i]);
            }
            return library;
        }
        private static void PlayFromLibrary(Dictionary<int, string> musicLibrary)
        {
            Console.Clear();
            SetGreenColor(() => { ConsoleLine(); });
            ColorfulWriteLine("Available Songs:", ConsoleColor.Green);
            SetGreenColor(() => { ConsoleLine(); });
            foreach (var song in musicLibrary)
            {
                ColorfulWriteLine($"{song.Key}. {Path.GetFileName(song.Value)}", ConsoleColor.Magenta);
            }
            SetGreenColor(() => { ConsoleLine(); });
            if (int.TryParse(Console.ReadLine(), out int songNumber) && musicLibrary.ContainsKey(songNumber))
            {
                PlayAudio(musicLibrary[songNumber]);
            }
            else
            {   
            }
        }
        private static void PlayAudio(string filePath)
        {
            try
            {
                using (var audioFile = new AudioFileReader(filePath))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    Console.Clear();
                    SetGreenColor(() => { ConsoleLine(); });
                    ColorfulWriteLine($"Now Playing: {Path.GetFileName(filePath)}", ConsoleColor.DarkCyan);
                    SetGreenColor(() => { ConsoleLine(); });
                    ColorfulWriteLine("Press 'P' to Pause/Resume, 'S' to Stop.", ConsoleColor.Yellow);
                    outputDevice.Play();
                    while (outputDevice.PlaybackState != PlaybackState.Stopped)
                    {
                        if (Console.KeyAvailable)
                        {
                            var key = Console.ReadKey(true).Key;
                            if (key == ConsoleKey.P)
                            {
                                if (outputDevice.PlaybackState == PlaybackState.Playing)
                                {
                                    outputDevice.Pause();
                                    ColorfulWriteLine("Paused. Press 'P' to Resume. Press 'S' to Stop.", ConsoleColor.Yellow);
                                }
                                else if (outputDevice.PlaybackState == PlaybackState.Paused)
                                {
                                    outputDevice.Play();
                                    ColorfulWriteLine("Resumed. Press 'P' to Pause.", ConsoleColor.Green);
                                }
                            }
                            else if (key == ConsoleKey.S)
                            {
                                outputDevice.Stop();
                                ColorfulWriteLine("Music stopped.", ConsoleColor.Red);
                                break;
                            }
                        }
                        Thread.Sleep(1000);
                    }
                    ColorfulWriteLine("Returning to Music menu...", ConsoleColor.Magenta);
                    Thread.Sleep(2000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing audio: {ex.Message}");
            }
        }
        private static void StartColorSlideshow()
        {
            Console.Clear();
            Console.WriteLine("=== Color Slideshow ===");
            Console.WriteLine("Press any key to stop the slideshow...");
            Random random = new Random();
            while (!Console.KeyAvailable)
            {
                Console.BackgroundColor = GetRandomConsoleColor(random);
                Console.Clear();
                Thread.Sleep(500); 
            }
            Console.ResetColor();
            Console.Clear();
            ColorfulWriteLine("Slideshow stopped. Returning to main menu...", ConsoleColor.Yellow);
            Thread.Sleep(1000);
        }
        private static ConsoleColor GetRandomConsoleColor(Random random)
        {
            Array colors = Enum.GetValues(typeof(ConsoleColor));
            return (ConsoleColor)colors.GetValue(random.Next(colors.Length));
        }
    }
}
