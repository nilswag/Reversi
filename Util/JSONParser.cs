
using System.Text.Json;

namespace Reversi.Util
{

    /// <summary></summary>
    /// Based on: https://www.pietschsoft.com/post/2024/06/18/csharp-read-text-and-json-file-contents-into-variable-in-memory
    public class JSONParser
    {
        /// <summary>String that contains the file path to the json file.</summary>
        public string FilePath { get; }

        private readonly JsonElement? Root;

        public JSONParser(string filePath)
        {
            FilePath = filePath;
        }

        public bool Load()
        {
            try
            {
                using FileStream fs = new(FilePath, FileMode.Open, FileAccess.Read);
            } catch (Exception e)
            {
                Console.WriteLine($"Unable to load JSON: {e.Message}");
                return false;
            }

            return true;
        }

        public T GetProperty<T>(string key)
        {

            return null;
        }

    }

}
