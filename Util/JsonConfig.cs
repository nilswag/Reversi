
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Reversi.Util
{

    /* 
     * Summarized and explained by ChatGPT to learn api for this class.
     * https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonnode?view=net-9.0
     * https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonarray?view=net-9.0
     * https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonnode.tojsonstring?view=net-9.0
     * https://learn.microsoft.com/en-us/dotnet/api/system.io.file?view=net-9.0
     */

    /// <summary>
    /// Class to parse JSON files.
    /// </summary>
    public class JsonConfig
    {

        public JsonNode Root { get; private set; }
        private readonly string _path;

        /// <summary>
        /// Constructor for the JSON parser class.
        /// </summary>
        /// <param name="path">The path of the JSON file.</param>
        public JsonConfig(string path)
        {
            _path = AppContext.BaseDirectory + "/../../../" + path;
            string content = File.ReadAllText(_path);
            Root = JsonNode.Parse(content);
            if (Root == null) throw new Exception($"Could not read JSON: {_path}");
        }

        /// <summary>
        /// Save the current state of the json root node to the config file.
        /// </summary>
        public void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_path, Root.ToJsonString(options));
        }

    }

}
