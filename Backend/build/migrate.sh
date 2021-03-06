#!/bin/bash

migrationName=$(date +%s)

printf "[x] Migration %s started\n\n" "${migrationName}"

# Setup projects

cd ../
workingDir=$(pwd)

declare -A assemblies

assemblies["OneGate.Backend.Core.Assets.Database.Migrations"]="Core/Assets/src"
assemblies["OneGate.Backend.Core.Analytics.Database.Migrations"]="Core/Analytics/src"
assemblies["OneGate.Backend.Core.Engines.Database.Migrations"]="Core/Engines/src"
assemblies["OneGate.Backend.Core.Timeseries.Database.Migrations"]="Core/Timeseries/src"
assemblies["OneGate.Backend.Core.Users.Database.Migrations"]="Core/Users/src"

declare -A connectionStrings

connectionStrings["OneGate.Backend.Core.Assets.Database.Migrations"]="server=localhost;port=5432;database=assets;username=test;password=test"
connectionStrings["OneGate.Backend.Core.Analytics.Database.Migrations"]="server=localhost;port=5432;database=analytics;username=test;password=test"
connectionStrings["OneGate.Backend.Core.Engines.Database.Migrations"]="server=localhost;port=5432;database=engines;username=test;password=test"
connectionStrings["OneGate.Backend.Core.Timeseries.Database.Migrations"]="server=localhost;port=5432;database=timeseries;username=test;password=test"
connectionStrings["OneGate.Backend.Core.Users.Database.Migrations"]="server=localhost;port=5432;database=users;username=test;password=test"

# Run dotnet migrations
for name in "${!assemblies[@]}"; do
  # .NET publish
  printf "\n\n[x] Run migration for %s\n\n" "${name}"
  
  cd "${workingDir}" || exit
  cd "projects/${assemblies[${name}]}/${name}" || exit
  
  dotnet ef migrations add "${migrationName}" && dotnet ef database update "${migrationName}" --connection "${connectionStrings[${name}]}"
done