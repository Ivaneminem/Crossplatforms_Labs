#!/bin/bash

/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"

brew install --cask dotnet-sdk

cd /vagrant
dotnet run run lab1 --input Lab_1/INPUT.txt --output Lab_1/OUTPUT.txt