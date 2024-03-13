using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Net.Security;
using Microsoft.JSInterop;
using BlazorBookShop.Client.StringHelpers;
using System.Net.Http.Headers;
using BlazorBookShop.Client.Helpers;
using BlazorBookShop.Shared.DTOs;

namespace BlazorBookShop.Client.Auth
{
    public class HelperAuthenticationProvider :  AuthenticationStateProvider,IAuthenticationService
    {
        private readonly IJSRuntime js;
        private readonly HttpClient _httpClient;
        private ITokenService _tokenService;
        private readonly string tokenKey = "TokenKey";

        public HelperAuthenticationProvider(IJSRuntime js, HttpClient httpClient, ITokenService tokenService)
        {
            this.js = js;
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        private AuthenticationState anonymous =>
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await js.GetTokenInLocalStorage(tokenKey);
            if (token == null)
            {
                return anonymous;
            }

            return CreateAuthenticationState(token);


            //await Task.Delay(3000);
            //var user = new ClaimsIdentity(new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name,"Tarun")
            //    //new Claim(ClaimTypes.Role,"Admin")
            //},"Test");
            //return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(user)));
        }

        private AuthenticationState CreateAuthenticationState(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(_tokenService.GetClaims(token),"JWT")));
        }

        public async Task Login(DtoToSend tokenData)
        {
            await js.SetTokenInLocalStorage(tokenKey, tokenData.Token);
            var authenticationState = CreateAuthenticationState(tokenData.Token);
            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }

        public async Task Logout()
        {
           await js.RemoveTokenInLocalStorage(tokenKey);
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(anonymous));


        }
    }
}
