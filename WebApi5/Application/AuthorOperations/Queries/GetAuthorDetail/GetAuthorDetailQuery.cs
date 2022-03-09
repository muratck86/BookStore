using System;
using System.Linq;
using WebApi5.DbOperations;
using AutoMapper;

namespace WebApi5.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int AuthorId;
        public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _dbContext.Authors.Where(author => author.Id == AuthorId).SingleOrDefault();
            if (author == null)
                throw new NullReferenceException("Yazar bulunamadı");
            AuthorDetailViewModel result = _mapper.Map<AuthorDetailViewModel>(author); 
            return result;
        }

    }

    public class AuthorDetailViewModel
    {
        public string FullName { get; set; }
        public string BirthDate { get; set; }
    }

}