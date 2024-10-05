namespace EndPoints.AppSettings;

/// <summary>
/// Настройки SwaggerUI
/// </summary>
internal class SwaggerSettings
{
    /// <summary>
    /// Путь конечной точки для получения объекта Swagger в формате JSON
    /// </summary>
    internal static string JsonEndpointPath => "/swagger/v1/swagger.json";
    
    /// <summary>
    /// Текстовая метка (название) конечной точки для получения объекта Swagger в формате JSON
    /// </summary>
    internal static string JsonEndpointName => "v1";
}