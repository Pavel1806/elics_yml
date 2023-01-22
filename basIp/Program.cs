// See https://aka.ms/new-console-template for more information
using basIp;

Console.WriteLine("Hello, World!");

//await basIp.BasIpYml.DownLoadxml(new[] {1,11});

//await OzonApi.CategoryAttribute();

//await OzonApi.CategoryAttributeValues();

await OzonApi.ProductImport(await basIp.BasIpYml.DownLoadxml(new[] { 1, 11 }));