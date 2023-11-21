namespace TestEFCore80
{
    public class ValidationSchema : BaseValidationSchema
    {

        public Guid? MainId { get; set; }
        public string? MainSchemaVersion { get; set; }
        public string? MainSchemaName { get; set; }
        public Guid? AgentId { get; set; }
        public bool Deleted { get; set; }
        public IList<FilesType>? Files { get; set; }
        public IEnumerable<string>? FilesForValidation { get => Files?.Where(x => x.Selected).Select(f => f.FileName!).Distinct(); }
        public string? EditedBy { get; set; }
    }
    public class FilesType : IEquatable<FilesType>
    {
        public int Id { get; set; }
        public bool IsMain { get; set; }
        public string? Name { get; set; }
        public string? FileName { get; set; }
        public string? DisplayName { get; set; }
        public bool Selected { get; set; }
        public bool Headers { get; set; }
        public string[]? UniqueKey { get; set; }
        public string? DocTypeName { get; set; }
        public FlowDirectory FlowDirectory { get; set; }
        public string? Description { get; set; }
        public ICollection<Column>? Columns { get; set; }
        public ICollection<Column>? HeadersColumns { get; set; }

        public bool Equals(FilesType? other)
        {
            if (other is null)
                return false;

            return Id == other.Id;
        }
        public override bool Equals(object? obj) => Equals(obj as FilesType);
        public override int GetHashCode() => Id.GetHashCode();
    }

    public enum FlowDirectory
    {
        In,
        Out
    }
    public class Column
    {
        public int Id { get; set; }
        public string? ColumnName { get; set; }
        public bool IsMain { get; set; }
        public int ColumnIndex { get; set; }
        public bool Required { get; set; }
        public string? DataType { get; set; }
        public int MaxLength { get; set; }
        public object? DefaultValue { get; set; }
        public object[]? PossibleValues { get; set; }
        private int[]? _decimalPlace;
        public int[]? DecimalPlace { get => _decimalPlace is null || !_decimalPlace.Any() ? null : _decimalPlace; set => _decimalPlace = value; }
        public bool TwoFieldsRequired { get; set; }
        public bool CanBeNegative { get; set; }
        public int[]? CheckFieldIndex { get; set; }
        public string[]? ValueTocheck { get; set; }
        public bool RequiredEmpty { get; set; }   //todo pamietac o tym dziadostwie podczas wrfzucanai na prod. w konfiguracj brakuje q w nazwie. poprawic reczne w cosmos
        public string[]? MoreValuesTocheck { get; set; }
        public int CheckIndex { get; set; }
        public string? DateTimeFormat { get; set; }
        public string? Description { get; set; }
        public IEnumerable<LinkedColumn>? LinkedColumns { get; set; }
        public string? RegexPattern { get; set; }
        public int? PId { get; set; }
    }

    public class LinkedColumn
    {
        public ColumnType ColumnType { get; set; }
        public int ColumnIndex { get; set; }
        public string[]? ValueTocheck { get; set; }
        public string[]? MoreValuesTocheck { get; set; }
        public string? ColumnName { get; set; }
    }

    public enum ColumnType
    {
        Header,
        Position
    }

    public class Rootobject
    {
        public string? _id { get; set; }
        public string? version { get; set; }
        public string? name { get; set; }
        public bool isMain { get; set; }
        public Filestype2[]? filesType { get; set; }
    }

    public class Filestype2
    {
        public string? name { get; set; }
        public bool headers { get; set; }
        public bool selected { get; set; }
        public string? fileName { get; set; }
        public Column2[]? columns { get; set; }
        public string? version { get; set; }
        public string[]? uniqeKey { get; set; }
        public string? docTypeName { get; set; }
        public Headerscolumn2[]? headersColumns { get; set; }
    }

    public class Column2
    {
        public string? columnName { get; set; }
        public int? columnIndex { get; set; }
        public bool? required { get; set; }
        public string? dataType { get; set; }
        public int? maxLength { get; set; }
        public object? defaultValue { get; set; }
        public object[]? possibleValues { get; set; }
        public int[]? decimalPlace { get; set; }
        public int twoFieldsRequired { get; set; }
        public bool canBeNegative { get; set; }
        public int[]? checkFieldIndex { get; set; }
        public string[]? valueTocheck { get; set; }
        public bool reuiredEmpty { get; set; }
        public string[]? moreValuesTocheck { get; set; }
        public int checkIndex { get; set; }
        public string? dateTimeFormat { get; set; }
    }

    public class Headerscolumn2
    {
        public string? columnName { get; set; }
        public int columnIndex { get; set; }
        public bool required { get; set; }
        public string? dataType { get; set; }
        public int maxLength { get; set; }
        public int? defaultValue { get; set; }
        public object[]? possibleValues { get; set; }
        public string? dateTimeFormat { get; set; }
        public bool reuiredEmpty { get; set; }
        public int[]? decimalPlace { get; set; }
        public bool requiredEmpty { get; set; }
    }

}
