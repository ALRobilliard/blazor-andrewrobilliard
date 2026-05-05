---
title: "Modernizing My Digital Presence: Migrating from Gatsby/AWS to Blazor WASM & Azure"
date: "2026-05-05"
description: "Why I traded my Gatsby.js site for a custom Blazor WebAssembly solution hosted on Azure Static Web Apps."
type: blog
tags: ["Azure", "Blazor", "DotNet", "CloudArchitecture", "PowerPlatform"]
---

With my domain sitting stagnant for so long, I thought it was due time for an update. As
someone who works in the Microsoft and .NET space, I took the opportunity to both modernsise
my tech stack while aligning the site to the tools and ecosystem a use on a day-to-day basis.

My previous site was a Gatsby.js site hosted in an AWS S3 bucket, built when I was exploring various JavaScript frameworks in 2018/2019. While functional, it didn't represent my daily expertise in the Microsoft ecosystem, and was starting to get a bit stale. Here is how—and why—I migrated everything to a custom **Blazor WebAssembly** site hosted on **Azure Static Web Apps**.

## The Motivation for Change

Gatsby served me well for years, but as the project entered maintenance mode following its acquisition by Netlify, the lack of updates and plugin rot started to make things a bit bleak. More importantly, as a .NET developer, I wanted a site that also reflected my normal tech stack.

### The New Architecture

I moved away from a "Black Box" static site generator to a transparent, high-performance C# architecture:

1. **Frontend:** Blazor WebAssembly (WASM).
2. **Content Engine:** Custom Markdown-to-HTML service using `Markdig`.
3. **Metadata:** Strongly-typed YAML frontmatter parsing via `YamlDotNet`.
4. **Hosting:** Azure Static Web Apps (SWA).
5. **CI/CD:** Automated GitHub Actions for build-time post indexing.

## The Additional Engineering Choices

### 1. Build-Time Automation
Instead of a database, I implemented a custom **Post Indexer**. During the build process, a .NET console utility scans my `/content` folder and generates a `posts.json` file. This allows the frontend to stay fast and "stateless" while still providing a structured list of posts for the home page.

### 2. Bringing Logic to the Edge
By moving from AWS S3 to Azure Static Web Apps, I gained integrated CI/CD and managed SSL out of the box.

### 3. Performance & Cost
The move reduced my hosting costs from "a few dollars" to effectively **zero** thanks to the Azure SWA Free Tier. More importantly, using .NET's AOT compilation and Brotli compression, I've managed to keep the WASM payload lean and fast.

## Looking Ahead

Now that the site is on a newer framework, I'm hoping it will become less of a "tech blog I never used"