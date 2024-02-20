using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using moment3_mvc_entity.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Book { get; set; } = default!;
    public DbSet<Author> Author { get; set; } = default!;
    public DbSet<Rental> Rental { get; set; } = default!;
    
}
