---
title: 💵 Split Wallet
date: "2019-04-03"
type: Project
projectIcons:
  - name: docker
    cssClass: devicon-docker-plain-wordmark colored
  - name: csharp
    cssClass: devicon-csharp-plain colored
  - name: dot-net
    cssClass: devicon-dot-net-plain-wordmark colored
  - name: postgresql
    cssClass: devicon-postgresql-plain colored
  - name: react
    cssClass: devicon-react-original colored
  - name: heroku
    cssClass: devicon-heroku-original-wordmark colored
projectLink: "https://github.com/ALRobilliard/Split"
---

Manage your money, easily share expenses with others, take control.

Split is a web application that seeks to make financial and transactional management simple and easy. The main target audience is people who need to "split" multiple transaction in their day to day lives - couples, or flatmates groups of friends.

## The Project

### The Backend

The backend is structured as a .NET core web API, which connects to a hosted PostgreSQL database containing transactional and user data. The API in it's current state surfaces basic CRUD actions for all of the entities required by the process. It also handles authentication & authorization through an implementation of [IdentityServer](https://identityserver.io/).

The API exists within a Docker container to provide a stable and consistent deployment. This container is hosted in [Heroku](https://www.heroku.com/), alongside the PostgreSQL database it connects to.

### The Frontend

The frontend is a React SPA consistency of basic data tables and create/update pages for each entity. Once I've finished a working prototype, the intention is to provide a dashboard with configurable graphs and charts to analyse this data.

### Next Steps

With mostly minor tweaks required to get version 1 up and running, the next big step is to provide a mobile interface for the functionality that has been delivered so far.
