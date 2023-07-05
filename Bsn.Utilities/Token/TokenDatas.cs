using Bsn.Utilities.Token.Enums;

namespace Bsn.Utilities.Token
{
    public static class TokenDatas
    {
        public static readonly IReadOnlyDictionary<TokenKeys, string> TokenKeys = new Dictionary<TokenKeys, string>()
        {
             { Enums.TokenKeys.Id, "idUser" },
             { Enums.TokenKeys.UserName, "username" },
             { Enums.TokenKeys.IsAdmin, "isAdmin" },
             { Enums.TokenKeys.Permissions, "permissions" },
             { Enums.TokenKeys.Expire, "exp" },
             { Enums.TokenKeys.Session, "session" }
        };
    }
}
