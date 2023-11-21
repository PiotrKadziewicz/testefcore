using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TestEFCore80;

using SomeDbContext someDbContext = new SomeDbContext();

var toSave = new ValidationSchema
{
    Id = Guid.NewGuid(),
    Type = "X",
    Enviroment = "Y",
    Files = new List<FilesType>()
    {
       new()
       {
            Id = 1, Name = "Test",
            Columns = new List<Column>
            {
                 new()
                 {
                    Id = 1,
                    ColumnName = "testColumn"
                 }
            },
            HeadersColumns =  new List<Column>
            {
                new()
                {
                    Id = 1,
                    ColumnName = "testColumn"
                }
            }
       }
    }
};
await someDbContext.Validations.AddAsync(toSave);
await someDbContext.SaveChangesAsync();

var res = await someDbContext.Validations.WithPartitionKey("X").ToListAsync();
_ = "";
