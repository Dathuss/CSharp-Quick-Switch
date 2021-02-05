using System;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CSharpQuickSwitch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is a simple program that will do a switch statement for x numbers.\n");
            int count = -1;
            int format = 0;
            string variableName;
            string addValue = "";
            bool willBreak = false;

            bool completed = false;
            while (completed == false)
            {
                completed = true;
                Console.WriteLine("How many cases ??");
                try
                {
                    count = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Incorrect number");
                    completed = false;
                }
            }
            completed = false;
            while (completed == false)
            {
                completed = true;
                Console.WriteLine("What is the format of the number ? (0 for decimal, 1 for binary, 2 for hexadecimal)");
                try
                {
                    string r = Convert.ToString(Console.ReadKey().KeyChar);
                    Console.WriteLine();
                    switch (r)
                    {
                        case "0":
                            format = 10;
                            break;
                        case "1":
                            format = 2;
                            break;
                        case "2":
                            format = 16;
                            break;
                        default: throw new Exception();
                    }
                }
                catch
                {
                    Console.WriteLine("Incorrect format provided.");
                    completed = false;
                }
            }
            completed = false;
            while (completed == false)
            {
                completed = true;
                Console.WriteLine("Will it return or break ? (B/R)");
                string r = Convert.ToString(Console.ReadKey().KeyChar);
                Console.WriteLine();

                if (r == "B" || r == "b")
                {
                    willBreak = true;
                }
                else if (r == "R" || r == "r")
                {
                    willBreak = false;
                    Console.WriteLine("In this case, do you want to add something after return (like \"\" for example) ? Leave blank for nothing.");
                    addValue = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Incorrect value.");
                    completed = false;
                }
            }
            Console.WriteLine("What's the variable name ?");
            variableName = Console.ReadLine();

            string result = string.Format("switch ({0})", variableName);
            result += @"
{
    ";

            for (int i = 0; i < count; i++) //  I mean, the following code is pretty terrible, right ?
            {
                if (format == 2)
                {
                    result += string.Format(
                    @"case 0b_{0}:
        ", 
                    Convert.ToString(i, toBase:2));
                }
                else if (format == 16)
                {
                    string v = Convert.ToString(i, toBase: format);
                    string r = "";
                    foreach (char c in v) //If it's hexadecimal, convert the lower characters to upper characters (for readability)
                    {
                        try
                        {
                            char upped = char.ToUpper(c);
                            r += upped;
                        }
                        catch { r += c; }
                    }

                    result += string.Format(
                            @"case 0x{0}:
        ", r);
                }
                else
                {
                    result += string.Format(
                    @"case {0}:
        ", 
                    Convert.ToString(i, toBase:format));
                }

                if (willBreak)
                {
                    result += @"
        break;";
                }
                else
                {
                    result += string.Format("return {0};", addValue);
                }
                if (i + 1 != count) result += @"
    ";
            }
            result += @"
}";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Documents\switchresult.txt";
            FileStream fs = new FileStream(path, FileMode.Create);
            fs.Write(Encoding.ASCII.GetBytes(result));
            fs.Dispose();

            Console.WriteLine("Result stored in {0}.\nPress any key to end.", path);
            ProcessStartInfo pro = new ProcessStartInfo("notepad", path);
            Console.ReadKey();
            Process.Start(pro);
        }
    }
}
