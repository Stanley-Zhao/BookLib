using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibDAL.UnitTest
{
    public class UnitTestBase
    {
        protected string Status_Lending { get; } = "Lending";
        protected string Status_Ready { get; } = "Ready";

        protected string BookType_English { get; } = "English";
        protected string BookType_Management { get; } = "Management";
        protected string BookType_Program { get; } = "Program";

        protected void StartTest(string name)
        {
            Log(string.Format("{0} - {1}", nameof(StartTest).ToString(), name));
        }

        protected void EndTest(string name)
        {
            Log(string.Format("{0} - {1}", nameof(EndTest).ToString(), name));
        }

        protected void Log(string log)
        {
            Console.WriteLine(log);
        }
    }
}
