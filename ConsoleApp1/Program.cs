using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        class Report
        {
            public string Name { get; set; }
            public string DepartmentId { get; set; }
            public bool IsPaid { get; set; }
            public double PayAmount { get; set; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var criterion = new BooleanCriterion("Report Is Paid", nameof(Report.IsPaid), true);
            var criterion2 = new NumberCriterion("Report Is Chawr", nameof(Report.PayAmount), 100, Comparison.GreaterThanOrEqual);
            var criterion3 = new OrCriterion("Report Is Chawr Or Is Paid", new List<Criterion> { criterion, criterion2 });
            var criterion4 = new AndCriterion("Report Is Chawr And Is Paid", new List<Criterion> { criterion, criterion2 });
            var criterion5 = new TextCriterion("Report Is From the First Department", nameof(Report.DepartmentId), "1", TextComparison.Equal);

            var report = new Report { Name = "Report1", DepartmentId = "1", IsPaid = true, PayAmount = 50 };
            var report2 = new Report { Name = "Report2", DepartmentId = "1", IsPaid = false, PayAmount = 100 };
            var report3 = new Report { Name = "Report3", DepartmentId = "2", IsPaid = true, PayAmount = 250 };

            var reports = new List<Report> { report, report2, report3 };

            var criteria = new List<Criterion>() { criterion, criterion2, criterion3, criterion4, criterion5 };

            foreach (var r in reports)
            {
                foreach(var c in criteria)
                {
                    Console.WriteLine($"{r.Name} Matches {c.Title}: {c.Matches(r)}");
                }
                Console.WriteLine();
            }

            // If Department is in (1, 3, 5, 9) Then
            //    => Step Approval Template 1
            // Else if Department is in (8, 9, 10) Then
            //    => Step Approval Template 2
            // Else
            //    => Step Approval Template 3

            // If Role is in
        }
    }
}
