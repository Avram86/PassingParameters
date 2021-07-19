using System;

namespace PassingParameters
{
    class Program
    {
        static void Main(string[] args)
        {
            //REF
            //int nr = 12;
            //Console.WriteLine($"i={nr} (before calling {nameof(Increment)})");
            //Increment(ref nr);
            //Console.WriteLine($"i={nr} (after calling {nameof(Increment)})");


            //OUT
            int nr = 12;
            Console.WriteLine($"i={nr} (before calling {nameof(IncrementOut)})");
            IncrementOut(nr, out nr);
            Console.WriteLine($"i={nr} (after calling {nameof(IncrementOut)})");

            int x, y;
            if(TryParseCoordinates("100,200", out x, out y))
            {
                Console.WriteLine($"Parse succeeded, x={x}, y={y}");
            }
            else
            {
                Console.WriteLine($"Parse failed, x={x}, y={y}");
            }

            Person p = new Person { FirstName = "John", LastName = "Doe" };
            Console.WriteLine($"Person {nameof(p.FirstName)}={p.FirstName}, {nameof(p.LastName)}={p.LastName} (before calling {nameof(PrintPerson)})");
            ChangePerson(ref p);
            Console.WriteLine($"Person {nameof(p.FirstName)}={p.FirstName}, {nameof(p.LastName)}={p.LastName} (after calling {nameof(PrintPerson)})");
        }

        private static void Increment(ref int nr)
        {
            nr = nr + 1;
        }

        private static void IncrementOut(int value, out int nr)
        {
            nr = value;
            nr = nr + 1;
        }

        private static void Print(int nr)
        {
            Console.WriteLine($"nr={nr} (before change)");
            nr = 100;
            Console.WriteLine($"nr={nr} (after change)");
        }

        //se persisita schimbarea
        private static void PrintPerson(Person p)
        {
            Console.WriteLine($"Person {nameof(p.FirstName)}={p.FirstName}, {nameof(p.LastName)}={p.LastName} (before change)");
            p.LastName = "Test";
            Console.WriteLine($"Person {nameof(p.FirstName)}={p.FirstName}, {nameof(p.LastName)}={p.LastName} (after change)");
        }

        //se persista schimbarea chiar daca tipul referinta se schimba
        private static void ChangePerson(ref Person p)
        {
            Console.WriteLine($"Person {nameof(p.FirstName)}={p.FirstName}, {nameof(p.LastName)}={p.LastName} (before change)");
            //p.LastName = "Test";
            p = new Person { FirstName="Changed", LastName="Person"};
            Console.WriteLine($"Person {nameof(p.FirstName)}={p.FirstName}, {nameof(p.LastName)}={p.LastName} (after change)");
        }

        public static bool TryParseCoordinates(string coords ,out int x, out int y)
        {
            x = 0;
            y = 0;

            if (string.IsNullOrEmpty(coords))
            {
                return false;
            }

            string[] parts = coords.Split(",", StringSplitOptions.RemoveEmptyEntries);
            bool canParseX = (parts.Length > 0) && int.TryParse(parts[0], out x);
            bool canParseY = (parts.Length > 1) && int.TryParse(parts[1], out y);

            return canParseX || canParseY;
        }

        public static bool TryParsePerson(string fullname, out Person p)
        {
            p = null;
            if (string.IsNullOrEmpty(fullname))
            {
                return false;
            }

            string[] parts = fullname.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string firstName = string.Empty;
            string lastName = string.Empty;

            bool canParseFirstName = parts.Length > 0;
            if (canParseFirstName)
            {
                firstName = parts[0];
            }

            bool canParseLastName = parts.Length > 1;
            if (canParseLastName)
            {
                lastName = parts[1];
            }

            if(canParseFirstName || canParseLastName)
            {
                p = new Person { FirstName = firstName, LastName = lastName };
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
