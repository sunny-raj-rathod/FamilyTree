using System;
using System.Collections.Generic;
using System.Linq;
using FamilyTree.Models;

namespace FamilyTree.Controller
{
    /// <inheritdoc />
    /// <summary>
    /// Class for handling Lengaburu Family Tree
    /// </summary>
    public class LengaburuFamily
    {
        private readonly Family _family;

        private Dictionary<string, RelationType> relations = new Dictionary<string, RelationType>();
        
        public LengaburuFamily(string kingName, string queenName)
        {
            var king = new Person(kingName, Gender.Male, true);
            king.AddSpouse(queenName);

            _family = new Family(king);

            InitializeRelations();
        }

        private void InitializeRelations()
        {
            relations["paternaluncle"] = RelationType.PaternalUncle;
            relations["maternaluncle"] = RelationType.MaternalUncle;
            relations["paternalaunt"] = RelationType.PaternalAunt;
            relations["maternalaunt"] = RelationType.MaternalAunt;
            relations["sisterinlaw"] = RelationType.SisterInLaw;
            relations["brotherinlaw"] = RelationType.BrotherInLaw;
            relations["cousins"] = RelationType.Cousins;
            relations["father"] = RelationType.Father;
            relations["mother"] = RelationType.Mother;
            relations["children"] = RelationType.Children;
            relations["son"] = RelationType.Son;
            relations["daughter"] = RelationType.Daughter;
            relations["brother"] = RelationType.Brother;
            relations["sister"] = RelationType.Sister;
            relations["granddaughter"] = RelationType.GrandDaughter;
            relations["spouse"] = RelationType.Spouse;
        }
        
        public bool AddToFamily(string person, string relative, RelationType relation)
        {
            return _family.AddToFamily(person, relative, relation);
        }

        public RelationType GetRelation(string personName, string relativeName)
        {
            var person = _family.GetMember(personName);
            var relative = _family.GetMember(relativeName);
            
            return person.GetRelationship(relative);
        }

        public List<string> GetRelatives(string personName, string relation)
        {
            var person = _family.GetMember(personName);
            
            return person.GetRelative(relations[relation.ToLower()]).Select(s => s.Name).ToList();
        }

        public string GetMotherWithMostGirls()
        {
            var mother = _family.GetMotherWithMostGirls();
            return mother != null ? mother.Name : string.Empty;
        }

        public void ViewFamilyTree()
        {
            _family.ViewFamilyTree();
        }
    }
}