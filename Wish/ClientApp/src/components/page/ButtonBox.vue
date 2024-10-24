<template>
    <div class="but-box">
        <slot />
        <el-button v-assembly:[assembly]="butTypes.add" v-visible="actionList[actionKeys.add]" type="primary" icon="el-icon-plus" @click="onAdd">
            {{ $t("buttom.add") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.createinvoice" v-visible="actionList[actionKeys.createInvoice]" type="primary" icon="el-icon-plus" @click="createInvoice">
            {{ $t("buttom.createInvoice") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.createinventory" v-visible="actionList[actionKeys.createInventory]" type="primary" icon="el-icon-plus" @click="createInventory">
            {{ $t("buttom.createInventory") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.taskreload" v-visible="actionList[actionKeys.taskreload]" type="primary" :disabled="isDisabledReload" icon="el-icon-refresh" @click="taskReload">
            {{ $t("buttom.taskreload") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.taskfinish" v-visible="actionList[actionKeys.taskfinish]" type="primary" :disabled="isDisabledReload" icon="el-icon-check" @click="taskFinish">
            {{ $t("buttom.taskfinish") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.taskclose" v-visible="actionList[actionKeys.taskclose]" type="primary" :disabled="isDisabledReload" icon="el-icon-close" @click="taskClose">
            {{ $t("buttom.taskclose") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.dealcmdsend" v-visible="actionList[actionKeys.dealcmdsend]" type="primary" :disabled="isSrmCmd" icon="el-icon-arrow-left" @click="dealcmdSend">
            {{ $t("buttom.dealcmdsend") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.cmdresend" v-visible="actionList[actionKeys.cmdresend]" type="primary" :disabled="isSrmCmdResend" icon="el-icon-arrow-left" @click="cmdResend">
            {{ $t("buttom.cmdresend") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.openbin" v-visible="actionList[actionKeys.openbin]" type="primary" :disabled="isOpenBin" icon="el-icon-arrow-left" @click="openBin">
            {{ $t("buttom.openBin") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.closebin" v-visible="actionList[actionKeys.closebin]" type="primary" :disabled="isCloseBin" icon="el-icon-arrow-left" @click="closeBin">
            {{ $t("buttom.closeBin") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.dealsrmpick" v-visible="actionList[actionKeys.dealsrmpick]" type="primary" :disabled="isSrmCmd" icon="el-icon-arrow-up" @click="dealsrmPick">
            {{ $t("buttom.dealsrmpick") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.dealsrmput" v-visible="actionList[actionKeys.dealsrmput]" type="primary" :disabled="isSrmCmd" icon="el-icon-arrow-down" @click="dealsrmPut">
            {{ $t("buttom.dealsrmput") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.dealsrmrefuse" v-visible="actionList[actionKeys.dealsrmrefuse]" type="primary" :disabled="isSrmTaskOut" icon="el-icon-info" @click="dealsrmRefuse">
            {{ $t("buttom.dealsrmrefuse") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.dealsrmtaskin" v-visible="actionList[actionKeys.dealsrmtaskin]" type="primary"  icon="el-icon-caret-left" @click="dealsrmTaskIn">
            {{ $t("buttom.dealsrmtaskin") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.dealsrmtaskout" v-visible="actionList[actionKeys.dealsrmtaskout]" type="primary" :disabled="isSrmTaskOut" icon="el-icon-caret-right" @click="dealsrmTaskOut">
            {{ $t("buttom.dealsrmtaskout") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.deviceopen" v-visible="actionList[actionKeys.deviceOpen]" type="primary" :disabled="isDisabledOpenOperation" icon="el-icon-caret-right" @click="deviceOpen">
            {{ $t("buttom.deviceopen") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.deviceclose" v-visible="actionList[actionKeys.deviceClose]" type="primary" :disabled="isDisabledCloseOperation" icon="el-icon-caret-right" @click="deviceClose">
            {{ $t("buttom.deviceclose") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.devicestepinit" v-visible="actionList[actionKeys.devicestepInit]" type="primary" :disabled="isDisabledOperation" icon="el-icon-caret-right" @click="devicestepInit">
            {{ $t("buttom.devicestepinit") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.devicewait" v-visible="actionList[actionKeys.deviceWait]" type="primary" :disabled="isDisabledOperation" icon="el-icon-caret-right" @click="deviceWait">
            {{ $t("buttom.devicewait") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.manualout" v-visible="actionList[actionKeys.manualOut]" type="primary" :disabled="isDisabledOperation" icon="el-icon-caret-right" @click="manualOut">
            {{ $t("buttom.manualout") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.plcopen" v-visible="actionList[actionKeys.plcOpen]" type="primary" :disabled="isDisabledOpenOperation" icon="el-icon-caret-right" @click="plcOpen">
            {{ $t("buttom.plcopen") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.plcclose" v-visible="actionList[actionKeys.plcClose]" type="primary" :disabled="isDisabledCloseOperation" icon="el-icon-caret-right" @click="plcClose">
            {{ $t("buttom.plcclose") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.handleinter" v-visible="actionList[actionKeys.handleInter]" type="primary" :disabled="isDisabledInterHandle" icon="el-icon-caret-right" @click="handleInter">
            {{ $t("buttom.handleinter") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.edit" v-visible="actionList[actionKeys.edit]" type="primary" :disabled="isDisabledEdit" icon="el-icon-edit" @click="onEdit">
            {{ $t("buttom.edit") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.deleted" v-visible="actionList[actionKeys.batchDelete]" type="primary" :disabled="isDisabledEelete" icon="el-icon-delete" @click="onBatchDelete">
            {{ $t("buttom.delete") }}
        </el-button>
        <el-button v-assembly:[assembly]="butTypes.imported" v-visible="actionList[actionKeys.import]" type="primary" icon="el-icon-upload" @click="onImported">
            {{ $t("buttom.import") }}
        </el-button>
        <el-dropdown v-visible="[actionList[actionKeys.exportExcel],actionList[actionKeys.exportExcelByIds]]" class="dropdown-box" @command="onCommand">
            <el-button v-assembly:[assembly]="butTypes.export" type="primary">
                <i class="el-icon-download" /> <span>{{ $t("buttom.export") }}</span> <i class="el-icon-arrow-down el-icon--right" />
            </el-button>
            <el-dropdown-menu slot="dropdown">
                <el-dropdown-item v-visible="actionList[actionKeys.exportExcel]" command="onExportAll">
                    {{ $t("buttom.exportAll") }}
                </el-dropdown-item>
                <el-dropdown-item v-visible="actionList[actionKeys.exportExcelByIds]" command="onExport">
                    {{ $t("buttom.exportSelect") }}
                </el-dropdown-item>
            </el-dropdown-menu>
        </el-dropdown>
    </div>
</template>

<script lang='ts'>
import ExportExcel from "@/components/page/export/export-excel.vue";
import { butType } from "@/config/enum";
import { IEventFn } from "@/vue-custom/mixin/action-mixin";
import { Component, Prop, Vue } from "vue-property-decorator";

@Component({
    components: {
        ExportExcel
    },
    directives: {
        assembly: {
            inserted: (el, binding) => {
                const { arg, value } = binding;
                if (!(arg && arg.includes(value))) {
                    el.style.display = "none";
                }
            }
        }
    }
})
export default class ButBox extends Vue {
    // 增删改查 操作权限集合
    @Prop({ type: Array, default: () => Object.keys(butType) })
    assembly;
    // 父级选中的对象
    @Prop({ type: Array, default: [] })
    selectedData;
    // 导出参数 url地址
    @Prop({
        type: Object,
        default: () => {
            return { params: {}, url: "" };
        }
    })
    exportOption;
    // 权限列表
    @Prop({
        type: Object,
        default: () => {
            return {};
        }
    })
    actionList;
    // 定义 权限列表key
    @Prop({
        type: Object,
        default: () => {
            return {
                add: "add",
                edit: "edit",
                batchDelete: "batchDelete",
                import: "imported",
                exportExcel: "exportExcel",
                exportExcelByIds: "exportExcelByIds",
                //自定义按钮
                taskreload:"taskreload",
                taskfinish:"taskfinish",
                taskclose:"taskclose",
                createinvoice:"createinvoice",
                dealcmdsend:"dealcmdsend",
                dealsrmpick:"dealsrmpick",
                dealsrmput:"dealsrmput",
                dealsrmrefuse:"dealsrmrefuse",
                dealsrmtaskin:"dealsrmtaskin",
                dealsrmtaskout:"dealsrmtaskout",
                deviceopen:"deviceopen",
                deviceclose:"deviceclose",
                devicestepinit:"devicestepinit",
                plcopen:"plcopen",
                plcclose:"plcclose",
                handleinter:"handleinter",
                cmdresend:"cmdresend",
                devicewait:"devicewait",
                manualout:"manualout",
                openbin:"openbin",
                closebin:"closebin"
            };
        }
    })
    actionKeys;

    // 方法事件
    @Prop({
        type: Object,
        default: (): IEventFn => {
            return {
                onAdd: () => {},
                onEdit: () => {},
                onDelete: () => {},
                onBatchDelete: () => {},
                onImported: () => {},
                onExportAll: () => {},
                onExport: () => {},
                //自定义按钮
                taskReload:()=>{},
                taskFinish:()=>{},
                taskClose:()=>{},
                createInvoice:()=>{},
                dealcmdSend:()=>{},
                dealsrmPick:()=>{},
                dealsrmPut:()=>{},
                dealsrmRefuse:()=>{},
                dealsrmTaskIn:()=>{},
                dealsrmTaskOut:()=>{},
                deviceOpen:()=>{},
                deviceClose:()=>{},
                devicestepInit:()=>{},
                plcOpen:()=>{},
                plcClose:()=>{},
                handleInter:()=>{},
                cmdResend:()=>{},
                deviceWait:()=>{},
                manualOut:()=>{},
                openBin:()=>{},
                closeBin:()=>{},
            };
        }
    })
    events;

    butTypes = butType;
    /**
     * 判断选中数据数量
     */
    get isDisabledEdit() {
        if (this.selectedData.length === 1) {
            return false;
        }
        return true;
    }
    // 判读是否有选中值
    get isDisabledEelete() {
        if (this.selectedData.length > 0) {
            return false;
        }
        return true;
    }
    get isDisabledOperation() {
        if (this.selectedData.length > 0) {
            return false;
        }
        return true;
    }
    get isDisabledOpenOperation() {
        if (this.selectedData.length > 0) {
            for (const dataItem of this.selectedData) {
                // 在这里可以对每个选中的数据项进行操作
                console.log(dataItem);
                if(dataItem.IsEnabled==="false"){
                    return false;
                }
            }
        }
        return true;
    }
    get isDisabledInterHandle() {
        if (this.selectedData.length > 0) {
            for (const dataItem of this.selectedData) {
                // 在这里可以对每个选中的数据项进行操作
                console.log("手动回传"+JSON.stringify(dataItem));
                if(dataItem.returnFlag==="1"){
                    return false;
                }
            }
        }
        return true;
    }
    get isDisabledCloseOperation() {
        if (this.selectedData.length > 0) {
            for (const dataItem of this.selectedData) {
                // 在这里可以对每个选中的数据项进行操作
                console.log(dataItem);
                if(dataItem.IsEnabled==="true"){
                    return false;
                }
            }
        }
        return true;
    }
    onAdd() {
        this.events.onAdd ? this.events.onAdd() : this.$emit("onAdd");
    }
    onEdit() {
        if (!this.isDisabledEdit) {
            this.events.onEdit
                ? this.events.onEdit(this.selectedData[0])
                : this.$emit("onEdit", this.selectedData[0]);
        }
    }
    onBatchDelete() {
        if (!this.isDisabledEelete) {
            this.events.onBatchDelete
                ? this.events.onBatchDelete(this.selectedData)
                : this.$emit("onBatchDelete", this.selectedData);
        }
    }

    onDelete() {
        if (!this.isDisabledEelete) {
            this.events.onDelete
                ? this.events.onDelete(this.selectedData[0])
                : this.$emit("onDelete", this.selectedData[0]);
        }
    }
    onImported() {
        this.events.onImported
            ? this.events.onImported()
            : this.$emit("onImported");
    }
    onExportAll() {
        this.events.onExportAll
            ? this.events.onExportAll()
            : this.$emit("onExportAll");
    }
    onExport() {
        this.events.onExport ? this.events.onExport() : this.$emit("onExport");
    }
    onCommand(command) {
        if (command === "onExportAll") {
            this.onExportAll();
        } else if (command === "onExport") {
            this.onExport();
        }
    }
    //自定义按钮
    //任务重发
    taskReload(){
        if (!this.isDisabledReload) {
            this.events.taskReload
                ? this.events.taskReload(this.selectedData[0])
                : this.$emit("taskReload", this.selectedData[0]);
        }
    }
    //任务完成
    taskFinish(){
        if (!this.isDisabledReload) {
            this.events.taskFinish
                ? this.events.taskFinish(this.selectedData[0])
                : this.$emit("taskFinish", this.selectedData[0]);
        }
    }
    //任务关闭
    taskClose(){
        if (!this.isDisabledReload) {
            this.events.taskClose
                ? this.events.taskClose(this.selectedData[0])
                : this.$emit("taskClose", this.selectedData[0]);
        }
    }
    //下发完成
    dealcmdSend(){
        if (!this.isSrmCmd) {
            this.events.dealcmdSend
                ? this.events.dealcmdSend(this.selectedData[0])
                : this.$emit("dealcmdSend", this.selectedData[0]);
        }
    }
    //指令重发
    cmdResend(){
        if (!this.isSrmCmd) {
            this.events.cmdResend
                ? this.events.cmdResend(this.selectedData[0])
                : this.$emit("cmdResend", this.selectedData[0]);
        }
    }
    //启用货位
    openBin(){
        if (!this.isOpenBin) {
            this.events.openBin
                ? this.events.openBin(this.selectedData[0])
                : this.$emit("openBin", this.selectedData[0]);
        }
    }
    //禁用货位
    closeBin(){
        if (!this.isCloseBin) {
            this.events.closeBin
                ? this.events.closeBin(this.selectedData[0])
                : this.$emit("closeBin", this.selectedData[0]);
        }
    }
    //取货完成
    dealsrmPick(){
        if (!this.isSrmCmdResend) {
            this.events.dealsrmPick
                ? this.events.dealsrmPick(this.selectedData[0])
                : this.$emit("dealsrmPick", this.selectedData[0]);
        }
    }
    //放货完成
    dealsrmPut(){
        if (!this.isSrmCmd) {
            this.events.dealsrmPut
                ? this.events.dealsrmPut(this.selectedData[0])
                : this.$emit("dealsrmPut", this.selectedData[0]);
        }
    }
    //任务拒绝
    dealsrmRefuse(){
        if (!this.isSrmCmd) {
            this.events.dealsrmRefuse
                ? this.events.dealsrmRefuse(this.selectedData[0])
                : this.$emit("dealsrmRefuse", this.selectedData[0]);
        }
    }
    //入库请求
    dealsrmTaskIn(){
        // if (!this.isDisabledReload) {
            this.events.dealsrmTaskIn
                // ? this.events.dealsrmTaskIn(this.selectedData[0])
                // : this.$emit("dealsrmTaskIn", this.selectedData[0]);
        // }
    }
    //出库请求
    dealsrmTaskOut(){
        if (!this.isSrmTaskOut) {
            this.events.dealsrmTaskOut
                ? this.events.dealsrmTaskOut(this.selectedData[0])
                : this.$emit("dealsrmTaskOut", this.selectedData[0]);
        }
    }
    // 判读是否有选中值
    get isDisabledReload() {
        if (this.selectedData.length > 0) {
            return false;
        }
        return true;
    }
    /**
     * 打开设备
     */
    deviceOpen() {
        if (!this.isDisabledOpenOperation) {
            this.events.deviceOpen
                ? this.events.deviceOpen(this.selectedData)
                : this.$emit("deviceOpen", this.selectedData);
        }
    }
    /**
     * 关闭设备
     */
     deviceClose() {
        if (!this.isDisabledCloseOperation) {
            this.events.deviceClose
                ? this.events.deviceClose(this.selectedData)
                : this.$emit("deviceClose", this.selectedData);
        }
    }
    /**
     * 步序初始化
     */
     devicestepInit() {
        if (!this.isDisabledOperation) {
            this.events.devicestepInit
                ? this.events.devicestepInit(this.selectedData)
                : this.$emit("devicestepInit", this.selectedData);
        }
    }
    /**
     * 手动待命
     */
     deviceWait() {
        if (!this.isDisabledOperation) {
            this.events.deviceWait
                ? this.events.deviceWait(this.selectedData)
                : this.$emit("deviceWait", this.selectedData);
        }
    }
    /**
     * 手动出库
     * 
     */
     manualOut() {
        if (!this.isDisabledOperation) {
            this.events.manualOut
                ? this.events.manualOut(this.selectedData)
                : this.$emit("manualOut", this.selectedData);
        }
    }
    /**
     * 打开PLC
     */
     plcOpen() {
        if (!this.isDisabledOpenOperation) {
            this.events.plcOpen
                ? this.events.plcOpen(this.selectedData)
                : this.$emit("plcOpen", this.selectedData);
        }
    }
     
    /**
     * 关闭PLC
     */
     plcClose() {
        if (!this.isDisabledCloseOperation) {
            this.events.plcClose
                ? this.events.plcClose(this.selectedData)
                : this.$emit("plcClose", this.selectedData);
        }
    }

    /**
     * 手动回传
     */
     handleInter() {
        if (!this.isDisabledInterHandle) {
            this.events.handleInter
                ? this.events.handleInter(this.selectedData)
                : this.$emit("handleInter", this.selectedData);
        }
    }
    //判断出库申请的选择条件
    get isSrmTaskOut(){
        if(this.selectedData.length  === 1){
            for (const dataItem of this.selectedData) {
                // 在这里可以对每个选中的数据项进行操作
                console.log(dataItem);
                if('Task_Type' in dataItem){
                    if(dataItem.Task_Type.includes('OUT')){
                    return false;
                }
                }
                
            }
        }
        return true;
    }
    get isSrmCmd(){
        if (this.selectedData.length === 1) {
            return false;
        }
        return true;
    }
    get isOpenBin(){
        if (this.selectedData.length === 1) {
            for (const dataItem of this.selectedData) {
                // 在这里可以对每个选中的数据项进行操作
                console.log("开启货位"+JSON.stringify(dataItem));
                if('usedFlag' in dataItem){
                    if(dataItem.usedFlag === "0" ){
                    return false;
                }
                }
            }
        }
        return true;
    }
    get isCloseBin(){
        if (this.selectedData.length === 1) {
            for (const dataItem of this.selectedData) {
                // 在这里可以对每个选中的数据项进行操作
                console.log("开启货位"+JSON.stringify(dataItem));
                if('usedFlag' in dataItem){
                    if(dataItem.usedFlag === "1" ){
                    return false;
                }
                }
            }
        }
        return true;
    }
    get isSrmCmdResend(){
        if (this.selectedData.length === 1) {
            for (const dataItem of this.selectedData) {
                // 在这里可以对每个选中的数据项进行操作
                console.log(dataItem);
                if(dataItem.Exec_Status==="10" || dataItem.Exec_Status==="30"){
                    return false;
                }
            }
        }
        return true;
    }
    //创建发货单
    createInvoice(){
        this.events.createInvoice ? this.events.createInvoice() : this.$emit("createInvoice");
    }
    //创建盘点单
    createInventory(){
        this.events.createInvoice ? this.events.createInventory() : this.$emit("createInventory");
    }
}
</script>
<style lang="less" rel="stylesheet/less">
@import "../../assets/css/mixin.less";
.but-box {
    .flexbox(row, flex-end);
    margin: 15px 0;
    .dropdown-box {
        margin-left: 10px;
    }
}
</style>
