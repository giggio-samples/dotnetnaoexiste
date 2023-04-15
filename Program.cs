using NonEcsiste;
using NonEcsiste.LinqNaoExiste;
using NonEcsiste.Objetos;

LinqNaoExiste.LinqToGiggio();
var gandhi = new Humano("Mahatma Gandhi");
WriteLine(gandhi.Nome());
var chimpanze = new Chimpanze();
WriteLine(chimpanze.Nome());
ReadLine();
var student = new PrimaryConstructor_Student(1, "Giovanni", new[] { 6.7m, 8.5m });
WriteLine(student.GPA);
