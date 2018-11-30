#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["eStudent/eStudent.csproj", "eStudent/"]
RUN dotnet restore "eStudent/eStudent.csproj"
COPY . .
WORKDIR "/src/eStudent"
RUN dotnet build "eStudent.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "eStudent.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "eStudent.dll"]