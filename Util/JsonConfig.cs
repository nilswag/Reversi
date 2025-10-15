
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Reversi.Util
{

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
            JsonNode? tmpRoot = JsonNode.Parse(content) ?? throw new Exception($"Could not read JSON: {_path}");
            Root = tmpRoot;
        }

        /// <summary>
        /// Save the current state of the json root node to the config file.
        /// </summary>
        public void Save()
        {
            File.WriteAllText(_path, Root.ToJsonString());
        }

        public static JsonNode? Serialize<T>(T obj)
        {
            if (obj == null) return null;
            return JsonNode.Parse(JsonSerializer.Serialize<T>(obj));
        }

        public T? Deserialize<T>(string key)
        {
            JsonNode? node = Root[key];
            if (node == null) return default;
            return JsonSerializer.Deserialize<T>(node.ToJsonString());
        }

    }

}
