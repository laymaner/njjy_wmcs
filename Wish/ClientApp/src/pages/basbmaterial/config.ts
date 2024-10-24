import i18n from "@/lang";

export const ASSEMBLIES: Array<string> = [
  "add",
  "edit",
  "delete",
  "export",
  "imported"
];

export const TABLE_HEADER: Array<object> = [


    {
        key: "BarcodeFlag",
        label: i18n.t("basbmaterial.BarcodeFlag")
    },
    {
        key: "Brand",
        label: i18n.t("basbmaterial.Brand")
    },
    {
        key: "BuyerCode",
        label: i18n.t("basbmaterial.BuyerCode")
    },
    {
        key: "BuyerName",
        label: i18n.t("basbmaterial.BuyerName")
    },
    {
        key: "EmaterialVtime",
        label: i18n.t("basbmaterial.EmaterialVtime")
    },
    {
        key: "ErpBinNo",
        label: i18n.t("basbmaterial.ErpBinNo")
    },
    {
        key: "Material",
        label: i18n.t("basbmaterial.Material")
    },
    {
        key: "MaterialCategoryCode",
        label: i18n.t("basbmaterial.MaterialCategoryCode")
    },
    {
        key: "MaterialName",
        label: i18n.t("basbmaterial.MaterialName")
    },
    {
        key: "MaterialNameAlias",
        label: i18n.t("basbmaterial.MaterialNameAlias")
    },
    {
        key: "MaterialNameEn",
        label: i18n.t("basbmaterial.MaterialNameEn")
    },
    {
        key: "MaterialCode",
        label: i18n.t("basbmaterial.MaterialCode")
    },
    {
        key: "MaterialSpec",
        label: i18n.t("basbmaterial.MaterialSpec")
    },
    {
        key: "MaterialTypeDesc",
        label: i18n.t("basbmaterial.MaterialTypeDesc")
    },
    {
        key: "MaterialTypeCode",
        label: i18n.t("basbmaterial.MaterialTypeCode")
    },
    {
        key: "MaxDelayTimes",
        label: i18n.t("basbmaterial.MaxDelayTimes")
    },
    {
        key: "MaxDriedTimes",
        label: i18n.t("basbmaterial.MaxDriedTimes")
    },
    {
        key: "MinPkgQty",
        label: i18n.t("basbmaterial.MinPkgQty")
    },
    {
        key: "ProjectDrawingNo",
        label: i18n.t("basbmaterial.ProjectDrawingNo")
    },
    {
        key: "ProprietorCode",
        label: i18n.t("basbmaterial.ProprietorCode")
    },
    {
        key: "QcFlag",
        label: i18n.t("basbmaterial.QcFlag")
    },
    {
        key: "SharedFalg",
        label: i18n.t("basbmaterial.SharedFalg")
    },
    {
        key: "SkuRuleNo",
        label: i18n.t("basbmaterial.SkuRuleNo")
    },
    {
        key: "SluggishTime",
        label: i18n.t("basbmaterial.SluggishTime")
    },
    {
        key: "TechParm",
        label: i18n.t("basbmaterial.TechParm")
    },
    {
        key: "UnitCode",
        label: i18n.t("basbmaterial.UnitCode")
    },
    {
        key: "UnitWeight",
        label: i18n.t("basbmaterial.UnitWeight")
    },
    {
        key: "UsedFlag",
        label: i18n.t("basbmaterial.UsedFlag")
    },
    {
        key: "VirtualFlag",
        label: i18n.t("basbmaterial.VirtualFlag")
    },
    {
        key: "WarnOverdueLen",
        label: i18n.t("basbmaterial.WarnOverdueLen")
    },
    {
        key: "WhouseNo",
        label: i18n.t("basbmaterial.WhouseNo")
    },
    {
        key: "CompanyCode",
        label: i18n.t("basbmaterial.CompanyCode")
    },
    
  { isOperate: true, label: i18n.t(`table.actions`), actions: ["detail", "edit", "deleted"] } //操作列
];


