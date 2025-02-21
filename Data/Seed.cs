using System.Text.Json;
using dagnys_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace dagnys_api.Data;

        public static class Seed
        {
    
            public static async Task LoadProducts(DataContext context)
            {
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            if(context.Products.Any()) return;

            var json = File.ReadAllText("Data/json/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(json, options);

            if(products is not null && products.Count > 0){
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }


          public static async Task LoadSupplier(DataContext context)
            {
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            if(context.Suppliers.Any()) return;

            var json = File.ReadAllText("Data/json/suppliers.json");
            var suppliers = JsonSerializer.Deserialize<List<Supplier>>(json, options);

            if(suppliers is not null && suppliers.Count > 0){
                await context.Suppliers.AddRangeAsync(suppliers);
                await context.SaveChangesAsync();
            }
        }

         public static async Task LoadRecipe(DataContext context)
            {
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            if(context.Recipes.Any()) return;

            var json = File.ReadAllText("Data/json/recipes.json");
            var recipes = JsonSerializer.Deserialize<List<Recipe>>(json, options);

            if(recipes is not null && recipes.Count > 0){
                await context.Recipes.AddRangeAsync(recipes);
                await context.SaveChangesAsync();
            }
        }


        
         public static async Task LoadPurchase(DataContext context)
            {
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            if(context.Purchases.Any()) return;

            var json = File.ReadAllText("Data/json/purchases.json");
            var purchases = JsonSerializer.Deserialize<List<Purchase>>(json, options);

            if(purchases is not null && purchases.Count > 0){
                await context.Purchases.AddRangeAsync(purchases);
                await context.SaveChangesAsync();
            }
        }


           public static async Task LoadRawMaterial(DataContext context)
            {
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            if(context.RawMaterials.Any()) return;

            var json = File.ReadAllText("Data/json/materials.json");
            var materials = JsonSerializer.Deserialize<List<RawMaterial>>(json, options);

            if(materials is not null && materials.Count > 0){
                await context.RawMaterials.AddRangeAsync(materials);
                await context.SaveChangesAsync();
            }
        }
        public static async Task LoadSupplierRawMaterial(DataContext context)
        {
            var options = new JsonSerializerOptions{
                 PropertyNameCaseInsensitive = true 
                 };
            if (context.SupplierRawMaterials.Any()) return;
            var json = File.ReadAllText("Data/json/supplierRawMaterials.json");
            var supplierRawMaterials = JsonSerializer.Deserialize<List<SupplierRawMaterial>>(json, options);
            if (supplierRawMaterials is not null && supplierRawMaterials.Count > 0)
            {
                await context.SupplierRawMaterials.AddRangeAsync(supplierRawMaterials);
                await context.SaveChangesAsync();
                }
                
            }

public static async Task LoadAddressTypes(DataContext context)
{
    if (context.AddressTypes.Any()) return;

    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    var json = await File.ReadAllTextAsync("Data/json/addressTypes.json");
    var types = JsonSerializer.Deserialize<List<AddressType>>(json, options);

    if (types is not null && types.Count > 0)
    {
        await context.AddressTypes.AddRangeAsync(types);
        await context.SaveChangesAsync();
    }
}

/*public static async Task LoadCustomers(DataContext context)
{
    if (context.Customers.Any()) return;

    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    try
    {
        var json = await File.ReadAllTextAsync("Data/json/customers.json");
        var customers = JsonSerializer.Deserialize<List<CustomerPostViewModel>>(json, options);

        if (customers != null && customers.Count > 0)
        {
            foreach (var customer in customers)
            {
                // Skapa en ny kund
                var newCustomer = new Customer
                {
                    StoreName = customer.StoreName,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    ContactPerson = customer.ContactPerson
                };

                await context.Customers.AddAsync(newCustomer);

                // Kontrollera om adresser finns och hantera varje adress
                foreach (var address in customer.Addresses)
                {
                    if (address == null) continue; // Om adressen är null, hoppa över den

                    // Kontrollera om postnummer finns i databasen
                    PostalAddress postalAddress = null;
                    if (!string.IsNullOrEmpty(address.PostalCode) && !string.IsNullOrEmpty(address.City))
                    {
                        postalAddress = await context.PostalAddresses
                            .FirstOrDefaultAsync(c => c.PostalCode.Replace(" ", "").Trim() == address.PostalCode.Replace(" ", "").Trim());
                    }

                    if (postalAddress == null)
                    {
                        postalAddress = new PostalAddress
                        {
                            PostalCode = address.PostalCode.Replace(" ", "").Trim(),
                            City = address.City.Trim()
                        };
                        await context.PostalAddresses.AddAsync(postalAddress);
                    }

                    // Kontrollera om adresslinjen redan finns
                    Address customerAddress = null;
                    if (!string.IsNullOrEmpty(address.AddressLine))
                    {
                        customerAddress = await context.Addresses
                            .FirstOrDefaultAsync(c => c.AddressLine.Trim().ToLower() == address.AddressLine.Trim().ToLower());
                    }

                    if (customerAddress == null)
                    {
                        customerAddress = new Address
                        {
                            AddressLine = address.AddressLine,
                            AddressTypeId = (int)address.AddressType,
                            PostalAddress = postalAddress
                        };

                        await context.Addresses.AddAsync(customerAddress);
                    }

                    // Lägg till kundens adress
                    newCustomer.CustomerAddresses.Add(new CustomerAddress
                    {
                        Address = customerAddress,
                        Customer = newCustomer
                    });
                }

                // Spara ändringarna för varje kund
                await context.SaveChangesAsync();
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
       
    }
}*/






}
    
