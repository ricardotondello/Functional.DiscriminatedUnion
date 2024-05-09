# 🍊🍏 Functional Discriminated Union

[![Build](https://github.com/ricardotondello/Functional.DiscriminatedUnion/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/ricardotondello/Functional.DiscriminatedUnion/actions/workflows/dotnet.yml)
[![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=ricardotondello_Functional.DiscriminatedUnion&metric=alert_status)](https://sonarcloud.io/dashboard?id=ricardotondello_Functional.DiscriminatedUnion)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=ricardotondello_Functional.DiscriminatedUnion&metric=coverage)](https://sonarcloud.io/component_measures?id=ricardotondello_Functional.DiscriminatedUnion&metric=coverage)
[![NuGet latest version](https://badgen.net/nuget/v/Functional.DiscriminatedUnion/latest)](https://nuget.org/packages/Functional.DiscriminatedUnion)
[![NuGet downloads](https://img.shields.io/nuget/dt/Functional.DiscriminatedUnion)](https://www.nuget.org/packages/Functional.DiscriminatedUnion)

Functional.DiscriminatedUnion is a C# library that provides a lightweight implementation of discriminated unions, also known as tagged unions or sum types, for functional programming in C#.

## Overview
Discriminated unions are a powerful concept in functional programming that allow you to define a type that can hold values of several different types. Each value is tagged with its type, allowing for pattern matching and type-safe handling of these values.

## Features
- **Lightweight Implementation**: The library provides a minimalistic implementation of discriminated unions, keeping the syntax simple and easy to use.
- **Type Safety**: Discriminated unions enforce type safety at compile time, preventing runtime errors due to mismatched types.
- **Pattern Matching**: Pattern matching is supported for extracting values from the discriminated unions, enabling elegant and concise code.

## Installation 🚀

To use Functional.DiscriminatedUnion in your C# project, you can install it via NuGet Package Manager:

```powershell
dotnet add package Functional.DiscriminatedUnion
```

## Usage 🛠️

The library defines the OneResult class, which represents a discriminated union of one or more types. Here's a brief overview of the available classes:

- **OneResult<T1>**: Represents a discriminated union with one type.
- **OneResult<T1, T2>**: Represents a discriminated union with two types.
- **OneResult<T1, T2, T3>**: Represents a discriminated union with three types.
- **OneResult<T1, T2, T3, T4>**: Represents a discriminated union with four types.

**Usage:**
```csharp
public record UserCreated(int Id, string Name){}
public record UserUpdated(int Id, string Name, DateTime UpdatedAt){}

public OneResult<UserCreated, UserUpdated> CreateOrUpdateUser()
{
    if (!exists)
    {
        return new UserCreated(1, "User Name"); // Implicit conversion to a discriminated union
    }

    return new UserUpdated(1, "User Name changed", DateTime.UtcNow); // Implicit conversion to a discriminated union
}

var result = CreateOrUpdateUser();

var output = result.Match(
    value => $"Result is a UserCreated: {value}",
    value => $"Result is a UserUpdated: {value}"
); // Pattern matching to extract value based on its type

UserCreated userCreated = result.AsT1();
Console.WriteLine($"Id: {userCreated.Id}, Name: {userCreated.Name}");

//TryGetValue

if (result.TryGetT1(out UserCreated userCreated)
{
    Console.WriteLine($"result is a UserCreated");
}
else
{
    Console.WriteLine($"result is something else!");
}

//Actionable

result.When(
    actUserCreated =>
    {
        DoSomething(actUserCreated);
    },
    actUserUpdated =>
    {
        DoSomethingElse(actUserUpdated);
    });
```

## Contributing 👥

Contributions are welcome! If you find a bug or have a feature request, please open an issue on GitHub.
If you would like to contribute code, please fork the repository and submit a pull request.

## License 📄

This project is licensed under the MIT License.
See [LICENSE](https://github.com/ricardotondello/Functional.DiscriminatedUnion/blob/main/LICENSE) for more information.

## Support ☕

<a href="https://www.buymeacoffee.com/ricardotondello" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/v2/default-yellow.png" alt="Buy Me A Coffee" style="height: 60px !important;width: 217px !important;" ></a>
