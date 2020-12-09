#!/bin/bash

printf "[x] Build started"

# Setup directory

cd ../

# Setup assemblies

declare -A assemblies

assemblies["OneGate.Backend.Gateway"]="Gateway/src"

assemblies["OneGate.Backend.Core.User.Service"]="Core/User/src"
assemblies["OneGate.Backend.Core.Asset.Service"]="Core/Asset/src"
assemblies["OneGate.Backend.Core.Series.Service"]="Core/Series/src"

assemblies["OneGate.Backend.Engines.FakeEngine"]="Engines/src"
assemblies["OneGate.Backend.Engines.FakeStaticEngine"]="Engines/src"

# Dll build
for name in "${!assemblies[@]}"; do
  # .NET publish
  printf "\n\n[x] Build DLL for %s\n\n" "${name}"
  
  dotnet publish "projects/${assemblies[${name}]}/${name}" -c Release -o "out/${name}"
done

# Setup containers

declare -A images

images["OneGate.Backend.Gateway"]="onegate/gateway"

images["OneGate.Backend.Core.User.Service"]="onegate/user_service"
images["OneGate.Backend.Core.Asset.Service"]="onegate/asset_service"
images["OneGate.Backend.Core.Series.Service"]="onegate/series_service"

images["OneGate.Backend.Engines.FakeEngine"]="onegate/fake_engine"
images["OneGate.Backend.Engines.FakeStaticEngine"]="onegate/fake_static_engine"

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
