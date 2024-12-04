var builder = WebApplication.CreateBuilder(args);

// Ajoute BlobService dans les services
builder.Services.AddSingleton<BlobService>();

var app = builder.Build();

// Configure le pipeline HTTP (pas de changement ici)
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
