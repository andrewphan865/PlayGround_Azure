using Azure.Storage.Blobs.Specialized;


Console.WriteLine("Azure Blob Storage - Getting Started Samples\n");
GettingStarted.CallBlobGettingStartedSamples();

Console.WriteLine("Azure Blob Storage - Advanced Samples\n ");
Advanced.CallBlobAdvancedSamples().Wait();

Console.WriteLine();
Console.WriteLine("Press any key to exit.");
Console.ReadLine();


// Copy blob to another container in the same storage account
var storageAccountConnectionString = "DefaultEndpointsProtocol=https;AccountName=ahj89storage;AccountKey=egDjRYfwnAVKAj5EryxMpl+ljklAEqK9J0X3klxvNo58pfUKgaY0uUztIbmQtTEaRbenhxMMh+Zj+AStcZvgSA==;EndpointSuffix=core.windows.net";
var sourceContainer = "firstcontainer";
var destContainer = "secondcontainer";
var sourceFile = "How to increase performance.docx";
var destFile= "How to increase performance-copy.docx";

var sourceClient = new BlockBlobClient(storageAccountConnectionString, sourceContainer, sourceFile);
var destClient = new BlockBlobClient(storageAccountConnectionString, destContainer, destFile);

await destClient.StartCopyFromUriAsync(sourceClient.Uri);


