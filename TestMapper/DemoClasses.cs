using System;

namespace MapperDemo
{
    public class Student
    {
        public string ForeName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public int Id {get; set; }

        public override string ToString()
        {
            return "Student: ForeName=" + ForeName + " LastName=" + LastName + " Dob=" + Dob.ToShortDateString() + " Id=" + Id ;
        }
    }
        public class Dto
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime Dob { get; set; }
        public int RecordNumber { get; set; }
        public override string ToString()
        {
            return "Dto: FirstName=" + FirstName + " LastName=" + LastName + " Dob=" + Dob.ToShortDateString()
                +" RecordNumber="+RecordNumber;
        }
    }

        public class DatabaseRecord
        {
            public string ForeName { get; set; }
            public string LastName { get; set; }
            public DateTime Dob { get; set; }
            public int Id { get; set; }
            public string Address { get; set; }

        public override string ToString()
        {
            return "DatabaseRecord: " + ForeName + " " + LastName + " Dob "+Dob.ToShortDateString()+" Id  " + Id + " " + Address;
        }
    }

    }

