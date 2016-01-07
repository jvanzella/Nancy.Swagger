﻿using ApprovalTests;
using ApprovalTests.Reporters;
using Nancy.Swagger.Modules;
using Nancy.Swagger.Services;
using Nancy.Testing;
using Xunit;

namespace Nancy.Swagger.Annotations.Tests
{
    [UseReporter(typeof(XUnitReporter))]
    public class SwaggerAnnotationsProviderTests
    {
        private readonly Browser _browser;

        public SwaggerAnnotationsProviderTests()
        {
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                with.ApplicationStartup((container, pipelines) =>
                {
                    container.Register<ISwaggerMetadataProvider, SwaggerAnnotationsProvider>();
                });

                with.Module<SwaggerModule>();
                with.Module<TestRoutesModule>();
            });

            _browser = new Browser(bootstrapper);
        }

        [Fact]
        public void Get_ApiDocsRootpath_ReturnsResourceListing()
        {
            ApproveJsonResponse(_browser.Get("/api-docs"));
        }

        private static void ApproveJsonResponse(BrowserResponse response)
        {
            Approvals.VerifyJson(response.Body.AsString());
        }
    }
}