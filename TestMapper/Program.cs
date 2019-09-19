using Mapper;
using System;
using System.Collections.Generic;

namespace MapperDemo
{

    class Program
    {
        public static void Main()
        {
            DemoMapStudentToDto();
            DemoMapStudentToDtoWithForcedMatching();
            DemoMapStudentToDtoWithExcludedMatching();
            Console.ReadLine();
        }

        private static void DemoMapStudentToDtoWithExcludedMatching()
        {
            Student student = new Student();
            Dto dto = new Dto();
            DisplayInformation(Constants.PromptMapStudentToDtoWithExclusion, student, dto, true);
            Mapper < Student, Dto > mapper = new Mapper<Student, Dto>();
            mapper.Exclude(nameof(Dto.LastName));
            mapper.Map(student, dto);
            DisplayInformation("", student, dto, false);
        }
        private static void DemoMapStudentToDto()
        {
            Student student = new Student();
            Dto dto = new Dto();
            DisplayInformation(Constants.PromptMapStudentToDto, student, dto, true);
            Mapper<Student, Dto> mapper = new Mapper<Student, Dto>();
            mapper.Map(student, dto);
            DisplayInformation("", student, dto, false);

        }
        private static void DemoMapStudentToDtoWithForcedMatching()
        {
            Student student = new Student();
            Dto dto = new Dto();
            DisplayInformation(Constants.PromptForcedMapStudentToDto, student, dto, true);
            Mapper<Student, Dto> mapper = new Mapper<Student, Dto>();
            mapper.ForceMatch(nameof(student.ForeName), nameof(dto.FirstName));
            mapper.ForceMatch(nameof(student.Id), nameof(dto.RecordNumber));
            mapper.Map(student, dto);
           DisplayInformation("", student, dto, false);
        }

        private static void DisplayInformation(string prompt, Student student, Dto dto, bool isBefore)
        {
            string studentMsg = "Student before mapping: ";
            string dtoMsg = "Dto before mapping: ";
            if (!isBefore)
            {
                studentMsg = studentMsg.Replace("before", "after");
                dtoMsg = dtoMsg.Replace("before", "after");
            }
            string formattedPrompt = string.IsNullOrEmpty(prompt) ? prompt : "\n***" + prompt + "***\n\n";
            List<string> messages = new List<string>
            {
                formattedPrompt,
                $"{studentMsg,Constants.Alignment}"+student+"\n",
                $"{dtoMsg,Constants.Alignment}"+dto
            };
            foreach (string s in messages)
            {
                Console.Write(s);
            }
            Console.WriteLine();
        }
    }
}
