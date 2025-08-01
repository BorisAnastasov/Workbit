﻿namespace Workbit.Common
{
    public class DataConstants
    {
        public static class Constants
        {
            public const string DateFormatShort = "dd-MM-yyyy";
            public const string DateFormatLong = "dd-MM-yyyy HH:mm";
        }

        public class Department 
        {
            public const int NameMaxLen = 50;
            public const int NameMinLen = 3;

            public const int DescriptionMaxLen = 150;
            public const int DescriptionMinLen = 5;
        }

		public class Job 
        {
            public const int TitleMaxLen = 50;
            public const int TitleMinLen = 3;

            public const int DescriptionMaxLen = 150;
            public const int DescriptionMinLen = 5;
        }
        public static class ApplicationUser
        {
            public const int FirstNameMaxLen = 150;
            public const int FirstNameMinLen = 3;

            public const int LastNameMaxLen = 200;
            public const int LastNameMinLen = 3;

            public const int PhoneMaxLen = 15;
            public const int PhoneMinLen = 8;
        }

		public static class Company
		{
			public const int NameMaxLen = 100;
			public const int NameMinLen = 1;

			public const int AddressMaxLen = 200;
			public const int AddressMinLen = 3;

			public const int ContactPhoneMaxLen = 15;
			public const int ContactPhoneMinLen = 8;

			public const string ContactPhoneRegex = @"^\+?[0-9\s\-]+$";
		}

        public static class Payment 
        {
            public const int NotesMaxLen = 150;
        }
    }
}
