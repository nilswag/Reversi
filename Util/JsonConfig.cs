
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Reversi.Util
{

    // Based on: https://www.pietschsoft.com/post/2024/06/18/csharp-read-text-and-json-file-contents-into-variable-in-memory
    /// <summary>
    /// Class to parse JSON files.
    /// </summary>
    public class JsonConfig
    {

        /// <summary>
        /// The root of the JSON file.
        /// </summary>
        public JsonNode? Root { get; private set; }

        /// <summary>
        /// Constructor for the JSON parser class.
        /// </summary>
        /// <param name="path">The path of the JSON file.</param>
        public JsonConfig(string path)
        {
            try
            {
                string content = File.ReadAllText(AppContext.BaseDirectory + "/../../../" + path);
                Root = JsonNode.Parse(content);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to load JSON: {e.Message}");
            }
        }

        public void Save()
        {
            
        }

    }

}
