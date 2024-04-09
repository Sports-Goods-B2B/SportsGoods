# SportsGoods

App Start-up:
After cloning the repository and opening the project, set SportsGoods.Web as the start up project and try to run the project.
If there are any errors, clean and rebuild the solution.

Issue: Unable to Resolve NuGet Packages
Description:
Encountered error NU1100 when attempting to build the project: 
"Error NU1100: Unable to resolve 'Microsoft.EntityFrameworkCore (>= 8.0.3)' for 'net8.0'.
PackageSourceMapping is enabled, the following source(s) were not considered: Microsoft Visual Studio Offline Packages, nuget.org."

Solution:

1.Check Package Sources: Verify that nuget.org is listed as an available package source and is enabled in Visual Studio's NuGet Package Manager settings. Also, ensure that Microsoft Visual Studio Offline Packages is enabled.
2.Update NuGet Package Manager: Install any available updates for NuGet Package Manager via Visual Studio's Extensions Manager.
3.Clear NuGet Package Cache: Delete the contents of the C:\Users\{YourUsername}\.nuget\packages directory to clear the NuGet package cache. Restart Visual Studio and attempt building the project again.
4.Check Target Framework Compatibility: Ensure that the target framework (net8.0) is compatible with the version of Microsoft.EntityFrameworkCore (>= 8.0.3) being installed. Check compatibility matrices on official documentation or NuGet package pages.
5.Retry Installation: If issues persist, retry installing the Microsoft.EntityFrameworkCore package after some time, as transient network issues or server outages may affect package resolution.
