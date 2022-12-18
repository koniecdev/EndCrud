// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Linq;
using System.Security.Claims;

namespace IdentityServer
{
	public class SeedData
	{
		public static void EnsureSeedData(string connectionString)
		{
			var services = new ServiceCollection();
			services.AddLogging();
			services.AddDbContext<ApplicationDbContext>(options =>
			   options.UseSqlServer(connectionString));

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			using (var serviceProvider = services.BuildServiceProvider())
			{
				using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
				{
					var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
					var member = roleManager.FindByNameAsync("member").Result;
					if(member == null)
					{
						member = new() { Name = "member" };
						var r = roleManager.CreateAsync(member).Result;
						if(r != IdentityResult.Success)
						{
							throw new Exception("Adding roles problem");
						}
					}

					var admin = roleManager.FindByNameAsync("admin").Result;
					if(admin == null)
					{
						admin = new() { Name = "admin" };
						var r = roleManager.CreateAsync(admin).Result;
						if (r != IdentityResult.Success)
						{
							throw new Exception("Adding roles problem admin");
						}
					}

					var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
					context.Database.Migrate();

					var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
					var alice = userMgr.FindByNameAsync("alice").Result;
					if (alice == null)
					{
						alice = new ApplicationUser
						{
							UserName = "alice",
							Email = "AliceSmith@email.com",
							EmailConfirmed = true,
						};
						var result = userMgr.CreateAsync(alice, "Pass123$").Result;
						if (!result.Succeeded)
						{
							throw new Exception(result.Errors.First().Description);
						}

						result = userMgr.AddClaimsAsync(alice, new Claim[]{
							new Claim(JwtClaimTypes.Name, "Alice Smith"),
							new Claim(JwtClaimTypes.GivenName, "Alice"),
							new Claim(JwtClaimTypes.FamilyName, "Smith"),
							new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
						}).Result;
						if (!result.Succeeded)
						{
							throw new Exception(result.Errors.First().Description);
						}
						Log.Debug("alice created");
					}
					else
					{
						Log.Debug("alice already exists");
					}

					var bob = userMgr.FindByNameAsync("bob").Result;
					if (bob == null)
					{
						bob = new ApplicationUser
						{
							UserName = "bob",
							Email = "BobSmith@email.com",
							EmailConfirmed = true
						};
						var result = userMgr.CreateAsync(bob, "Pass123$").Result;
						if (!result.Succeeded)
						{
							throw new Exception(result.Errors.First().Description);
						}

						result = userMgr.AddClaimsAsync(bob, new Claim[]{
							new Claim(JwtClaimTypes.Name, "Bob Smith"),
							new Claim(JwtClaimTypes.GivenName, "Bob"),
							new Claim(JwtClaimTypes.FamilyName, "Smith"),
							new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
							new Claim("location", "somewhere")
						}).Result;
						if (!result.Succeeded)
						{
							throw new Exception(result.Errors.First().Description);
						}
						Log.Debug("bob created");
					}
					else
					{
						Log.Debug("bob already exists");
					}

					var defaultAdmin = userMgr.FindByEmailAsync("iaidoka1@koniec.dev").Result;
					if(!userMgr.IsInRoleAsync(defaultAdmin, "admin").Result)
					{
						var r = userMgr.AddToRoleAsync(defaultAdmin, "admin").Result;
						if (r != IdentityResult.Success)
						{
							throw new Exception("Adding roles addToRole");
						}
					}
				}
			}
		}
	}
}
