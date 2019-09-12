using Mapper;
using System;
using System.Collections.Generic;

namespace MapperDemo
{

    class Program
    {
        public static void Main()
        {

            Student student = new Student();
            Dto dtoA = new Dto();
            Dto dtoB = new Dto();
            DemoMapStudentToDto(student, dtoA);
            DemoMapStudentToDtoWithPairing(student, dtoB);
            Console.ReadLine();
        }
        private static void DemoMapStudentToDto(Student student, Dto dto)
        {
            DisplayInformation(Constants.PromptMapStudentToDto, student, dto, true);
            Mapper<Student, Dto> mapper = new Mapper<Student, Dto>();
            mapper.Map(student, dto);
            DisplayInformation("", student, dto, false);
        }
        private static void DemoMapStudentToDtoWithPairing(Student student, Dto dto)
        {
            DisplayInformation(Constants.PromptPairedMapStudentToDto, student, dto, true);
            Mapper<Student, Dto> mapper = new Mapper<Student, Dto>();
            mapper.Pair(nameof(student.ForeName), nameof(dto.FirstName));
            mapper.Pair(nameof(student.Id), nameof(dto.RecordNumber));
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
