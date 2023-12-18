namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis
{
    public static class Constants
    {
        public enum Status
        {
            ACCOUNT_LOCKED = -1
        }

        public static class Users
        {
            public const string STUDENT_ID_PREFIX = "SV";
            public const string TEACHER_ID_PREFIX = "GV";
        }

        public static class ErrorMessages
        {
            public const string STRING_FIXED_LENGTH = "{0} field must have a fixed length of {1} characters.";
            public const string NOT_FOUND = "{0} not found.";
            public const string DATE_MUST_BE_EARLIER_THAN_CURRENT_TIME = "{0} must be a valid date and earlier than the current date.";
            public const string TEXT_ONLY = "{0} is text only field.";
            public const string TEXT_AND_NUMBER_ONLY = "{0} is text and number only field.";
            public const string INVALID_GENDER = "Gender is invalid. Please input from 1 to 3.";
            public const string INVALID_FIELD = "{0} is invalid";
            public const string INVALID_GUID = "{0} is invalid GUID.";
            public const string TEACHER_CAN_NOT_HAVE_CLASS = "Teacher can not have class asigned to";
            public const string PASSWORD_NOT_MATCHES = "Password does not matches with ConfirmPassword.";
            public const string INVALID_MINSCORE = "MinScore is invalid. Please input from 0 to 700.";
            public const string INVALID_MAXSCORE = "MaxScore is invalid. Please input from 0 to 700.";
            public const string MAXSCORE_MUST_BE_GREATER_THAN_MINSCORE = "MaxScore must be greater than or equal to MinScore";
        }

        public static class Regexes
        {
            public const string TEXT_ONLY = "^[a-zA-ZÀ-Ỹà-ỹ ]+$";
            public const string TEXT_AND_NUMBER_ONLY = "^[a-zA-Z0-9]*$";
            public const string PHONE_NUMBER = @"^\+(?:[0-9] ?){6,14}[0-9]$";
            public const string EMAIL = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            public const string WEBSITE_LINK = @"^(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
        }

        public static class Roles
        {
            public const string SINH_VIEN = "SINHVIEN";
            public const string LOP_TRUONG = "LOPTRUONG";
            public const string GIAO_VIEN = "GIAOVIEN";
            public const string TRUONG_KHOA = "TRUONGKHOA";
            public const string ADMIN = "ADMIN";
        }
    }
}   
