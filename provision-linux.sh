#!/bin/bash

sudo apt-get update
sudo apt-get install -y wget apt-transport-https
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0

# Перехід до директорії проекту
cd /vagrant

# Додавання NuGet пакету
#dotnet add package IBondarenko --version 1.0.0

# Запуск проекту
dotnet run run lab1 --input Lab_1/INPUT.txt --output Lab_1/OUTPUT.txt