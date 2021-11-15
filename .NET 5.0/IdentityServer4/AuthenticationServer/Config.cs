using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace AuthenticationServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                 new IdentityResources.OpenId(),
                 new IdentityResources.Profile(),
                 new IdentityResource
                 {
                     Name = "rc.scope",
                     UserClaims =
                     {
                        "rc.teste",
                     }
                 },
            };
        }

        public static IEnumerable<ApiScope> GetApis() =>
            new List<ApiScope>
            {
                new ApiScope
                {
                    Name = "ApiOne",
                },
                new ApiScope()
                {
                    Name = "ApiTwoClient",
                     UserClaims = { "Api.teste" },
                },
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client_id",
                    ClientSecrets= {new Secret("client_secret".ToSha256())},
                    // retrieve accessToken
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // wich apis/clients can access
                    AllowedScopes = { "ApiOne" },
                },

                new Client
                {
                    ClientId = "client_id_mvc",
                    ClientSecrets= {new Secret("client_secret_mvc".ToSha256())},
                    // retrieve accessToken
                    AllowedGrantTypes = GrantTypes.Code,

                    RequirePkce = true, 

                    RedirectUris = {"https://localhost:44377/signin-oidc"},
                    PostLogoutRedirectUris = { "https://localhost:44377/Home/Index" },

                    // wich apis/clients can access
                    AllowedScopes =
                    {
                        "ApiOne",
                        "ApiTwoClient",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "rc.scope",
                    },
                    // goes to a diferent endpoint to get all the user claims
                    //AlwaysIncludeUserClaimsInIdToken = true,

                    AllowOfflineAccess = true,
                    RequireConsent = false,
                },

                new Client
                {
                    // Java Script Implicit Flow - without Pkce
                    ClientId = "client_id_jsIF",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    RedirectUris = {"https://localhost:44379/Home/PrivacyImplicitFlow"},
                    PostLogoutRedirectUris = { "https://localhost:44379/Home/Index" },
                    
                    AllowedScopes =
                    {
                        "ApiOne",
                        IdentityServerConstants.StandardScopes.OpenId,
                    },

                     AllowAccessTokensViaBrowser = true,
                     RequireConsent = false,
                },

                new Client
                {
                    // Java Script OICD - with Pkce
                    ClientId = "client_id_jsOICD",

                    RequireClientSecret = false,

                    AllowedGrantTypes = GrantTypes.Code,

                    RequirePkce = true,

                    RedirectUris = {"https://localhost:44379/Home/PrivacyOicdFlow"},
                    PostLogoutRedirectUris = { "https://localhost:44379/Home/Index" },

                    AllowedCorsOrigins = { "https://localhost:44379" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "ApiOne",
                        "ApiTwoClient",
                        "rc.scope",
                    },

                     //1min default
                     AccessTokenLifetime = 1,

                     AllowAccessTokensViaBrowser = true,
                     RequireConsent = false,
                }
            };

    }
}
