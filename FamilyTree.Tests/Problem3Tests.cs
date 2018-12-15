using System;
using System.Collections.Generic;
using NUnit.Framework;
using FamilyTree.Models;

namespace FamilyTree.Tests
{
    [TestFixture]
    public class Problem3Tests
    {
        Controller.LengaburuFamily tree;

        [TestFixtureSetUp]
        public void Init()
        {
            tree = new Controller.LengaburuFamily("King Shan", "Queen Anga");

            tree.AddToFamily("Queen Anga", "Ish", RelationType.Son);
            tree.AddToFamily("Queen Anga", "Chit", RelationType.Son);
            tree.AddToFamily("Queen Anga", "Vich", RelationType.Son);
            tree.AddToFamily("Queen Anga", "Satya", RelationType.Daughter);

            tree.AddToFamily("Chit", "Ambi", RelationType.Spouse);
            tree.AddToFamily("Vich", "Lika", RelationType.Spouse);
            tree.AddToFamily("Satya", "Vyan", RelationType.Spouse);

            tree.AddToFamily("Ambi", "Drita", RelationType.Son);
            tree.AddToFamily("Ambi", "Vrita", RelationType.Son);

            tree.AddToFamily("Lika", "Vila", RelationType.Son);
            tree.AddToFamily("Lika", "Chika", RelationType.Daughter);

            tree.AddToFamily("Satya", "Satvy", RelationType.Daughter);
            tree.AddToFamily("Satya", "Savya", RelationType.Son);
            tree.AddToFamily("Satya", "Saayan", RelationType.Son);

            tree.AddToFamily("Drita", "Jaya", RelationType.Spouse);
            tree.AddToFamily("Vila", "Jnki", RelationType.Spouse);
            tree.AddToFamily("Chika", "Kpila", RelationType.Spouse);
            tree.AddToFamily("Satvy", "Asva", RelationType.Spouse);
            tree.AddToFamily("Savya", "Krpi", RelationType.Spouse);
            tree.AddToFamily("Saayan", "Mina", RelationType.Spouse);

            tree.AddToFamily("Jaya", "Jata", RelationType.Son);
            tree.AddToFamily("Jaya", "Driya", RelationType.Daughter);
            tree.AddToFamily("Jnki", "Lavnya", RelationType.Daughter);
            tree.AddToFamily("Krpi", "Kriya", RelationType.Son);
            tree.AddToFamily("Mina", "Misa", RelationType.Son);

            tree.AddToFamily("Driya", "Mnu", RelationType.Spouse);
            tree.AddToFamily("Lavnya", "Gru", RelationType.Spouse);
        }

        [Test]
        public void Test_MotherWithMostGirls()
        {
            string mother = tree.GetMotherWithMostGirls();

            Assert.IsNotNull(mother);
            Assert.True(mother.Equals("Queen Anga"));
        }

        [Test]
        public void Test_MotherWithMostGirls_AfterBoyAddition()
        {
            tree.AddToFamily("Jnki", "NewBoy", RelationType.Son);

            string mother = tree.GetMotherWithMostGirls();

            Assert.IsNotNull(mother);
            Assert.True(mother.Equals("Queen Anga"));
        }

        [Test]
        public void Test_MotherWithMostGirls_AfterGirlAddition()
        {
            tree.AddToFamily("Jnki", "NewGirl", RelationType.Daughter);

            var mother = tree.GetMotherWithMostGirls();
            
            Assert.IsNotNull(mother);
            Assert.True(mother.Equals("Jnki"));
        }
    }
}