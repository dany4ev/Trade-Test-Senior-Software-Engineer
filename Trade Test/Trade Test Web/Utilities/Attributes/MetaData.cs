namespace Trade_Test.Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    class Metadata : Attribute
    {
        public Metadata()
        {
            Value = "text/plain";
            IsText = true;
        }

        public string Value { get; set; }
        public bool IsText { get; set; }
        public bool IsBinary
        {
            get
            {
                return !IsText;
            }
            set
            {
                IsText = !value;
            }
        }
    }
}
