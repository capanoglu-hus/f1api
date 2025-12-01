using f1api.Data;
using f1api.Dtos;
using f1api.Models;
using Microsoft.EntityFrameworkCore;

namespace f1api.Services
{
    public class DriverService(AppDbContext context) : IDriverService
    {

        

        public async Task<DriverResponse> AddDriverAsync(CreateDriverRequest driver)
        {
            var newDriver = new Driver
            {
                Name = driver.Name,
                Team = driver.Team,
                Description = driver.Description
            };

            context.Drivers.Add(newDriver); 
            await context.SaveChangesAsync(); //bu olmazsa dbye asla kaydetmez 
            return new DriverResponse
            {
                Id = newDriver.Id,
                Name = newDriver.Name,
                Team = newDriver.Team,
                Description = newDriver.Description
            };
        }

       
        public async Task<List<DriverResponse>> GetAllDriverAsync() 
            => await context.Drivers.Select(c => new DriverResponse
            {
                Id = c.Id,
                Name = c.Name,
                Team = c.Team,
                Description = c.Description
            }).ToListAsync();

        public async Task<DriverResponse?> GetDriverByIdAsync(int id)
        {
            var driver =  await context.Drivers
                .Where(a => a.Id == id)
                .Select(a => new DriverResponse
                {   
                    Id = a.Id,
                    Name = a.Name,
                    Team = a.Team,
                    Description = a.Description
                })
                .FirstOrDefaultAsync();
            return driver;
        }

        public async Task<bool> UpdateDriverAsync(int id, UpdateDriverRequest driver)
        {
            var existingDriver = context.Drivers.Find(id);

            if (existingDriver is null)
                return false;

            existingDriver.Name = driver.Name;
            existingDriver.Team = driver.Team;
            existingDriver.Description = driver.Description;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDriverAsync(int id)
        {
            var existingDriver = context.Drivers.Find(id);

            if (existingDriver is null)
                return false;

            context.Drivers.Remove(existingDriver); 

            await context.SaveChangesAsync();
            return true;
        }
    }
}
