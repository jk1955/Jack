using WMS_Inventory_API;
using System.Text.Json.Serialization;
using IronBarCode;
using System.Drawing;

var builder = WebApplication.CreateBuilder(args);


// Barcode 

// Creating a barcode:
//GeneratedBarcode barcode = IronBarCode.BarcodeWriter.CreateBarcode("Container.Id", BarcodeWritingEncoding.All);

// Save barcode as in image:
//barcode.SaveAsPng("barcode.png");

//Image BarcodeImage = barcode.Image; // Can be used as Image

// Reading a barcode with IronBarcode:
// var resultFromFile = BarcodeReader.Read(@"file/barcode.png"); // From a file

//class Barcode
//{
//    static void Main (string[] args)
//    {
//        GeneratedBarcode barcode = IronBarCode.BarcodeWriter.CreateBarcode("Container.Id", BarcodeEncoding.AllOneDimensional);
//        barcode.SaveAsPng("barcode.png");
//    }
//}


// Add services to the container.

Console.WriteLine("TEST");


builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<InventoryContext>();
builder.Services.AddCors(options => options.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();