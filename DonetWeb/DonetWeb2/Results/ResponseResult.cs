using EF_Identity;

namespace DonetWeb2.Results
{
    /// <summary>
    /// 响应实体类
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int ReturnCode { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public object ErrorMessage { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 登录成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <returns></returns>

        public static ResponseResult LoginSuccess(object data, string token, bool admin)
        {
            return new ResponseResult { Data = data, Token = token, IsAdmin = admin, ErrorMessage = null, IsSuccess = true, ReturnCode = 200 };
        }

        /// <summary>
        /// 请求成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <returns></returns>
        public static ResponseResult Success(object data)
        {
            return new ResponseResult { Data = data, ErrorMessage = null, IsSuccess = true, ReturnCode = 200 };
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="str">错误信息</param>
        /// <param name="code">状态码</param>
        /// <returns></returns>
        public static ResponseResult Error(object str, int code)
        {
            return new ResponseResult { Data = null,Token=null,IsAdmin=false, ErrorMessage = str, IsSuccess = false, ReturnCode = code };
        }

        internal static object? Ok(User loginUser, string jwtToken, object value)
        {
            throw new NotImplementedException();
        }
    }
}
