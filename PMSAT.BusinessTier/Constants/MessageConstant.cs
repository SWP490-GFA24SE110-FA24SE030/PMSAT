using System.Data;
using System.Net.NetworkInformation;

namespace PMSAT.BusinessTier.Constants
{
    public static class MessageConstant
    {
        public static class LoginMessage
        {
            public const string InvalidUsernameOrPassword = "Tên đăng nhập hoặc mật khẩu không chính xác";
            public const string DeactivatedAccount = "Tài khoản đang bị vô hiệu hoá";
        }

        public static class User
        {
            public const string UserExisted = "User đã tồn tại";
            public const string CreateUserFailed = "Tạo tài khoản thất bại";
            public const string UserNotFoundMessage = "User không có trong hệ thống";
            public const string EmptyUserId = "Id không hợp lệ";
            public const string UpdateUserFailedMessage = "Cập nhật thông tin User thất bại";
            public const string UpdateUserSuccessMessage = "Cập nhật thông tin User thành công";
            public const string UpdateUserStatusFailedMessage = "Vô hiệu hóa tài khoản user thất bại";
            public const string UpdateUserStatusSuccessfulMessage = "Vô hiệu hóa tài khoản user thành công";
            public const string UserUnauthorizedMessage = "Bạn không được phép cập nhật status cho tài khoản này";
        }
    }
}