using System;
using System.Collections.Generic;

namespace FamilyTree.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Class for handling Lengaburu Family Tree
    /// </summary>
    public class Family
    {
        private readonly Person Root;
        
        public Family(Person king)
        {
            Root = king;
        }
        
        public Person GetMember(string personName)
        {
            return GetMember(Root, personName);
        }
        
        private Person GetMember(Person rootNode, string personName)
        {
            if (rootNode == null)
                return null;

            if (rootNode.Name.Equals(personName))
            {
                return rootNode;
            }
            
            if (rootNode.Spouse != null && rootNode.Spouse.Name.Equals(personName))
            {
                return rootNode.Spouse;
            }

            if (rootNode.Children == null) return null;
            foreach (var child in rootNode.Children)
            {
                var result = GetMember(child, personName);
                if (result != null)
                    return result;
            }

            return null;
        }
        
        public bool AddToFamily(string person, string relative, RelationType relation)
        {
            var personNode = GetMember(person);

            switch (relation)
            {
                case RelationType.Spouse:
                    return personNode.AddSpouse(relative);
                case RelationType.Son:
                case RelationType.Daughter:
                    return personNode.AddChild(relative, relation);
                default:
                    throw new ArgumentOutOfRangeException(nameof(relation), relation, null);
            }
        }

        public List<Person> GetRelatives(string person, RelationType relation)
        {
            var personNode = GetMember(person);

            return personNode.GetRelative(relation);
        }

        public Person GetMotherWithMostGirls()
        {
            var maxGirls = 0;
            Person mother = null;
            GetMotherWithMostGirls(Root, ref maxGirls, ref mother);
            return mother;
        }

        private void GetMotherWithMostGirls(Person root, ref int maxGirls, ref Person mother)
        {
            if(root?.Children == null)
            {
                return;
            }

            var girlCount = 0;
            foreach(var treeNode in root.Children)
            {
                if(treeNode.Gender == Gender.Female)
                {
                    girlCount++;
                }
            }

            if(girlCount > maxGirls)
            {
                maxGirls = girlCount;
                mother = (root.Gender == Gender.Female ? root : root.Spouse);
            }

            foreach (var treeNode in root.Children)
            {
                GetMotherWithMostGirls(treeNode, ref maxGirls, ref mother);
            }
        }

        public void ViewFamilyTree()
        {
            PrintFamilyTree(Root, 0);
        }
        
        private static void PrintFamilyTree(Person root, int level)
        {
            if (root == null)
                return;

            var tab = new string('\t', level);

            Console.Write($"{tab}(Level-{level}){root.Name}-{(root.Gender == Gender.Male ? "M" : "F")}");
            if (root.Spouse != null)
            {
                Console.WriteLine($", {root.Spouse.Name}-{(root.Spouse.Gender == Gender.Male ? "M" : "F")}");
            }
            else
            {
                Console.WriteLine();
            }

            if (root.Children == null) return;
            foreach (var child in root.Children)
            {
                PrintFamilyTree(child, level + 1);
            }
        }
    }
}