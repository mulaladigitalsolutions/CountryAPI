# CountryAPI

Please make sure you read through this document and follow each step before attempting to use this application.

Assumptions are that VS code or Visual studio has been pre-installed on your machine
 - If not, download VS studio 2022 Community or later with .Net Core 8 or higher
 - On VS Code, install Angular 19.1.5 or higher
 - Install Node 22.13.1 or higher
 - Package Manager: npm 10.9.2 or higher
 - Please see the readme files on the two apps for full context


1. Clone or download the following two solutions
 - CountryAPI
 - CountryApp
-----------------------------------------------------------
2. Running the Country API Code on VS Code
 - Navigate to the CountryAPI\CountryAPI folder
 - Open Terminal on the active folder
 - type dotnet run
 - It may take a few minutes to restore the files
 - Look for "Now listening on: http://localhost:5030" (It may differ in port numbers depending on your pc setup
 - Copy the url and update the country.service.ts file in the Angular App (CountryApp)

OR

2. Running the Country API Code on Visual Studio
 - Navigate to the CountryAPI folder
 - Open solution with Visual Studio 2022 or later with .Net Core 8 or higher
 - Make sure you are running on Administrator mode because sometimes Visual Studio can block some functionality, especially when dealing 	with hosted applications 
 - Click Run
 - It may take a few minutes to restore some packages
 - Look for a browser that will open with your swagger documentation
 - You can now test you API using swagger
 - Copy the url and update the country.service.ts file in the Angular App (CountryApp)

-----------------------------------------------------------
3. Running the Country App Code on Visual Studio Code
 - Navigate to the CountryApp folder
 - Locate a folder called services
 - Open the country.service.ts file in the Angular App (CountryApp)
 - Verify that the apiUrl string there is matches that of your api url (the one currently open on your browser)
 - Open Terminal on the active folder
 - Type ng serve -o and hit enter
 - It may take a few minutes to restore the files
 - Depending on your browser settings, it will run the application and open it on a new browser or tab

-----------------------------------------------------------


Happy Conding :)



