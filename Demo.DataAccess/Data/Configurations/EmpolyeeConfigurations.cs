using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Models.Empolyees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.DataAccess.Data.Configurations
{
    internal class EmpolyeeConfigurations : BaseEntityConfigurations<Empolyee>,IEntityTypeConfiguration<Empolyee>
    {
        public new void Configure(EntityTypeBuilder<Empolyee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("nvarchar(50)");
            builder.Property(E => E.Salary).HasColumnType("decimal(10,2)");

            builder.Property(E => E.Gender)
                .HasConversion(
                    gender => gender.ToString(),
                    toGender => (Gender)Enum.Parse(typeof(Gender), toGender)
                );

            builder.Property(E => E.EmpolyeeType)
               .HasConversion(
                   EmpolyeeType => EmpolyeeType.ToString(),
                   toEmpolyeeType => (EmpolyeeType)Enum.Parse(typeof(EmpolyeeType), toEmpolyeeType)
               );

            base.Configure(builder);
        }
    }
}
