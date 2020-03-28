// using System;
// using System.Linq;
// using Microsoft.EntityFrameworkCore;
// using NUnit.Framework;
// using WordFinder_Business;
// using WordFinder_Domain.Models;
// using WordFinder_Repository;
//
// namespace WordFinder_BusinessTests
// {
//     public class WordServiceTest
//     {
//         private DbContextOptions<ApiContext> _options;
//         
//         [SetUp]
//         public void Setup()
//         {
//             _options = new DbContextOptionsBuilder<ApiContext>()
//                 .UseInMemoryDatabase(databaseName: "Add_to_database")
//                 .Options;
//         }
//         
//         [Test]
//         public void FindWords()
//         {
//             using (var context = new ApiContext(_options))
//             {
//                 var service = new WordService(context);
//                 var words = service.FindWords("hello world");
//                 foreach (var word in words)
//                 {
//                     Console.WriteLine(word.Content);
//                 }
//                 Assert.That(words.Count(), Is.EqualTo(2));
//                 context.Database.EnsureDeleted();
//             }
//         }
//
//         [Test]
//         public void GetWordsForRepetition_WhenCalled_ReturnsOnlyThoseThatNeedToBeRepeated()
//         {
//             using (var context = new ApiContext(_options))
//             {
//                 context.Users.Add(new User() {Id = 1});
//                 context.Words.Add(new Word() // should be repeated
//                 {
//                     UserId = 1,
//                     TimesRepeated = 0
//                 });
//                 context.Words.Add(new Word() // should not be repeated
//                 {
//                     UserId = 1,
//                     TimesRepeated = 1, 
//                     LastRepetitionTime = DateTime.Now 
//                 });
//                 context.Words.Add(new Word() // should be repeated
//                 {
//                     UserId = 1,
//                     TimesRepeated = 1, 
//                     LastRepetitionTime = DateTime.Now - TimeSpan.FromDays(6)
//                 });
//                 context.Words.Add(new Word() // should not be repeated
//                 {
//                     UserId = 1,
//                     TimesRepeated = 4, 
//                     LastRepetitionTime = DateTime.Now - TimeSpan.FromDays(6)
//                 });
//                 context.SaveChanges();
//                 
//                 var service = new WordService(context);
//                 var words = service.GetWordsForRepetition(1);
//                 
//                 Assert.That(words.Count(), Is.EqualTo(2));
//             }
//         }
//     }
// }