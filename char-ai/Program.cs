namespace char_ai;

static class Program
{
    [STAThread]
    static void Main()
    {

#if !DEBUG
            KillOthers();
#endif
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
        // glone
    }
    private static void KillOthers()
    {
#if DEBUG
        return;
#endif

        try
        {
            var current = System.Diagnostics.Process.GetCurrentProcess();
            var others = System.Diagnostics.Process
                .GetProcessesByName(current.ProcessName)
                .Where(p => p.Id != current.Id)
                .ToList();

            foreach (var process in others)
            {
                try
                {
                    process.Kill(true);
                    process.WaitForExit(2000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to kill {process.ProcessName} ({process.Id}): {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"KillOthers() failed: {ex.Message}");
        }
    }

}