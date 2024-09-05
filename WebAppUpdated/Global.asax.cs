using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AppWithGacDependency
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Register the AssemblyResolve event handler to load assemblies with custom logic
            // if the CLR loader fails to load them. This handler must be registered before assemblies
            // requiring custom loading are loaded.
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(LoadAssemblyFromVersionSubdirectory);
        }

        // For more information on custom assembly loading, see
        // https://learn.microsoft.com/dotnet/standard/assembly/resolve-loads
        Assembly LoadAssemblyFromVersionSubdirectory(object sender, ResolveEventArgs args)
        {
            // Parse the assembly name with the AssemblyName constructor
            var assemblyName = new AssemblyName(args.Name);

            // Get the directory of the currently executing directory
            // (in order to load assemblies from a path relative to the executing assembly)
            var contentRoot = Path.GetDirectoryName(typeof(MvcApplication).Assembly.CodeBase).Replace(@"file:\", string.Empty);

            // Based on the assembly name and version (and public key token, if necessary),
            // load the assembly from the appropriate path
            if (assemblyName.Version == new Version(1, 1, 0, 0) && assemblyName.Name == "Greeter")
            {
                var assemblyPath = Path.Combine(contentRoot, "Binaries", "v1", "Greeter.dll");

                // Although the handler loads the assembly into the LoadFrom context,
                // it will be loaded into the Load context by the CLR when it is returned
                return Assembly.LoadFrom(assemblyPath);
            }
            else if (assemblyName.Version == new Version(2, 0, 0, 0) && assemblyName.Name == "Greeter")
            {
                var assemblyPath = Path.Combine(contentRoot, "Binaries", "v2", "Greeter.dll");
                return Assembly.LoadFrom(assemblyPath);
            }
            else
            {
                // If the assembly is not found, return null
                return null;
            }
        }
    }
}
