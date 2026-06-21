using System;


Console.WriteLine("Hello, World!");
//Basic datatypes
string name = "Husam";
int age = 19;
double height = 1.82;
bool isStudent = true;

var greeting =$"Hello, my name is {name}, I am {age} years old, my height is {height} meters, and it is {isStudent} that I am a student.";
Console.WriteLine(greeting);

//if statements
if (age >= 18 && age <= 65)
{
    Console.WriteLine("You are an adult.");
}
else if (age < 18)
{
    Console.WriteLine("You are a minor.");
}
else
{
    Console.WriteLine("You are old! :( ");

}
// loops
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"{i + 1}");
}

Console.WriteLine("While loop:");
while (age < 25)
{
    Console.WriteLine($"You are still young! Age: {age}");
    age++;
}
//arrays and lists
int[] fixedArray = { 1, 2, 3, 4, 5};
fixedArray[0] = 10; // Modifying the first element of the array

List<string> dynamicList = new List<string> { "Alice", "Bob", "Charlie" };
dynamicList.Add($"{name}"); // Adding an element to the list

foreach (var nameInList in dynamicList)
{
    Console.WriteLine($"name: {name}");
}

//functions
bool IsAdult(int age)
{
    return age >= 18;
}
Console.WriteLine($"Is {name} an adult? {IsAdult(age)}");

//switch statements
switch (age)
{
    case < 18:
        Console.WriteLine("You are a minor.");
        break;
    case >= 18 and <= 65:
        Console.WriteLine("You are an adult.");
        break;
    default:
        Console.WriteLine("You are old! :( ");
        break;
}

foreach (var num in fixedArray)
{
    int myNum = num switch
    {
        1 => 10,
        2 => 20,
        3 => 30,
        _ => num
    };
}
// Classes
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

// Constructor
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
// Method
    public void Introduce()
    {
        Console.WriteLine($"Hi, my name is {Name} and I am {Age} years old.");
    }
}

//in main
//Person person1 = new Person("Alice", 30);

//inheritance
public class Student : Person
{
    public string School { get; set; }
    public int year { get; set; }
    public Student(string name, int age, string school, int year) : base(name, age)
    {
        School = school;
        this.year = year;
    }

    public void Study()
    {
        Console.WriteLine($"{Name} is studying at {School}.");
    }
}
public class Teacher : Person
{
    public string Subject { get; set; }
    public string School { get; set; }
    public Teacher(string name, int age, string subject, string school) : base(name, age)
    {
        Subject = subject;
        School = school;
    }

    public void Teach()
    {
        Console.WriteLine($"{Name} is teaching {Subject} at {School}.");
    }
}
// overriding and polymorphism
public class Enemy
{
    protected int health = 100;
    protected int damage = 10;

    public virtual void SayCatchphrase(){
        Console.WriteLine("I will defeat you!");
    }

}

public class Boss : Enemy
{
    public override void SayCatchphrase()
    {
        Console.WriteLine("I am the boss, you cannot defeat me!");
    }
}

//interface
public interface IAttack
{
    void Attack();
}

public class Player : IAttack
{
    public void Attack()
    {
        Console.WriteLine("Player attacks with a sword!");
    }
}


// delegates
// Predicate
Predicate<int> isEven = number => number % 2 == 0;
Console.WriteLine($"Is 4 even? {isEven(4)}");
// Action
Action<string> greet = personName => Console.WriteLine($"Hello, {personName}!");
greet(name);
// Func
Func<int, int, int> add = (a, b) => a + b;
Console.WriteLine($"5 + 3 = {add(5, 3)}");

//Task and async/await
// In ecommerce application, we can use async/await to fetch product details from a database without blocking the main thread.
async Task<string> FetchProductDetailsAsync(int productId)
{
    // Simulating an asynchronous operation
    await Task.Delay(1000); // Simulate delay
    return $"Product details for product ID: {productId}";
}

