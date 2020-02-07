using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ExamInfo
    {
        #region 字段、变量
        
        /// <summary>
        /// 记录数据表的ID列最后一行的值
        /// </summary>
        public static long MySqlInsIndex = 0;

        /// <summary>
        /// 缓冲区大小
        /// </summary>
        public static int BufferSize = 255;
        #endregion
    }
}
