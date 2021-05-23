# This test project is intended to be used to test the XGB.Net project and NuGet package.

## Creating the nuget package.
- The project now uses .NET Standard. Packaging can be done via Visual Studio. Alternatively try https://docs.microsoft.com/en-us/nuget/create-packages/creating-a-package-dotnet-cli
- Right click XGBoost project -> properties > Package, edit details (such as incrementing version number)
- Set configuration to release
- Rebuild Project/Solution
- Right click project -> clean
- Right click project -> pack

Instead of using Visual Studio, after editing details, the following should work:
- build the package with the `/c/dev/tools/nuget.exe pack XGBoost/XGBoost.nuspec` command
- sign in to NuGet.org and click upload

## testing the new nuget package.
- Open the XGBoostTests.sln solution (not the XGBoost.sln as this does not use the NuGet package)
- Wait until the new package is indexed
- Open the Package Manager Console and type `Install-Package PicNet.XGBoost`
- Build
- Run tests

Alternatively to test locally:
- Right click project -> Manage nuget packages
- In the top right of the Manage Nuget package window, click the settings icon
- Add in a package source, pointing to the packed .nupkg file. Should be in XGBoost\bin\Release