﻿using System;
using System.Text;
using Multiformats.Base;
using Newtonsoft.Json;

namespace AgentFramework.Core.Extensions
{
    /// <summary>
    /// Formatting extensions
    /// </summary>
    public static class FormattingExtensions
    {
        /// <summary>
        /// Decode the array into a string using UTF8 byte mark
        /// </summary>
        /// <param name="array"></param>
        // ReSharper disable once InconsistentNaming
        public static string GetUTF8String(this byte[] array) => Encoding.UTF8.GetString(array);

        /// <summary>Decode the byte array and deserialize the JSON string into the specified object</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static T ToObject<T>(this byte[] array) => JsonConvert.DeserializeObject<T>(GetUTF8String(array));

        /// <summary>
        /// Deserializes a JSON string to object
        /// </summary>
        /// <returns>The json.</returns>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T ToObject<T>(this string value) => JsonConvert.DeserializeObject<T>(value);

        /// <summary>Encode the string into a byte array using UTF8 byte mark.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static byte[] GetUTF8Bytes(this string value) => Encoding.UTF8.GetBytes(value);

        /// <summary>
        /// Serializes an object to byte array using UTF8 byte order of the JSON graph
        /// </summary>
        /// <returns>The byte array.</returns>
        /// <param name="value">Value.</param>
        public static byte[] ToByteArray<T>(this T value) =>
            (value is string)
                ? throw new Exception("Use GetUTF8Bytes() extension for string types")
                : GetUTF8Bytes(ToJson(value));

        /// <summary>Converts the specified string, which encodes binary data as base-64 digits,
        /// to an equivalent 8-bit unsigned integer array.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte[] GetBytesFromBase64(this string value) => Convert.FromBase64String(value);

        /// <summary>Converts an array of 8-bit unsigned integers to its equivalent string
        /// representation that is encoded with base-64 digits.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToBase64String(this byte[] value) => Convert.ToBase64String(value);

        /// <summary>
        /// Converts an object to json string using default converter.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);

        /// <summary>
        /// Converts an object to json string using the provided <see cref="JsonSerializerSettings"/>
        /// </summary>
        /// <returns>The json.</returns>
        /// <param name="obj">Object.</param>
        /// <param name="settings">Settings.</param>
        public static string ToJson(this object obj, JsonSerializerSettings settings) =>
            JsonConvert.SerializeObject(obj, settings);

        /// <summary>
        /// Converts a string to base58 representation.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToBase58(this string value) =>
            Multibase.Base58.Encode(Encoding.UTF8.GetBytes(value));
    }
}