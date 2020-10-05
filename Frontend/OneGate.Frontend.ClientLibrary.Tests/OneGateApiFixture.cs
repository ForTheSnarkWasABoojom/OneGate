﻿using System;
using Microsoft.AspNetCore.Http;
using OneGate.Shared.Models.Account;
using Xunit;

namespace OneGate.Frontend.ClientLibrary.Tests
{
    public class OneGateApiFixture : IDisposable
    {
        public OneGateApi AdminApi { get; }
        public OneGateApi UserApi { get; }
        private int UserId { get; }
        public Uri EndpointUri { get; } = new Uri("http://localhost/api/v1/");

        private readonly Random _generator = new Random();
        
        public OneGateApiFixture()
        {
            // Administrator API.
            var adminAccessTokenDto = OneGateApi.CreateTokenAsync(EndpointUri, new OAuthDto
            {
                Username = "test@example.com",
                Password = "test"
            }, new ClientKeyDto
            {
                ClientKey = "test"
            }).Result;

            AdminApi = new OneGateApi(EndpointUri, adminAccessTokenDto.AccessToken);
            
            // User API.
            var accountGuid = Guid.NewGuid();
            UserId = OneGateApi.CreateAccountAsync(EndpointUri, new CreateAccountDto
            {
                FirstName = "TestUserAccount",
                LastName = "TestUserAccount",
                Email = $"{accountGuid}@example.com",
                Password = accountGuid.ToString()
            }, new ClientKeyDto
            {
                ClientKey = "test"
            }).Result.Id;

            var userAccessTokenDto = OneGateApi.CreateTokenAsync(EndpointUri, new OAuthDto
            {
                Username = $"{accountGuid}@example.com",
                Password = accountGuid.ToString()
            }, new ClientKeyDto
            {
                ClientKey = "test"
            }).Result;

            UserApi = new OneGateApi(EndpointUri, userAccessTokenDto.AccessToken);
        }
        
        public DateTime GetRandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;           
            return start.AddDays(_generator.Next(range));
        }
        
        public void Dispose()
        {
            AdminApi.DeleteAccountAsync(UserId);
        }
    }
}