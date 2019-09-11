using System;
using System.Globalization;

namespace MapperDemo
{
    public class Student
    {
        public string ForeName { get; set; } = Constants.StudentForeName;
        public string LastName { get; set; } = Constants.StudentLastName;
        public DateTime Dob { get; set; }=DateTime.Parse(Constants.StudentDob, CultureInfo.CurrentCulture);
        public int Id { get; set; } = Constants.StudentId;

        public override string ToString()
        {
            return "Student: ForeName=" + ForeName + " LastName=" + LastName + " Dob=" + Dob.ToShortDateString() + " Id=" + Id ;
        }
    }
        public class Dto
        {
        public string FirstName { get; set; } = Constants.DtoFirstName;
        public string LastName { get; set; } = Constants.DtoLastName;
        public DateTime Dob { get; set; } = DateTime.Parse(Constants.DtoDob, CultureInfo.CurrentCulture);
        public int RecordNumber { get; set; } = Constants.DtoRecordNumber;
        public override string ToString()
        {
            return "Dto: FirstName=" + FirstName + " LastName=" + LastName + " Dob=" + Dob.ToShortDateString()
                +" RecordNumber="+RecordNumber;
        }
    }

 

    }

