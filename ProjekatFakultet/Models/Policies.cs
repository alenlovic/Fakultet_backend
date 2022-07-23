using Microsoft.AspNetCore.Authorization;

namespace ProjekatFakultet.Models
{
    public static class Policies
    {
        public const string User = "User";
        public const string Admin = "Admin"; 
        public const string Service = "Service";


        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();
        }

        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build(); 
        }

        public static AuthorizationPolicy ServicePolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Service).Build();
        }

    }
}
