using MovieStub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStub.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<MovieStubAppUser> GetAll();
        MovieStubAppUser Get(string id);
        void Insert(MovieStubAppUser entity);
        void Update(MovieStubAppUser entity);
        void Delete(MovieStubAppUser entity);
    }
}
