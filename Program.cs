using System;

namespace NonEcsiste
{
    class Program
    {
        static void Main(string[] args)
        {
            // LinqNaoExiste.LinqToGiggio();
            var gandhi = new Humano("Mahatma Gandhi");
            Console.WriteLine(gandhi.Nome());
            var chimpanze = new Chimpanze();
            Console.WriteLine(chimpanze.Nome());
            var foo = new MyGenericChild1<string, object>("abc");
            var barString = foo.MakeList("def");
            var barObject = foo.MakeList(new object());
            var baz = foo.Echo("hello");
            var foo2 = new MyGenericChild2<Chimpanze, Animal>(chimpanze);
            var barChimpanze = foo2.MakeList(chimpanze);
            var barAnimal = foo2.MakeList((Animal)chimpanze);
            var baz2 = foo2.Echo("hello");
            Console.ReadLine();
        }
    }
}
