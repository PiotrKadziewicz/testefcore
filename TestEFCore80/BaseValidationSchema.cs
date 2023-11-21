using System;

namespace TestEFCore80
{
    public class BaseValidationSchema
    {
        public Guid Id { get; set; }
        public string? Enviroment { get; set; }
        public string? Type { get; set; }
        public string? Version { get; set; }
        public string? Name { get; set; }
        public Guid? ProcessId { get; set; }
        public bool? ExampleFiles { get; set; }
        public bool IsMain { get; set; }
    }

}
