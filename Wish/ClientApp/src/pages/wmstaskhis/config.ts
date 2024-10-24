import i18n from "@/lang";

export const ASSEMBLIES: Array<string> = [
  "add",
  "edit",
  "delete",
  "export",
  "imported"
];

export const TABLE_HEADER: Array<object> = [


    // {
    //     key: "whouseNo",
    //     label: i18n.t("wmstaskhis.whouseNo")
    // },
    {
        key: "wmsTaskNo",
        label: i18n.t("wmstaskhis.wmsTaskNo"),
        width:180
    },
    {
        key: "orderNo",
        label: i18n.t("wmstaskhis.orderNo"),
        width:180
    },
    // {
    //     key: "wmsTaskType",
    //     label: i18n.t("wmstaskhis.wmsTaskType"),
    //     width:180
    // },
    // {
    //     key: "taskStatus",
    //     label: i18n.t("wmstaskhis.taskStatus")
    // },
    {
        key: "taskStatusDesc",
        label: i18n.t("wmstaskhis.taskStatus"),
        width:120
    },
    {
        key: "taskTypeNo",
        label: i18n.t("wmstaskhis.taskTypeNo")
    },
    {
        key: "palletBarcode",
        label: i18n.t("wmstaskhis.palletBarcode"),
        width:120
    },
    {
        key: "frLocationNo",
        label: i18n.t("wmstaskhis.frLocationNo"),
        width:120
    },
    // {
    //     key: "frLocationType",
    //     label: i18n.t("wmstaskhis.frLocationType")
    // },
    {
        key: "toLocationNo",
        label: i18n.t("wmstaskhis.toLocationNo"),
        width:120
    },
    // {
    //     key: "toLocationType",
    //     label: i18n.t("wmstaskhis.toLocationType")
    // },
    // {
    //     key: "loadedType",
    //     label: i18n.t("wmstaskhis.loadedType")
    // },
    // {
    //     key: "matHeight",
    //     label: i18n.t("wmstaskhis.matHeight")
    // },
    // {
    //     key: "matLength",
    //     label: i18n.t("wmstaskhis.matLength")
    // },
    // {
    //     key: "matQty",
    //     label: i18n.t("wmstaskhis.matQty")
    // },
    // {
    //     key: "matWeight",
    //     label: i18n.t("wmstaskhis.matWeight")
    // },
    // {
    //     key: "matWidth",
    //     label: i18n.t("wmstaskhis.matWidth")
    // },
    
    
    // {
    //     key: "proprietorCode",
    //     label: i18n.t("wmstaskhis.proprietorCode")
    // },
    // {
    //     key: "regionNo",
    //     label: i18n.t("wmstaskhis.regionNo")
    // },
    {
        key: "roadwayNo",
        label: i18n.t("wmstaskhis.roadwayNo")
    },
    {
        key: "stockCode",
        label: i18n.t("wmstaskhis.stockCode"),
        width:180
    },
    {
        key: "taskDesc",
        label: i18n.t("wmstaskhis.taskDesc"),
        width:180
    },
    // {
    //     key: "feedbackDesc",
    //     label: i18n.t("wmstaskhis.feedbackDesc")
    // },
    // {
    //     key: "feedbackStatus",
    //     label: i18n.t("wmstaskhis.feedbackStatus")
    // },
    // {
    //     key: "taskPriority",
    //     label: i18n.t("wmstaskhis.taskPriority")
    // },
    {
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


