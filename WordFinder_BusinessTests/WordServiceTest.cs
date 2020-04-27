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
                 .UseInMemoryDatabase(databaseName: "Add_to_database")
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
                 .UseInMemoryDatabase(databaseName: "Add_to_database")
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
                 var words = context.Words;
                 
                 
                 var repetitionTimeSumBeforeRepeating = getRepetitionTimesSum();

                 service.RepeatWords(1, words.Select(w => w.Id) );
                 
                 var repetitionTimeSumAfterRepeating = getRepetitionTimesSum();

                 long getRepetitionTimesSum()
                 {
                     var wordsRepetitionTimeSum = 0;
                     foreach (var word in words)
                     {
                         wordsRepetitionTimeSum += word.TimesRepeated;
                     }

                     return wordsRepetitionTimeSum;
                 }
                 
                 Assert.That(
                     repetitionTimeSumAfterRepeating - repetitionTimeSumBeforeRepeating,
                     Is.EqualTo(words.Count()));
                 Assert.That(words.Find(1l).TimesRepeated, Is.EqualTo(1));
             }
         }
     }
 }