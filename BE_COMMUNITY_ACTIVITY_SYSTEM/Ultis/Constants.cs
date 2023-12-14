namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis
{
    public static class Constants
    {
        public static class Users
        {
            public const string STUDENT_ID_PREFIX = "SV";
            public const string TEACHER_ID_PREFIX = "GV";
        }

        public static class ErrorMessages
        {
            public const string STRING_FIXED_LENGTH = "{0} field must have a fixed length of {1} characters.";
            public const string USER_NOT_FOUND = "User not found.";
            public const string CLASS_NOT_FOUND = "Class not found.";
            public const string DATE_MUST_BE_EARLIER_THAN_CURRENT_TIME = "{0} must be a valid date and earlier than the current date.";
            public const string TEXT_ONLY = "{0} is text only field.";
            public const string INVALID_GENDER = "Gender is invalid. Please input from 1 to 3.";
            public const string INVALID_FIELD = "{0} is invalid";
            public const string INVALID_GUID = "{0} is invalid GUID.";
            public const string TEACHER_CAN_NOT_HAVE_CLASS = "Teacher can not have class asigned to";
        }

        public static class Regexes
        {
            public const string TEXT_ONLY = "^[a-zA-Z ]+$";
            public const string PHONE_NUMBER = @"^\+(?:[0-9] ?){6,14}[0-9]$";
            public const string EMAIL = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            public const string WEBSITE_LINK = @"^(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
        }

    }
}   
