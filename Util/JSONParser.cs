
using System.Text.Json;

namespace Reversi.Util
{

    /// Based on: https://www.pietschsoft.com/post/2024/06/18/csharp-read-text-and-json-file-contents-into-variable-in-memory
    public class JSONParser
    {

        public JsonElement Root { get; private set; }

        public JSONParser(string content)
        {
            try
            {
                JsonDocument doc = JsonDocument.Parse(content);
                Root = doc.RootElement;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to load JSON: {e.Message}");
            }
        }
    }

}
