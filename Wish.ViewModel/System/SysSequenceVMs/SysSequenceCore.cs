using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using NPOI.SS.Formula.Functions;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Wish.System.Model;


namespace Wish.ViewModel.System.SysSequenceVMs
{
    public partial class SysSequenceVM
    {
        /// <summary>
        /// 获取序列号
        /// </summary>
        /// <param name="seqCode"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GetSequence(string seqCode, int count = 1)
        {
            var code = "";
            var seq = DC.Set<SysSequence>().Where(x => x.SeqCode == seqCode).FirstOrDefault();
            if (seq == null)
            {
                throw new Exception($"Sequecnce 序列信息 {seqCode} 未配置");
            }
            SqlParameter[] param = {
                        new SqlParameter("@_vSeqCode",SqlDbType.VarChar, 200),
                        new SqlParameter("@_iSeqCount", SqlDbType.Int),
                        new SqlParameter("@_vSeqID", SqlDbType.VarChar, 100),
                        new SqlParameter("@_iReturnCode", SqlDbType.Int),
                        new SqlParameter("@_vReturnMsg", SqlDbType.VarChar, 500)
                 };
            param[0].Value = seqCode;
            param[0].Direction = ParameterDirection.Input;
            param[1].Value = 1;
            param[1].Direction = ParameterDirection.Input;
            param[2].Value = "";
            param[2].Direction = ParameterDirection.Output;
            param[3].Value = 0;
            param[3].Direction = ParameterDirection.Output;
            param[4].Value = "";
            param[4].Direction = ParameterDirection.Output;

            DC.RunSP("SP_GET_SEQUENCES", param);
            if (param[3].Value.ToString() == "0")
            {
                code = param[2].Value.ToString();
            }
            else
            {
                throw new Exception($"生成序列号失败, 异常信息：{param[4].Value}");
            }

            return code;
        }
        /// <summary>
        /// 获取序列号async
        /// </summary>
        /// <param name="seqCode"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> GetSequenceAsync(string seqCode, int count = 1)
        {
            //await DC.Database.BeginTransactionAsync();
            var code = "";
            var seq = await DC.Set<SysSequence>().Where(x => x.SeqCode == seqCode).FirstOrDefaultAsync();
            if (seq == null)
            {
                throw new Exception($"Sequecnce 序列信息 {seqCode} 未配置");
            }
            SqlParameter[] param = {
                        new SqlParameter("@_vSeqCode",SqlDbType.VarChar, 200),
                        new SqlParameter("@_iSeqCount", SqlDbType.Int),
                        new SqlParameter("@_vSeqID", SqlDbType.VarChar, 100),
                        new SqlParameter("@_iReturnCode", SqlDbType.Int),
                        new SqlParameter("@_vReturnMsg", SqlDbType.VarChar, 500)
                 };
            param[0].Value = seqCode;
            param[0].Direction = ParameterDirection.Input;
            param[1].Value = 1;
            param[1].Direction = ParameterDirection.Input;
            param[2].Value = "";
            param[2].Direction = ParameterDirection.Output;
            param[3].Value = 0;
            param[3].Direction = ParameterDirection.Output;
            param[4].Value = "";
            param[4].Direction = ParameterDirection.Output;

            DC.RunSP("SP_GET_SEQUENCES", param);
            //await DC.Database.CommitTransactionAsync();
            if (param[3].Value.ToString() == "0")
            {
                code = param[2].Value.ToString();
            }
            else
            {
                throw new Exception($"生成序列号失败, 异常信息：{param[4].Value}");
            }

            return code;
        }
    }
}
