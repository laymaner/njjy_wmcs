import UploadBox from "@/components/page/upload/index.vue";
import { createBlob } from "@/util/files";
import { listToString } from "@/util/string";
import { Component, Vue } from "vue-property-decorator";
import { Action, State } from "vuex-class";

type EventFunction = (data: Object | String | Array<any>) => void;
type DefaultFunction = () => void;

export interface IActionEvent {
  onAdd?: DefaultFunction;
  onEdit?: EventFunction;
  onDetail?: EventFunction;
  onDelete?: EventFunction;
  onBatchDelete?: EventFunction;
  onImported?: DefaultFunction;
  onExportAll?: DefaultFunction;
  onExport?: EventFunction;
  //自定义按钮方法
  taskReload?:EventFunction;//任务重发
  taskFinish?:EventFunction;//任务完成
  taskClose?: EventFunction;//任务关闭
  createInvoice?:DefaultFunction;//创建发货单
  createInventory?:DefaultFunction;//创建盘点单
  dealcmdSend?:EventFunction;//指令下发处理（下发完成）
  dealsrmPick?:EventFunction;//取货完成
  dealsrmPut?:EventFunction;//放货完成
  dealsrmRefuse?:EventFunction;//任务拒绝
  dealsrmTaskIn?:DefaultFunction;//入库申请
  dealsrmTaskOut?:EventFunction;//出库申请
  deviceOpen?:EventFunction;//连接设备
  deviceClose?:EventFunction;//关闭设备
  devicestepInit?:EventFunction;//设备步序初始
  plcOpen?:EventFunction;//连接PLC
  plcClose?:EventFunction;//关闭PLC
  handleInter?:EventFunction;//手动回传接口
  cmdResend?:EventFunction;//指令重发
  deviceWait?:EventFunction;//设备手动待命
  manualOut?:EventFunction;//手动出库
  openBin?:EventFunction;//启用货位
  closeBin?:EventFunction;//禁用货位
}

/**
 * 首页中的按钮部分，添加/修改/删除/导出/导出
 */

function mixinFunc(ASSEMBLIES: Array<string> = []) {
  @Component({
    components: {
      UploadBox
    }
  })
  class actionMixins extends Vue {
    @Action("search") search;
    @Action("batchDelete") batchDelete;
    @Action("deleted") deleted;
    @Action("exportExcel") exportExcel;
    @Action("exportExcelByIds") exportExcelByIds;
    @Action("detail") detail;
    @Action("imported") imported;
    @Action("getExcelTemplate") getExcelTemplate;
    //自定义方法
    @Action("taskreload") taskreload;
    @Action("taskfinish") taskfinish;
    @Action("taskclose") taskclose;
    @Action("createinvoice") createinvoice;
    @Action("createinventory") createinventory;
    @Action("dealcmdsend") dealcmdsend;
    @Action("dealsrmpick") dealsrmpick;
    @Action("dealsrmput") dealsrmput;
    @Action("dealsrmrefuse") dealsrmrefuse;
    @Action("dealsrmtaskin") dealsrmtaskin;
    @Action("dealsrmtaskout") dealsrmtaskout;
    @Action("deviceopen") deviceopen;
    @Action("deviceclose") deviceclose;
    @Action("devicestepinit") devicestepinit;
    @Action("plcopen") plcopen;
    @Action("plcclose") plcclose;
    @Action("handleinter") handleinter;
    @Action("cmdresend") cmdresend;
    @Action("devicewait") devicewait;
    @Action("manualout") manualout;
    @Action("openbin") openbin;
    @Action("closebin") closebin;

    // api 授权权限列表
    @State actionList;
    // 展示的动作按钮
    assembly: Array<string> = ASSEMBLIES;
    // 表单弹出框内容
    dialogIsShow: Boolean = false;
    // 表单弹出框内容
    dialogcreateIsShow: Boolean = false;
    // 打开选中数据
    dialogData: Object = {};
    // 打开详情状态（增删改查）
    dialogStatus: String = "";
    // 导入
    uploadIsShow: Boolean = false;
    //输入框内容--新增
    inputValue: String = "";
    inputValue2: String = "";
    /**
     * 事件方法list
     */
    get actionEvent(): IActionEvent {
      return {
        onAdd: this.onAdd,
        onEdit: this.onEdit,
        onDetail: this.onDetail,
        onDelete: this.onDelete,
        onBatchDelete: this.onBatchDelete,
        onImported: this.onImported,
        onExportAll: this.onExportAll,
        onExport: this.onExport,
        //自定义按钮
        taskReload:this.taskReload,
        taskFinish:this.taskFinish,
        taskClose:this.taskClose,
        createInvoice:this.createInvoice,
        createInventory:this.createInventory,
        dealcmdSend:this.dealcmdSend,
        dealsrmPick:this.dealsrmPick,
        dealsrmPut:this.dealsrmPut,
        dealsrmRefuse:this.dealsrmRefuse,
        dealsrmTaskIn:this.dealsrmTaskIn,
        dealsrmTaskOut:this.dealsrmTaskOut,
        deviceOpen:this.deviceOpen,
        deviceClose:this.deviceClose,
        devicestepInit:this.devicestepInit,
        plcOpen:this.plcOpen,
        plcClose:this.plcClose,
        handleInter:this.handleInter,
        cmdResend:this.cmdResend,
        deviceWait:this.deviceWait,
        manualOut:this.manualOut,
        openBin:this.openBin,
        closeBin:this.closeBin,
        //
      };
    }
    /**
     * 打开详情弹框（默认框）
     * @param status
     * @param data
     */
    openDialog(status, data = {}) {
      this.dialogIsShow = true;
      this.dialogStatus = status;
      this.dialogData = data;
    }
    /**
     * 打开新增单据弹窗
     * @param status
     */
    openAddDialog(status){
      this.dialogcreateIsShow = true;
      this.dialogStatus = status;
    }
    /**
     * 查询接口 ★★★★★
     * @param params
     */
    privateRequest(params) {
      return this.search(params);
    }
    /**
     * 添加
     */
    onAdd() {
      this.openDialog(this.$actionType.add);
    }
    /**
     * 修改
     * @param data
     */
    onEdit(data) {
      this.openDialog(this.$actionType.edit, { ID: data.ID });
    }
    /**
     * 详情
     * @param data
     */
    onDetail(data) {
      this.openDialog(this.$actionType.detail, { ID: data.ID });
    }
    /**
     * 单个删除
     * @param params
     */
    onDelete(params) {
      this.onConfirm().then(() => {
        const parameters = [params.ID];
        this.batchDelete(parameters).then(res => {
          this["$notify"]({
            title: this.$t("form.successfullyDeleted"),
            type: "success"
          });
          this["onHoldSearch"]();
        });
      });
    }
    /**
     * 多个删除
     */
    onBatchDelete() {
      this.onConfirm().then(() => {
        const parameters = listToString(this["selectData"], "ID");
        console.log('this["selectData"]', this["selectData"], parameters);
        this.batchDelete(parameters)
          .then(res => {
            this["$notify"]({
              title: this.$t("form.successfullyDeleted"),
              type: "success"
            });
            this["onHoldSearch"]();
          })
          .catch(err => {
            this["$notify"]({
              title: this.$t("form.failedToDelete"),
              type: "error"
            });
          });
      });
    }
    /**
     * 导出全部
     */
    onExportAll() {
      const parameters = {
        ...this["searchFormClone"],
        Page: this["pageDate"].currentPage,
        Limit: this["pageDate"].pageSize
      };
      this.exportExcel(parameters).then(res => {
        createBlob(res);
        this["$notify"]({
          title: this.$t("form.ExportSucceeded"),
          type: "success"
        });
      });
    }
    /**
     * 导出单个
     */
    onExport() {
      const parameters = listToString(this["selectData"], "ID");
      this.exportExcelByIds(parameters).then(res => {
        createBlob(res);
        this["$notify"]({
          title: this.$t("form.ExportSucceeded"),
          type: "success"
        });
      });
    }
    /**
     * open importbox
     */
    onImported() {
      this.uploadIsShow = true;
    }
    /**
     * 下载
     */
    onDownload() {
      this.getExcelTemplate().then(res => createBlob(res));
    }
    /**
     * 导入
     * @param fileData
     */
    onImport(fileData) {
      const parameters = {
        UploadFileId: fileData.Id
      };
      this.imported(parameters).then(res => {
        this["$notify"]({
          title: this.$t("form.ImportSucceeded"),
          type: "success"
        });
        this["onHoldSearch"]();
      });
    }
    /**
     * 删除确认
     * @param title
     */
    onConfirm(title?: string) {
      if (!title) {
        title = this.$t("form.confirmDeletion");
      }
      return this["$confirm"](title, this.$t("form.prompt"), {
        confirmButtonText: this.$t("buttom.delete"),
        cancelButtonText: this.$t("buttom.cancel"),
        type: "warning"
      });
    }
    //自定义方法
    /**
     * 任务重发
     *  @param data
     */
    taskReload(data){
      this.onReloadConfirm().then(() => {
        const parameters = {
          taskId:data.ID,
          operationReason:this.inputValue
        }
        this.taskreload(parameters).then(res => {
          this["onHoldSearch"]();
          if(res.status==="0"){
            this["$notify"]({
              title: this.$t("form.resendSucceeded")+res.msg,
              type: "success"
            });
            this["onHoldSearch"]();
          }else{
            this["$notify"]({
              title: this.$t("form.errorOperation")+res.msg,
              type: "error"
            });
            this["onHoldSearch"]();
          }
        });
      });
    }
    onReloadConfirm(title?: string) {
      if (!title) {
        title = this.$t("form.confirmResend");
      }
      return this["$prompt"](title, this.$t("form.prompt"), 
      {
        confirmButtonText: this.$t("buttom.confirm"),
        cancelButtonText: this.$t("buttom.cancel"),
        type: "warning",
        input: {
          type: 'text',
          placeholder: this.$t('form.enterText')
        }
      }).then(result => {
        this.inputValue = result.value; // 保存输入框的值
        return result.action === 'confirm';
      });
    }
  /**
   * 任务完成
   * @param data
   */
  taskFinish(data){
    this.onFinishConfirm().then(() => {
      const parameters = {
        taskId:data.ID,
        operationReason:this.inputValue
      }
      this.taskfinish(parameters).then(res => {
        if(res.status==="0"){
          this["$notify"]({
            title: this.$t("form.finishSucceeded")+res.msg,
            type: "success"
          });
          this["onHoldSearch"]();
        }else{
          this["$notify"]({
            title: this.$t("form.errorOperation")+res.msg,
            type: "error"
          });
          this["onHoldSearch"]();
        }
      });
    });
  }
  onFinishConfirm(title?: string) {
    if (!title) {
      title = this.$t("form.confirmFinish");
    }
    return this["$prompt"](title, this.$t("form.prompt"), 
    {
      confirmButtonText: this.$t("buttom.confirm"),
      cancelButtonText: this.$t("buttom.cancel"),
      type: "warning",
      input: {
        type: 'text',
        placeholder: this.$t('form.enterText')
      }
    }).then(result => {
      this.inputValue = result.value; // 保存输入框的值
      return result.action === 'confirm';
    });
  }

  /**
   * 任务关闭
   * @param data
   */
  taskClose(data){
    this.onCloseConfirm().then(() => {
      const parameters = {
        taskId:data.ID,
        operationReason:this.inputValue
      }
      this.taskclose(parameters).then(res => {
        if(res.status==="0"){
          this["$notify"]({
            title: this.$t("form.closeSucceeded")+res.msg,
            type: "success"
          });
          this["onHoldSearch"]();
        }else{
          this["$notify"]({
            title: this.$t("form.errorOperation")+res.msg,
            type: "error"
          });
          this["onHoldSearch"]();
        }
      });
    });
  }
  onCloseConfirm(title?: string) {
    if (!title) {
      title = this.$t("form.comfirmClose");
    }
    return this["$prompt"](title, this.$t("form.prompt"), 
    {
      confirmButtonText: this.$t("buttom.confirm"),
      cancelButtonText: this.$t("buttom.cancel"),
      type: "warning",
      input: {
        type: 'text',
        placeholder: this.$t('form.enterText')
      }
    }).then(result => {
      this.inputValue = result.value; // 保存输入框的值
      return result.action === 'confirm';
    });
  }
  /**
   * 创建出库单
   * 
   */
  createInvoice() {
    this.openAddDialog(this.$actionType.add);
  }
  /**
   * 创建盘点单
   * 
   */
  createInventory(){
    this.openAddDialog(this.$actionType.add);
  }
  /**
   * 下发完成（指令下发处理）
   * @param data
   */
  dealcmdSend(data){
    this.onDealCmdSendConfirm().then(() => {
      console.log("下发完成"+JSON.stringify(data));
      console.log("下发完成2"+data.Device_No);
      console.log("下发完成3"+data.Pallet_Barcode);
      const parameters = {
        deviceNo:data.Device_No,
        palletBarcode:data.Pallet_Barcode
      }
      console.log("下发完成4"+JSON.stringify(parameters));
      this.dealcmdsend(parameters).then(res => {
        console.log("下发完成5"+JSON.stringify(res))
        if(res.status==="0"){
          this["$notify"]({
            title: this.$t("form.sendSucceeded")+res.msg,
            type: "success"
          });
          this["onHoldSearch"]();
        }else{
          this["$notify"]({
            title: this.$t("form.errorOperation")+res.msg,
            type: "error"
          });
          this["onHoldSearch"]();
        }
      });
    });
  }

  onDealCmdSendConfirm(title?: string) {
    if (!title) {
      title = this.$t("form.comfirmDealCmdSend");
    }
    return this["$confirm"](title, this.$t("form.prompt"), 
    {
      confirmButtonText: this.$t("buttom.confirm"),
      cancelButtonText: this.$t("buttom.cancel"),
      type: "warning"
    });
  }

  /**
   * 取货完成
   * @param data
   */
  dealsrmPick(data){
    this.onDealSrmPickConfirm().then(() => {
      const parameters = {
        deviceNo:data.Device_No,
        palletBarcode:data.Pallet_Barcode
      }
      this.dealsrmpick(parameters).then(res => {
        if(res.status==="0"){
          this["$notify"]({
            title: this.$t("form.pickSucceeded")+res.msg,
            type: "success"
          });
          this["onHoldSearch"]();
        }else{
          this["$notify"]({
            title: this.$t("form.errorOperation")+res.msg,
            type: "error"
          });
          this["onHoldSearch"]();
        }
      });
    });
  }
  onDealSrmPickConfirm(title?: string) {
    if (!title) {
      title = this.$t("form.comfirmDealSrmPick");
    }
    return this["$confirm"](title, this.$t("form.prompt"), 
    {
      confirmButtonText: this.$t("buttom.confirm"),
      cancelButtonText: this.$t("buttom.cancel"),
      type: "warning"
    });
  }

  /**
   * 放货完成
   * @param data
   */
  dealsrmPut(data){
    this.onDealSrmPutConfirm().then(() => {
      const parameters = {
        deviceNo:data.Device_No,
        palletBarcode:data.Pallet_Barcode
      }
      this.dealsrmput(parameters).then(res => {
        if(res.status==="0"){
          this["$notify"]({
            title: this.$t("form.putSucceeded")+res.msg,
            type: "success"
          });
          this["onHoldSearch"]();
        }else{
          this["$notify"]({
            title: this.$t("form.errorOperation")+res.msg,
            type: "error"
          });
          this["onHoldSearch"]();
        }
      });
    });
  }
  onDealSrmPutConfirm(title?: string) {
    if (!title) {
      title = this.$t("form.comfirmDealSrmPut");
    }
    return this["$confirm"](title, this.$t("form.prompt"), 
    {
      confirmButtonText: this.$t("buttom.confirm"),
      cancelButtonText: this.$t("buttom.cancel"),
      type: "warning"
    });
  }

  /**
   * 任务拒绝
   * @param data
   */
  dealsrmRefuse(data){
    this.onDealSrmRefuseConfirm().then(() => {
      const parameters = {
        deviceNo:data.Device_No,
        palletBarcode:data.Pallet_Barcode
      }
      this.dealsrmrefuse(parameters).then(res => {
        if(res.status==="0"){
          this["$notify"]({
            title: this.$t("form.refuseSucceeded")+res.msg,
            type: "success"
          });
          this["onHoldSearch"]();
        }else{
          this["$notify"]({
            title: this.$t("form.errorOperation")+res.msg,
            type: "error"
          });
          this["onHoldSearch"]();
        }
      });
    });
  }
  onDealSrmRefuseConfirm(title?: string) {
    if (!title) {
      title = this.$t("form.comfirmDealSrmrRefuse");
    }
    return this["$confirm"](title, this.$t("form.prompt"), 
    {
      confirmButtonText: this.$t("buttom.confirm"),
      cancelButtonText: this.$t("buttom.cancel"),
      type: "warning"
    });
  }
  /**
   * 出库申请
   * @param data
   */
  dealsrmTaskOut(data){
    this.onDealSrmTaskOutConfirm().then(() => {
      const parameters = {
        deviceNo:data.Device_No,
        palletBarcode:data.Pallet_Barcode
      }
      this.dealsrmtaskout(parameters).then(res => {
        if(res.status==="0"){
          this["$notify"]({
            title: this.$t("form.taskOutSucceeded")+res.msg,
            type: "success"
          });
          this["onHoldSearch"]();
        }else{
          this["$notify"]({
            title: this.$t("form.errorOperation")+res.msg,
            type: "error"
          });
          this["onHoldSearch"]();
        }
      });
    });
  }
  onDealSrmTaskOutConfirm(title?: string) {
    if (!title) {
      title = this.$t("form.comfirmDealSrmTaskOut");
    }
    return this["$confirm"](title, this.$t("form.prompt"), 
    {
      confirmButtonText: this.$t("buttom.confirm"),
      cancelButtonText: this.$t("buttom.cancel"),
      type: "warning"
    });
  }
  
  /**
   * 入库申请
   * 
   */
  dealsrmTaskIn(){
    this.onDealSrmTaskInConfirm().then(() => {
      const parameters = {
        deviceNo:this.inputValue,
        palletBarcode:this.inputValue2
      }
      this.dealsrmtaskin(parameters).then(res => {
        if(res.status==="0"){
          this["$notify"]({
            title: this.$t("form.taskInSucceeded")+res.msg,
            type: "success"
          });
          this["onHoldSearch"]();
        }else{
          this["$notify"]({
            title: this.$t("form.errorOperation")+res.msg,
            type: "error"
          });
          this["onHoldSearch"]();
        }
      });
    });
  }
  onDealSrmTaskInConfirm(title?: string) {
    if (!title) {
      title = this.$t("form.comfirmTaskIn");
    }
    return this["$prompt"](title, this.$t("form.prompt"), 
    {
      confirmButtonText: this.$t("buttom.confirm"),
      cancelButtonText: this.$t("buttom.cancel"),
      type: "warning",
      input: [{
        type: 'text',
        placeholder: this.$t('form.deviceNo')
      },{
        type: 'text',
        placeholder: this.$t('form.palletBarcode')
      }]
    }).then(result => {
      this.inputValue = result.value[0]; // 保存输入框的值
      this.inputValue2 = result.value[1];
      return result.action === 'confirm';
    });
  }
  /**
   * 打开设备
   */
  deviceOpen(){
    this.ondeviceOpenConfirm().then(() => {
      const parameters = this.listToLongArray(this["selectData"], "ID");
      console.log('this["selectData"]', this["selectData"], parameters);
      this.deviceopen(parameters).then(res => {
        if(res.status==="0"){
          this["$notify"]({
            title: this.$t("form.openoperationSucceeded")+res.msg,
            type: "success"
          });
          this["onHoldSearch"]();
        }else{
          this["$notify"]({
            title: this.$t("form.errorOperation")+res.msg,
            type: "error"
          });
          this["onHoldSearch"]();
        }
      });
    });
  }
  ondeviceOpenConfirm(title?: string) {
    if (!title) {
      title = this.$t("form.deviceOpenConfirm");
    }
    return this["$confirm"](title, this.$t("form.prompt"), 
    {
      confirmButtonText: this.$t("buttom.confirm"),
      cancelButtonText: this.$t("buttom.cancel"),
      type: "warning"
    });
  }
  
  listToLongArray(data, propertyName) {
    return data.map(item => item[propertyName]).filter(id =>!isNaN(id)).map(id => parseInt(id));
  };
  /**
   * 关闭设备
   */
  deviceClose(){
    this.ondeviceCloseConfirm().then(() => {
      const parameters = this.listToLongArray(this["selectData"], "ID");
      console.log('this["selectData"]', this["selectData"], parameters);
      this.deviceclose(parameters).then(res => {
        if(res.status==="0"){
          this["$notify"]({
            title: this.$t("form.closeoperationSucceeded")+res.msg,
            type: "success"
          });
          this["onHoldSearch"]();
        }else{
          this["$notify"]({
            title: this.$t("form.errorOperation")+res.msg,
            type: "error"
          });
          this["onHoldSearch"]();
        }
      });
    });
  }
  ondeviceCloseConfirm(title?: string) {
    if (!title) {
      title = this.$t("form.deviceCloseConfirm");
    }
    return this["$confirm"](title, this.$t("form.prompt"), 
    {
      confirmButtonText: this.$t("buttom.confirm"),
      cancelButtonText: this.$t("buttom.cancel"),
      type: "warning"
    });
  }
  

  /**
   * 步序初始
   */
  devicestepInit(){
    this.ondevicestepInitConfirm().then(() => {
      const parameters = this.listToLongArray(this["selectData"], "ID");
      console.log('this["selectData"]', this["selectData"], parameters);
      this.devicestepinit(parameters).then(res => {
        if(res.status==="0"){
          this["$notify"]({
            title: this.$t("form.stepinitSucceeded")+res.msg,
            type: "success"
          });
          this["onHoldSearch"]();
        }else{
          this["$notify"]({
            title: this.$t("form.errorOperation")+res.msg,
            type: "error"
          });
          this["onHoldSearch"]();
        }
      });
    });
  }
  ondevicestepInitConfirm(title?: string) {
    if (!title) {
      title = this.$t("form.stepinitConfirm");
    }
    return this["$confirm"](title, this.$t("form.prompt"), 
    {
      confirmButtonText: this.$t("buttom.confirm"),
      cancelButtonText: this.$t("buttom.cancel"),
      type: "warning"
    });
  }
  
  /**
   * 手动待命
   */
  deviceWait(){
    this.ondeviceWaitConfirm().then(() => {
      const parameters = this.listToLongArray(this["selectData"], "ID");
      console.log('this["selectData"]', this["selectData"], parameters);
      this.devicewait(parameters).then(res => {
        if(res.status==="0"){
          this["$notify"]({
            title: this.$t("form.deviceWaitSucceeded")+res.msg,
            type: "success"
          });
          this["onHoldSearch"]();
        }else{
          this["$notify"]({
            title: this.$t("form.errorOperation")+res.msg,
            type: "error"
          });
          this["onHoldSearch"]();
        }
      });
    });
  }

  ondeviceWaitConfirm(title?: string) {
    if (!title) {
      title = this.$t("form.deviceWaitConfirm");
    }
    return this["$confirm"](title, this.$t("form.prompt"), 
    {
      confirmButtonText: this.$t("buttom.confirm"),
      cancelButtonText: this.$t("buttom.cancel"),
      type: "warning"
    });
  }

  /**
   * 手动出库
   */
  manualOut(){
    this.onmanualOutConfirm().then(() => {
      const parameters = this.listToLongArray(this["selectData"], "ID");
      console.log('this["selectData"]', this["selectData"], parameters);
      this.manualout(parameters).then(res => {
        if(res.status==="0"){
          this["$notify"]({
            title: this.$t("form.manualOutSucceeded")+res.msg,
            type: "success"
          });
          this["onHoldSearch"]();
        }else{
          this["$notify"]({
            title: this.$t("form.errorOperation")+res.msg,
            type: "error"
          });
          this["onHoldSearch"]();
        }
      });
    });
  }

  onmanualOutConfirm(title?: string) {
    if (!title) {
      title = this.$t("form.manualOutConfirm");
    }
    return this["$confirm"](title, this.$t("form.prompt"), 
    {
      confirmButtonText: this.$t("buttom.confirm"),
      cancelButtonText: this.$t("buttom.cancel"),
      type: "warning"
    });
  }

  /**
   * 打开PLC
   */
  plcOpen(){
    this.onplcOpenConfirm().then(() => {
      const parameters = this.listToLongArray(this["selectData"], "ID");
      console.log('this["selectData"]', this["selectData"], parameters);
      this.plcopen(parameters).then(res => {
        if(res.status==="0"){
          this["$notify"]({
            title: this.$t("form.openoperationSucceeded")+res.msg,
            type: "success"
          });
          this["onHoldSearch"]();
        }else{
          this["$notify"]({
            title: this.$t("form.errorOperation")+res.msg,
            type: "error"
          });
          this["onHoldSearch"]();
        }
      });
    });
  }
  onplcOpenConfirm(title?: string) {
    if (!title) {
      title = this.$t("form.plcopenConfirm");
    }
    return this["$confirm"](title, this.$t("form.prompt"), 
    {
      confirmButtonText: this.$t("buttom.confirm"),
      cancelButtonText: this.$t("buttom.cancel"),
      type: "warning"
    });
  }
  

  /**
   * 关闭PLC
   */
  plcClose(){
    this.onplcCloseConfirm().then(() => {
      const parameters = this.listToLongArray(this["selectData"], "ID");
      console.log('this["selectData"]', this["selectData"], parameters);
      this.plcclose(parameters).then(res => {
        if(res.status==="0"){
          this["$notify"]({
            title: this.$t("form.closeoperationSucceeded")+res.msg,
            type: "success"
          });
          this["onHoldSearch"]();
        }else{
          this["$notify"]({
            title: this.$t("form.errorOperation")+res.msg,
            type: "error"
          });
          this["onHoldSearch"]();
        }
      });
    });
  }
  onplcCloseConfirm(title?: string) {
    if (!title) {
      title = this.$t("form.plccloseConfirm");
    }
    return this["$confirm"](title, this.$t("form.prompt"), 
    {
      confirmButtonText: this.$t("buttom.confirm"),
      cancelButtonText: this.$t("buttom.cancel"),
      type: "warning"
    });
  }
  /**
   * 手动回传
   */
  handleInter(){
    this.onhandleInterConfirm().then(() => {
      const parameters = this.listToLongArray(this["selectData"], "ID");
      console.log('this["selectData"]', this["selectData"], parameters);
      this.handleinter(parameters).then(res => {
        if(res.status==="0"){
          this["$notify"]({
            title: this.$t("form.handleInterSucceeded")+res.msg,
            type: "success"
          });
          this["onHoldSearch"]();
        }else{
          this["$notify"]({
            title: this.$t("form.errorOperation")+res.msg,
            type: "error"
          });
          this["onHoldSearch"]();
        }
      });
    });
  }
  onhandleInterConfirm(title?: string) {
    if (!title) {
      title = this.$t("form.handleInterConfirm");
    }
    return this["$confirm"](title, this.$t("form.prompt"), 
    {
      confirmButtonText: this.$t("buttom.confirm"),
      cancelButtonText: this.$t("buttom.cancel"),
      type: "warning"
    });
  }
  /**
   * 指令重发
   */
  cmdResend(data){
    this.oncmdResendConfirm().then(() => {
      const parameters = {
        deviceNo:data.Device_No,
        palletBarcode:data.Pallet_Barcode
      }
      console.log('this["selectData"]', this["selectData"], parameters);
      this.cmdresend(parameters).then(res => {
        if(res.status==="0"){
          this["$notify"]({
            title: this.$t("form.cmdResendSucceeded")+res.msg,
            type: "success"
          });
          this["onHoldSearch"]();
        }else{
          this["$notify"]({
            title: this.$t("form.errorOperation")+res.msg,
            type: "error"
          });
          this["onHoldSearch"]();
        }
      });
    });
  }
  oncmdResendConfirm(title?: string) {
    if (!title) {
      title = this.$t("form.cmdResendConfirm");
    }
    return this["$confirm"](title, this.$t("form.prompt"), 
    {
      confirmButtonText: this.$t("buttom.confirm"),
      cancelButtonText: this.$t("buttom.cancel"),
      type: "warning"
    });
  }

  /**
   * 启用货位
   */
  openBin(data){
    this.onopenBinConfirm().then(() => {
      const parameters = {
        binNo:data.binNo
      }
      console.log('this["selectData"]', this["selectData"], parameters);
      this.openbin(parameters).then(res => {
        if(res.status==="0"){
          this["$notify"]({
            title: this.$t("form.openBinSucceeded")+res.msg,
            type: "success"
          });
          this["onHoldSearch"]();
        }else{
          this["$notify"]({
            title: this.$t("form.errorOperation")+res.msg,
            type: "error"
          });
          this["onHoldSearch"]();
        }
      });
    });
  }
  onopenBinConfirm(title?: string) {
    if (!title) {
      title = this.$t("form.openBinConfirm");
    }
    return this["$confirm"](title, this.$t("form.prompt"), 
    {
      confirmButtonText: this.$t("buttom.confirm"),
      cancelButtonText: this.$t("buttom.cancel"),
      type: "warning"
    });
  }

  /**
   * 禁用货位
   */
  closeBin(data){
    this.oncloseBinConfirm().then(() => {
      const parameters = {
        binNo:data.binNo
      }
      console.log('this["selectData"]', this["selectData"], parameters);
      this.closebin(parameters).then(res => {
        if(res.status==="0"){
          this["$notify"]({
            title: this.$t("form.closeBinSucceeded")+res.msg,
            type: "success"
          });
          this["onHoldSearch"]();
        }else{
          this["$notify"]({
            title: this.$t("form.errorOperation")+res.msg,
            type: "error"
          });
          this["onHoldSearch"]();
        }
      });
    });
  }
  oncloseBinConfirm(title?: string) {
    if (!title) {
      title = this.$t("form.closeBinConfirm");
    }
    return this["$confirm"](title, this.$t("form.prompt"), 
    {
      confirmButtonText: this.$t("buttom.confirm"),
      cancelButtonText: this.$t("buttom.cancel"),
      type: "warning"
    });
  }
  }
  return actionMixins;
}
export default mixinFunc;
