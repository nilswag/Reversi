
using System.Text.Json;

namespace Reversi.Util
{

    /// Based on: https://www.pietschsoft.com/post/2024/06/18/csharp-read-text-and-json-file-contents-into-variable-in-memory
    public class JSONParser(string filePath)
    {
        public string FilePath { get; private set; } = filePath;

        private JsonDocument? _doc;

        public JsonElement? Root { get; private set; }

        public bool Load()
        {
            try
            {
                using FileStream fs = new(FilePath, FileMode.Open, FileAccess.Read);
                _doc = JsonDocument.Parse(fs);
                Root = _doc.RootElement;
            } catch (Exception e)
            {
                Console.WriteLine($"Unable to load JSON: {e.Message}");
                return false;
            }

            return true;
        }

    }

}
