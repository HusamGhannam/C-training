using System;

public class Program
{
    static void Main(string[] args)
    {
        Animal myDog = new Dog("Buddy");
        Animal myCat = new Cat("Whiskers");

        myDog.MakeSound(); // Output: Buddy barks.
        myDog.Eat(); // Output: Buddy is eating Meat.
        myCat.MakeSound(); // Output: Whiskers meows.
        myCat.Eat(); // Output: Whiskers is eating fish.
    }
}
public interface IAnimal
{
    void Eat();
}
public class Animal: IAnimal
{
    public string Name { get; set; }

    public Animal(string name)
    {
        Name = name;
    }
    public virtual void MakeSound()
    {
        Console.WriteLine($"{Name} makes a sound.");
    }
    public virtual void Eat()
    {
        Console.WriteLine($"{Name} is eating.");
    }
}

public class Dog : Animal, IAnimal
{
    public Dog(string name) : base(name)
    {
    }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} barks.");
    }

    public override void Eat()
    {
        Console.WriteLine($"{Name} is eating Meat.");
    }
}

public class Cat : Animal, IAnimal
{
    public Cat(string name) : base(name)
    {
    }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} meows.");
    }
    public override void Eat()
    {
        Console.WriteLine($"{Name} is eating fish.");
    }
}

