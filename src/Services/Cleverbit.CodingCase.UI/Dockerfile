#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Cleverbit.CodingCase.UI/Cleverbit.CodingCase.UI.csproj", "src/Services/Cleverbit.CodingCase.UI/"]
COPY ["src/Services/Cleverbit.CodingCase.Application/Cleverbit.CodingCase.Application.csproj", "src/Services/Cleverbit.CodingCase.Application/"]
COPY ["src/Services/Cleverbit.CodingCase.Domain/Cleverbit.CodingCase.Domain/Cleverbit.CodingCase.Domain.csproj", "src/Services/Cleverbit.CodingCase.Domain/Cleverbit.CodingCase.Domain/"]
RUN dotnet restore "src/Services/Cleverbit.CodingCase.UI/Cleverbit.CodingCase.UI.csproj"
COPY . .
WORKDIR "/src/src/Services/Cleverbit.CodingCase.UI"
RUN dotnet build "Cleverbit.CodingCase.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cleverbit.CodingCase.UI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cleverbit.CodingCase.UI.dll"]