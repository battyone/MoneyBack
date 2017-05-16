﻿using System;
using Java.IO;
using NUnit.Framework;
using MoneyBack.Entities;
using MoneyBack.Helpers;
using MoneyBack.Orm;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;
using SQLiteNetExtensionsAsync.Extensions;


namespace MoneyBack.Android.Tests
{
    [TestFixture]
    public class RepositoryTests
    {
        private readonly DatabaseContext _dbContext = new DatabaseContext(true);

        [Test]
        public void one_new_person_inserted_adds_one_new_row()
        {
            // given
            var person = new Person()
            {
                Name = "A",
                LastName = "B"
            };


            // when
            var rows = _dbContext.People.Insert(person).Result;

            // then

            Assert.AreEqual(1, rows);
        }

        [Test]
        public void new_person_added_has_id_primarykey_generated()
        {
            // given
            var person1 = new Person
            {
                Name = "A",
                LastName = "B"
            };

            var person2 = new Person
            {
                Name = "A",
                LastName = "B"
            };


            // when
            var n1 = _dbContext.People.Insert(person1).Result; // getting Result in order to force Task's completion before continuing
            var n2 = _dbContext.People.Insert(person2).Result;
            var people = _dbContext.People.GetAll().Result;
            // then
            Assert.Greater(person1.Id, 0);
            Assert.AreEqual(person2.Id, person1.Id + 1);
        }
    }
}