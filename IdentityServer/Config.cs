﻿using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServer
{
    /// <summary>
    /// Class holding test Identity Server configuration.
    /// </summary>
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources
            => new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource()
                {
                    Name = "role",
                    DisplayName = "User role",
                    UserClaims = new[] { ClaimTypes.Role }
                }
            };

        public static IEnumerable<ApiScope> GetApiScopes()
            => new List<ApiScope>
            {
                new ApiScope("hotel_api", "My API", new string[] { ClaimTypes.Role })
            };

        public static IEnumerable<Client> GetClients()
            => new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RequireClientSecret = false,
                    AllowedScopes = { "hotel_api", "openid", "profile", "role" }
                },
                new Client
                {
                    ClientId = "swagger",
                    ClientName = "Swagger UI",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireClientSecret = false,
                    AllowedScopes = { "hotel_api" },
                    AllowedCorsOrigins = { "http://localhost:5000" }
                }
            };

        public static List<TestUser> GetUsers()
            => new()
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",
                    Claims =
                    {
                        new Claim(ClaimTypes.Role, "Admin")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password"
                }
            };
    }
}
