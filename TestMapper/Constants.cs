using System;

namespace MapperDemo
{
    public static class Constants
    {
        public const string PromptMapStudentToDto = "Mapping Student To Dto with matching property names only";
        public const string PromptForcedMapStudentToDto = "Mapping Student To Dto with forced pairing ForeName/FirstName Id/RecordNumber";
        public const string PromptMapStudentToDtoWithExclusion = "Excluding LastName From Mapping";
        public const int Alignment = -25;//left align min length 25
        public const string StudentForeName = "Anne";
        public const string StudentLastName = "Other";
        public const string StudentDob = "7/8/1994";
        public const int StudentId = 66;
        public const string DtoFirstName = "Someone";
        public const string DtoLastName = "Else";
        public const string DtoDob = "12/11/2003";
        public const int DtoRecordNumber = 99;
    }
}
