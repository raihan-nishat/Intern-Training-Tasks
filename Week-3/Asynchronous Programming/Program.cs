using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{  
    // download file

    static async Task<string> DownloadFileAsync(string filename)
    {
        Console.WriteLine($"downloading {filename}...");
        
        int time= Random.Shared.Next(1000, 3001);
        await Task.Delay(time);

        if (Random.Shared.Next(3) == 0)
        {
            throw new Exception($"download failed!");
        }

        Console.WriteLine($"{filename} downloaded in {time/1000.0:F1}s)");
        
        return $"contents of {filename}";
    }

    // process file
    
    static async Task<int> ProcessFileAsync(string contents)
    {
        Console.WriteLine($"Processing file...");

        int time = Random.Shared.Next(1000, 3001);
        await Task.Delay(time);

        int result = contents.Length;
        Console.WriteLine($"processing finished. Result = {result}");
        
        return result;
    }

    // main method
    
    static async Task Main()
    {
        Stopwatch stp = Stopwatch.StartNew();

        string[] files = { "File1.txt", "File2.txt", "File3.txt" };

        var downloads = new List<Task<string>>();
        
        foreach (var file in files)
        {
            downloads.Add(DownloadFileAsync(file));
        }

        var processingTasks = new List<Task<int>>();
        
        int success = 0;
        int failed = 0;

        while (downloads.Count > 0)
        {
            Task<string> finished = await Task.WhenAny(downloads);
            downloads.Remove(finished);

            try
            {
                string contents = await finished;
                success++;

                processingTasks.Add(ProcessFileAsync(contents));
            }

            catch (Exception e)
            {
                failed++;
                Console.WriteLine($"error: {e.Message}");
            }
        }

        await Task.WhenAll(processingTasks);
        stp.Stop();

        // final result

        Console.WriteLine("\nFINAL RESULT ");
        Console.WriteLine($"succeeded: {success}");
        Console.WriteLine($"failed: {failed}");
        Console.WriteLine($"total time: {stp.Elapsed.TotalSeconds:F2} seconds");
        Console.WriteLine();
        Console.ReadLine();
    }
}
