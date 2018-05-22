# This test project is intended to be used to test the XGB.Net project and NuGet package.

## Creating the nuget package.
- edit XGBoost.nuspec and make changes required including incrementing the version number
- build the package with the `/c/dev/tools/nuget.exe pack XGBoost/XGBoost.nuspec` command
- sign in to NuGet.org and click upload


## testing the new nuget package.
- Open the XGBoostTests.sln solution (not the XGBoost.sln as this does not use the NuGet package)
- Wait until the new package is indexed
- Open the Package Manager Console and type `Install-Package PicNet.XGBoost`
- Build
- Run tests