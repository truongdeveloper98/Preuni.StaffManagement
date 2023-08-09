namespace PreUni.StaffManagement.Core.CustomExcceptions
{
    public class WrongRoleException: Exception
    {
        public WrongRoleException(string message) : base(message)
        {
        }
    }

}
