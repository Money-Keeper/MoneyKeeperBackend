using MoneyKeeper;

WebApplication
    .CreateBuilder(args)
    .ConfigureBuilder()
    .Build()
    .ConfigureApp(args)
    .Run();