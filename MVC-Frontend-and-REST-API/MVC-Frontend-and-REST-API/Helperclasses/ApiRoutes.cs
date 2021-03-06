using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Frontend_and_REST_API.Helperclasses
{
    public class ApiRoutes
    {
        const string _v1 = "api/v1";

        public static class Artist
        {
            const string prefixV1 = _v1 + "/artists";

            public const string ArtistsV1 = prefixV1;
            public const string RemoveV1 = prefixV1 + "/{id}";
        }

        public static class User
        {
            const string prefixV1 = _v1 + "/users";

            public const string RegisterV1 = prefixV1 + "/register";
            public const string LoginV1 = prefixV1 + "/login";
            public const string RefreshTokenV1 = prefixV1 + "/refresh";
            public const string GetRole = prefixV1 + "/role";
        }
    }
}
