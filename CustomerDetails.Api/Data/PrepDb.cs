using CustomerDetails.Core;

namespace CustomerDetails.Api.Data;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }
    }

    private static void SeedData(AppDbContext? appDbContext)
    {
        var p1 = Guid.NewGuid();
        var p2 = Guid.NewGuid();


        if (!appDbContext.Profession.Any())
        {
            Console.WriteLine("seeding data");
            appDbContext.Profession.AddRange(
             new Profession { Id = p1, Title = "Doctor" },
             new Profession
             {
                 Id = p2,
                 Title = "Software developer"
             }

          );

            appDbContext.SaveChanges();

            if (!appDbContext.Person.Any())
            {
                Console.WriteLine("seeding data");
                appDbContext.Person.AddRange(
                 new Person { Id = Guid.NewGuid(), FirstName = "Matthew", LastName = "Hall", DateOfBirth = new DateTime(1980, 1, 23), ProfessionId = appDbContext.Profession.FirstOrDefault(x => x.Id == p1).Id },
                 new Person { Id = Guid.NewGuid(), FirstName = "Pei Sin", LastName = "Ang", DateOfBirth = new DateTime(1990, 1, 27), ProfessionId = appDbContext.Profession.FirstOrDefault(x => x.Id == p2).Id }


              );


                appDbContext.SaveChanges();

            }
            else
            {
                Console.WriteLine("we got data");
            }

            Console.WriteLine(p1);
        }
    }
}
