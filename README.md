# MudBlazorWeatherApp

A simple weather web application built with Blazor WebAssembly and MudBlazor. It fetches weather data from the Norwegian Meteorological Institute (MET) API and displays it in a modern UI.

## Features
- Search for weather by city name (limited to a set of hardcoded cities)
- Displays temperature, humidity, wind speed, precipitation, and weather icons
- Responsive and clean UI using [MudBlazor](https://mudblazor.com/)

## Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Git](https://git-scm.com/)

### Clone the Repository
```bash
git clone https://github.com/yourusername/MudBlazorWeatherApp.git
cd MudBlazorWeatherApp
```

### Install Dependencies
The required NuGet packages (including MudBlazor) will be restored automatically when you build the project. If you want to restore them manually, run:
```bash
dotnet restore
```

If you need to install MudBlazor manually, run:
```bash
dotnet add MudBlazorWeatherApp.Client package MudBlazor
```

### Set Up the MET API Key
1. Register for a free API key at [MET API](https://api.met.no/).
2. Open `MudBlazorWeatherApp/appsettings.json` (in the main project folder).
3. Replace the value of `WeatherApiKey` with your own key:
   ```json
   {
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "AllowedHosts": "*",
     "WeatherApiKey": "your-api-key-here"
   }
   ```

### Build and Run the App
```bash
dotnet build MudBlazorWeatherApp.Client
dotnet run --project MudBlazorWeatherApp.Client
```

The app will start and you can access it in your browser at `http://localhost:5071` (or the port shown in the terminal).

## Notes
- For simplicity, only a set of common cities are supported for search. The app uses hardcoded latitude and longitude values for these cities to fetch weather data from the MET API.
- If you search for a city that is not in the hardcoded list, you will get a "City not found" message.

## Troubleshooting
- If you get errors about missing files or runtime config, make sure you are running the `MudBlazorWeatherApp.Client` project, not the server project.
- If you change the API key, rebuild the project.
- If you get errors about missing MudBlazor or other packages, run `dotnet restore` or install them manually as shown above.

## License
MIT
