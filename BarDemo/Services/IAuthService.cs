﻿using System;
using System.Threading.Tasks;


namespace BarDemo.Services
{
    public interface IAuthService
    {
        Task SignInAsync(string clientId,
                         Uri authUrl,
                         Uri callbackUrl,
                         Action<string> tokenCallback,
                         Action<string> errorCallback);
    }
}
