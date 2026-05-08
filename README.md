# Finnish Reference Number Validator

A .NET library for validating Finnish reference numbers (viitenumero) according to the [Finanssiala specification](http://www.finanssiala.fi/maksujenvalitys/dokumentit/kotimaisen_viitteen_rakenneohje.pdf).

[![.NET 9](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/)
[![NuGet](https://img.shields.io/nuget/v/FinnishReferenceNumberValidator.svg)](https://www.nuget.org/packages/FinnishReferenceNumberValidator/)

## Features

- Validates Finnish reference numbers with proper checksum verification
- Supports reference numbers with spaces
- Provides detailed validation messages
- Handles leading zeros correctly
- .NET 9.0 compatible

## Installation

Install the package from NuGet:

```bash
dotnet add package FinnishReferenceNumberValidator
```

Or via the Package Manager Console:

```powershell
Install-Package FinnishReferenceNumberValidator
```

## Usage

### Basic Validation

```csharp
using FinnishReferenceNumberValidator;

// Validate a reference number
var result = ReferenceNumberValidator.IsValidFinnishReference("12304561");

if (result.IsValid)
{
    Console.WriteLine("Valid reference number!");
}
else
{
    Console.WriteLine($"Invalid: {result.ValidationMessage}");
}
```

### Checking Validation Results

```csharp
var result = ReferenceNumberValidator.IsValidFinnishReference("12304562");

Console.WriteLine($"Is Valid: {result.IsValid}");
Console.WriteLine($"Message: {result.ValidationMessage}");

// Output:
// Is Valid: False
// Message: Invalid checksum
```

### Reference Numbers with Spaces

The validator accepts reference numbers with spaces (common in printed invoices):

```csharp
var result = ReferenceNumberValidator.IsValidFinnishReference("12304 561");
Console.WriteLine(result.IsValid); // True
```

### Validation Examples

| Reference Number | Valid | Reason |
|------------------|-------|--------|
| `12304561` | ✅ | Valid checksum |
| `12304 561` | ✅ | Valid with spaces |
| `12304562` | ❌ | Invalid checksum |
| `123` | ❌ | Too short (min 4 digits) |
| `123456789012345678901` | ❌ | Too long (max 20 digits) |
| `A12304561` | ❌ | Contains non-numeric characters |
| `-12304561` | ❌ | Negative numbers not allowed |
| `1230456,1` | ❌ | Decimals not allowed |
| `null` or `""` | ❌ | Empty reference number |

## API Reference

### ReferenceNumberValidator.IsValidFinnishReference(string referenceNumber)

Validates a Finnish reference number according to the Finanssiala specification.

**Parameters:**
- `referenceNumber` (string): The reference number to validate (4-20 digits, spaces allowed)

**Returns:**
- `ReferenceNumberCheckResult`: Contains validation result and message

### ReferenceNumberCheckResult Properties

- `IsValid` (bool): Indicates whether the reference number is valid
- `ValidationMessage` (string): Describes the validation result or error

## Validation Rules

Finnish reference numbers must meet the following criteria:

1. **Length**: 4-20 characters (excluding leading zeros)
2. **Characters**: Only numeric digits (0-9) and spaces
3. **Checksum**: Must pass the weighted checksum validation using weights [7, 3, 1]
4. **Format**: No negative numbers or decimals

The checksum is calculated using the following algorithm:
- Starting from the second-to-last digit, multiply each digit by weights 7, 3, 1 (repeating)
- Sum all the products
- Calculate check digit: `(10 - (sum % 10)) % 10`
- The last digit must match the calculated check digit

## Example Application

```csharp
using System;
using FinnishReferenceNumberValidator;

class Program
{
    static void Main()
    {
        Console.WriteLine("Finnish Reference Number Validator");
        Console.WriteLine("==================================\n");

        string[] testReferences = 
        {
            "12304561",      // Valid
            "12304 561",     // Valid with space
            "12304562",      // Invalid checksum
            "123",           // Too short
            "ABC123"         // Invalid characters
        };

        foreach (var reference in testReferences)
        {
            var result = ReferenceNumberValidator.IsValidFinnishReference(reference);
            
            Console.WriteLine($"Reference: {reference}");
            Console.WriteLine($"Valid: {result.IsValid}");
            Console.WriteLine($"Message: {result.ValidationMessage}");
            Console.WriteLine();
        }
    }
}
```

## Requirements

- .NET 9.0 or later

## License

This project is open source. Check the repository for license details.

---

## Suomeksi

C#-toteutus suomalaisen viitenumeron validointiin Finanssialan määritysten mukaisesti.

### Käyttö

```csharp
var tulos = ReferenceNumberValidator.IsValidFinnishReference("12304561");

if (tulos.IsValid)
{
    Console.WriteLine("Kelvollinen viitenumero!");
}
else
{
    Console.WriteLine($"Virheellinen: {tulos.ValidationMessage}");
}
```

### Validointisäännöt

- Pituus: 4-20 merkkiä
- Sallitut merkit: Numerot ja välilyönnit
- Tarkistusnumero lasketaan painokertoimilla [7, 3, 1]
