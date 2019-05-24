using System;
using System.Collections.Generic;
using System.Text;

namespace LogisticsChain.Entity.Authentication
{
    /// <summary>
    /// 验证接收实体
    /// </summary>
  public  class Verification: BaseAuthentication
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
     

    }
}
