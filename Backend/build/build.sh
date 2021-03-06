#!/bin/bash

printf "[x] Build started"

# Setup directory

cd ../

# Setup assemblies

declare -A assemblies

assemblies["OneGate.Backend.Gateway.User.Api"]="Gateway/User/src"
assemblies["OneGate.Backend.Gateway.Admin.Api"]="Gateway/Admin/src"

assemblies["OneGate.Backend.Core.Assets.Api"]="Core/Assets/src"
assemblies["OneGate.Backend.Core.Analytics.Api"]="Core/Analytics/src"
assemblies["OneGate.Backend.Core.Engines.Api"]="Core/Engines/src"
assemblies["OneGate.Backend.Core.Timeseries.Api"]="Core/Timeseries/src"
assemblies["OneGate.Backend.Core.Users.Api"]="Core/Users/src"

assemblies["OneGate.Backend.Core.Timeseries.Worker"]="Core/Timeseries/src"

assemblies["OneGate.Backend.Engines.Fake.Static"]="Engines/Fake/src"
assemblies["OneGate.Backend.Engines.Fake.Worker"]="Engines/Fake/src"

# Dll build
for name in "${!assemblies[@]}"; do
  # .NET publish
  printf "\n\n[x] Build DLL for %s\n\n" "${name}"
  
  dotnet publish "projects/${assemblies[${name}]}/${name}" -c Release -o "out/${name}"
done

# Setup containers

declare -A images

assemblies["OneGate.Backend.Gateway.User.Api"]="onegate/gateway-user-api"
assemblies["OneGate.Backend.Gateway.Admin.Api"]="onegate/gateway-admin-api"

assemblies["OneGate.Backend.Core.Assets.Api"]="onegate/core-assets-api"
assemblies["OneGate.Backend.Core.Analytics.Api"]="onegate/core-analytics-api"
assemblies["OneGate.Backend.Core.Engines.Api"]="onegate/core-engines-api"
assemblies["OneGate.Backend.Core.Timeseries.Api"]="onegate/core-timeseries-api"
assemblies["OneGate.Backend.Core.Users.Api"]="onegate/core-users-api"

assemblies["OneGate.Backend.Core.Timeseries.Worker"]="onegate/core-timeseries-worker"

assemblies["OneGate.Backend.Engines.Fake.Static"]="onegate/engines-fake-static"
assemblies["OneGate.Backend.Engines.Fake.Worker"]="onegate/engines-fake-worker"

# Images build
for name in "${!images[@]}"; do
  # Setup dockerfile
  sed -e "s/\${ASSEMBLY_NAME}/${name}/g" "build/templates/.net/Dockerfile" > "out/${name}/Dockerfile"
  
  # Build
  printf "\n\n[x] Build Docker for %s\n\n" "${name}"
  
  docker build "out/${name}" -t "${images[${name}]}:latest"
done

# Remove build files
rm -rf "out"
