﻿using Microsoft.EntityFrameworkCore;
using WebLibraryAPI.Models.Auth;
using WebLibraryAPI.Models.Domain;

namespace WebLibraryAPI.Data;

public partial class LibraryDbContext(DbContextOptions
      <LibraryDbContext> options) : DbContext(options)
{
    public virtual DbSet<Member> Members { get; set; }
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<BookComment> BookComments { get; set; }
    public virtual DbSet<FavouriteBook> Favourites { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Member>().HasMany(b => b.MyBooks).WithOne(a => a.Member).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Member>().HasMany(b => b.FavouriteBooks).WithOne(a => a.Member).OnDelete(DeleteBehavior.Cascade);      
      
        modelBuilder.Entity<BookComment>(entity =>
        {
            entity.HasKey(k => k.CommentId);
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

