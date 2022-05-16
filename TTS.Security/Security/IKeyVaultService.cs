using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTS.Security
{
    public interface IKeyVaultService
    {
        string GetSecret(string key, string kvUrl, AzAppRegistration taxTechApp);
    }
}
