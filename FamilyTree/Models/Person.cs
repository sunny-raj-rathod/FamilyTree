using System;
using System.Collections.Generic;
using System.Threading;

namespace FamilyTree.Models
{
    public class Person
    {
        private Person Parent { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public Person Spouse { get; set; }
        private bool IsRoyalMember { get; set; }
        public List<Person> Children{ get; set; }
        private int Generation { get; set; }
        
        private delegate List<Person> RelationHandlerDelegate();

        private readonly Dictionary<RelationType, Delegate> _relationDelegates = new Dictionary<RelationType, Delegate>();

        private void InitializeDelegates()
        {
            _relationDelegates[RelationType.Brother] = new RelationHandlerDelegate(GetBrothers);
            _relationDelegates[RelationType.Sister] = new RelationHandlerDelegate(GetSisters);
            _relationDelegates[RelationType.Father] = new RelationHandlerDelegate(GetFather);
            _relationDelegates[RelationType.Daughter] = new RelationHandlerDelegate(GetDaughters);
            _relationDelegates[RelationType.Son] = new RelationHandlerDelegate(GetSons);
            _relationDelegates[RelationType.Children] = new RelationHandlerDelegate(GetChildren);
            _relationDelegates[RelationType.Mother] = new RelationHandlerDelegate(GetMother);
            _relationDelegates[RelationType.Cousins] = new RelationHandlerDelegate(GetAllCousins);
            _relationDelegates[RelationType.GrandDaughter] = new RelationHandlerDelegate(GetGrandDaughter);
            _relationDelegates[RelationType.BrotherInLaw] = new RelationHandlerDelegate(GetBrothersInLaw);
            _relationDelegates[RelationType.SisterInLaw] = new RelationHandlerDelegate(GetSistersInLaw);
            _relationDelegates[RelationType.MaternalAunt] = new RelationHandlerDelegate(GetMaternalAunt);
            _relationDelegates[RelationType.PaternalAunt] = new RelationHandlerDelegate(GetPaternalAunt);
            _relationDelegates[RelationType.PaternalUncle] = new RelationHandlerDelegate(GetPaternalUncle);
            _relationDelegates[RelationType.MaternalUncle] = new RelationHandlerDelegate(GetMaternalUncle);
        }

        public Person(string name, Gender gender, bool isRoyalMember = false)
        {
            this.Name = name;
            this.Gender = gender;
            this.Spouse = null;
            this.IsRoyalMember = isRoyalMember;
            InitializeDelegates();
        }
        
        public bool AddChild(string childName, RelationType relation)
        {
            var child =
                new Person(childName, (relation == RelationType.Son ? Gender.Male : Gender.Female), true)
                {
                    Generation = this.Generation + 1
                };

            if (this.IsRoyalMember)
            {
                child.Parent = this;
                if (Children == null)
                    Children = new List<Person>();
                
                Children.Add(child);
            }
            else
            {
                child.Parent = this.Spouse;
                if (this.Spouse.Children == null)
                    this.Spouse.Children = new List<Person>();
                
                this.Spouse.Children.Add(child);
            }

            return true;
        }
        
        public bool AddSpouse(string spouseName)
        {
            this.Spouse =
                new Person(spouseName, (this.Gender == Gender.Male ? Gender.Female : Gender.Male),
                    !this.IsRoyalMember)
                {
                    Spouse = this,
                    Children =  this.Children,
                    Generation = this.Generation
                };
            return true;
        }
        
        public List<Person> GetRelative(RelationType relation)
        {
            var functionDelegate = _relationDelegates[relation];
            var result = (List<Person>)functionDelegate.DynamicInvoke();
            return result;
        }

        public RelationType GetRelationship(Person relative)
        {
            return Generation == relative.Generation ? 
                CheckSameGenerationRelations(relative) : 
                CheckDifferentGenerationRelations(relative);
        }
        
        private RelationType CheckSameGenerationRelations(Person relative)
        {
            if (IsBrother(relative))
                return RelationType.Brother;
            
            if (IsSister(relative))
                return RelationType.Sister;

            if (IsCousin(relative))
                return RelationType.Cousins;
            
            if (IsBrotherInLaw(relative))
                return RelationType.BrotherInLaw;

            return IsSisterInLaw(relative) ? RelationType.SisterInLaw : RelationType.Invalid;
        }

        private bool IsSibling(Person person)
        {
            return (Parent == person.Parent && (IsRoyalMember && person.IsRoyalMember));
        }
        
        private bool IsBrother(Person person)
        {
            return IsSibling(person) && person.IsMale();
        }
        
        private bool IsSister(Person person)
        {
            return IsSibling(person) && person.IsFemale();
        }
        
        private bool IsInLaw(Person person)
        {
            return (!IsRoyalMember || !person.IsRoyalMember);
        }
        
        private bool IsBrotherInLaw(Person person)
        {
            return IsInLaw(person) && person.IsMale();
        }
        
        private bool IsSisterInLaw(Person person)
        {
            return IsInLaw(person) && person.IsFemale();
        }
        
        private bool IsCousin(Person person)
        {
            return (Parent != person.Parent && (IsRoyalMember && person.IsRoyalMember));
        }
        
        private bool IsMale()
        {
            return Gender == Gender.Male;
        }
        
        private bool IsFemale()
        {
            return Gender == Gender.Female;
        }
        
        private RelationType CheckDifferentGenerationRelations(Person relative)
        {
            if (IsFather(relative))
                return RelationType.Father;
            if (IsMother(relative))
                return RelationType.Mother;

            if (IsSon(relative))
                return RelationType.Son;

            if (IsDaughter(relative))
                return RelationType.Daughter;

            if (IsGrandDaughter(relative))
                return RelationType.GrandDaughter;
            
            if (IsPaternalUncle(relative))
                return RelationType.PaternalUncle;
            
            if (IsPaternalAunt(relative))
                return RelationType.PaternalAunt;
            
            if (IsMaternalUncle(relative))
                return RelationType.MaternalUncle;
            
            return IsMaternalAunt(relative) ? RelationType.MaternalAunt : RelationType.Invalid;
        }
        
        private bool IsParent(Person person)
        {
            return (this.Parent == person || (this.Parent.Spouse != null && this.Parent.Spouse == person));
        }
        
        private bool IsChild(Person person)
        {
            return this == person.Parent;
        }
        
        private bool IsFather(Person person)
        {
            return IsParent(person) && person.IsMale();
        }
        
        private bool IsMother(Person person)
        {
            return IsParent(person) && person.IsFemale();
        }
        
        private bool IsSon(Person person)
        {
            return IsChild(person) && person.IsMale();
        }
        
        private bool IsDaughter(Person person)
        {
            return IsChild(person) && person.IsFemale();
        }
        
        private bool IsGrandDaughter(Person person)
        {
            return person.Parent?.Parent != null && person.Parent.Parent == this && person.IsFemale();
        }
        
        private int GenerationGap(Person person)
        {
            return Generation - person.Generation;
        }
        
        private bool IsPaternalUncle(Person person)
        {
            return (GenerationGap(person) == 1) && (Parent != null && Parent.IsMale()) && person.IsMale();
        }
        
        private bool IsPaternalAunt(Person person)
        {
            return (GenerationGap(person) == 1) && (Parent != null && Parent.IsMale()) && person.IsFemale();
        }
        
        private bool IsMaternalUncle(Person person)
        {
            return (GenerationGap(person) == 1) && (Parent != null && Parent.IsFemale()) && person.IsMale();
        }
        
        private bool IsMaternalAunt(Person person)
        {
            return (GenerationGap(person) == 1) && (Parent != null && Parent.IsFemale()) && person.IsFemale();
        }

        private List<Person> GetSiblings(Gender gender, bool getAll = false)
        {
            var siblings = new List<Person>();

            if (Parent?.Children == null) return siblings;
            
            foreach (var treeNode in Parent.Children)
            {
                if (treeNode != this && (treeNode.Gender == gender || getAll))
                    siblings.Add(treeNode);
            }

            return siblings;
        }
        
        private List<Person> GetAllSiblings()
        {
            return GetSiblings(Gender.Male, true);
        }
        
        private List<Person> GetBrothers()
        {
            return GetSiblings(Gender.Male);
        }

        private List<Person> GetSisters()
        {
            return GetSiblings(Gender.Female);
        }
        
        private List<Person> GetParent(Gender gender)
        {
            var parents = new List<Person>();

            if (Parent == null) return null;

            parents.Add(Parent.Gender == gender ? Parent : Parent.Spouse);

            return parents;
        }

        private List<Person> GetFather()
        {
            return GetParent(Gender.Male);
        }

        private List<Person> GetMother()
        {
            return GetParent(Gender.Female);
        }
        
        private List<Person> GetChildren(Gender gender, bool getAll = false)
        {
            var children = new List<Person>();

            if (this.Children != null)
            {
                foreach (var treeNode in Children)
                {
                    if (getAll || treeNode.Gender == gender)
                        children.Add(treeNode);
                }
                return children;
            }

            if (Spouse?.Children == null) return children;
            
            foreach (var treeNode in Spouse.Children)
            {
                if (getAll || treeNode.Gender == gender)
                    children.Add(treeNode);
            }
            return children;
        }

        private List<Person> GetChildren()
        {
            return GetChildren(Gender.Male, true);
        }

        private List<Person> GetSons()
        {
            return GetChildren(Gender.Male);
        }

        private List<Person> GetDaughters()
        {
            return GetChildren(Gender.Female);
        }
        
        private List<Person> GetCousins(Gender gender, bool getAll = false)
        {
            var cousins = new List<Person>();

            if (Parent == null)
                return cousins;
            
            foreach(var item in Parent.GetAllSiblings())
            {
                var subresult = item.GetChildren();
                if(subresult != null)
                    cousins.AddRange(subresult);
            }

            return cousins;
        }
        
        private List<Person> GetAllCousins()
        {
            return GetCousins(Gender.Male, true);
        }

        private List<Person> GetGrandDaughter()
        {
            var children = GetChildren();

            var result = new List<Person>();
            foreach (var child in children)
            {
                var subresult = child.GetDaughters();
                if (subresult != null)
                    result.AddRange(subresult);
            }

            return result;
        }
        
        private List<Person> GetUncleOrAunt(Person parent, Gender gender)
        {
            var result = new List<Person>();

            if (parent == null) return result;
            
            if (parent.IsRoyalMember)
            {
                result.AddRange(parent.GetSiblingSpouses(gender));
                result.AddRange(parent.GetSiblings(gender));
            }
            else if(parent.Spouse != null)
            {
                result.AddRange(parent.Spouse.GetSiblingSpouses(gender));
                result.AddRange(parent.Spouse.GetSiblings(gender));
            }

            return result;
        }
        
        private List<Person> GetPaternalUncle()
        {
            var father = GetFather();
            
            var result = new List<Person>();

            if (father == null || father.Count == 0) return result;

            return GetUncleOrAunt(father[0], Gender.Male);
        }

        private List<Person> GetMaternalUncle()
        {
            var mother = GetMother();
            
            var result = new List<Person>();
            
            if (mother == null || mother.Count == 0) return result;
            
            return GetUncleOrAunt(mother[0], Gender.Male);
        }

        private List<Person> GetPaternalAunt()
        {
            var father = GetFather();
            
            var result = new List<Person>();
            
            if (father == null || father.Count == 0) return result;
            
            return GetUncleOrAunt(father[0], Gender.Female);
        }

        private List<Person> GetMaternalAunt()
        {
            var mother = GetMother();
            
            var result = new List<Person>();

            if (mother == null || mother.Count == 0) return result;
            
            return GetUncleOrAunt(mother[0], Gender.Female);
        }
        
        private List<Person> GetSiblingSpouses(Gender gender, bool getAllSiblingsSpouse = false)
        {
            var result = new List<Person>();

            if (IsRoyalMember)
            {
                var siblings = GetSiblings(gender == Gender.Male ? Gender.Female : Gender.Male);
                
                foreach (var treeNode in siblings)
                {
                    if(treeNode.Spouse != null)
                        result.Add(treeNode.Spouse);
                }
                
                var cousins = GetCousins(gender == Gender.Male ? Gender.Female : Gender.Male);
                
                foreach (var treeNode in cousins)
                {
                    if(treeNode.Spouse != null)
                        result.Add(treeNode.Spouse);
                }
            }
            else
            {
                result.AddRange(Spouse.GetSiblings(gender));
                result.AddRange(Spouse.GetCousins(gender));
            }
            
            return result;
        }

        private List<Person> GetBrothersInLaw()
        {
            return GetSiblingSpouses(Gender.Male);
        }

        private List<Person> GetSistersInLaw()
        {
            return GetSiblingSpouses(Gender.Female);
        }
    }
}
