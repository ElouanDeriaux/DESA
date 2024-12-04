var builder = WebApplication.CreateBuilder(args);

// Ajout du service BlobService pour l'injection de dépendance
builder.Services.AddSingleton<BlobService>();

// Ajouter Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Configuration de la pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
