namespace HiddenLove.DataAccess.TableAccesses
{
    public sealed class StepTemplatesTableAccess : SingleKeyDefaultTableAccess
    {
        protected override string TableName => "StepTemplates";
        protected override string PrimaryKeyName => "Id";
    }
}