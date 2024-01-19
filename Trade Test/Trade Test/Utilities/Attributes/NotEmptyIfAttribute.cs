using System.ComponentModel.DataAnnotations;

namespace Trade_Test.Utilities.Attributes
{
    public class NotEmptyIfAttribute : NotEmptyAttribute
    {
        private string _propertyName { get; set; }

        public NotEmptyIfAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            object instance = context.ObjectInstance;
            Type type = instance.GetType();

            bool.TryParse(type.GetProperty(_propertyName).GetValue(instance)?.ToString(), out bool propertyValue);

            return propertyValue ? base.IsValid(value, context) : ValidationResult.Success;
        }
    }
}
