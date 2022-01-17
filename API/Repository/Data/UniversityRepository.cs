using API.Context;
using API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class UniversityRepository : GeneralRepository<MyContext,University,int>
    {
        private readonly MyContext myContext;
        public UniversityRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public IEnumerable GetUniversityStat()
        {
            var query = (from education in myContext.Set<Education>()
                         join university in myContext.Set<University>()
                            on education.UniversityId equals university.UniversityId
                         group education by university.UniversityName into g
                         select new
                         {
                             UniversityName = g.Key,
                             count = g.Count()
                         });
            return query.ToList();
        }
    }
}
