using dotenv.net;

namespace Configuration
{
    public static class AppConfiguration
    {
        private static IDictionary<string, string> envVars = null;
        static AppConfiguration()
        {
            DotEnv.Load();
            envVars = DotEnv.Read();
        }

        public static string KeyVaultUrl => 
            envVars["KEY_VAULT_URL"] 
            ?? throw new InvalidOperationException("KEY_VAULT_URL environment variable is not set");

        public static void ValidateConfiguration()
        {
            try
            {
                _ = KeyVaultUrl;
                Console.WriteLine("✓ Configuration validation passed");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"✗ Configuration validation failed: {ex.Message}");
                throw;
            }
        }
    }
}