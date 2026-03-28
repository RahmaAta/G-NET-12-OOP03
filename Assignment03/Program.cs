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