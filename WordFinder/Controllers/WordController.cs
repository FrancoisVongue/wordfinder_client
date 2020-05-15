using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Internal;
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
        
        [HttpGet]
        public ActionResult GetUserWords([FromQuery(Name = "amount")] int amount = 10)
        {
            var _userId = JWThandler.GetUserId(GetToken());
            var words = _service.GetUserWords(_userId, amount);
            var mappedWords = _mapper.Map<IEnumerable<Word>, IEnumerable<WordDTO>>(words);
            
            return Ok(mappedWords);
        }

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
        
        [HttpPost("specific")]
        public ActionResult GetSpecificWords(SearchInfo info)
        {
            HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);

            using (StreamReader stream = new StreamReader(HttpContext.Request.Body))
            {
                string body = stream.ReadToEnd();
                Console.WriteLine(body);
            }
            
            var _userId = JWThandler.GetUserId(GetToken());
            var foundWords = _service.SearchWords(_userId, info);

            var mappedWords = _mapper.Map<IEnumerable<Word>, IEnumerable<WordDTO>>(foundWords);
            return Ok(mappedWords); 
        }

        [HttpPatch("word/{id}")]
        public ActionResult UpdateWord(long id, WordDTO word)
        {
            if (!ModelState.IsValid) 
                return BadRequest("Invalid word format");

            try
            {
                var _userId = JWThandler.GetUserId(GetToken());
                var domainWord = _mapper.Map<WordDTO, Word>(word);
                var updatedWord = _service.UpdateWord(_userId, domainWord);
                var mappedWord = _mapper.Map<Word, WordDTO>(updatedWord);
                return Ok(mappedWord);
            }
            catch(SecurityException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("repeat")]
        public ActionResult GetWordsForRepetition([FromQuery(Name = "amount")] int number = 10)
        {
            var _userId = JWThandler.GetUserId(GetToken());
            var wordsToRepeat = _service.GetWordsForRepetition(_userId, number);
            var mappedWords = _mapper.Map<IEnumerable<Word>, IEnumerable<WordDTO>>(wordsToRepeat);
            return Ok(mappedWords);
        }
        
        [HttpPost("repeat")]
        public ActionResult RepeatWords(IEnumerable<long> wordsIds)
        {
            var _userId = JWThandler.GetUserId(GetToken());
            var repeatedWords = _service.RepeatWords(_userId, wordsIds);
            if (repeatedWords == null)
                return BadRequest("Couldn't find words to repeat");
            
            var mappedWords = _mapper.Map<IEnumerable<Word>, IEnumerable<WordDTO>>(repeatedWords);
            return Ok(mappedWords);
        }
        
        [HttpPost("/text/words")]
        public ActionResult GetWordsFromText(Topic topic)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid text format");
            
            var _userId = _userService.GetUserByToken(GetToken()).Id;
            var words = _service.FindNewWords(_userId, topic);
            var mappedWords = _mapper.Map<IEnumerable<Word>, IEnumerable<WordDTO>>(words);
            
            return Ok(mappedWords);
        }

        private string GetToken()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            return token;
        }
    }
}