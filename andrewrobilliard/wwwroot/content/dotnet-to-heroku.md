---
title: Deploying .NET Core to Heroku
titleIcon: 🚀
date: "2020-08-31"
type: "blog"
---

I've always loved coding in C#. Equally, I love using Heroku to host personal projects - they have a nice UI, and [a free tier!](https://www.heroku.com/pricing)

However, there are a few limitations. Notably, .NET Core is not supported out of the box 😢. We can fix that!

## The Fix

While Heroku isn't able to run C# code directly, [they do have support for Docker containers](https://www.heroku.com/deploy-with-docker). With that our solution is clear - dockerize all the things!

## Deploying a .NET Core App to Docker

The following is not meant to be a .NET Core course, but you might pick up some basics on navigating the base files if you're a beginner. What we will cover:

- Creating a new .NET Core MVC project from the command-line
- Modifying the base files to be Heroku & Docker compatible
- Configuring a new Heroku app
- Deploy our app to Heroku 🚀

### 1. Getting Started

Before we get into changing some code, we need to get our foundation components in order. Before continuing, you should have:

1. .NET Core installed on your device

   - I'm using v3.1 for this tutorial, which can be found [here](https://dotnet.microsoft.com/download/dotnet-core/3.1)

2. Docker installed on your device

   - You can follow the steps [here](https://docs.docker.com/get-docker/)

3. A Heroku account! - https://signup.heroku.com/
4. Install the [Heroku CLI](https://devcenter.heroku.com/articles/heroku-cli), which allows us to communicate with the Container Registry.

With those out of the way, we're ready to go.

### 2. Creating a New .NET Core App

First off, we need to create our new .NET Core app. I'll be following the steps for setting up a basic .NET Core MVC project, which can be found [here](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-3.1&tabs=visual-studio).

### 3. Setting Up a New Dockerfile

The dockerfile required is pretty basic, with nothing too complex happening.

```dockerfile
# Dockerfile

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .

# Run the app on container startup
# Use your project name for the second parameter
# e.g. MyProject.dll
ENTRYPOINT [ "dotnet", "HerokuApp.dll" ]
```

Additionally, you should probably setup a .dockerignore file to keep your image size down.

```dockerfile
# .dockerignore

bin/
obj/
```

If you want to test out your container, you can run

```bash
docker build -t YourAppName .

# The name variable (abc) is simply used to refer to the
# container later when we want to close everything down.
docker run -d -p 8080:80 --name abc YourAppName
```

Navigate to http://localhost:8080 and you should see your app running.

All looking okay? Great! Lets shutdown the container locally. We don't need it running here if it's about to be up on the internet!

```bash
docker rm --force abc
```

### 4. Configuring a New App in Heroku

1. Sign into Heroku, and create a new app from your personal dashboard at https://dashboard.heroku.com/apps

- You'll need to provide an app name, and deployment region

2. Heroku will then display the various deployment options. We're going to be using **Container Registry**
   ![Heroku Deployment Options](https://dev-to-uploads.s3.amazonaws.com/i/5npszhxskx1snumvvc0b.png)

### 5. Releasing to Heroku 🚀

Last but not least, let's get it online.

1\. Using the command-line, login to the Heroku container registry

```bash
heroku container:login
```

2\. If you didn't above, make sure you build your docker
container!

```bash
docker build -t YourAppName .
```

3\. Push your newly built container up to Heroku

```bash
# 'YourAppName' should be the name of the app you
# configured in Heroku in step 4.
heroku container:push -a YourAppName web
```

4\. Finally, release!

```bash
heroku container:release -a YourAppName web
```

Now, if you just navigate to https://your-app-name.herokuapp.com, you should see the base app. Wait... that didn't work! What happend?!

![Alt Text](https://dev-to-uploads.s3.amazonaws.com/i/herstg4eltqx335au7ts.png)

Take a look at the heroku logs

```bash
heroku logs --tail
```

Within the scary looking logs, you'll see that .NET Core failed to start 'Kestrel', which is the web server for .NET Core. This means our app wasn't able to launch 😢.

![Alt Text](https://dev-to-uploads.s3.amazonaws.com/i/g2uoy445ehg52ohblcl4.png)

If you remember when we started our docker container locally back when we configure it, one of the parameters we passed was a port. When Heroku gives us a port to use, our app needs to know about it!

Modify the end of the Dockerfile to be

```dockerfile
# Dockerfile

# ...

# ENTRYPOINT [ "dotnet", "HerokuApp.dll" ]
# Use the following instead for Heroku
CMD ASPNETCORE_URLS=http://*:$PORT dotnet HerokuApp.dll
```

This allows the our container to use the Heroku-provided port on startup.

Re-deploy everything to Heroku:

1. Build docker image
2. Do a heroku container:push
3. Do a heroku container:release

And voila - your very own .NET Core app hosted on the web.

## Conclusion

I hope the above tutorial is helpful, and allows some newbies to get started on their own .NET Core side projects.

If anyone reading has some extra tips, or a better approach, please leave a comment below!

Thanks for reading 👋
