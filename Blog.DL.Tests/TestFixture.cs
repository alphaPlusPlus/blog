using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Blog.DL.Tests
{
    public class TestFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly TestServer Server;
        private readonly HttpClient Client;

        public TestFixture()
        {
            var builder = new WebHostBuilder()
                .UseContentRoot(@"C:\Users\BHU11725\source\repos\Blog\Blog.API\")
                .UseStartup<TStartup>();

            Server = new TestServer(builder);
            Client = new HttpClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }
    }
}
