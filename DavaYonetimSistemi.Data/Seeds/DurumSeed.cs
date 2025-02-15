using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DavaYonetimDB.Data;
using DavaYonetimDB.Models;

public static class DurumSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!context.DurumDava.Any())
        {
            var durumlar = new List<DurumDava>
            {
                new DurumDava { Name = "DERDEST" },
                new DurumDava { Name = "KARAR" },
                new DurumDava { Name = "İSTİNAF" },
                new DurumDava { Name = "TEMYİZ" },
                new DurumDava { Name = "ANAYASA MAHKEMESİ" },
                new DurumDava { Name = "HİTAM" }
            };

            await context.DurumDava.AddRangeAsync(durumlar);
        }

        if (!context.DurumIcra.Any())
        {
            var durumlar = new List<DurumIcra>
            {
                new DurumIcra { Name = "DERDEST" },
                new DurumIcra { Name = "DERDEST İTİRAZLI" },
                new DurumIcra { Name = "DERDEST TEMİNAT" },
                new DurumIcra { Name = "İNFAZ" },
                new DurumIcra { Name = "İNFAZ ZAMAN AŞIMI" },
                new DurumIcra { Name = "İMHA" },
                new DurumIcra { Name = "İTİRAZLI" },
                new DurumIcra { Name = "KAPALI" },
                new DurumIcra { Name = "DÜŞME" }
            };

            await context.DurumIcra.AddRangeAsync(durumlar);
        }

        await context.SaveChangesAsync();
    }
} 