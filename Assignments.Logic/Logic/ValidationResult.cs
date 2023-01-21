namespace Assignments.Logic.Password
{
    public enum ValidationResult
    {
        PasswordIsValid,
        NotMinimumLengthError,
        HasNoUpperCaseError,
        HasNoLowerCaseError,
        HasNoDigitError,
        HasNoSpecialCharError,
        CannotHaveSpacesError,
        CannotHaveNumberAtStartOrEndError,
        CheckUsernameNotEqualToPasswordError,
        CheckIfPreviouslyUsedFromFileError,
        PasswordChangedSuccess,
    };
}
