namespace TplDemo.Core.Extensions
{
    using Newtonsoft.Json;

    /// <summary>
    /// Object extentions.
    /// </summary>
    public static class ObjectExtentions
    {
        /// <summary>
        /// Converts an object to JSON string.
        /// </summary>
        /// <returns>The json.</returns>
        /// <param name="obj">Object.</param>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
