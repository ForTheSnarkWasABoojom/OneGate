#!/bin/bash

printf "[x] Build started"

# Setup directory

cd ../

# Setup assemblies

declare -A assemblies

assemblies["OneGate.Backend.Gateway.UserApi"]="Gateway/User/src"
assemblies["OneGate.Backend.Gateway.AdminApi"]="Gateway/Admin/src"

assemblies["OneGate.Backend.Core.Records"]="Core/Records/src"
assemblies["OneGate.Backend.Core.Timeseries"]="Core/Timeseries/src"
assemblies["OneGate.Backend.Core.Users"]="Core/Users/src"

assemblies["OneGate.Backend.Engines.FakeEngine"]="Engines/Fake/src"
assemblies["OneGate.Backend.Engines.FakeStaticEngine"]="Engines/Fake/src"

# Dll build
for name in "${!assemblies[@]}"; do
  # .NET publish
  printf "\n\n[x] Build DLL for %s\n\n" "${name}"
  
  dotnet publish "projects/${assemblies[${name}]}/${name}" -c Release -o "out/${name}"
done

# Setup containers

declare -A images

assemblies["OneGate.Backend.Gateway.UserApi"]="onegate/gateway-user-api"
assemblies["OneGate.Backend.Gateway.AdminApi"]="onegate/gateway-admin-api"

assemblies["OneGate.Backend.Core.Records"]="onegate/core-records"
assemblies["OneGate.Backend.Core.Timeseries"]="onegate/core-timeseries"
assemblies["OneGate.Backend.Core.Users"]="onegate/core-users"

assemblies["OneGate.Backend.Engines.FakeEngine"]="onegate/engine-fake"
assemblies["OneGate.Backend.Engines.FakeStaticEngine"]="onegate/core-fake-static"

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
