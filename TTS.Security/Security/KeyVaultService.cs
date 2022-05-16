using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTS.Security
{
    public class KeyVaultService : IKeyVaultService
    {
        public string GetSecret(string key, string kvUrl, AzAppRegistration taxTechApp)
        {
            ClientSecretCredential _client_secret = new ClientSecretCredential(taxTechApp.TenantId, taxTechApp.ClientId, taxTechApp.clientSecret);

            SecretClient _secret_client = new SecretClient(new Uri(kvUrl), _client_secret);

            var secret = _secret_client.GetSecret(key);

            return secret.Value.Value;
        }
    }
}

