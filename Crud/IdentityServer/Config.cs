// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
	public static class Config
	{
		public static IEnumerable<IdentityResource> IdentityResources =>
		new List<IdentityResource>
		{
			new IdentityResources.OpenId(),
			new IdentityResources.Profile(),
			new IdentityResource(name: "user", userClaims: new[] { JwtClaimTypes.Id, JwtClaimTypes.Email, JwtClaimTypes.Name})
		};

		public static IEnumerable<ApiScope> ApiScopes =>
			new List<ApiScope>
			{
				new ApiScope("scope1"),
				new ApiScope("scope2"),
				new ApiScope("api1")
			};

		public static IEnumerable<Client> Clients =>
			new Client[]
			{
			   new Client
			   {
					ClientId = "client",
					ClientName = "Client for Postman user",
					AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
					ClientSecrets = { new Secret("secret".Sha256()) },
					AllowedScopes = { "api1", "user"},
					AlwaysSendClientClaims = true,
					AlwaysIncludeUserClaimsInIdToken = true,
					AllowAccessTokensViaBrowser = true
			   },
			   new Client
			   {
					ClientId = "swagger",
					ClientName = "Client for Swagger user",
					AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
					ClientSecrets = {new Secret("secret".Sha256())},
					AllowedScopes = {"api1", "user", "openid"},
					AlwaysSendClientClaims = true,
					AlwaysIncludeUserClaimsInIdToken = true,
					AllowAccessTokensViaBrowser=true,
					RedirectUris = { "https://localhost:7177/swagger/oauth2-redirect.html" },
					AllowedCorsOrigins = { "https://localhost:7177" }
			   },
			   new Client
			   {
					ClientId = "blazor",
					ClientName = "FrontEnd App",
					AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
					RequirePkce = true,
					RequireClientSecret = false,
					AllowedScopes = {"api1", "user", "openid", "profile"},
					AlwaysSendClientClaims = true,
					AllowedCorsOrigins = { "https://localhost:7091" },
					RedirectUris = { "https://localhost:7091/authentication/login-callback" },
					PostLogoutRedirectUris = { "https://localhost:7091" },
					Enabled = true
			   },
				new Client
				{
					ClientId = "mvc",
					ClientSecrets = { new Secret("secret".Sha256()) },

					AllowedGrantTypes = GrantTypes.Code,

					RedirectUris = { "https://localhost:7281/signin-oidc" },


					// where to redirect to after logout
					PostLogoutRedirectUris = { "https://localhost:7281/signout-callback-oidc" },


					AllowedScopes = new List<string>
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						"api1"
					}
				}
			};
	}
}