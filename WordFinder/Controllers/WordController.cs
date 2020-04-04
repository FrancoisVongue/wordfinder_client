using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordFinder_Business;
using WordFinder_Domain.Models;
using WordFinder_Domain.ServiceIO;

namespace WordFinder.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class WordsController : ControllerBase
    {
        private readonly WordService _service;
        private readonly long _userId = 1;

        public WordsController(WordService service)
        {
            _service = service;
        }
        
        /* GET WORDS FROM TEXT */
        [HttpPost("/text")]
        public ActionResult GetWordsFromText(Topic topic)
        {
            var words = _service.FindNewWords(topic.Content, _userId);
            return Ok(words);
        }

        /* ADD WORDS */
        [HttpPost("")]
        public ActionResult AddWords(Topic topic)
        {
            var addedWords = _service.AddWords(topic, _userId);
            return Ok(addedWords);
        }

        [HttpGet("searchinfo")]
        public ActionResult GetSearchInfo()
        {
            var tags = _service.GetUserCollection<Tag>(_userId);
            var topics = _service.GetUserCollection<Topic>(_userId);
            return Ok(new SearchInfo(){TagIds = tags, TopicIds = topics});
        }

        /* GET WORDS FOR USER */
        [HttpPost("search/{topicId?}")]
        public ActionResult GetUserWords(long? topicId, [FromBody] List<long> tagIds)
        {
            var words = _service.UserWords(_userId);
            if (topicId == null && !tagIds.Any())
                return Ok(words);

            var filteredWords = _service.SearchWords(words, topicId, tagIds);
            return Ok(filteredWords);
        }
    }
}