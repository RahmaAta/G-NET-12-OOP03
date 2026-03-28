// =======================
// Q1: Relationship Types
// =======================

// a) A University has Departments. If the university is closed, the departments no longer exist.
// Composition (strong ownership, dependent lifecycle)

// b) A Driver uses a Car. The driver does not own the car.
// Association (uses relationship, no ownership)

// c) A Dog is an Animal.
// Inheritance (is-a relationship)

// d) A Team has Players. If the team is deleted, the players still exist.
// Aggregation (weak ownership, independent lifecycle)

// e) A method receives a Logger as a parameter and calls it inside the method only.
// Dependency (temporary usage)

// =======================
// Q2: Access Modifiers & Sealed
// =======================

// a) Protected field:
// - Child class in different assembly can access it? yes (through inheritance only)
// - Can it be accessed through an object instance from outside? NO

// b) Difference:
// protected internal  >> accessible in same assembly OR derived class in another assembly
// private protected   >> accessible in same assembly AND only in derived classes

// c) sealed keyword:
// - On a class: prevents inheritance (cannot be a base class)
// - On a method: prevents further overriding

// d) Can we create an object from a sealed class?
// yes, because sealed only prevents inheritance, not object creation

// Example:
//sealed class Car { }

//class Program
//{
//    static void Main()
//    {
//        Car c = new Car(); // valid
//    }
//}

// =======================
// Movie Ticket System
// =======================

class Ticket
{
    private static int counter = 0;

    public int TicketId { get; }
    public string MovieName { get; set; }

    private decimal price;
    public decimal Price
    {
        get => price;
        set
        {
            if (value > 0)
                price = value;
        }
    }

    public Ticket(string movieName, decimal price)
    {
        TicketId = ++counter;
        MovieName = movieName;
        Price = price;
    }

    public decimal PriceAfterTax => Price * 1.14m;

    public static int GetTotalTickets()
    {
        return counter;
    }

    public override string ToString()
    {
        return $"Ticket #{TicketId} | {MovieName} | Price: {Price} EGP | After Tax: {PriceAfterTax:F2} EGP";
    }
}

class StandardTicket : Ticket
{
    public string SeatNumber { get; set; }

    public StandardTicket(string movieName, decimal price, string seatNumber)
        : base(movieName, price)
    {
        SeatNumber = seatNumber;
    }

    public override string ToString()
    {
        return base.ToString() + $" | Seat: {SeatNumber}";
    }
}

class VIPTicket : Ticket
{
    public bool LoungeAccess { get; set; }
    public decimal ServiceFee { get; } = 50;

    public VIPTicket(string movieName, decimal price, bool loungeAccess)
        : base(movieName, price)
    {
        LoungeAccess = loungeAccess;
    }

    public override string ToString()
    {
        return base.ToString() +
               $" | Lounge: {(LoungeAccess ? "Yes" : "No")} | Service Fee: {ServiceFee} EGP";
    }
}

sealed class IMAXTicket : Ticket
{
    public bool Is3D { get; set; }

    public IMAXTicket(string movieName, decimal price, bool is3D)
        : base(movieName, price)
    {
        Is3D = is3D;

        if (Is3D)
            Price += 30;
    }

    public override string ToString()
    {
        return base.ToString() + $" | IMAX 3D: {(Is3D ? "Yes" : "No")}";
    }
}

class Projector
{
    public void Start()
    {
        Console.WriteLine("Projector started.");
    }

    public void Stop()
    {
        Console.WriteLine("Projector stopped.");
    }
}

class Cinema
{
    public string CinemaName { get; set; }

    private Ticket[] tickets = new Ticket[20];

    private Projector projector = new Projector();

    public Cinema(string name)
    {
        CinemaName = name;
    }

    public bool AddTicket(Ticket t)
    {
        for (int i = 0; i < tickets.Length; i++)
        {
            if (tickets[i] == null)
            {
                tickets[i] = t;
                return true;
            }
        }
        return false;
    }

    public void PrintAllTickets()
    {
        Console.WriteLine("\n========== All Tickets ==========");
        foreach (var t in tickets)
        {
            if (t != null)
                Console.WriteLine(t);
        }
    }

    public void OpenCinema()
    {
        Console.WriteLine("========== Cinema Opened ==========");
        projector.Start();
    }

    public void CloseCinema()
    {
        projector.Stop();
    }
}

static class BookingHelper
{
    private static int counter = 0;

    public static string GenerateBookingReference()
    {
        counter++;
        return $"BK-{counter}";
    }
}

class Program
{
    static void Main()
    {
        Cinema cinema = new Cinema("VOX");

        cinema.OpenCinema();
        Console.WriteLine();

        Ticket t1 = new StandardTicket("Inception", 120, "A-5");
        Ticket t2 = new VIPTicket("Avengers", 200, true);
        Ticket t3 = new IMAXTicket("Dune", 180, false);

        cinema.AddTicket(t1);
        cinema.AddTicket(t2);
        cinema.AddTicket(t3);

        cinema.PrintAllTickets();

        Console.WriteLine("\n========== Statistics ==========");
        Console.WriteLine($"Total Tickets Created: {Ticket.GetTotalTickets()}");

        Console.WriteLine($"\nBooking Ref 1: {BookingHelper.GenerateBookingReference()}");
        Console.WriteLine($"Booking Ref 2: {BookingHelper.GenerateBookingReference()}");

        Console.WriteLine();

        cinema.CloseCinema();
    }
}
