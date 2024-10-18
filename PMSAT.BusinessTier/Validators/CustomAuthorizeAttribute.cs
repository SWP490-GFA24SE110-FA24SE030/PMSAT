using Microsoft.AspNetCore.Authorization;
using PMSAT.BusinessTier.Enums;
using PMSAT.BusinessTier.Utils;

namespace PMSAT.BusinessTier.Validators
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute(params RoleEnum[] roleEnums)
        {
            var allowedRolesAsString = roleEnums.Select(x => x.GetDescriptionFromEnum());
            Roles = string.Join(",", allowedRolesAsString);
        }
    }
}