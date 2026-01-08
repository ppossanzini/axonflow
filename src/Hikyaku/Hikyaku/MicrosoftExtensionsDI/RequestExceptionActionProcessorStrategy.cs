namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Represents the strategy for applying request exception action processors.
/// </summary>
/// <remarks>
/// The strategy determines how exception action processors are registered and executed
/// within the service pipeline. This affects the order of component registration
/// and application in the processing pipeline.
/// </remarks>
public enum RequestExceptionActionProcessorStrategy
{
  /// <summary>
  /// Applies exception action processors only for unhandled exceptions.
  /// </summary>
  /// <remarks>
  /// This strategy ensures that exception action processors are triggered exclusively
  /// for exceptions that have not been explicitly handled within the pipeline. It helps
  /// in scenarios where specific handling logic is required for unhandled exceptions
  /// to maintain consistent application behavior and error processing.
  /// </remarks>
  ApplyForUnhandledExceptions,

  /// <summary>
  /// Specifies that the exception action processors should be applied for all exceptions.
  /// </summary>
  /// <remarks>
  /// This member ensures that exception action processors are invoked regardless of whether
  /// the exception is considered handled or unhandled by the application.
  /// </remarks>
  ApplyForAllExceptions
}