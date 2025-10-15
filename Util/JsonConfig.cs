
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

        private readonly JsonNode? _root;
        private readonly string _path;

        /// <summary>
        /// Constructor for the JSON parser class.
        /// </summary>
        /// <param name="path">The path of the JSON file.</param>
        public JsonConfig(string path)
        {
            _path = AppContext.BaseDirectory + "/../../../" + path;
            string content = File.ReadAllText(_path);
            _root = JsonNode.Parse(content);
        }

        /// <summary>
        /// Save the current state of the json root node to the config file.
        /// </summary>
        public void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_path, _root!.ToJsonString(options));
        }

        /// <summary>
        /// Sets a value in the config json.
        /// </summary>
        /// <typeparam name="T">The type of the value to be set.</typeparam>
        /// <param name="key">The key of the name of the value to be set.</param>
        /// <param name="value">The value to be set.</param>
        public void SetValue<T>(string key, T value)
        {
            if (value is Array arr)
            {
                JsonArray jsonArr = new JsonArray();
                // Danger!!!! JsonValue.Create only works for common types, no custom reference types!!!
                foreach (var item in arr) jsonArr.Add(JsonValue.Create(item));
                _root![key] = jsonArr;
            }
            else _root![key] = JsonValue.Create(value);
        }

        /// <summary>
        /// Wrapper around getting value from json root object.
        /// </summary>
        /// <typeparam name="T">The type you expect to be returned.</typeparam>
        /// <param name="key">The key of the value in the json.</param>
        /// <returns>The value in the json.</returns>
        /// <exception cref="Exception">An exception that should not happen.</exception>
        public T GetValue<T>(string key)
        {
            JsonNode? node = _root![key];
            return node == null ? throw new Exception("This should not happen!") : node.GetValue<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">The type to be expected in the returned array.</typeparam>
        /// <param name="key">The key of the value in the json.</param>
        /// <returns>An array of a specified type.</returns>
        /// <exception cref="Exception">An exception that should not happen.</exception>
        public T[] GetArray<T>(string key)
        {
            JsonArray? array = _root![key]!.AsArray();
            return array == null ? throw new Exception("This should not happen!") : array.Select(i => i!.GetValue<T>()).ToArray();
        }

    }

}
