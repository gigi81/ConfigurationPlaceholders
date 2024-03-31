using ConfigurationPlaceholders;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.Configuration;

/// <summary>
///     Extensions for adding placeholders support.
/// </summary>
public static class ConfigurationPlaceholderEx
{
    /// <summary>
    ///     Adds support for placeholders in configuration sources.
    /// </summary>
    /// <param name="applicationBuilder"><see cref="IHostApplicationBuilder" />.</param>
    /// <param name="placeholderResolvers">Placeholder value resolvers.</param>
    /// <param name="missingPlaceholderValueStrategy">How to handle placeholders with missing values.</param>
    /// <returns><paramref name="applicationBuilder"/>.</returns>
    public static IHostApplicationBuilder AddConfigurationPlaceholders(
        this IHostApplicationBuilder applicationBuilder,
        IList<IPlaceholderResolver> placeholderResolvers,
        MissingPlaceholderValueStrategy missingPlaceholderValueStrategy = MissingPlaceholderValueStrategy.VerifyAllAtStartup)
    {
        applicationBuilder.Configuration.AddConfigurationPlaceholders(
            placeholderResolvers,
            missingPlaceholderValueStrategy);
        
        return applicationBuilder;
    }

    /// <summary>
    ///     Adds support for placeholders in configuration sources.
    /// </summary>
    /// <param name="applicationBuilder"><see cref="IHostApplicationBuilder" />.</param>
    /// <param name="placeholderResolver">Placeholder value resolver.</param>
    /// <param name="missingPlaceholderValueStrategy">How to handle placeholders with missing values.</param>
    /// <returns><paramref name="applicationBuilder"/>.</returns>
    public static IHostApplicationBuilder AddConfigurationPlaceholders(
        this IHostApplicationBuilder applicationBuilder,
        IPlaceholderResolver placeholderResolver,
        MissingPlaceholderValueStrategy missingPlaceholderValueStrategy = MissingPlaceholderValueStrategy.VerifyAllAtStartup)
    {
        return applicationBuilder.AddConfigurationPlaceholders(
            new List<IPlaceholderResolver> { placeholderResolver },
            missingPlaceholderValueStrategy );
    }

    /// <summary>
    ///     Adds support for placeholders in configuration sources.
    /// </summary>
    /// <param name="hostBuilder"><see cref="IHostBuilder" />.</param>
    /// <param name="placeholderResolvers">Placeholder value resolvers.</param>
    /// <param name="missingPlaceholderValueStrategy">How to handle placeholders with missing values.</param>
    /// <returns><see cref="IHostBuilder" />.</returns>
    public static IHostBuilder AddConfigurationPlaceholders(
        this IHostBuilder hostBuilder,
        IList<IPlaceholderResolver> placeholderResolvers,
        MissingPlaceholderValueStrategy missingPlaceholderValueStrategy = MissingPlaceholderValueStrategy.VerifyAllAtStartup )
    {
        hostBuilder.ConfigureAppConfiguration( ( _, config ) =>
        {
            config.AddConfigurationPlaceholders( placeholderResolvers, missingPlaceholderValueStrategy);
        });

        return hostBuilder;
    }

    /// <summary>
    ///     Adds support for placeholders in configuration sources.
    /// </summary>
    /// <param name="hostBuilder"><see cref="IHostBuilder" />.</param>
    /// <param name="placeholderResolver">Placeholder value resolver.</param>
    /// <param name="missingPlaceholderValueStrategy">How to handle placeholders with missing values.</param>
    /// <returns><see cref="IHostBuilder" />.</returns>
    public static IHostBuilder AddConfigurationPlaceholders( this IHostBuilder hostBuilder,
                                                             IPlaceholderResolver placeholderResolver,
                                                             MissingPlaceholderValueStrategy missingPlaceholderValueStrategy = MissingPlaceholderValueStrategy.VerifyAllAtStartup ) =>
        hostBuilder.AddConfigurationPlaceholders( new List<IPlaceholderResolver> { placeholderResolver },
                                                  missingPlaceholderValueStrategy );

    /// <summary>
    ///     Adds support for placeholders in configuration sources.
    /// </summary>
    /// <param name="configurationBuilder"><see cref="IConfigurationBuilder" />.</param>
    /// <param name="placeholderResolvers">Placeholder value resolvers.</param>
    /// <param name="missingPlaceholderValueStrategy">How to handle placeholders with missing values.</param>
    /// <returns><see cref="IConfigurationBuilder" />.</returns>
    public static IConfigurationBuilder AddConfigurationPlaceholders( this IConfigurationBuilder configurationBuilder,
                                                                      IList<IPlaceholderResolver> placeholderResolvers,
                                                                      MissingPlaceholderValueStrategy missingPlaceholderValueStrategy = MissingPlaceholderValueStrategy.VerifyAllAtStartup )
    {
        if ( configurationBuilder is IConfigurationRoot configuration )
            configurationBuilder.Add( new ResolvePlaceholdersConfigurationSource( configuration,
                                                                                  placeholderResolvers,
                                                                                  missingPlaceholderValueStrategy ) );
        else
        {
            var resolver = new ResolvePlaceholdersConfigurationSource( new List<IConfigurationSource>( configurationBuilder.Sources ),
                                                                       placeholderResolvers,
                                                                       missingPlaceholderValueStrategy );
            configurationBuilder.Sources.Clear();
            configurationBuilder.Add( resolver );
        }

        return configurationBuilder;
    }

    /// <summary>
    ///     Adds support for placeholders in configuration sources.
    /// </summary>
    /// <param name="configurationBuilder"><see cref="IConfigurationBuilder" />.</param>
    /// <param name="placeholderResolver">Placeholder value resolver.</param>
    /// <param name="missingPlaceholderValueStrategy">How to handle placeholders with missing values.</param>
    /// <returns><see cref="IConfigurationBuilder" />.</returns>
    public static IConfigurationBuilder AddConfigurationPlaceholders( this IConfigurationBuilder configurationBuilder,
                                                                      IPlaceholderResolver placeholderResolver,
                                                                      MissingPlaceholderValueStrategy missingPlaceholderValueStrategy = MissingPlaceholderValueStrategy.VerifyAllAtStartup ) =>
        configurationBuilder.AddConfigurationPlaceholders( new List<IPlaceholderResolver> { placeholderResolver },
                                                           missingPlaceholderValueStrategy );
}