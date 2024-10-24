
using MySqlConnector;
using NPOI.SS.Formula.Functions;
using Wish.Models;
using Wish.Service;
using System;
using System.Collections.Generic;
using System.Data;
using WalkingTec.Mvvm.Core;
using System.Linq;
using Microsoft.Data.SqlClient;
using Wish.System.Model;

namespace Wish.Services
{
    public class ProService
    {
        #region Pro_Interface_Orders任务完成
        //完成指令
        public static ResultInfo Orders(List<Order> order)
        {
            MySqlParameter[] param =
            {// VARCHAR(20),任务号
            new MySqlParameter("@IV_TASK_ID",MySqlDbType.VarChar, 20),
            //VARCHAR(20),任务号2
            new MySqlParameter("@IV_TASK_ID2", MySqlDbType.VarChar, 20),
            new MySqlParameter("@OV_RESULT_CODE", MySqlDbType.Int32, 2),
            new MySqlParameter("@OV_MSG", MySqlDbType.VarChar, 100)
            };

            param[1].Value = "";
            param[1].Direction = ParameterDirection.Input;
            int orderRow = order.Count;
            for (int i = 0; i < orderRow; i++)
            {
                param[i].Value = order[i].TaskID; ;
                param[i].Direction = ParameterDirection.Input;
            }
            param[2].Value = 0;
            param[2].Direction = ParameterDirection.Output;
            param[3].Value = "";
            param[3].Direction = ParameterDirection.Output;
            IDataContext dcf = DCService.GetInstance().GetDC();
            try
            {
                dcf.Database.BeginTransaction();
                dcf.RunSP("PRO_Interface_Orders", param);
                dcf.Database.CommitTransaction();
                ResultInfo resultInfo = new ResultInfo();
                if (param[2].Value.Equals(0))
                {
                    resultInfo.Result = "00";
                    resultInfo.Message = "发送成功";
                    return resultInfo;
                }
                else
                {
                    resultInfo.Result = "01";
                    resultInfo.Message = "失败" + param[3].Value.ToString();
                    return resultInfo;
                }
            }
            catch (Exception)
            {
                dcf.Database.RollbackTransaction();
                throw;
            }

        }
        #endregion 

        #region Pro_Interface_Orders取消任务
        //取消指令
        public static ResultInfo CancelOrders(List<OrderCancel> order)
        {
            MySqlParameter[] param =
            {// VARCHAR(20),任务号
            new MySqlParameter("@IV_TASK_ID",MySqlDbType.VarChar, 20),
            //VARCHAR(20),任务号2
            new MySqlParameter("@IV_TASK_ID2", MySqlDbType.VarChar, 20),
            new MySqlParameter("@OV_RESULT_CODE", MySqlDbType.Int32, 2),
            new MySqlParameter("@OV_MSG", MySqlDbType.VarChar, 100)
            };
            int orderRow = order.Count;
            param[1].Value = "";
            param[1].Direction = ParameterDirection.Input;
            for (int i = 0; i < orderRow; i++)
            {
                param[i].Value = order[i].TaskID;
                param[i].Direction = ParameterDirection.Input;
            }
            param[2].Value = 0;
            param[2].Direction = ParameterDirection.Output;
            param[3].Value = "";
            param[3].Direction = ParameterDirection.Output;
            IDataContext dcf = DCService.GetInstance().GetDC();
            try
            {
                dcf.Database.BeginTransaction();
                dcf.RunSP("PRO_Interface_Orders", param);
                dcf.Database.CommitTransaction();
                ResultInfo resultInfo = new ResultInfo();
                if (param[2].Value.Equals(0))
                {
                    resultInfo.Result = "00";
                    resultInfo.Message = "发送成功";
                    return resultInfo;
                }
                else
                {
                    resultInfo.Result = "01";
                    resultInfo.Message = "失败" + param[3].Value.ToString();
                    return resultInfo;
                }
            }
            catch (Exception)
            {
                dcf.Database.RollbackTransaction();
                throw;
            }

        }
        #endregion 

        #region PRO_Interface_Task_In入库任务
        //入库指令
        public static ResultInfo ProTaskIn(List<In> taskIns)
        {
            MySqlParameter[] param = {
            // VARCHAR(20),任务号
            new MySqlParameter("@IV_TASK_ID",MySqlDbType.VarChar, 20),
            //VARCHAR(20),任务号2
            new MySqlParameter("@IV_TASK_ID2", MySqlDbType.VarChar, 20),
            new MySqlParameter("@OV_RESULT_CODE", MySqlDbType.Int32, 2),
            new MySqlParameter("@OV_MSG", MySqlDbType.VarChar, 100)
                };
            int taskInsRow = taskIns.Count;
            param[1].Value = "";
            param[1].Direction = ParameterDirection.Input;
            for (int i = 0; i < taskInsRow; i++)
            {
                param[i].Value = taskIns[i].TaskID;
                param[i].Direction = ParameterDirection.Input;
            }
            param[2].Value = 0;
            param[2].Direction = ParameterDirection.Output;
            param[3].Value = "";
            param[3].Direction = ParameterDirection.Output;
            IDataContext dcf = DCService.GetInstance().GetDC();
            try
            {
                dcf.Database.BeginTransaction();
                dcf.RunSP("PRO_Interface_Task_In", param);
                dcf.Database.CommitTransaction();
                ResultInfo resultInfo = new ResultInfo();
                if (param[2].Value.Equals(0))
                {
                    resultInfo.Result = "00";
                    resultInfo.Message = "发送成功";
                    return resultInfo;
                }
                else
                {
                    resultInfo.Result = "01";
                    resultInfo.Message = "失败" + param[3].Value.ToString();
                    return resultInfo;
                }
            }
            catch (Exception)
            {
                dcf.Database.RollbackTransaction();
                throw;
            }

        }
        #endregion

        #region 获取序列号
        /// <summary>
        /// 获取序列号
        /// </summary>
        /// <param name="seqCode"></param>
        /// <returns></returns>
        public static string GetSequence(string seqCode)
        {

            IDataContext dcf = DCService.GetInstance().GetDC();
            try
            {
                dcf.Database.BeginTransaction();
                var seq = dcf.Set<SysSequence>().Where(x => x.SeqCode == seqCode).FirstOrDefault();
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


                dcf.RunSP("SP_GET_SEQUENCES", param);
                dcf.Database.CommitTransaction();
                ResultInfo resultInfo = new ResultInfo();
                string res = param[2].Value.ToString();
                return res;
            }
            catch (Exception)
            {
                if (dcf.Database.CurrentTransaction!=null)
                {
                    dcf.Database.RollbackTransaction();
                }
                throw;
            }

        }
        #endregion

        #region PRO_Interface_Task_Out出库任务
        //出库指令
        public static ResultInfo ProTaskOut(List<Out> taskOuts)
        {
            MySqlParameter[] param = {
            // VARCHAR(20),任务号
            new MySqlParameter("@IV_TASK_ID",MySqlDbType.VarChar, 20),
            //VARCHAR(20),任务号2
            new MySqlParameter("@IV_TASK_ID2", MySqlDbType.VarChar, 20),
            new MySqlParameter("@OV_RESULT_CODE", MySqlDbType.Int32, 2),
            new MySqlParameter("@OV_MSG", MySqlDbType.VarChar, 100)
            };
            int taskOutsRow = taskOuts.Count;
            param[1].Value = "";
            param[1].Direction = ParameterDirection.Input;
            for (int i = 0; i < taskOutsRow; i++)
            {
                param[i].Value = taskOuts[i].TaskID;
                param[i].Direction = ParameterDirection.Input;
            }
            param[2].Value = 0;
            param[2].Direction = ParameterDirection.Output;
            param[3].Value = "";
            param[3].Direction = ParameterDirection.Output;
            IDataContext dcf = DCService.GetInstance().GetDC();
            try
            {
                dcf.Database.BeginTransaction();
                dcf.RunSP("PRO_Interface_Task_Out", param);
                dcf.Database.CommitTransaction();
                ResultInfo resultInfo = new ResultInfo();
                if (param[2].Value.Equals(0))
                {
                    resultInfo.Result = "00";
                    resultInfo.Message = "发送成功";
                    return resultInfo;
                }
                else
                {
                    resultInfo.Result = "01";
                    resultInfo.Message = "失败" + param[3].Value.ToString();
                    return resultInfo;
                }
            }
            catch (Exception)
            {
                dcf.Database.RollbackTransaction();
                throw;
            }

        }
        #endregion

        #region PRO_Interface_Task_Move移库任务
        //移库指令
        public static ResultInfo ProTaskMove(List<Move> taskMoves)
        {
            MySqlParameter[] param = {
            // VARCHAR(20),任务号
            new MySqlParameter("@IV_TASK_ID",MySqlDbType.VarChar, 20),
            //VARCHAR(20),任务号2
            new MySqlParameter("@IV_TASK_ID2", MySqlDbType.VarChar, 20),
            new MySqlParameter("@OV_RESULT_CODE", MySqlDbType.Int32, 2),
            new MySqlParameter("@OV_MSG", MySqlDbType.VarChar, 100)
            };
            int taskMovesRow = taskMoves.Count;
            param[1].Value = "";
            param[1].Direction = ParameterDirection.Input;
            for (int i = 0; i < taskMovesRow; i++)
            {
                param[i].Value = taskMoves[i].TaskID;
                param[i].Direction = ParameterDirection.Input;
            }
            param[2].Value = 0;
            param[2].Direction = ParameterDirection.Output;
            param[3].Value = "";
            param[3].Direction = ParameterDirection.Output;
            IDataContext dcf = DCService.GetInstance().GetDC();
            try
            {
                dcf.Database.BeginTransaction();
                dcf.RunSP("PRO_Interface_Task_Move", param);
                dcf.Database.CommitTransaction();
                ResultInfo resultInfo = new ResultInfo();
                if (param[2].Value.Equals(0))
                {
                    resultInfo.Result = "00";
                    resultInfo.Message = "发送成功";
                    return resultInfo;
                }
                else
                {
                    resultInfo.Result = "01";
                    resultInfo.Message = "失败" + param[3].Value.ToString();
                    return resultInfo;
                }
            }
            catch (Exception)
            {
                dcf.Database.RollbackTransaction();
                throw;
            }

        }
        #endregion

        #region PRO_Interface_Task_Check盘点任务
        //盘点指令
        public static ResultInfo ProTaskCheck(List<Check> taskChecks)
        {
            MySqlParameter[] param = {
            // VARCHAR(20),任务号
            new MySqlParameter("@IV_TASK_ID",MySqlDbType.VarChar, 20),
            //VARCHAR(20),任务号2
            new MySqlParameter("@IV_TASK_ID2", MySqlDbType.VarChar, 20),
            new MySqlParameter("@OV_RESULT_CODE", MySqlDbType.Int32, 2),
            new MySqlParameter("@OV_MSG", MySqlDbType.VarChar, 100)
            };
            int taskChecksRow = taskChecks.Count;
            param[1].Value = "";
            param[1].Direction = ParameterDirection.Input;
            for (int i = 0; i < taskChecksRow; i++)
            {
                param[i].Value = taskChecks[i].TaskID;
                param[i].Direction = ParameterDirection.Input;
            }
            param[2].Value = 0;
            param[2].Direction = ParameterDirection.Output;
            param[3].Value = "";
            param[3].Direction = ParameterDirection.Output;
            IDataContext dcf = DCService.GetInstance().GetDC();
            try
            {
                dcf.Database.BeginTransaction();
                dcf.RunSP("PRO_Interface_Task_Check", param);
                dcf.Database.CommitTransaction();
                ResultInfo resultInfo = new ResultInfo();
                if (param[2].Value.Equals(0))
                {
                    resultInfo.Result = "00";
                    resultInfo.Message = "发送成功";
                    return resultInfo;
                }
                else
                {
                    resultInfo.Result = "01";
                    resultInfo.Message = "失败" + param[3].Value.ToString();
                    return resultInfo;
                }
            }
            catch (Exception)
            {
                dcf.Database.RollbackTransaction();
                throw;
            }

        }
        #endregion

        #region PRO_Interface_Task_Check盘点确认任务
        //盘点确认指令
        public static ResultInfo ProTaskCheckConfirm(List<ConfirmCheck> taskCheckconfirms)
        {
            MySqlParameter[] param = {
            // VARCHAR(20),任务号
            new MySqlParameter("@IV_TASK_ID",MySqlDbType.VarChar, 20),
            //VARCHAR(20),任务号2
            new MySqlParameter("@IV_TASK_ID2", MySqlDbType.VarChar, 20),
            new MySqlParameter("@OV_RESULT_CODE", MySqlDbType.Int32, 2),
            new MySqlParameter("@OV_MSG", MySqlDbType.VarChar, 100)
            };
            int taskCheckconfirmsRow = taskCheckconfirms.Count;
            param[1].Value = "";
            param[1].Direction = ParameterDirection.Input;
            for (int i = 0; i < taskCheckconfirmsRow; i++)
            {
                param[i].Value = taskCheckconfirms[i].TaskID;
                param[i].Direction = ParameterDirection.Input;
            }
            param[2].Value = 0;
            param[2].Direction = ParameterDirection.Output;
            param[3].Value = "";
            param[3].Direction = ParameterDirection.Output;
            IDataContext dcf = DCService.GetInstance().GetDC();
            try
            {
                dcf.Database.BeginTransaction();
                dcf.RunSP("PRO_Interface_Task_CheckConfirm", param);
                dcf.Database.CommitTransaction();
                ResultInfo resultInfo = new ResultInfo();
                if (param[2].Value.Equals(0))
                {
                    resultInfo.Result = "00";
                    resultInfo.Message = "发送成功";
                    return resultInfo;
                }
                else
                {
                    resultInfo.Result = "01";
                    resultInfo.Message = "失败" + param[3].Value.ToString();
                    return resultInfo;
                }
            }
            catch (Exception)
            {
                dcf.Database.RollbackTransaction();
                throw;
            }

        }
        #endregion

        #region PRO_Interface_Task_CarryStation站台搬运任务
        //站台搬运指令
        public static ResultInfo ProTaskCarry(List<Carrystation> taskCarrys)
        {
            MySqlParameter[] param = {
            // VARCHAR(20),任务号
            new MySqlParameter("@IV_TASK_ID",MySqlDbType.VarChar, 20),
            //VARCHAR(20),任务号2
            new MySqlParameter("@IV_TASK_ID2", MySqlDbType.VarChar, 20),
            new MySqlParameter("@OV_RESULT_CODE", MySqlDbType.Int32, 2),
            new MySqlParameter("@OV_MSG", MySqlDbType.VarChar, 100)
            };
            int taskCarrysRow = taskCarrys.Count;
            param[1].Value = "";
            param[1].Direction = ParameterDirection.Input;
            for (int i = 0; i < taskCarrysRow; i++)
            {
                param[i].Value = taskCarrys[i].TaskID;
                param[i].Direction = ParameterDirection.Input;
            }
            param[2].Value = 0;
            param[2].Direction = ParameterDirection.Output;
            param[3].Value = "";
            param[3].Direction = ParameterDirection.Output;
            IDataContext dcf = DCService.GetInstance().GetDC();
            try
            {
                dcf.Database.BeginTransaction();
                dcf.RunSP("PRO_Interface_Task_CarryStation", param);
                dcf.Database.CommitTransaction();
                ResultInfo resultInfo = new ResultInfo();
                if (param[2].Value.Equals(0))
                {
                    resultInfo.Result = "00";
                    resultInfo.Message = "发送成功";
                    return resultInfo;
                }
                else
                {
                    resultInfo.Result = "01";
                    resultInfo.Message = "失败" + param[3].Value.ToString();
                    return resultInfo;
                }
            }
            catch (Exception)
            {
                dcf.Database.RollbackTransaction();
                throw;
            }

        }
        #endregion

        #region PRO_Interface_Task_Location回站台任务
        //回站台指令
        public static ResultInfo ProTaskLocation(List<Location> taskLocations)
        {
            MySqlParameter[] param =  {
            // VARCHAR(20),任务号
            new MySqlParameter("@IV_TASK_ID",MySqlDbType.VarChar, 20),
            //VARCHAR(20),任务号2
            new MySqlParameter("@IV_TASK_ID2", MySqlDbType.VarChar, 20),
            new MySqlParameter("@OV_RESULT_CODE", MySqlDbType.Int32, 2),
            new MySqlParameter("@OV_MSG", MySqlDbType.VarChar, 100)
            };
            int taskLocationsRow = taskLocations.Count;
            param[1].Value = "";
            param[1].Direction = ParameterDirection.Input;
            for (int i = 0; i < taskLocationsRow; i++)
            {
                param[i].Value = taskLocations[i].TaskID;
                param[i].Direction = ParameterDirection.Input;
            }
            param[2].Value = 0;
            param[2].Direction = ParameterDirection.Output;
            param[3].Value = "";
            param[3].Direction = ParameterDirection.Output;
            IDataContext dcf = DCService.GetInstance().GetDC();
            try
            {
                dcf.Database.BeginTransaction();
                dcf.RunSP("PRO_Interface_Task_Location", param);
                dcf.Database.CommitTransaction();
                ResultInfo resultInfo = new ResultInfo();
                if (param[2].Value.Equals(0))
                {
                    resultInfo.Result = "00";
                    resultInfo.Message = "发送成功";
                    return resultInfo;
                }
                else
                {
                    resultInfo.Result = "01";
                    resultInfo.Message = "失败" + param[3].Value.ToString();
                    return resultInfo;
                }
            }
            catch (Exception)
            {
                dcf.Database.RollbackTransaction();
                throw;
            }

        }
        #endregion
    }
}
