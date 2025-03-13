using OH.ETL.Entities.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OH.ETL.Entities.MappingConfiguration.System
{
    internal class SysQuartzOptionConfig : EntityMappingConfiguration<QuartzOption>
    {
        public override void Map(EntityTypeBuilder<QuartzOption> builderTable)
        {
            //builderTable.Property(x => x.Item).HasMaxLength(45); 
        }
    }
}
