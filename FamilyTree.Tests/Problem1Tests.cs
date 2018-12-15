using System.Collections.Generic;
using NUnit.Framework;
using FamilyTree.Models;

namespace FamilyTree.Tests
{
    [TestFixture]
    public class Problem1Tests
    {
        private FamilyTree.Controller.LengaburuFamily _tree;

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
            var result = _tree.GetRelatives("Ish", "Brother");

            Assert.True(result.Count == 2);
            Assert.True(result.Contains("Chit"));
            Assert.True(result.Contains("Vich"));
        }

        [Test]
        public void Test_Sister_Relation()
        {
            var result = _tree.GetRelatives("Ish", "Sister");

            Assert.True(result.Count == 1);
            Assert.True(result.Contains("Satya"));
        }

        [Test]
        public void Test_Father_Relation()
        {
            var result = _tree.GetRelatives("Vila", "Father");

            Assert.True(result.Count == 1);
            Assert.True(result.Contains("Vich"));
        }

        [Test]
        public void Test_Mother_Relation()
        {
            var result = _tree.GetRelatives("Vila", "Mother");

            Assert.True(result.Count == 1);
            Assert.True(result.Contains("Lika"));
        }

        [Test]
        public void Test_Children_Relation()
        {
            var result = _tree.GetRelatives("Drita", "Children");

            Assert.True(result.Count == 2);
            Assert.True(result.Contains("Jata"));
            Assert.True(result.Contains("Driya"));
        }

        [Test]
        public void Test_Sons_Relation()
        {
            var result = _tree.GetRelatives("Drita", "Son");

            Assert.True(result.Count == 1);
            Assert.True(result.Contains("Jata"));
        }

        [Test]
        public void Test_Daughters_Relation()
        {
            var result = _tree.GetRelatives("Drita", "Daughter");

            Assert.True(result.Count == 1);
            Assert.True(result.Contains("Driya"));
        }

        [Test]
        public void Test_Cousins_Relation()
        {
            var result = _tree.GetRelatives("Drita", "Cousins");

            Assert.True(result.Count == 5);
            Assert.True(result.Contains("Vila"));
            Assert.True(result.Contains("Chika"));
            Assert.True(result.Contains("Satvy"));
            Assert.True(result.Contains("Savya"));
            Assert.True(result.Contains("Saayan"));
        }

        [Test]
        public void Test_GrandDaughter_Relation()
        {
            var result = _tree.GetRelatives("Chit", "GrandDaughter");

            Assert.True(result.Count == 1);
            Assert.True(result.Contains("Driya"));
        }

        [Test]
        public void Test_PaternalUncle_Relation()
        {
            var result = _tree.GetRelatives("Drita", "PaternalUncle");

            Assert.True(result.Count == 3);
            Assert.True(result.Contains("Ish"));
            Assert.True(result.Contains("Vich"));
            Assert.True(result.Contains("Vyan"));
        }

        [Test]
        public void Test_MaternalUncle_Relation()
        {
            var result = _tree.GetRelatives("Savya", "MaternalUncle");

            Assert.True(result.Count == 3);
            Assert.True(result.Contains("Ish"));
            Assert.True(result.Contains("Vich"));
            Assert.True(result.Contains("Chit"));
        }

        [Test]
        public void Test_PaternalAunt_Relation()
        {
            var result = _tree.GetRelatives("Drita", "PaternalAunt");

            Assert.True(result.Count == 2);
            Assert.True(result.Contains("Satya"));
            Assert.True(result.Contains("Lika"));
        }

        [Test]
        public void Test_MaternalAunt_Relation()
        {
            var result = _tree.GetRelatives("Drita", "MaternalAunt");

            Assert.True(result.Count == 2);
            Assert.True(result.Contains("Satya"));
            Assert.True(result.Contains("Lika"));
        }

        [Test]
        public void Test_BrotherInLaw_Relation()
        {
            var result = _tree.GetRelatives("Vyan", "BrotherInLaw");

            Assert.True(result.Count == 3);
            Assert.True(result.Contains("Ish"));
            Assert.True(result.Contains("Vich"));
            Assert.True(result.Contains("Chit"));
        }

        [Test]
        public void Test_SisterInLaw_Relation()
        {
            var result = _tree.GetRelatives("Ish", "SisterInLaw");

            Assert.True(result.Count == 2);
            Assert.True(result.Contains("Ambi"));
            Assert.True(result.Contains("Lika"));
        }
    }
}