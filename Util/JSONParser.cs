
using System.Text.Json;

namespace Reversi.Util
{

    // Based on: https://www.pietschsoft.com/post/2024/06/18/csharp-read-text-and-json-file-contents-into-variable-in-memory
    /// <summary>
    /// Class to parse JSON files.
    /// </summary>
    public class JSONParser
    {

        /// <summary>
        /// The root of the JSON file.
        /// </summary>
        public JsonElement Root { get; private set; }

        /// <summary>
        /// Constructor for the JSON parser class.
        /// </summary>
        /// <param name="content">The content of the JSON file.</param>
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
