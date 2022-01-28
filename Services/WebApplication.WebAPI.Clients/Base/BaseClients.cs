﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.WebAPI.Clients.Base
{
    public abstract class BaseClient
    {
        protected HttpClient Http { get; }

        protected string Address { get; }

        protected BaseClient(HttpClient Client, string Address)
        {
            Http = Client;
            this.Address = Address;
        }

        protected T? Get<T>(string url) => GetAsync<T>(url).Result;
        protected async Task<T?> GetAsync<T>(string url)
        {
            var response = await Http.GetAsync(url).ConfigureAwait(false);
            return await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>()
                .ConfigureAwait(false);
        }

        protected HttpResponseMessage Post<T>(string url, T value) => PostAsync<T>(url, value).Result;
        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T value, CancellationToken Cancel = default)
        {
            var response = await Http.PostAsJsonAsync(url, value, Cancel).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Put<T>(string url, T value) => PutAsync<T>(url, value).Result;
        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T value, CancellationToken Cancel = default)
        {
            var response = await Http.PutAsJsonAsync(url, value, Cancel).ConfigureAwait(false);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;
        protected async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken Cancel = default)
        {
            var response = await Http.DeleteAsync(url, Cancel).ConfigureAwait(false);
            return response;
        }
    }
}
