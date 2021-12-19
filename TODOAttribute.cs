using System;
// ReSharper disable UnusedMember.Global

namespace Util
{
    /// <summary>
    /// Attribute for marking classes or methods that are incomplete
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public sealed class ToDoAttribute : Attribute
    {
        public ToDoAttribute()
        {
            Comment = "No comment entered!";
        }

        /// <summary>
        /// Mark code file with a comment
        /// </summary>
        /// <param name="comment"></param>
        public ToDoAttribute(string comment)
        {
            Comment = comment;
        }

        /// <summary>
        /// Retrieve comment for TODO entry
        /// </summary>
        public string Comment { get; }
    }
}