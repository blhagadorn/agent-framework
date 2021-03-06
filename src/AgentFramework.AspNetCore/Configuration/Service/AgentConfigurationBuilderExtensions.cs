﻿using System.Net.Http;
using AgentFramework.AspNetCore.Runtime;
using AgentFramework.Core.Contracts;
using AgentFramework.Core.Handlers;
using AgentFramework.Core.Runtime;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AgentFramework.AspNetCore.Configuration.Service
{
    public static class ServiceBuilderExtensions
    {
        internal static AgentConfigurationBuilder AddDefaultServices(this AgentConfigurationBuilder builder)
        {
            builder.Services.TryAddSingleton<IEventAggregator, EventAggregator>();
            builder.Services.TryAddSingleton<IConnectionService, DefaultConnectionService>();
            builder.Services.TryAddSingleton<ICredentialService, DefaultCredentialService>();
            builder.Services.TryAddSingleton<ILedgerService, DefaultLedgerService>();
            builder.Services.TryAddSingleton<IPoolService, DefaultPoolService>();
            builder.Services.TryAddSingleton<IProofService, DefaultProofService>();
            builder.Services.TryAddSingleton<IProvisioningService, DefaultProvisioningService>();
            builder.Services.TryAddSingleton<IMessageService, DefaultMessageService>();
            builder.Services.TryAddSingleton<HttpClient>();
            builder.Services.TryAddSingleton<ISchemaService, DefaultSchemaService>();
            builder.Services.TryAddSingleton<ITailsService, DefaultTailsService>();
            builder.Services.TryAddSingleton<IWalletRecordService, DefaultWalletRecordService>();
            builder.Services.TryAddSingleton<IWalletService, DefaultWalletService>();
            return builder;
        }

        public static AgentConfigurationBuilder AddMessageHandler<TMessageHandler>(this AgentConfigurationBuilder builder) where TMessageHandler : class,
            IMessageHandler
        {
            builder.Services.AddSingleton<IMessageHandler, TMessageHandler>();
            builder.Services.TryAddSingleton<TMessageHandler>();
            return builder;
        }

        public static AgentConfigurationBuilder OverrideDefaultMessageHandlers(this AgentConfigurationBuilder builder)
        {
            builder.RegisterCoreMessageHandlers = false;
            return builder;
        }

        public static AgentConfigurationBuilder AddMemoryCacheLedgerService(this AgentConfigurationBuilder builder, MemoryCacheEntryOptions options = null)
        {
            builder.AddExtendedLedgerService<MemoryCacheLedgerService>()
                   .Services.AddMemoryCache()
                            .AddSingleton(_ => options);

            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedConnectionService<TService, TImplementation>(this AgentConfigurationBuilder builder)
            where TService : class, IConnectionService
            where TImplementation : class, TService, IConnectionService
        {
            builder.Services.AddSingleton<TImplementation>();
            builder.Services.AddSingleton<IConnectionService>(x => x.GetService<TImplementation>());
            builder.Services.AddSingleton<TService>(x => x.GetService<TImplementation>());
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedConnectionService<TImplementation>(this AgentConfigurationBuilder builder)
            where TImplementation : class, IConnectionService
        {
            builder.Services.AddSingleton<IConnectionService, TImplementation>();
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedCredentialService<TService, TImplementation>(this AgentConfigurationBuilder builder)
            where TService : class, ICredentialService
            where TImplementation : class, TService, ICredentialService
        {
            builder.Services.AddSingleton<TImplementation>();
            builder.Services.AddSingleton<ICredentialService>(x => x.GetService<TImplementation>());
            builder.Services.AddSingleton<TService>(x => x.GetService<TImplementation>());
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedCredentialService<TImplementation>(this AgentConfigurationBuilder builder)
            where TImplementation : class, ICredentialService
        {
            builder.Services.AddSingleton<ICredentialService, TImplementation>();
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedLedgerService<TService, TImplementation>(this AgentConfigurationBuilder builder)
            where TService : class, ILedgerService
            where TImplementation : class, TService, ILedgerService
        {
            builder.Services.AddSingleton<TImplementation>();
            builder.Services.AddSingleton<ILedgerService>(x => x.GetService<TImplementation>());
            builder.Services.AddSingleton<TService>(x => x.GetService<TImplementation>());
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedLedgerService<TImplementation>(this AgentConfigurationBuilder builder)
            where TImplementation : class, ILedgerService
        {
            builder.Services.AddSingleton<ILedgerService, TImplementation>();
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedPoolService<TService, TImplementation>(this AgentConfigurationBuilder builder)
            where TService : class, IPoolService
            where TImplementation : class, TService, IPoolService
        {
            builder.Services.AddSingleton<TImplementation>();
            builder.Services.AddSingleton<IPoolService>(x => x.GetService<TImplementation>());
            builder.Services.AddSingleton<TService>(x => x.GetService<TImplementation>());
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedPoolService<TImplementation>(this AgentConfigurationBuilder builder)
            where TImplementation : class, IPoolService
        {
            builder.Services.AddSingleton<IPoolService, TImplementation>();
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedProofService<TService, TImplementation>(this AgentConfigurationBuilder builder)
            where TService : class, IProofService
            where TImplementation : class, TService, IProofService
        {
            builder.Services.AddSingleton<TImplementation>();
            builder.Services.AddSingleton<IProofService>(x => x.GetService<TImplementation>());
            builder.Services.AddSingleton<TService>(x => x.GetService<TImplementation>());
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedProofService<TImplementation>(this AgentConfigurationBuilder builder)
            where TImplementation : class, IProofService
        {
            builder.Services.AddSingleton<IProofService, TImplementation>();
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedProvisioningService<TService, TImplementation>(this AgentConfigurationBuilder builder)
            where TService : class, IProvisioningService
            where TImplementation : class, TService, IProvisioningService
        {
            builder.Services.AddSingleton<TImplementation>();
            builder.Services.AddSingleton<IProvisioningService>(x => x.GetService<TImplementation>());
            builder.Services.AddSingleton<TService>(x => x.GetService<TImplementation>());
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedProvisioningService<TImplementation>(this AgentConfigurationBuilder builder)
            where TImplementation : class, IProvisioningService
        {
            builder.Services.AddSingleton<IProvisioningService, TImplementation>();
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedMessageService<TService, TImplementation>(this AgentConfigurationBuilder builder)
            where TService : class, IMessageService
            where TImplementation : class, TService, IMessageService
        {
            builder.Services.AddSingleton<TImplementation>();
            builder.Services.AddSingleton<IMessageService>(x => x.GetService<TImplementation>());
            builder.Services.AddSingleton<TService>(x => x.GetService<TImplementation>());
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedMessageService<TImplementation>(this AgentConfigurationBuilder builder)
            where TImplementation : class, IMessageService
        {
            builder.Services.AddSingleton<IMessageService, TImplementation>();
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedSchemaService<TService, TImplementation>(this AgentConfigurationBuilder builder)
            where TService : class, ISchemaService
            where TImplementation : class, TService, ISchemaService
        {
            builder.Services.AddSingleton<TImplementation>();
            builder.Services.AddSingleton<ISchemaService>(x => x.GetService<TImplementation>());
            builder.Services.AddSingleton<TService>(x => x.GetService<TImplementation>());
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedSchemaService<TImplementation>(this AgentConfigurationBuilder builder)
            where TImplementation : class, ISchemaService
        {
            builder.Services.AddSingleton<ISchemaService, TImplementation>();
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedTailsService<TService, TImplementation>(this AgentConfigurationBuilder builder)
            where TService : class, ITailsService
            where TImplementation : class, TService, ITailsService
        {
            builder.Services.AddSingleton<TImplementation>();
            builder.Services.AddSingleton<ITailsService>(x => x.GetService<TImplementation>());
            builder.Services.AddSingleton<TService>(x => x.GetService<TImplementation>());
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedTailsService<TImplementation>(this AgentConfigurationBuilder builder)
            where TImplementation : class, ITailsService
        {
            builder.Services.AddSingleton<ITailsService, TImplementation>();
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedWalletRecordService<TService, TImplementation>(this AgentConfigurationBuilder builder)
            where TService : class, IWalletRecordService
            where TImplementation : class, TService, IWalletRecordService
        {
            builder.Services.AddSingleton<TImplementation>();
            builder.Services.AddSingleton<IWalletRecordService>(x => x.GetService<TImplementation>());
            builder.Services.AddSingleton<TService>(x => x.GetService<TImplementation>());
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedWalletRecordService<TImplementation>(this AgentConfigurationBuilder builder)
            where TImplementation : class, IWalletRecordService
        {
            builder.Services.AddSingleton<IWalletRecordService, TImplementation>();
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedWalletService<TService, TImplementation>(this AgentConfigurationBuilder builder)
            where TService : class, IWalletService
            where TImplementation : class, TService, IWalletService
        {
            builder.Services.AddSingleton<TImplementation>();
            builder.Services.AddSingleton<IWalletService>(x => x.GetService<TImplementation>());
            builder.Services.AddSingleton<TService>(x => x.GetService<TImplementation>());
            return builder;
        }

        public static AgentConfigurationBuilder AddExtendedWalletService<TImplementation>(this AgentConfigurationBuilder builder)
            where TImplementation : class, IWalletService
        {
            builder.Services.AddSingleton<IWalletService, TImplementation>();
            return builder;
        }
    }
}
