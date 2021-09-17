using System;

namespace Gubbins.Models
{
    /// <summary>
    /// Contains information on whether a method or function succeeded or failed
    /// and any errors, if the latter. Also contains a result of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type that will be stored in the output. WARNING: Do NOT use this class to store <see cref="bool"/>
    /// values. Instead, use the non-generic <see cref="Result"/> class instead, making use of the Succeeded or
    /// Failed property. 
    /// </typeparam>
    /// <remarks>
    /// Setting the generic type T to a <see cref="bool"/> will result in an <see cref="ArgumentException"/> if the constructor that indicates success is called.
    /// This is to guard against inadvertently calling the wrong constructor with your output. When using a type of bool and trying to construct a successful
    /// result with your output, the non-generic constructor will be called instead (that accepts the single parameter of succeeded). This could lead to unintended behaviour.
    /// </remarks>
    public class Result<T> : Result
    {
        /// <inheritdoc />
        public Result()
        {
        }

        /// <inheritdoc />
        public Result(bool succeeded) : base(succeeded)
        {
            GuardAgainstTypeBool();
        }

        /// <inheritdoc />
        public Result(Error error) : base(error)
        {
        }

        /// <inheritdoc />
        public Result(string errorDescription, string? errorTechnicalDetails = null, Exception? errorException = null) : base(errorDescription, errorTechnicalDetails, errorException)
        {
        }

        /// <summary>
        /// Constructs an instance that has succeeded with the supplied result output.
        /// </summary>
        /// <param name="output">The output of the operation.</param>
        public Result(T? output) : base(true)
        {
            Output = output;
        }
        
        /// <summary>
        /// Constructs an instance with both the specified result output and specified success
        /// or failure.
        /// </summary>
        /// <param name="output">The output of the operation.</param>
        /// <param name="succeeded">Whether the operation succeeded.</param>
        public Result(T? output, bool succeeded) : base(succeeded)
        {
            Output = output;
        }
        
        /// <summary>
        /// The resulting output. May only be populated if the result was successful.
        /// </summary>
        public T? Output { get; set; }

        /// <summary>
        /// Text output indicating that the operation either succeeded or failed with any
        /// errors.
        /// </summary>
        /// <returns>An indication of the operation's success or failure and any errors (if failed).</returns>
        public override string ToString()
        {
            return Succeeded ? $"Operation succeeded with output: {Output?.ToString() ?? "[null]"}"
                             : base.ToString();
        }

        /// <summary>
        /// Throws and ArgumentException if the generic type T of this class is bool.
        /// </summary>
        private void GuardAgainstTypeBool()
        {
            if (typeof(T) == typeof(bool))
            {
                throw new ArgumentException("This class should not be used to store bool types for output. Instead, use the non-generic Result class instead, making use of the Succeeded or Failed properties.");
            }
        }
    }
}