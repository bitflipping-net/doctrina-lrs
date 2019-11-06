## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio Code or 2019](https://www.visualstudio.com/downloads/)
* [.NET Core SDK 3.0](https://www.microsoft.com/net/download/dotnet-core/3.0)
* [Node JS](https://nodejs.org/en/download/)

### Setup
Follow these steps to get your development environment set up:

   1. Clone the repository
   2. Initialize submodule in the local configuration
      ```
      git submodule init
      ```
   3. Fetch all data for each submodule
      ```
      git submodule update
      ```

Next pick your development tool

#### Command line/VS Code (RECOMMENDED)
Follow these steps to get your development environment set up:

  1. Within the `.\src\` directory, restore required packages by running:
     ```
     dotnet restore .\Doctrina.sln
     ```
  2. Next, build the solution by running:
     ```
     dotnet build .\Doctrina.sln --no-restore
     ```
  4. Next, within the `.\src\Doctrina.WebUI\` start the server by running:
     ```
	  dotnet run
	  ```
  5. Launch [http://localhost:5000/](http://localhost:5000/) in your browser to view the React SPA

  6. Launch [https://localhost:5001/xapi/about](https://localhost:5001/xapi/about) in your browser to view the xAPI about resource

#### Visual Studio
Follow these steps to get your development environment set up:
1. Open the `Doctrina.sln` inside the `src` folder.
2. Hit `F5` or `CTRL+F5` to start from Doctrina.WebUI.csproj
3. Launch [https://localhost:44338/](https://localhost:44338/)
4. Launch [https://localhost:44338/xapi/about](https://localhost:44338/xapi/about) in your browser to view the xAPI about resource

5. Try posting a statem

#### Docker Compose (WIP)

1. At the src directory, build the image by running:
   ```
   docker-compose build
   ```
2. After image has finished building, start a new container by running:
   ```
   docker run doctrina
   ```

### Testing
After following the steps for setup, do the following to run the `lrs-conformance-test-suite`

1. Within the `.\lrs-conformance-test-suite` run the following:
   ```
   npm install
   ```
1.1 If installation fails, do the following:
   ```
   npm update
   ```
1.2
   ```
   npm audit fix
   ```
2. After npm packages have been installed, run the following:
   ```
   node bin/console_runner.js -e https://localhost:5001 -a -u admin@example.com -p zKR4gkYNHP5tvH
   ```


## Technologies
* .NET Core 3.0
* ASP.NET Core 3.0
* Entity Framework Core 3.0
* NodeJS
* React
* Office React Fabric UI

## License
This project is licensed under the AGPLv3 License - see the [LICENSE](https://github.com/bitflipping-solutions/doctrina-lrs/blob/develop/LICENSE) file for details.
