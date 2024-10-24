import i18n from "@/lang";

export const ASSEMBLIES: Array<string> = [
//   "add",
//   "edit",
  "delete",
  "export",
  "imported",
  "dealcmdsend",
  "dealsrmpick",
  "dealsrmput",
  "dealsrmrefuse",
//   "dealsrmtaskin",
  "dealsrmtaskout",
  "cmdresend"
];

export const TABLE_HEADER: Array<object> = [


    {
        key: "SubTask_No",
        label: i18n.t("srmcmd.SubTask_No"),
        width:180
    },
    {
        key: "Task_No",
        label: i18n.t("srmcmd.Task_No"),
        width:180
    },
    {
        key: "Serial_No",
        label: i18n.t("srmcmd.Serial_No")
    },
    {
        key: "Device_No",
        label: i18n.t("srmcmd.Device_No")
    },
    // {
    //     key: "Fork_No",
    //     label: i18n.t("srmcmd.Fork_No")
    // },
    // {
    //     key: "Station_Type",
    //     label: i18n.t("srmcmd.Station_Type")
    // },
    {
        key: "Check_Point",
        label: i18n.t("srmcmd.Check_Point")
    },
    // {
    //     key: "Task_Cmd",
    //     label: i18n.t("srmcmd.Task_Cmd")
    // },
    {
        key: "Task_Cmd_Desc",
        label: i18n.t("srmcmd.Task_Cmd")
    },
    {
        key: "Task_Type",
        label: i18n.t("srmcmd.Task_Type")
    },
    {
        key: "Pallet_Barcode",
        label: i18n.t("srmcmd.Pallet_Barcode"),
        width:120
    },
    {
        key: "WaferID",
        label: i18n.t("srmcmd.WaferID"),
        width: 200
    },
    {
        key: "Exec_Status_Desc",
        label: i18n.t("srmcmd.Exec_Status"),
        width:120
    }, 
    {
        key: "Remark_Desc",
        label: i18n.t("srmcmd.Remark_Desc"),
        width: 500
    },
    {
        key: "From_Station",
        label: i18n.t("srmcmd.From_Station")
    },
    {
        key: "From_ForkDirection",
        label: i18n.t("srmcmd.From_ForkDirection")
    },
    {
        key: "From_Column",
        label: i18n.t("srmcmd.From_Column")
    },
    {
        key: "From_Layer",
        label: i18n.t("srmcmd.From_Layer")
    },
    {
        key: "From_Deep",
        label: i18n.t("srmcmd.From_Deep")
    },
    {
        key: "To_Station",
        label: i18n.t("srmcmd.To_Station")
    },
    {
        key: "To_ForkDirection",
        label: i18n.t("srmcmd.To_ForkDirection")
    },
    {
        key: "To_Column",
        label: i18n.t("srmcmd.To_Column")
    },
    {
        key: "To_Layer",
        label: i18n.t("srmcmd.To_Layer")
    },
    {
        key: "To_Deep",
        label: i18n.t("srmcmd.To_Deep")
    },
    {
        key: "Recive_Date",
        label: i18n.t("srmcmd.Recive_Date"),
        width: 150
    },
    {
        key: "Begin_Date",
        label: i18n.t("srmcmd.Begin_Date"),
        width: 150
    },
    {
        key: "Pick_Date",
        label: i18n.t("srmcmd.Pick_Date"),
        width: 150
    },
    {
        key: "Put_Date",
        label: i18n.t("srmcmd.Put_Date"),
        width: 150
    },
    {
        key: "Finish_Date",
        label: i18n.t("srmcmd.Finish_Date"),
        width: 150
    },{
        key: "CreateBy",
        label: i18n.t("CommonString.CreateBy"),
        width: 100
    },
    {
        key: "CreateTime",
        label: i18n.t("CommonString.CreateTime"),
        width: 150
    },{
        key: "UpdateBy",
        label: i18n.t("CommonString.UpdateBy"),
        width: 100
    },
    {
        key: "UpdateTime",
        label: i18n.t("CommonString.UpdateTime"),
        width: 150
    },
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


