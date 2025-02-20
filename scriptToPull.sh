#!/bin/bash

# Stop the existing application service if it exists
sudo systemctl stop halfandhalf.service || true

# Clear the deployment directory
sudo rm -rf /var/www/halfandhalf/*

# Clone/pull the latest code
cd /var/www/halfandhalf
git pull origin main || git clone https://gitlab.cnsalab.net/your-username/HalfAndHalf.git .

# Restore and publish the application
dotnet restore
dotnet publish -c Release -o ./publish

# Update the service file if needed
sudo cp /var/www/halfandhalf/halfandhalf.service /etc/systemd/system/
sudo systemctl daemon-reload

# Start the application
sudo systemctl start halfandhalf.service
sudo systemctl enable halfandhalf.service
