// <copyright file="ILogService.cs" company="Etersoul">
// This code is part of Trilogic Data Project.
// </copyright>
// <author>William</author>
namespace Trilogic.Utility
{
    using System;

    /// <summary>
    /// log service interface.
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Write the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Write(string message);
    }
}