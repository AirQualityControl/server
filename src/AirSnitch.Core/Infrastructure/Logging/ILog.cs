using System;

namespace AirSnitch.Core.Infrastructure.Logging
{
	/// <summary>
	/// Interface that declare a set of operations for client loggin
	/// </summary>
    public interface ILog
    {
	    /// <summary>
        /// Log a message object with the <see cref="System.Diagnostics.Trace"/> level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        void Trace(object message);

        /// <summary>
        /// Log a message object with the <see cref="System.Diagnostics.Trace"/> level including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        void Trace(object message, Exception exception);

        /// <summary>
        /// Log a message with the <see cref="System.Diagnostics.Trace"/> level.
        /// </summary>
        /// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
        /// <param name="args">the list of format arguments</param>
        void TraceFormat(string format, params object[] args);
        

		/// <summary>
		/// Log a message object with the <see cref="System.Diagnostics.Debug"/> level.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		void Debug( object message );

		/// <summary>
		/// Log a message object with the <see cref="System.Diagnostics.Debug"/> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Debug(object message, Exception exception );

        /// <summary>
        /// Log a message with the <see cref="System.Diagnostics.Debug"/> level.
        /// </summary>
        /// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
        /// <param name="args">the list of format arguments</param>
        void DebugFormat(string format, params object[] args);
        
        /// <summary>
        /// Log a message object with the Info level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        void Info(object message);

        /// <summary>
        /// Log a message object with the Info level including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        void Info(object message, Exception exception);

        /// <summary>
        /// Log a message with the Info level.
        /// </summary>
        /// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
        /// <param name="args">the list of format arguments</param>
        void InfoFormat(string format, params object[] args);

        /// <summary>
        /// Log a message object with the Info level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        void Warn(object message);

        /// <summary>
        /// Log a message object with the Info level including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        void Warn(object message, Exception exception);

        /// <summary>
        /// Log a message with the Warn level.
        /// </summary>
        /// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
        /// <param name="args">the list of format arguments</param>
        void WarnFormat(string format, params object[] args);

        /// <summary>
		/// Log a message object with the Warn level.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		void Error( object message );

		/// <summary>
		/// Log a message object with the Error level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Error( object message, Exception exception );

        /// <summary>
        /// Log a message with the Error level.
        /// </summary>
        /// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
        /// <param name="args">the list of format arguments</param>
        void ErrorFormat(string format, params object[] args);

        /// <summary>
		/// Log a message object with the Error level.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		void Fatal( object message );

		/// <summary>
		/// Log a message object with the Error level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Fatal( object message, Exception exception );

        /// <summary>
        /// Log a message with the Error level.
        /// </summary>
        /// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
        /// <param name="args">the list of format arguments</param>
        void FatalFormat(string format, params object[] args);
    }
}