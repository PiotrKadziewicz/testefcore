using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace TestEFCore80
{
    public class ValidationEntityConfiguration : IEntityTypeConfiguration<ValidationSchema>
    {
        public void Configure(EntityTypeBuilder<ValidationSchema> builder)
        {
            builder.HasPartitionKey(f => f.Type);
            builder.HasKey(f => new { f.Id, f.Enviroment, f.Deleted });

            builder.OwnsMany(x => x.Files, files =>
            {
                files.Property(prop => prop.FlowDirectory).HasConversion(src => src.ToString(), dst => Enum.Parse<FlowDirectory>(dst));
                files.Property(prop => prop.UniqueKey).HasConversion(src => src.FromListToStringJoin(), dst => dst.SplitObj());
                files.OwnsMany(om => om.Columns, col =>
                {
                    Columns(col);
                });
                files.OwnsMany(om => om.HeadersColumns, col =>
                {
                    Columns(col);
                });
            });
        }
        private void Columns(OwnedNavigationBuilder<FilesType, Column> col)
        {

            col.Property(pr => pr.DecimalPlace).HasConversion(src => src.FromListToStringJoin(), dst => dst.SplitObjToInt());
            col.Property(pr => pr.CheckFieldIndex).HasConversion(src => src.FromListToStringJoin(), dst => dst.SplitObjToInt());
            col.Property(col => col.ValueTocheck).HasConversion(src => src.FromListToStringJoin(), dst => dst.SplitObj());
            col.Property(col => col.MoreValuesTocheck).HasConversion(src => src.FromListToStringJoin(), dst => dst.SplitObj());
            col.Property(col => col.DefaultValue).HasConversion(src => JsonConvert.SerializeObject(src), dst => Deserialize<object>(dst));
            col.Property(col => col.PossibleValues).HasConversion(src => JsonConvert.SerializeObject(src), dst => DeserializeArray<object>(dst));
            col.Property(pr => pr.DecimalPlace).HasConversion(src => src.FromListToStringJoin(), dst => dst.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(d => Convert.ToInt32(d)).ToArray());
            col.Property(pr => pr.CheckFieldIndex).HasConversion(src => src.FromListToStringJoin(), dst => dst.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(d => Convert.ToInt32(d)).ToArray());
            col.Property(col => col.LinkedColumns).HasConversion(src => JsonConvert.SerializeObject(src), dst => DeserializeArray<LinkedColumn>(dst));
        }

        private T[]? DeserializeArray<T>(string dst)
        {
            T[]? result = JsonConvert.DeserializeObject<T[]>(dst);
            if (result?.Any() == true)
                return result;
            else
                return null;

        }

        private T? Deserialize<T>(string dst)
        {
            T? result = JsonConvert.DeserializeObject<T>(dst);
            if (string.IsNullOrWhiteSpace(result?.ToString()))
                return default;
            else
                return result;
        }
    }
}
