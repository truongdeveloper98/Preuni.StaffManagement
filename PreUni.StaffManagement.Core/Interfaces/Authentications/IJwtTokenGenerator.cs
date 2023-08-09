namespace PreUni.StaffManagement.Core.Interfaces.Authentications
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(int id, string userName);
    }
}
