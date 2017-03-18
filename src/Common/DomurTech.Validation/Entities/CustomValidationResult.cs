namespace DomurTech.Validation.Entities
{
    public class CustomValidationResult
    {
        public string PropertyName { get; set; }

        public string ErrorMessage { get; set; }

        public object AttemptedValue { get; set; }

        public object CustomState { get; set; }
    }
}
