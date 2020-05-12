 using System;
 using System.Collections.Generic;
 using System.Linq;
 using Microsoft.EntityFrameworkCore;
 using NUnit.Framework;
 using WordFinder_Business;
 using WordFinder_Domain.Models;
 using WordFinder_Repository;

 namespace WordFinder_BusinessTests
 {
     public class WordServiceTest
     {
         private IEnumerable<Word> _words;
         
         [SetUp]
         public void Setup()
         {
             
             _words = new List<Word>()
             {
                 new Word()  { UserId = 1, TimesRepeated = 0, Id = 1},
                 new Word()  { UserId = 1, TimesRepeated = 1, Id = 2},
                 new Word()  { UserId = 1, TimesRepeated = 2, Id = 3},
                 new Word()  { UserId = 1, TimesRepeated = 3, Id = 4},
                 new Word()  { UserId = 1, TimesRepeated = 4, Id = 5},
                 new Word()  { UserId = 1, TimesRepeated = 5, Id = 6},
             };
         }
         
         [Test]
         public void GetWordsForRepetition_WhenCalled_ReturnsOnlyThoseThatNeedToBeRepeated()
         {
             var options = new DbContextOptionsBuilder<ApiContext>()
                 .UseInMemoryDatabase(databaseName: "Get_for_repetition")
                 .Options;
             
             using (var context = new ApiContext(options))
             {
                 context.Users.Add(new User() {Id = 1});
                 foreach (var word in _words)
                 {
                     context.Words.Add(word); 
                 }
                 
                 context.SaveChanges();
                 
                 var service = new WordService(context);
                 var threeWords = service.GetWordsForRepetition(1, 3);
                 var wordsRepetitionTimes = threeWords.Select(w => w.TimesRepeated);
                 
                 Assert.That(threeWords.Count(), Is.EqualTo(3));
                 Assert.That(wordsRepetitionTimes, Is.Ordered);
                 Assert.That(wordsRepetitionTimes, Does.Not.Contain(4));
             }
         }
         
         [Test]
         public void RepeatWords_WhenCalled_IncrementsRepetitionTimes()
         {
             var options = new DbContextOptionsBuilder<ApiContext>()
                 .UseInMemoryDatabase(databaseName: "Repeat_words")
                 .Options;
             
             using (var context = new ApiContext(options))
             {
                 var user = context.Users.Add(new User() {Id = 1}).Entity;
                 var word = context.Words.Add(new Word() {Id = 1, TimesRepeated = 1, UserId = 1}).Entity;
                 context.SaveChanges();
                 var service = new WordService(context);

                 service.RepeatWords(user.Id, new long[] {word.Id} );
                 Assert.That(word.TimesRepeated, Is.EqualTo(2));
             }
         }
         
         [Test]
         public void GetTopicFromDatabase_IfThereIsExistingTopicWithTheSameName_ReturnsTopicFromTheDatabase()
         {
             var options = new DbContextOptionsBuilder<ApiContext>()
                 .UseInMemoryDatabase(databaseName: "Get_Topic_From_Db")
                 .Options;
             
             using (var context = new ApiContext(options))
             {
                 var user = context.Users.Add(new User() {Id = 1}).Entity;
                 var topic = context.Topics
                     .Add(new Topic() {Name = "Keras", Id = 1, UserId = 1, Content = "test"}).Entity;
                 context.SaveChanges();
                 
                 var service = new WordService(context);

                 var topicFromDatabase = service.GetTopicFromDatabase(user.Id, new Topic(){Name = "Keras"});
                 Assert.That(topicFromDatabase, Is.EqualTo(topic));
                 Assert.That(topicFromDatabase.Content, Is.EqualTo("test"));
             }
         }
         
         [Test]
         public void GetTopicFromDatabase_IfThereIsNoTopicWithTheSameName_AddsNewTopicToDb()
         {
             var options = new DbContextOptionsBuilder<ApiContext>()
                 .UseInMemoryDatabase(databaseName: "Add_Topic_To_Db")
                 .Options;
             
             using (var context = new ApiContext(options))
             {
                 var user = context.Users.Add(new User() {Id = 1, FirstName = "test"}).Entity;
                 context.SaveChanges();
                 
                 var service = new WordService(context);
                 var topicFromDatabase = service.GetTopicFromDatabase(user.Id, new Topic() {Name = "Keras", Id = 1, Content = "test"});
                 
                 context.Topics.Find(1l);
                 Assert.That(topicFromDatabase.User.FirstName, Is.EqualTo("test"));
                 Assert.That(topicFromDatabase.Content, Is.EqualTo("test"));
             }
         }
     }
 }