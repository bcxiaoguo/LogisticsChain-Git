using System;
using System.Collections.Generic;
using System.Text;

namespace LogisticsChain.Entity.Authentication
{
    public class BaseAuthentication
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }
    }
}
