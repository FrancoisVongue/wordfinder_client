using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordFinder_Business;
using WordFinder_Domain.Models;
using WordFinder_Domain.ServiceIO;

namespace WordFinder.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/[controller]")]
    public class WordsController : ControllerBase
    {
        private readonly WordService _service;
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public WordsController(WordService service, UserService userService, IMapper mapper)
        {
            _service = service;
            _userService = userService;
            _mapper = mapper;
        }
        
       
        [Authorize]
        [HttpGet("{amount=50}")]
        public ActionResult GetUserWords([FromQuery(Name = "amount")] int amount)
        {
            var _userId = JWThandler.GetUserId(GetToken());
            var words = _service.GetUserWords(_userId, amount);
            var mappedWords = _mapper.Map<IEnumerable<Word>, IEnumerable<WordDTO>>(words);
            
            return Ok(mappedWords);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddWords(IEnumerable<WordDTO> words)
        {
            var _userId = JWThandler.GetUserId(GetToken());
            var domainWords = _mapper.Map<IEnumerable<WordDTO>, IEnumerable<Word>>(words);
            var addedWords = _service.AddWords(_userId, domainWords);

            var mappedWords =
                _mapper.Map<IEnumerable<Word>, IEnumerable<WordDTO>>(addedWords);

            return Ok(mappedWords);
        }
        
        [Authorize]
        [HttpPost("tags")]
        public ActionResult AddTags(IEnumerable<TagInfo> tags)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid tag names found");
            
            var _userId = JWThandler.GetUserId(GetToken());
            var domainTags = _mapper.Map<IEnumerable<TagInfo>, IEnumerable<Tag>>(tags);
            var addedTags = _service.AddTags(_userId, domainTags);
            var mappedTags = 
                _mapper.Map<IEnumerable<Tag>, IEnumerable<TagInfo>>(addedTags);

            return Ok(mappedTags);
        }
        
        [HttpGet("specific")]
        public ActionResult GetSpecificWords(SearchInfo info)
        {
            var _userId = JWThandler.GetUserId(GetToken());
            var foundWords = _service.SearchWords(_userId, info);
            
            return Ok(); // todo return words
        }
        
        
        // [HttpPost]
        // public ActionResult GetWordsFromText(Topic topic)
        // {
        //     var _userId = _userService.GetUserByToken(GetToken()).Id;
        //     var words = _service.FindNewWords(topic.Content, _userId);
        //     return Ok(words);
        // }

        // [HttpPost("text")]
        // public ActionResult AddWords(Topic topic)
        // {
        //     var _userId = _userService.GetUser(HttpContext).Id;
        //     var addedTopic = _service.AddTopic(topic, _userId);
        //     return Ok(addedTopic);
        // }

        private string GetToken()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return token;
        }
    }
}