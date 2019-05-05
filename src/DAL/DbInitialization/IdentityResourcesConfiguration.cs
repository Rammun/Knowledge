// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityDAL.DbInitialization
{
    public static class IdentityResourcesConfiguration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("kn-api", "Knowledge API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "kn-api" }
                },

                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "kn-api" },
                    AllowOfflineAccess = true,
                    RequireClientSecret = false
                },

                // OpenID Connect hybrid flow client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris           = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "kn-api"
                    },

                    AllowOfflineAccess = true
                },
                // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =           { "http://localhost:5003/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:5003" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "kn-api"
                    }
                },

                new Client
                {
                    ClientId = "spa",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    // белый список адресов на который клиентское приложение может попросить
                    // перенаправить User Agent, важно для безопасности
                    RedirectUris =
                    {
                        // адрес перенаправления после логина
                        "http://localhost:5003/callback.html",
                        "http://localhost:5003/popup.html",

                        // адрес перенаправления при автоматическом обновлении access_token через iframe
                        "http://localhost:5003/silent.html"
                    },

                    PostLogoutRedirectUris = { "http://localhost:5003/index.html" },

                    // список scopes, разрешённых именно для данного клиентского приложения
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "kn-api"
                    },

                    // адрес клиентского приложения, просим сервер возвращать нужные CORS-заголовки
                    AllowedCorsOrigins = { "http://localhost:5003" },

                    // от этой настройки зависит размер токена, 
                    // при false можно получить недостающую информацию через UserInfo endpoint
                    AlwaysIncludeUserClaimsInIdToken = true,

                    AccessTokenLifetime = 3600, // секунд, это значение по умолчанию
                    IdentityTokenLifetime = 300, // секунд, это значение по умолчанию

                    // разрешено ли получение refresh-токенов через указание scope offline_access
                    AllowOfflineAccess = false,
                }
            };
        }
    }
}