using System;
using System.Collections.Generic;
using NUnit.Framework;
using FamilyTree.Models;

namespace FamilyTree.Tests
{
    [TestFixture]
    public class Problem4Tests
    {
        private Controller.LengaburuFamily _tree;

        [TestFixtureSetUp]
        public void Init()
        {
            _tree = new Controller.LengaburuFamily("King Shan", "Queen Anga");

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

        [Test]
        public void Test_Brother_Relation()
        {
            var relation = _tree.GetRelation("Ish", "Vich");

            Assert.True(relation.Equals(RelationType.Brother));
        }

        [Test]
        public void Test_Sister_Relation()
        {
            RelationType relation = _tree.GetRelation("Ish", "Satya");

            Assert.True(relation.Equals(RelationType.Sister));
        }

        [Test]
        public void Test_Father_Relation()
        {
            RelationType relation = _tree.GetRelation("Jata", "Drita");
            Assert.True(relation.Equals(RelationType.Father));
        }

        [Test]
        public void Test_Mother_Relation()
        {
            RelationType relation = _tree.GetRelation("Jata", "Jaya");
            Assert.True(relation.Equals(RelationType.Mother));
        }

        [Test]
        public void Test_Sons_Relation()
        {
            RelationType relation = _tree.GetRelation("Drita", "Jata");
            Assert.True(relation.Equals(RelationType.Son));
        }

        [Test]
        public void Test_Daughters_Relation()
        {
            RelationType relation = _tree.GetRelation("Drita", "Driya") ;
            Assert.True(relation.Equals(RelationType.Daughter));
        }

        [Test]
        public void Test_Cousins_Relation()
        {
            RelationType relation = _tree.GetRelation("Drita", "Vila");
            Assert.True(relation.Equals(RelationType.Cousins));
        }

        [Test]
        public void Test_GrandDaughter_Relation()
        {
            RelationType relation = _tree.GetRelation("Vich", "Lavnya");
            Assert.True(relation.Equals(RelationType.GrandDaughter));
        }

        [Test]
        public void Test_PaternalUncle_Relation()
        {
            RelationType relation = _tree.GetRelation("Kriya", "Saayan");
            Assert.True(relation.Equals(RelationType.PaternalUncle));
        }

        [Test]
        public void Test_MaternalUncle_Relation()
        {
            RelationType relation = _tree.GetRelation("Satvy", "Vich");
            Assert.True(relation.Equals(RelationType.MaternalUncle));
        }

        [Test]
        public void Test_PaternalAunt_Relation()
        {
            RelationType relation = _tree.GetRelation("Kriya", "Satvy");

            Assert.True(relation.Equals(RelationType.PaternalAunt));
        }

        [Test]
        public void Test_MaternalAunt_Relation()
        {
            RelationType relation = _tree.GetRelation("Satvy", "Lika");
            Console.WriteLine(relation);

            Assert.True(relation.Equals(RelationType.MaternalAunt));
        }

        [Test]
        public void Test_BrotherInLaw_Relation()
        {
            RelationType relation = _tree.GetRelation("Ambi", "Vich");
            Assert.True(relation.Equals(RelationType.BrotherInLaw));
        }

        [Test]
        public void Test_SisterInLaw_Relation()
        {
            RelationType relation = _tree.GetRelation("Vich", "Ambi");
            Assert.True(relation.Equals(RelationType.SisterInLaw));
        }
    }
}