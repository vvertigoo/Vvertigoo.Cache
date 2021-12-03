#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Vvertigoo.Cache/Vvertigoo.Cache.csproj", "Vvertigoo.Cache/"]
RUN dotnet restore "Vvertigoo.Cache/Vvertigoo.Cache.csproj"
COPY . .
WORKDIR "/src/Vvertigoo.Cache"
RUN dotnet build "Vvertigoo.Cache.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vvertigoo.Cache.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vvertigoo.Cache.dll"]