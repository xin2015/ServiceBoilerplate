using ServiceBoilerplate.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;

namespace ServiceBoilerplate.DAL
{
    public class ServiceBoilerplateMetadataSource : FluentMetadataSource
    {
        protected override IList<MappingConfiguration> PrepareMapping()
        {
            List<MappingConfiguration> configurations = new List<MappingConfiguration>();

            configurations.Add(GetAQIMRCHDMappingConfiguration());
            configurations.Add(GetAQIMRCDDMappingConfiguration());
            configurations.Add(GetAQIMRSHDMappingConfiguration());
            configurations.Add(GetAQIMRSDDMappingConfiguration());
            configurations.Add(GetAQCIMRCMDMappingConfiguration());
            configurations.Add(GetAQCIMRCSDMappingConfiguration());
            configurations.Add(GetAQCIMRCYDMappingConfiguration());
            configurations.Add(GetMissingDataRecordMappingConfiguration());

            return configurations;
        }

        protected override MetadataContainer CreateModel()
        {
            MetadataContainer container = base.CreateModel();
            container.NameGenerator.RemoveCamelCase = false;
            container.NameGenerator.ResolveReservedWords = false;
            container.NameGenerator.SourceStrategy = NamingSourceStrategy.Property;
            return container;
        }

        private MappingConfiguration<AQIMRCHD> GetAQIMRCHDMappingConfiguration()
        {
            MappingConfiguration<AQIMRCHD> configuration = new MappingConfiguration<AQIMRCHD>();

            configuration.MapType().ToTable(typeof(AQIMRCHD).Name);

            configuration.HasProperty(x => x.Code).IsIdentity().HasColumnType("nvarchar").HasLength(16);
            configuration.HasProperty(x => x.Time).IsIdentity();
            configuration.HasProperty(x => x.PrimaryPollutant).HasColumnType("nvarchar").HasLength(64);
            configuration.HasProperty(x => x.Type).HasColumnType("nvarchar").HasLength(8);

            return configuration;
        }

        private MappingConfiguration<AQIMRCDD> GetAQIMRCDDMappingConfiguration()
        {
            MappingConfiguration<AQIMRCDD> configuration = new MappingConfiguration<AQIMRCDD>();

            configuration.MapType().ToTable(typeof(AQIMRCDD).Name);

            configuration.HasProperty(x => x.Code).IsIdentity().HasColumnType("nvarchar").HasLength(16);
            configuration.HasProperty(x => x.Time).IsIdentity();
            configuration.HasProperty(x => x.PrimaryPollutant).HasColumnType("nvarchar").HasLength(64);
            configuration.HasProperty(x => x.Type).HasColumnType("nvarchar").HasLength(8);

            return configuration;
        }

        private MappingConfiguration<AQIMRSHD> GetAQIMRSHDMappingConfiguration()
        {
            MappingConfiguration<AQIMRSHD> configuration = new MappingConfiguration<AQIMRSHD>();

            configuration.MapType().ToTable(typeof(AQIMRSHD).Name);

            configuration.HasProperty(x => x.Code).IsIdentity().HasColumnType("nvarchar").HasLength(16);
            configuration.HasProperty(x => x.Time).IsIdentity();
            configuration.HasProperty(x => x.PrimaryPollutant).HasColumnType("nvarchar").HasLength(64);
            configuration.HasProperty(x => x.Type).HasColumnType("nvarchar").HasLength(8);

            return configuration;
        }

        private MappingConfiguration<AQIMRSDD> GetAQIMRSDDMappingConfiguration()
        {
            MappingConfiguration<AQIMRSDD> configuration = new MappingConfiguration<AQIMRSDD>();

            configuration.MapType().ToTable(typeof(AQIMRSDD).Name);

            configuration.HasProperty(x => x.Code).IsIdentity().HasColumnType("nvarchar").HasLength(16);
            configuration.HasProperty(x => x.Time).IsIdentity();
            configuration.HasProperty(x => x.PrimaryPollutant).HasColumnType("nvarchar").HasLength(64);
            configuration.HasProperty(x => x.Type).HasColumnType("nvarchar").HasLength(8);

            return configuration;
        }

        private MappingConfiguration<AQCIMRCMD> GetAQCIMRCMDMappingConfiguration()
        {
            MappingConfiguration<AQCIMRCMD> configuration = new MappingConfiguration<AQCIMRCMD>();

            configuration.MapType().ToTable(typeof(AQCIMRCMD).Name);

            configuration.HasProperty(x => x.Code).IsIdentity().HasColumnType("nvarchar").HasLength(16);
            configuration.HasProperty(x => x.Time).IsIdentity();
            configuration.HasProperty(x => x.PrimaryPollutant).HasColumnType("nvarchar").HasLength(64);

            return configuration;
        }

        private MappingConfiguration<AQCIMRCSD> GetAQCIMRCSDMappingConfiguration()
        {
            MappingConfiguration<AQCIMRCSD> configuration = new MappingConfiguration<AQCIMRCSD>();

            configuration.MapType().ToTable(typeof(AQCIMRCSD).Name);

            configuration.HasProperty(x => x.Code).IsIdentity().HasColumnType("nvarchar").HasLength(16);
            configuration.HasProperty(x => x.Time).IsIdentity();
            configuration.HasProperty(x => x.PrimaryPollutant).HasColumnType("nvarchar").HasLength(64);

            return configuration;
        }

        private MappingConfiguration<AQCIMRCYD> GetAQCIMRCYDMappingConfiguration()
        {
            MappingConfiguration<AQCIMRCYD> configuration = new MappingConfiguration<AQCIMRCYD>();

            configuration.MapType().ToTable(typeof(AQCIMRCYD).Name);

            configuration.HasProperty(x => x.Code).IsIdentity().HasColumnType("nvarchar").HasLength(16);
            configuration.HasProperty(x => x.Time).IsIdentity();
            configuration.HasProperty(x => x.PrimaryPollutant).HasColumnType("nvarchar").HasLength(64);

            return configuration;
        }

        private MappingConfiguration<MissingDataRecord> GetMissingDataRecordMappingConfiguration()
        {
            MappingConfiguration<MissingDataRecord> configuration = new MappingConfiguration<MissingDataRecord>();

            configuration.MapType().ToTable(typeof(MissingDataRecord).Name);

            configuration.HasProperty(x => x.EntityName).IsIdentity().HasColumnType("nvarchar").HasLength(64);
            configuration.HasProperty(x => x.Code).IsIdentity().HasColumnType("nvarchar").HasLength(16);
            configuration.HasProperty(x => x.Time).IsIdentity();
            configuration.HasProperty(x => x.Status).HasColumnType("bit");
            configuration.HasProperty(x => x.Exception).HasColumnType("nvarchar(max)");
            configuration.HasProperty(x => x.CreationTime).IsCalculatedOn(DateTimeAutosetMode.Insert);
            configuration.HasProperty(x => x.Timestamp).IsCalculatedOn(DateTimeAutosetMode.InsertAndUpdate);

            return configuration;
        }
    }
}
