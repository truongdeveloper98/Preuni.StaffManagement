
namespace PreUni.StaffManagement.Core.Utilities
{
	public class RandomPassword
	{
		public static string RandomString()
		{
			Random random = new();
			
			const string specialCharacters = "!@#$%^&*";
			const string charsUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			const string charsLower = "abcdefghijklmnopqrstuvwxyz";
			const string charNumer ="0123456789";
			var resultLower = new string(Enumerable.Repeat(charsLower, 3)
				.Select(s => s[random.Next(s.Length)]).ToArray());
			var resultUpper = new string(Enumerable.Repeat(charsUpper, 3)
				.Select(s => s[random.Next(s.Length)]).ToArray());
			var resultSpecial = new string(Enumerable.Repeat(specialCharacters, 3)
				.Select(s => s[random.Next(s.Length)]).ToArray());
			var resultNumber = new string(Enumerable.Repeat(charNumer, 3)
				.Select(s => s[random.Next(s.Length)]).ToArray());
			return resultUpper + resultLower + resultNumber + resultSpecial;
		}
	}
}
