# DND Manager

DND Manager is a web application to organize your data about playable, non-playable characters, maps, campaign notes and much more! The application was programmed in ASP .NET Core MVC and is  based on [Onion Architecture/Clean Architecture](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture).

![mainwhenloggedin](https://github.com/3elk4/DndManager/assets/33397049/2978d72e-e691-44bb-a20e-7bd4d4428182)

## Used Technologies and tools

- .NET
- C#
- AutoMapper
- FluentValitadion
- MediatR
- MySql
- QuestPDF
- FontAwesome
- Bootstrap
- Toastr

## Identity

Authentication and authorization are enabled by using e-mail and password. The application shows only playable and non-playable characters created by the user. 

## Playable Characters

The application allows to view, create, and edit characters. All information about playable characters is split into subsections as feats, proficiencies, spells, or even actions - user can insert, sort and use quick actions during combats! Character creation window contains only the most important information like class, race, and background. Editing individual subsections allows users to expand the information about the character. Only characters’ authors can modify their PCs.

![daisymain characterpage](https://github.com/3elk4/DndManager/assets/33397049/c5e6d42e-0e0b-4e21-9358-c0bd6daabc4f)

## Non-playable characters

As with PCs, the user can also handle their own non-playable characters. Although, it does not contain as much information and modifications as a playable character, it certainly allows for greater freedom in modifications. Original and specialized BBEG or shopkeepers can be handled by that.

![npc](https://github.com/3elk4/DndManager/assets/33397049/f80d0084-8e09-4413-905d-a398eafe9d79)

## From web to pdf

It is possible to export characters to a PDF file, detailing character information in a more readable way. PDF creation is supported by the QuestPdf library.

![daisypdf](https://github.com/3elk4/DndManager/assets/33397049/67b2806c-6310-432c-a77a-ff5019ae9ae4)

## Currently working on

- roles for authorization - like dungeon master or player
- groups for players and dungeon master with chat
- secret dungeon master information not available for players
- creating and updating maps
- system for session’s notes (available for players)
- ability to change some fields in pdf
