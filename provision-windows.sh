# Install Chocolatey
Set-ExecutionPolicy Bypass -Scope Process -Force
[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072
iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))

# Install .NET SDK 8.0
choco install dotnet-8.0-sdk -y

# Refresh environment variables
refreshenv

# Verify installation
dotnet --version

# Додавання NuGet пакету
#dotnet add package IBondarenko --version 1.0.0

# Navigate to the project directory
cd C:\project

# Run LAB4
dotnet run --project Lab_4 -- --input Lab_1\INPUT.txt --output Lab_1\OUTPUT.txt

Write-Host "Windows environment setup complete and LAB1 executed"