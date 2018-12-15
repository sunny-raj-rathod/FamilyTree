using System;
using FamilyTree.Controller;
using FamilyTree.Models;

namespace FamilyTree.Main
{
    public static class Program
    {
        private static LengaburuFamily _tree = null;

        private static void InitializeTree()
        {
            _tree = new LengaburuFamily("King Shan", "Queen Anga");
            _tree.AddToFamily("Queen Anga", "Ish", RelationType.Son);
            _tree.AddToFamily("Queen Anga", "Chit", RelationType.Son);
            _tree.AddToFamily("Queen Anga", "Vich", RelationType.Son);
            _tree.AddToFamily("Queen Anga", "Satya", RelationType.Daughter);

            _tree.AddToFamily("Chit", "Ambi", RelationType.Spouse);
            _tree.AddToFamily("Vich", "Lika", RelationType.Spouse);
            _tree.AddToFamily("Satya", "Vyan", RelationType.Spouse);

            _tree.AddToFamily("Ambi", "Drita", RelationType.Son);
            _tree.AddToFamily("Ambi", "Vrita", RelationType.Son);

            _tree.AddToFamily("Lika", "Vila", RelationType.Son);
            _tree.AddToFamily("Lika", "Chika", RelationType.Daughter);

            _tree.AddToFamily("Satya", "Satvy", RelationType.Daughter);
            _tree.AddToFamily("Satya", "Savya", RelationType.Son);
            _tree.AddToFamily("Satya", "Saayan", RelationType.Son);


            _tree.AddToFamily("Drita", "Jaya", RelationType.Spouse);
            _tree.AddToFamily("Vila", "Jnki", RelationType.Spouse);
            _tree.AddToFamily("Chika", "Kpila", RelationType.Spouse);
            _tree.AddToFamily("Satvy", "Asva", RelationType.Spouse);
            _tree.AddToFamily("Savya", "Krpi", RelationType.Spouse);
            _tree.AddToFamily("Saayan", "Mina", RelationType.Spouse);

            _tree.AddToFamily("Jaya", "Jata", RelationType.Son);
            _tree.AddToFamily("Jaya", "Driya", RelationType.Daughter);
            _tree.AddToFamily("Jnki", "Lavnya", RelationType.Daughter);
            _tree.AddToFamily("Krpi", "Kriya", RelationType.Son);
            _tree.AddToFamily("Mina", "Misa", RelationType.Son);

            _tree.AddToFamily("Driya", "Mnu", RelationType.Spouse);
            _tree.AddToFamily("Lavnya", "Gru", RelationType.Spouse);
        }
        
        private static void ProcessInput(int key)
        {
            switch (key)
            {
                case 1:
                    InitializeTree();
                    break;
                case 2:
                    _tree.ViewFamilyTree();
                    break;
                case 3:
                {
                    Console.Write("Person Name:");
                    var personName = Console.ReadLine();
                    Console.Write("Son Name:");
                    var relativeName = Console.ReadLine();
                    _tree.AddToFamily(personName, relativeName, RelationType.Son);
                    break;
                }
                case 4:
                {
                    Console.Write("Person Name:");
                    var personName = Console.ReadLine();
                    Console.Write("Daughter Name:");
                    var relativeName = Console.ReadLine();
                    _tree.AddToFamily(personName, relativeName, RelationType.Daughter);
                    break;
                }
                case 5:
                {
                    Console.Write("Person Name:");
                    var personName = Console.ReadLine();
                    Console.Write("Spouse Name:");
                    var relativeName = Console.ReadLine();
                    _tree.AddToFamily(personName, relativeName, RelationType.Spouse);
                    break;
                }
                case 6:
                {
                    Console.Write("Person Name:");
                    var personName = Console.ReadLine();
                    Console.Write("Relation:");
                    var relationName = Console.ReadLine();
                    
                    var result = _tree.GetRelatives(personName, relationName);
                    
                    foreach(var item in result)
                    {
                        Console.WriteLine(item);
                    }
                    break;
                }
                case 7:
                {
                    Console.Write("Person Name:");
                    var personName = Console.ReadLine();
                    Console.Write("Relative Name:");
                    var relativeName = Console.ReadLine();
                    
                    var relation = _tree.GetRelation(personName, relativeName);
                    
                    Console.WriteLine(relation.ToString());
                    
                    break;
                }
                case 8:
                {
                    Console.WriteLine(_tree.GetMotherWithMostGirls());
                    break;
                }
                default:
                    break;
            }
        }
        
        private static void Main()
        {
            const int exitMenu = 9;
            var key = 0;
            do
            {
                Console.WriteLine("Welcome to Lengaburu Family:");
                Console.WriteLine("Choose Option:");
                Console.WriteLine("1. Initialize Default Family of King Shan");
                Console.WriteLine("2. Print Family");
                Console.WriteLine("3. Add Son To Family");
                Console.WriteLine("4. Add Daughter To Family");
                Console.WriteLine("5. Add Spouse");
                Console.WriteLine("6. Get Relatives");
                Console.WriteLine("7. Get Relation");
                Console.WriteLine("8. Get Mother with Most Girls");
                Console.WriteLine("9. Exit");

                key = Convert.ToInt32(Console.ReadLine());
                ProcessInput(key);
            } while (key != exitMenu);

            Console.WriteLine("FIN!");
        }
    }
}
