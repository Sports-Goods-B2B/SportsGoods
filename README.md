# SportsGood

App Start-up: After cloning the repository and opening the project, set SportsGoods.Web as the start up project and try to run the project. If there are any errors, clean and rebuild the solution.

Issue: Unable to Resolve NuGet Packages Description: Encountered error NU1100 when attempting to build the project: "Error NU1100: Unable to resolve 'Microsoft.EntityFrameworkCore (>= 8.0.3)' for 'net8.0'. PackageSourceMapping is enabled, the following source(s) were not considered: Microsoft Visual Studio Offline Packages, nuget.org."

Solution:

1.Check Package Sources: Verify that nuget.org is listed as an available package source and is enabled in Visual Studio's NuGet Package Manager settings. Also, ensure that Microsoft Visual Studio Offline Packages is enabled. 2.Update NuGet Package Manager: Install any available updates for NuGet Package Manager via Visual Studio's Extensions Manager. 3.Clear NuGet Package Cache: Delete the contents of the C:\Users{YourUsername}.nuget\packages directory to clear the NuGet package cache. Restart Visual Studio and attempt building the project again. 4.Check Target Framework Compatibility: Ensure that the target framework (net8.0) is compatible with the version of Microsoft.EntityFrameworkCore (>= 8.0.3) being installed. Check compatibility matrices on official documentation or NuGet package pages. 5.Retry Installation: If issues persist, retry installing the Microsoft.EntityFrameworkCore package after some time, as transient network issues or server outages may affect package resolution.

End-Point Summary and Documentation:
We have added the GetAllProducts end-point in our web-api project which returns a list of products based on given page number and page size.
We are using a properly configured swagger in the program.cs file.
Because we are using CQRS pattern we have created Query and Query handler. 
We have created ProductExtension class that maps the properties of the Product class to ProductDTO which we are using to pass information to the swagger UI.
Query Handler functionality is tested. 
To test the functionality of the GetAllMethods end-point: 
1. Run the Web-Api project.
2. You should see the GetAllProducts() method, click on it and then click try it out.
3. There are default values on the parameters but you can set custom ones for yourself (keep in mind that they have to be positive or 0, otherwise we have handled and send an exception message)
4. If the parameters are correct you should see a list of products with a count of the pageSize parameter.
