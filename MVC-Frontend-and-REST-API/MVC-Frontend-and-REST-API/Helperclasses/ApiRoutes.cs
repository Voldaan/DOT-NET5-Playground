using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Frontend_and_REST_API.Helperclasses
{
    public class ApiRoutes
    {
        const string v1 = "api/v1";

        public static class Example
        {
            const string prefixV1 = v1 + "/example";

            public const string IndexV1 = prefixV1 + "/index";
        }
    }
}
