﻿using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spear.Consul;
using Spear.Core;
using Spear.Protocol.Http;
using Spear.Protocol.Tcp;
using Spear.ProxyGenerator;
using Spear.Tests.Contracts;
using System;
using System.Threading.Tasks;

namespace Spear.Tests.Client.Benchmark
{
    [MemoryDiagnoser]//内存评测
    public class SpearBenchmarks
    {
        private IServiceProvider _provider;
        private ITestContract _contract;

        [Params("shay", "12345")]
        public string Name;

        [GlobalSetup]
        public void Setup()
        {
            var services = new ServiceCollection()
                .AddMicroClient(opt =>
                {
                    opt.AddJsonCoder()
                        .AddHttpProtocol()
                        .AddTcpProtocol()
                        .AddConsul("http://192.168.0.252:8500");
                });
            _provider = services.BuildServiceProvider();
            var proxy = _provider.GetService<IProxyFactory>();
            _contract = proxy.Create<ITestContract>();
        }

        [Benchmark]
        public async Task Notice()
        {
            await _contract.Notice(Name);
        }

        [Benchmark]
        public async Task Get()
        {
            var result = await _contract.Get(Name);
            _provider.GetService<ILogger<SpearBenchmarks>>().LogInformation(result);
        }
    }
}
