namespace Trade_Test.Models.Constants
{
    public static class Constants
    {
        public enum SqlType
        {
            SELECT,
            UPDATE,
            INSERT,
            DELETE,
            EXEC
        }

        public const string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";

        public const string RefreshTokenType = "Web";

        public const string PasswordResetSuccessMessage = "Your request has been processed. If the email you entered was valid and your account was active then a link which will enable you to reset your password will be sent to your email address";

        public const string PasswordResetMessage = "If the data you provided is correct, then we will reset your password";

        public const string InputValidationFailedMessage = "Input validation failed";

        public const string RecordNotFoundMessage = "Record not found";

        public const string WrongPasswordMessage = "You Entered Wrong Old Password";

        public const string OldPasswordMessage = "Old And New Passwords Are Equal";

        public const string ExistingPasswordMessage = "New Password Already used previously kindly choose another password";

        public const string ValidationErrorMessage = "Data Validation Error";

        public const string GeneralErrorMessage = "An error occured while processing";

        public const string FieldRequiredMessage = "Cannot be empty, field required.";

        public const string SuccessMessage = "Success";

        public const string ErrorMessage = "Error";

        public const string UserDoesNotExistMessage = "User Doesn't exist";

        public const string FileTypeErrorMessage = "FileType upload not allowed";

        public const string FilesUploadLimitMessage = "Only 10 files can be uploaded at a time";

        public const string FileDeletedErrorMessage = "File(s) were deleted";

        public const string FileNotFound = "File not found";

        public const string FileNameSizeErrorMessage = "File name exceeds 80 character limt";

        public static string FilesSizeLimitMessage = "Files size exceeds the allowed limit";

        public static string FileEmptyErrorMessage = "File being uploaded cannot be empty";
    }
}
