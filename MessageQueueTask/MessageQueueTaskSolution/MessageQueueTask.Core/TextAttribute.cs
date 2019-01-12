using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace MessageQueueTask.Core
{
    /// <summary>
    /// The text attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public sealed class TextAttribute : Attribute
    {
        private static readonly Hashtable SEnumFields = Hashtable.Synchronized(new Hashtable());
        private string _text;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextAttribute"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public TextAttribute(string text)
        {
            _text = text;
        }

        /// <summary>
        /// Gets value from enum.
        /// </summary>
        /// <param name="enumeration">The enumeration.</param>
        /// <returns>
        /// Returns the text value.
        /// </returns>
        public static string GetValueFromEnum(Enum enumeration)
        {
            string result = (enumeration == null) ? string.Empty : enumeration.ToString();
            TextAttribute[] information = GetInformation(enumeration);

            if (information.Length == 0)
            {
                return result;
            }

            return information[0]._text;
        }

        /// <summary>
        /// Gets information.
        /// </summary>
        /// <param name="enumeration">The enumeration.</param>
        /// <returns>
        /// Returns an array of text attributes.
        /// </returns>
        private static TextAttribute[] GetInformation(Enum enumeration)
        {
            Type type = enumeration.GetType();
            string enumText = enumeration.ToString();
            List<KeyValuePair<string, TextAttribute[]>> fieldInfo = GetFieldInfo(type);
            return fieldInfo.Find((KeyValuePair<string, TextAttribute[]> item) => string.Equals(enumText, item.Key, StringComparison.OrdinalIgnoreCase)).Value;
        }

        /// <summary>
        /// Gets field info.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// Returns field info.
        /// </returns>
        private static List<KeyValuePair<string, TextAttribute[]>> GetFieldInfo(Type type)
        {
            string key = type.ToString();
            List<KeyValuePair<string, TextAttribute[]>> list;

            if (!SEnumFields.Contains(key))
            {
                list = FillFieldInfo(type);
                SEnumFields[key] = list;
            }
            else
            {
                list = SEnumFields[key] as List<KeyValuePair<string, TextAttribute[]>>;
            }

            return list;
        }

        /// <summary>
        /// Fills field info.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// Returns a collection of text attributes.
        /// </returns>
        private static List<KeyValuePair<string, TextAttribute[]>> FillFieldInfo(Type type)
        {
            return FillFieldInfo(type.GetFields());
        }

        /// <summary>
        /// Fills field info.
        /// </summary>
        /// <param name="fields">The fields.</param>
        /// <returns>
        /// Returns the collection of text attributes.
        /// </returns>
        private static List<KeyValuePair<string, TextAttribute[]>> FillFieldInfo(FieldInfo[] fields)
        {
            var list = new List<KeyValuePair<string, TextAttribute[]>>();

            for (int i = 0; i < fields.Length; i++)
            {
                list.Add(new KeyValuePair<string, TextAttribute[]>(fields[i].Name, (TextAttribute[])fields[i].GetCustomAttributes(typeof(TextAttribute), false)));
            }

            return list;
        }
    }
}
