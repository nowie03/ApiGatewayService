﻿using System.Security.Policy;

namespace ApiGatewayService.RequestModels
{
    public class LoginRequest
    {
        public string email { get; set; }
        public string password { get; set; }

      
    }
}