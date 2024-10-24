import i18n from "@/lang";

export const ASSEMBLIES: Array<string> = [
    "add",
    "edit",
    "delete",
    "export",
    "imported",
    "taskreload",
    "taskfinish",
    "taskclose"
];

export const TABLE_HEADER: Array<object> = [

    // {
    //     key: "whouseNo",
    //     label: i18n.t("wmstask.whouseNo")
    // },
    {
        key: "wmsTaskNo",
        label: i18n.t("wmstask.wmsTaskNo"),
        width:180
    },
    {
        key: "orderNo",
        label: i18n.t("wmstask.orderNo"),
        width:180
    },
    // {
    //     key: "wmsTaskType",
    //     label: i18n.t("wmstask.wmsTaskType")
    // },
    // {
    //     key: "taskStatus",
    //     label: i18n.t("wmstask.taskStatus"),
    //     width:120
    // },
    {
        key: "taskStatusDesc",
        label: i18n.t("wmstask.taskStatus"),
        width:120
    },
    {
        key: "taskTypeNo",
        label: i18n.t("wmstask.taskTypeNo")
    },
    {
        key: "palletBarcode",
        label: i18n.t("wmstask.palletBarcode"),
        width:120
    },
    {
        key: "frLocationNo",
        label: i18n.t("wmstask.frLocationNo"),
        width:120
    },
    // {
    //     key: "frLocationType",
    //     label: i18n.t("wmstask.frLocationType")
    // },
    {
        key: "toLocationNo",
        label: i18n.t("wmstask.toLocationNo"),
        width:120
    },
    // {
    //     key: "toLocationType",
    //     label: i18n.t("wmstask.toLocationType")
    // },
    // {
    //     key: "loadedType",
    //     label: i18n.t("wmstask.loadedType")
    // },
    // {
    //     key: "matHeight",
    //     label: i18n.t("wmstask.matHeight")
    // },
    // {
    //     key: "matLength",
    //     label: i18n.t("wmstask.matLength")
    // },
    // {
    //     key: "matQty",
    //     label: i18n.t("wmstask.matQty")
    // },
    // {
    //     key: "matWeight",
    //     label: i18n.t("wmstask.matWeight")
    // },
    // {
    //     key: "matWidth",
    //     label: i18n.t("wmstask.matWidth")
    // },
    
    
    // {
    //     key: "proprietorCode",
    //     label: i18n.t("wmstask.proprietorCode")
    // },
    // {
    //     key: "regionNo",
    //     label: i18n.t("wmstask.regionNo")
    // },
    {
        key: "roadwayNo",
        label: i18n.t("wmstask.roadwayNo")
    },
    {
        key: "stockCode",
        label: i18n.t("wmstask.stockCode"),
        width:180
    },
    {
        key: "taskDesc",
        label: i18n.t("wmstask.taskDesc"),
        width:180
    },
    // {
    //     key: "feedbackDesc",
    //     label: i18n.t("wmstask.feedbackDesc"),
    //     width:180
    // },
    // {
    //     key: "feedbackStatus",
    //     label: i18n.t("wmstask.feedbackStatus")
    // },
    // {
    //     key: "taskPriority",
    //     label: i18n.t("wmstask.taskPriority")
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


