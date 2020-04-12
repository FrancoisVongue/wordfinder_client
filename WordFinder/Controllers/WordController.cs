using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
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
        private readonly UserService _userService;

        public WordsController(WordService service, UserService userService)
        {
            _service = service;
            _userService = userService;
        }
        
        [Authorize]
        [HttpPost]
        public ActionResult GetWordsFromText(Topic topic)
        {
            var _userId = _userService.GetUser(HttpContext).Id;
            var words = _service.FindNewWords(topic.Content, _userId);
            return Ok(words);
        }

        [Authorize]
        [HttpPost("text")]
        public ActionResult AddWords(Topic topic)
        {
            var _userId = _userService.GetUser(HttpContext).Id;
            var addedWords = _service.AddWords(topic, _userId);
            return Ok(addedWords);
        }

        /* GET GENERAL INFORMATION ABOUT USER LEXICON */ 
        [HttpGet("shallow")]
        public ActionResult GetShallowInfo()
        {
            var _userId = _userService.GetUser(HttpContext).Id;
            var info = _service.GetShallowInfo(_userId);
            return Ok(info);
        }

        /* GET WORDS FOR USER */
        [Authorize]
        [HttpGet("lexicon")]
        public ActionResult GetUserWords(long? topicId, [FromBody] List<long> tagIds)
        {
            var _userId = _userService.GetUser(HttpContext).Id;
            var words = _service.UserWords(_userId);
            if (topicId == null && !tagIds.Any())
                return Ok(words);

            var filteredWords = _service.SearchWords(words, topicId, tagIds);
            return Ok(filteredWords);
        }
    }
}