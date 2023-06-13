using IdContext.Core.Enumerable;
using IdContext.Core.ValueObject;
using Microsoft.AspNetCore.Identity;

namespace IdContext.Core.Entity;

public class User : IdentityUser
{
   		public  UserStatus Status { get; private set; } = default!;
		// It distiguish between users that have been authenticated by a Social Provider, they are not going to use two factor, becouse theirs provider is already implenenting it.
		public bool IsAuthenticatedExternaly { get; private set; } = default!;
        public UserProfile? Profile {get;private set;}

		private User() { }
		public static User CreateExternalUser(string email)
		{
			var newUser = new User
			{
				Status = UserStatus.Active,
				Email = email,
				EmailConfirmed = true,
				UserName = email,
				IsAuthenticatedExternaly = true,
				TwoFactorEnabled = false,
				LockoutEnabled = false,
			};
			return newUser;
		}

		public static User CreateLocalUser(string email)
		{
			var newUser = new User
			{
				Status = UserStatus.Active,
				Email = email,
				UserName = email,
				IsAuthenticatedExternaly = false,
				TwoFactorEnabled = false,
				LockoutEnabled = false
			};
			return newUser;
		}


		public void UseTwoFactor()
		{
			TwoFactorEnabled = true;
		}

		public void DeactiveTwoFactor()
		{
			TwoFactorEnabled = false;
		}
}