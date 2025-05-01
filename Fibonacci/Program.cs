namespace Fibonacci
{
    using System.Runtime.InteropServices;

    namespace Fibonacci
    {
        internal class Program
        {
            static void FibonacciCalculation(ulong upper_limit)
            {
                if (upper_limit == 0) Console.WriteLine("Error! Upper limit is 0.");

                ulong fib = 0, temp1 = 1, temp2 = 0;

                while (true)
                {
                    fib = checked(temp1 + temp2);
                    if (fib > upper_limit) break;
                    Console.WriteLine(fib);
                    temp1 = temp2;
                    temp2 = fib;
                }

                Console.WriteLine("Done! Calculated number: " + temp2);
            }
            static void Main(string[] args)
            {
                ThreadPool.SetMaxThreads(100, 100);
                ThreadPool.SetMinThreads(5, 5);
                while (true)
                {
                    string? user_input = "";
                    ulong input_converted = 0;

                    Console.WriteLine("Fibonacci number calculation started!");
                    Console.WriteLine("Enter upper fibonacci number limit (type exit to stop): ");
                    user_input = Console.ReadLine();
                    if (!(user_input != null))
                    {
                        Console.WriteLine("Input was null! Defaulting to 100...");
                        input_converted = 100;
                    }
                    if (user_input.ToLower() == "exit")
                    {
                        Console.WriteLine("Exiting application!");
                        break;
                    }
                    if (!ulong.TryParse(user_input, out input_converted))
                    {
                        Console.WriteLine("Failed to parse the number! Defaulting to 100...");
                        input_converted = 100;
                    }
                    try
                    {
                        ThreadPool.QueueUserWorkItem(state => FibonacciCalculation(input_converted));
                    }
                    catch (OverflowException oex)
                    {
                        Console.WriteLine("Number overflow happened! Did you input a number bigger then 18 446 744 073 709 551 615?");
                        Console.WriteLine($"Exception message: {oex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unexpected error happened! {ex.Message}\nStack trace: {ex.StackTrace}");
                    }
                }
            }
        }
    }

}
