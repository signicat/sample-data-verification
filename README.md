# Signicat Data Verification Code Example

Sample code in ASP.NET Core to test the Signicat Data Verification API.

> **This repository is for demo purposes only**

## Documentation

For the complete integration guide, refer to the [Data Verification documentation](https://developer.signicat.com/docs/data-verification/code-examples.html).

## Prerequisites

Before you begin, ensure that you have the following tools installed in your development machine:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [.NET CLI](https://learn.microsoft.com/en-us/dotnet/core/tools/)

Also, you need to create an API client (ID and secret) associated with your Signicat account. You can create your client credentials in the [Signicat Dashboard](https://dashboard.signicat.com/).

## Installation

1. Clone or download the project.
2. Replace the `<YOUR_CLIENT_ID>` and `<YOUR_CLIENT_SECRET>` with actual values.
3. In a terminal, go to the `src` folder of this project and enter the command `dotnet run`.

## Testing

After starting the sample application, navigate to http://localhost:7650 in your browser. You should view the response `Signicat Data Verification Sample App.`.

## Usage

### Perform basic organisation lookup in Norway

In your browser, go to [http://localhost:7650/organization/basic/no/no-brreg/989584022](http://localhost:7650/organization/basic/no/no-brreg/989584022). This endpoint uses actual data from Brønnøysundregistrene (Norwegian business registry).

## Next steps

Learn more about Data Verification in the Signicat [developer documentation](https://developer.signicat.com/docs/data-verification/).
