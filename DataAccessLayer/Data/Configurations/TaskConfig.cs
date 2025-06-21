using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DataAccessLayer.Data.Configurations
{
    public class TaskConfig : IEntityTypeConfiguration<TaskTable>
    {
        public void Configure(EntityTypeBuilder<TaskTable> builder)
        {
            builder.ToTable("Tasks")
                .HasKey(t => t.Id);
            builder.Property(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(t => t.Description).HasMaxLength(500).IsRequired(false);

            builder.Property(p => p.CreatedDate)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();
            builder.Property(p => p.LastModifiedDate)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();


        }
    }
}
