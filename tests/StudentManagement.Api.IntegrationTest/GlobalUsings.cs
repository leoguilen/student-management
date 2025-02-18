﻿global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Mvc.Testing;
global using Microsoft.AspNetCore.TestHost;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using StudentManagement.Infrastructure.Data.Contexts;
global using System.Net.Http.Headers;
global using Xunit.Abstractions;
global using Bogus;
global using StudentManagement.Core.Entities;
global using System.Net.Http.Json;
global using FluentAssertions;
global using StudentManagement.Api.Contracts.Requests;
global using StudentManagement.Api.Contracts.Responses;
global using StudentManagement.Api.IntegrationTest.Fixtures;
global using System.Net;
global using Microsoft.AspNetCore.Http;