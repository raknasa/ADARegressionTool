namespace DBComparer
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class DynamicExtension
    {
        #region Methods

        /// <summary>
        /// Propertieses as string.
        /// Design specificly to petapoco dynamics
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>a string with the key and the value</returns>
        public static string PropertiesAsString(this Object obj)
        {
            var tmp = new StringBuilder();
            foreach (var kv in (IDictionary<string, object>) obj)
            {
                if (kv.Value == null)
                {
                    tmp.AppendFormat("\n\t{0}= N/A;", kv.Key);
                }
                else
                {
                    tmp.AppendFormat("\n\t{0}= {1};", kv.Key, kv.Value);
                }
            }
            return tmp.ToString();
        }

        #endregion Methods
    }
}