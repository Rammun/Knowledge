﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL.Entities;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DAL.DbInitialization
{
	public class DbInitializer
	{
		UserManager<ApplicationUser> _userManager;
		RoleManager<IdentityRole> _roleManager;
		ApplicationDbContext _applicationDbContext;
		ConfigurationDbContext _configurationDbContext;
		PersistedGrantDbContext _persistedGrantDbContext; 

		public DbInitializer(
			UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager,
			ApplicationDbContext applicationDbContext,
			ConfigurationDbContext configurationDbContext,
			PersistedGrantDbContext persistedGrantDbContext)
		{
			_userManager = userManager;
			_roleManager = roleManager;	
			_applicationDbContext = applicationDbContext;
			_configurationDbContext = configurationDbContext;
			_persistedGrantDbContext = persistedGrantDbContext;
		}

		public void Initialize()
		{
			_applicationDbContext.Database.Migrate();
			_configurationDbContext.Database.Migrate();
			_persistedGrantDbContext.Database.Migrate();

			var userMgr = _userManager;
			var alice = userMgr.FindByNameAsync("alice").Result;
			if (alice == null)
			{
				alice = new ApplicationUser
				{
					UserName = "alice"
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
						new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
						new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
						new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
						new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
					}).Result;
				if (!result.Succeeded)
				{
					throw new Exception(result.Errors.First().Description);
				}
				Console.WriteLine("alice created");
			}
			else
			{
				Console.WriteLine("alice already exists");
			}

			var bob = userMgr.FindByNameAsync("bob").Result;
			if (bob == null)
			{
				bob = new ApplicationUser
				{
					UserName = "bob"
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
						new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
						new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
						new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
						new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
						new Claim("location", "somewhere")
					}).Result;
				if (!result.Succeeded)
				{
					throw new Exception(result.Errors.First().Description);
				}
				Console.WriteLine("bob created");
			}
			else
			{
				Console.WriteLine("bob already exists");
			}


			if (!_configurationDbContext.Clients.Any())
			{
				foreach (var client in Config.GetClients().ToList())
				{
					_configurationDbContext.Clients.Add(client.ToEntity());
				}
				_configurationDbContext.SaveChanges();
			}

			if (!_configurationDbContext.IdentityResources.Any())
			{
				foreach (var resource in Config.GetIdentityResources().ToList())
				{
					_configurationDbContext.IdentityResources.Add(resource.ToEntity());
				}
				_configurationDbContext.SaveChanges();
			}

			if (!_configurationDbContext.ApiResources.Any())
			{
				foreach (var resource in Config.GetApiResources().ToList())
				{
					_configurationDbContext.ApiResources.Add(resource.ToEntity());
				}
				_configurationDbContext.SaveChanges();
			}
		}
	}
}