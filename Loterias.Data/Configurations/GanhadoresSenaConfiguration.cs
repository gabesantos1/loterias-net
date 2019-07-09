using Loterias.Domain.Entities.Sena;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loterias.Data.Configurations
{
    public class GanhadoresSenaConfiguration : GanhadoresConfiguration<GanhadoresSena>
    {
        public override void Configure(EntityTypeBuilder<GanhadoresSena> builder)
        {
            builder.ToTable("sena_ganhadoressena");
            base.Configure(builder);
        }
    }
}