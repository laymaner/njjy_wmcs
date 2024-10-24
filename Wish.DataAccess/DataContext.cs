using System.Linq;
using System.Threading.Tasks;
using Com.Wish.Model.Base;
using Com.Wish.Model.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WalkingTec.Mvvm.Core;
using Wish.Areas.BasWhouse.Model;
using Wish.Areas.Config.Model;
using Wish.HWConfig.Models;
using Wish.Model.DevConfig;
using Wish.Model.Interface;
using Wish.Model.System;
using Wish.System.Model;
using Wish.TaskConfig.Model;
using WMS.Model.Base;

namespace Wish.DataAccess
{
    public class DataContext : FrameworkContext
    {
        public DbSet<FrameworkUser> FrameworkUsers { get; set; }

        #region WMS基础信息
        public DbSet<BasBCustomer> BasBCustomers { get; set; }
        public DbSet<BasBDepartment> basBDepartments { get; set; }
        public DbSet<BasBMaterial> BasBMaterials { get; set; }
        public DbSet<BasBMaterialCategory> BasBMaterialCategories { get; set; }
        public DbSet<BasBMaterialType> BasBMaterialTypes { get; set; }
        public DbSet<BasBMslMaintain> BasBMslMaintains { get; set; }
        public DbSet<BasBProprietor> BasBProprietors { get; set; }
        public DbSet<BasBSku> BasBSkus { get; set; }
        public DbSet<BasBSupplier> BasBSuppliers { get; set; }
        public DbSet<BasBSupplierBin> BasBSupplierBins { get; set; }
        public DbSet<BasBUnit> BasBUnits { get; set; }
        #endregion

        #region 设备信息
        public DbSet<DBConfig> DBConfigs { get; set; }
        public DbSet<PlcConfig> PlcConfigs { get; set; }
        public DbSet<StandardDevice> Devices { get; set; }
        public DbSet<DeviceConfig> DeviceConfigs { get; set; }
        public DbSet<DeviceTaskLog> DeviceTaskLogs { get; set; }
        public DbSet<DeviceStatusLog> DeviceStatusLogs { get; set; }
        public DbSet<DeviceAlarmLog> DeviceAlarmLogs { get; set; }
        public DbSet<MotDevParts> MotDevPartss { get; set; }
        public DbSet<MotDevPartsHis> MotDevPartsHiss { get; set; }
        //public DbSet<MapMonitor> DeviceMonitors { get; set; }
        #endregion

        #region WCS指令
        public DbSet<SrmCmd> SrmCmds { get; set; }
        public DbSet<SrmCmdHis> SrmCmdHiss { get; set; }
        #endregion

        #region WMS仓库信息
        public DbSet<BasWArea> BasWAreas { get; set; }
        public DbSet<BasWBin> BasWBins { get; set; }
        public DbSet<BasWErpWhouse> BasWErpWhouses { get; set; }
        public DbSet<BasWErpWhouseBin> BasWErpWhouseBins { get; set; }
        public DbSet<BasWLoc> BasWLocs { get; set; }
        public DbSet<BasWLocGroup> BasWLocGroups { get; set; }
        public DbSet<BasWPallet> BasWPallets { get; set; }
        public DbSet<BasWPalletType> BasWPalletTypes { get; set; }
        public DbSet<BasWRack> BasWRacks { get; set; }
        public DbSet<BasWRegion> BasWRegions { get; set; }
        public DbSet<BasWRegionType> BasWRegionTypes { get; set; }
        public DbSet<BasWRoadway> BasWRoadways { get; set; }
        public DbSet<BasWWhouse> BasWWhouses { get; set; }
        #endregion


        #region WMS盘点信息
        public DbSet<WmsItnInventory> WmsItnInventorys { get; set; }
        public DbSet<WmsItnInventoryDtl> WmsItnInventoryDtls { get; set; }
        public DbSet<WmsItnInventoryDtlHis> WmsItnInventoryDtlHiss { get; set; }
        public DbSet<WmsItnInventoryHis> WmsItnInventoryHiss { get; set; }
        public DbSet<WmsItnInventoryRecord> WmsItnInventoryRecords { get; set; }
        public DbSet<WmsItnInventoryRecordDif> WmsItnInventoryRecordDifs { get; set; }
        public DbSet<WmsItnInventoryRecordDifHis> WmsItnInventoryRecordDifHiss { get; set; }
        public DbSet<WmsItnInventoryRecordHis> WmsItnInventoryRecordHiss { get; set; }
        #endregion



        #region WMS入库信息
        public DbSet<WmsInOrder> WmsInOrders { get; set; }
        public DbSet<WmsInOrderDtl> WmsInOrderDtls { get; set; }
        public DbSet<WmsInOrderDtlHis> WmsInOrderDtlHiss { get; set; }
        public DbSet<WmsInOrderHis> WmsInOrderHiss { get; set; }
        public DbSet<WmsInReceipt> WmsInReceipts { get; set; }
        public DbSet<WmsInReceiptDtl> WmsInReceiptDtls { get; set; }
        public DbSet<WmsInReceiptDtlHis> WmsInReceiptDtlHiss { get; set; }
        public DbSet<WmsInReceiptHis> WmsInReceiptHiss { get; set; }
        public DbSet<WmsInReceiptIqcRecord> WmsInReceiptIqcRecords { get; set; }
        public DbSet<WmsInReceiptIqcRecordHis> WmsInReceiptIqcRecordHiss { get; set; }
        public DbSet<WmsInReceiptIqcResult> WmsInReceiptIqcResults { get; set; }
        public DbSet<WmsInReceiptIqcResultHis> WmsInReceiptIqcResultHiss { get; set; }
        public DbSet<WmsInReceiptRecord> WmsInReceiptRecords { get; set; }
        public DbSet<WmsInReceiptRecordHis> WmsInReceiptRecordHiss { get; set; }
        public DbSet<WmsInReceiptUniicode> WmsInReceiptUniicodes { get; set; }
        public DbSet<WmsInReceiptUniicodeHis> WmsInReceiptUniicodeHiss { get; set; }
        #endregion



        #region WMS检验信息
        public DbSet<WmsItnQc> WmsItnQcs { get; set; }
        public DbSet<WmsItnQcDtl> WmsItnQcDtls { get; set; }
        public DbSet<WmsItnQcDtlHis> WmsItnQcDtlHiss { get; set; }
        public DbSet<WmsItnQcHis> WmsItnQcHiss { get; set; }
        public DbSet<WmsItnQcRecord> WmsItnQcRecords { get; set; }
        public DbSet<WmsItnQcRecordHis> WmsItnQcRecordHiss { get; set; }
        #endregion


        #region WMS移库信息
        public DbSet<WmsItnMove> WmsItnMoves { get; set; }
        public DbSet<WmsItnMoveDtl> WmsItnMoveDtls { get; set; }
        public DbSet<WmsItnMoveDtlHis> WmsItnMoveDtlHiss { get; set; }
        public DbSet<WmsItnMoveHis> WmsItnMoveHiss { get; set; }
        public DbSet<WmsItnMoveRecord> WmsItnMoveRecords { get; set; }
        public DbSet<WmsItnMoveRecordHis> WmsItnMoveRecordHiss { get; set; }

        #endregion



        #region WMS出库信息
        public DbSet<WmsOutInvoice> WmsOutInvoices { get; set; }
        public DbSet<WmsOutInvoiceDtl> WmsOutInvoiceDtls { get; set; }
        public DbSet<WmsOutInvoiceDtlHis> WmsOutInvoiceDtlHiss { get; set; }
        public DbSet<WmsOutInvoiceHis> WmsOutInvoiceHiss { get; set; }
        public DbSet<WmsOutInvoiceJob> WmsOutInvoiceJobs { get; set; }
        public DbSet<WmsOutInvoiceRecord> WmsOutInvoiceRecords { get; set; }
        public DbSet<WmsOutInvoiceRecordHis> WmsOutInvoiceRecordHiss { get; set; }
        public DbSet<WmsOutInvoiceUniicode> WmsOutInvoiceUniicodes { get; set; }
        public DbSet<WmsOutInvoiceUniicodeHis> WmsOutInvoiceUniicodeHiss { get; set; }
        public DbSet<WmsOutWave> WmsOutWaves { get; set; }
        public DbSet<WmsOutWaveHis> WmsOutWaveHiss { get; set; }
        #endregion


        #region WMS上架信息
        public DbSet<WmsPutaway> WmsPutaways { get; set; }
        public DbSet<WmsPutawayHis> WmsPutawayHiss { get; set; }
        public DbSet<WmsPutawayDtl> WmsPutawayDtls { get; set; }
        public DbSet<WmsPutawayDtlHis> WmsPutawayDtlHiss { get; set; }

        #endregion



        #region WMS下架信息
        public DbSet<WmsPutdown> WmsPutdowns { get; set; }
        public DbSet<WmsPutdownHis> WmsPutdownHiss { get; set; }
        public DbSet<WmsPutdownDtl> WmsPutdownDtls { get; set; }
        public DbSet<WmsPutdownDtlHis> WmsPutdownDtlHiss { get; set; }
        #endregion



        #region WMS库存信息
        public DbSet<WmsStock> WmsStocks { get; set; }
        public DbSet<WmsStockHis> WmsStockHiss { get; set; }
        public DbSet<WmsStockAdjust> WmsStockAdjusts { get; set; }
        public DbSet<WmsStockDtl> WmsStockDtls { get; set; }
        public DbSet<WmsStockDtlHis> WmsStockDtlHiss { get; set; }
        public DbSet<WmsStockReconcileResult> WmsStockReconcileResults { get; set; }
        public DbSet<WmsStockReconcileResultHis> WmsStockReconcileResultHiss { get; set; }
        public DbSet<WmsStockUniicode> WmsStockUniicodes { get; set; }
        public DbSet<WmsStockUniicodeHis> WmsStockUniicodeHiss { get; set; }
        #endregion


        #region WMS任务信息
        public DbSet<WmsTask> WmsTasks { get; set; }
        public DbSet<WmsTaskHis> WmsTaskHiss { get; set; }
        #endregion


        #region WMS配置信息

        public DbSet<CfgDepartmentErpWhouse> CfgDepartmentErpWhouses { get; set; }
        public DbSet<CfgBusiness> CfgBusinesss { get; set; }
        public DbSet<CfgBusinessModule> CfgBusinessModules { get; set; }
        public DbSet<CfgBusinessParam> CfgBusinessParams { get; set; }
        public DbSet<CfgBusinessParamValue> CfgBusinessParamValues { get; set; }
        public DbSet<CfgDocLoc> CfgDocLocs { get; set; }
        public DbSet<CfgDocType> CfgDocTypes { get; set; }
        public DbSet<CfgDocTypeDtl> CfgDocTypeDtls { get; set; }
        public DbSet<CfgPrintTemplate> CfgPrintTemplates { get; set; }
        public DbSet<CfgRelationship> CfgRelationships { get; set; }
        public DbSet<CfgRelationshipType> CfgRelationshipTypes { get; set; }
        public DbSet<CfgStrategy> CfgStrategys { get; set; }
        public DbSet<CfgStrategyDtl> CfgStrategyDtls { get; set; }
        public DbSet<CfgStrategyItem> CfgStrategyItems { get; set; }
        public DbSet<CfgStrategyType> CfgStrategyTypes { get; set; }
        public DbSet<CfgPrintMachine> CfgPrintMachines { get; set; }
        public DbSet<CfgPrintTask> CfgPrintTasks { get; set; }
        public DbSet<CfgErpWhouse> CfgErpWhouses { get; set; }
        #endregion


        #region WMS系统表
        public DbSet<SysSequence> SysSequences { get; set; }
        public DbSet<SysDictionary> SysDictionarys { get; set; }
        public DbSet<SysParameter> SysParameters { get; set; }
        public DbSet<SysParameterValue> SysParameterValues { get; set; }
        public DbSet<SysEmail> SysEmails { get; set; }
        public DbSet<SysMapMonitor> SysMapMonitors { get; set; }


        #endregion
        #region WMS接口表
        public DbSet<InterfaceConfig> InterfaceConfigs { get; set; }
        public DbSet<InterfaceSendBack> InterfaceSendBacks { get; set; }
        public DbSet<InterfaceSendBackHis> InterfaceSendBackHiss { get; set; }
        #endregion

        public DataContext(CS cs)
             : base(cs)
        {
        }

        public DataContext(string cs, DBTypeEnum dbtype)
            : base(cs, dbtype)
        {
        }

        public DataContext(string cs, DBTypeEnum dbtype, string version = null)
            : base(cs, dbtype, version)
        {
        }

        
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public override async Task<bool> DataInit(object allModules, bool IsSpa)
        {
            var state = await base.DataInit(allModules, IsSpa);
            bool emptydb = false;
            try
            {
                emptydb = Set<FrameworkUser>().Count() == 0 && Set<FrameworkUserRole>().Count() == 0;
            }
            catch { }
            if (state == true || emptydb == true)
            {
                //when state is true, means it's the first time EF create database, do data init here
                //当state是true的时候，表示这是第一次创建数据库，可以在这里进行数据初始化
                var user = new FrameworkUser
                {
                    ITCode = "admin",
                    Password = Utils.GetMD5String("000000"),
                    IsValid = true,
                    Name = "Admin"
                };

                var userrole = new FrameworkUserRole
                {
                    UserCode = user.ITCode,
                    RoleCode = "001"
                };
                
                Set<FrameworkUser>().Add(user);
                Set<FrameworkUserRole>().Add(userrole);
                await SaveChangesAsync();
            }
            return state;
        }

    }

    /// <summary>
    /// DesignTimeFactory for EF Migration, use your full connection string,
    /// EF will find this class and use the connection defined here to run Add-Migration and Update-Database
    /// </summary>
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            return new DataContext("your full connection string", DBTypeEnum.SqlServer);
        }
    }

}
