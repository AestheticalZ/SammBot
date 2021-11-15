﻿using System;
using System.Net.Http;

namespace SammBotNET.Services
{
    public class FunService
    {
        public readonly HttpClient UrbanClient = new()
        {
            BaseAddress = new Uri("https://api.urbandictionary.com")
        };
    }
}