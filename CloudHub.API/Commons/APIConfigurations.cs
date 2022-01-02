﻿using CloudHub.Domain.Services;
using CloudHub.Infra.Data;
using CloudHub.Infra.Services;

namespace CloudHub.API
{
    public class APIConfigurations : IEnvironmentSettings, IGoogleServicesConfigurations, IMongoConfigurations
    {
        public string EnvironmentName { get; private set; } = null!;
        public string BuildId { get; private set; } = null!;
        public string ConnectionString { get; private set; } = null!;
        public bool IsProductionModeEnabled { get; private set; } = false;
        public string GoogleTokenInfoApiUrl { get; private set; } = null!;
        public string MongoHost { get; private set; } = null!;
        public string MongoDatabase { get; private set; } = null!;


        private const string KEY_CONNECTION_STRING = "API_DATABASE";
        private const string KEY_BUILD_ID = "BUILD_ID";
        private const string KEY_PROD_MODE = "PRODUCTION_MODE";
        private const string KEY_ENV_NAME = "ASPNETCORE_ENVIRONMENT";
        private const string KEY_GOOGLE_TOKEN_URL = "GOOGLE_TOKEN_INFO_API_URL";
        private const string KEY_MONGO_HOST = "MONGO_HOST";
        private const string KEY_MONGO_DB_NAME = "MONGO_DB_NAME";


        public static APIConfigurations Load()
        {
            try
            {
                return FromEnvironment();
            }
            catch
            {
                return FromJson();
            }
        }

        private static APIConfigurations FromEnvironment()
        {
            bool isProduction = bool.Parse(GetEnvVar(KEY_PROD_MODE));
            string buildId = GetEnvVar(KEY_BUILD_ID);
            string envName = GetEnvVar(KEY_ENV_NAME);
            string connectionString = GetEnvVar(KEY_CONNECTION_STRING);
            string googleTokenInfoApiUrl = GetEnvVar(KEY_GOOGLE_TOKEN_URL);
            string mongoHost = GetEnvVar(KEY_MONGO_HOST);
            string mongoPublicDbName = GetEnvVar(KEY_MONGO_DB_NAME);
            return new()
            {
                BuildId = buildId,
                IsProductionModeEnabled = isProduction,
                ConnectionString = connectionString,
                EnvironmentName = envName,
                GoogleTokenInfoApiUrl = googleTokenInfoApiUrl,
                MongoHost = mongoHost,
                MongoDatabase = mongoPublicDbName
            };
        }

        private static APIConfigurations FromJson()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            bool isProduction = configuration.GetValue<bool>(KEY_PROD_MODE);
            string buildId = configuration.GetValue<string>(KEY_BUILD_ID);
            string envName = configuration.GetValue<string>(KEY_ENV_NAME);
            string connectionString = configuration.GetValue<string>(KEY_CONNECTION_STRING);
            string googleTokenInfoApiUrl = configuration.GetValue<string>(KEY_GOOGLE_TOKEN_URL);
            string mongoHost = configuration.GetValue<string>(KEY_MONGO_HOST);
            string mongoPublicDbName = configuration.GetValue<string>(KEY_MONGO_DB_NAME);
            return new()
            {
                BuildId = buildId,
                IsProductionModeEnabled = isProduction,
                ConnectionString = connectionString,
                EnvironmentName = envName,
                GoogleTokenInfoApiUrl = googleTokenInfoApiUrl,
                MongoHost = mongoHost,
                MongoDatabase = mongoPublicDbName
            };
        }

        private static string GetEnvVar(string var, string? defaultValue = null)
        {
            string? value = Environment.GetEnvironmentVariable(var);
            if (value != null) { return value; }
            if (defaultValue != null) { return defaultValue; }
            throw new MissingEnvironmentVariableException(var);
        }
    }
}
