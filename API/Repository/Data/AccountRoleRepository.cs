using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, int>
    {
        private readonly MyContext myContext;
        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public int SignManager(string nik)
        {
            var checkExist = myContext.AccountRoles.Where(ar => ar.AccountId == nik).Where(ar => ar.RoleId == 2).FirstOrDefault();
            //var checkExist = myContext.AccountRoles.Find(nik);

            if (checkExist != null)
            {
                return 2; //Sudah jadi manager
            }
            var data = new AccountRole()
            {
                AccountId = nik,
                RoleId = 2
            };
            myContext.AccountRoles.Add(data);
            return myContext.SaveChanges();
        }
    }
}
