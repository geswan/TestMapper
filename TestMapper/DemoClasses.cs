using System;
using System.Globalization;

namespace MapperDemo
{
    public class Student
    {
        public string ForeName { get; set; } = Constants.StudentForeName;
        public string LastName { get; set; } = Constants.StudentLastName;
        public DateTime Dob { get; set; } = DateTime.Parse(Constants.StudentDob, CultureInfo.CurrentCulture);
        public int Id { get; set; } = Constants.StudentId;
       // public string SerialNo { get; set; } = "Abc";
        public override string ToString()
        {
            return $"{"Student:",-9}{"ForeName=",-10}{ForeName,-10}LastName = {LastName,-6}Dob={Dob.ToShortDateString()} {"Id = ",-15}{Id}";
        }
    }
    public class Dto
    {
        public string FirstName { get; set; } = Constants.DtoFirstName;
        public string LastName { get; set; } = Constants.DtoLastName;
        public DateTime Dob { get; set; } = DateTime.Parse(Constants.DtoDob, CultureInfo.CurrentCulture);
        public int RecordNumber { get; set; } = Constants.DtoRecordNumber;
      //  public int SerialNo { get; set; } = 123;
        public override string ToString()
        {
            return $"{"Dto:",-9}{"FirstName=",-10}{FirstName,-10}LastName = {LastName,-6}Dob={Dob.ToShortDateString()} {"RecordNumber = ",-15}{RecordNumber}";
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }

}

