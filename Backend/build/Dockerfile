# Builder
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
ARG PROJECT_PATH
ARG PROJECT_NAME
WORKDIR /app

COPY . .
RUN dotnet publish ${PROJECT_PATH}/${PROJECT_NAME} -c Release -o out

# Runner
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ARG PROJECT_NAME
ENV PROJECT_NAME_ENV=$PROJECT_NAME
WORKDIR /app

COPY --from=build-env /app/out .
ENTRYPOINT dotnet ${PROJECT_NAME_ENV}.dll