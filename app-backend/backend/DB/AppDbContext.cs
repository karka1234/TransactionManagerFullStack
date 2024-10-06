using System;
using backend.Models.Services_AccountingAPI;
using Microsoft.EntityFrameworkCore;

namespace backend.DB;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) 
    {}
}
