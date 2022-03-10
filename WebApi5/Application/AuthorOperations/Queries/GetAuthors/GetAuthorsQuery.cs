using System.Linq;
using WebApi5.DbOperations;
using System.Collections.Generic;
using AutoMapper;
using WebApi5.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi5.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authorList = _dbContext.Authors.OrderBy(x => x.Id).ToList<Author>();

            List<AuthorsViewModel> vm = _mapper.Map<List<AuthorsViewModel>>(authorList); 
            return vm;
        }
        
    }

    public class AuthorsViewModel
    {
        public int Id {get; set;}
        public string FullName { get; set; }
        public string BirthDate { get; set; }
    }
}