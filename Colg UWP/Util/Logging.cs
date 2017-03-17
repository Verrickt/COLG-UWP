using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;

namespace Colg_UWP.Util
{
    public class Logging
    {
        private static StorageFolder _folder = ApplicationData.Current.TemporaryFolder;

        private static StorageFile _file;

        private static bool _isDebugMode = false;

        public static async Task InitializationAsync()
        {
            string filename = $"[{DateTime.Now:yyyy-MM-dd hh-mm-ss}]Log.txt";
            _file = await _folder.CreateFileAsync(filename, CreationCollisionOption.FailIfExists);
            WriteLine($"Logging file Path {_file.Path}");
        }

        static Logging()
        {
#if DEBUG
            _isDebugMode = true;
#endif
        }

        public static void WriteLine(string content)
        {
            string description = $"[{DateTime.Now}] Thread {Environment.CurrentManagedThreadId} ";
            string str = description + content;
            if (_isDebugMode)
            {
                Debug.WriteLine(str);
            }
            Task.Run(async () =>
            {
                await FileIO.AppendLinesAsync(_file, str.GetSingle());
            });
        }

        public static void WriteLineIf(bool condition, string content)
        {
            if (condition)
            {
                WriteLine(content);
            }
        }

        public static void WriteLineIf(bool condition, string content, string trueContent, string falseContent)
        {
            if (condition)
            {
                WriteLine(content + trueContent);
            }
            else
            {
                WriteLine(content + falseContent);
            }
        }

        public static void WriteLineIf(bool condition, string trueContent, string falseContent)
        {
            WriteLine(condition ? trueContent : falseContent);
        }
    }
}