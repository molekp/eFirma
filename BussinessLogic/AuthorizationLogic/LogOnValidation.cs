using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces;

namespace BussinessLogic.AuthorizationLogic
{
    public interface IMyLogOnValidation
    {
        bool CheckPassword(string a_userName, string a_password);
    }

    public class MyLogOnValidation : IMyLogOnValidation
    {
        private MyMembershipProvider MembershipProvider{ get; set; }
   

        public MyLogOnValidation()
        {
            MembershipProvider = new MyMembershipProvider();
        }
        
        public bool CheckPassword(string a_userName, string a_password)
        {
            return MembershipProvider.ValidateUser(a_userName, a_password);
        }

    }
}