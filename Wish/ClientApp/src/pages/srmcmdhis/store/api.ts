﻿import config from "@/config/index";
import { contentType } from "@/config/enum";
const reqPath = config.headerApi + "/srmcmdhis/";
const optionPath = config.headerApi + "/option/";

// 列表
const search = {
  url: reqPath + "search",
  method: "post",
  dataType: "array"
};
// 添加
const add = {
  url: reqPath + "Add",
  method: "post"
};
// 删除
const deleted = {
  url: reqPath + "Delete/{ID}",
  method: "get"
};
// 批量删除
const batchDelete = {
  url: reqPath + "BatchDelete",
  method: "post"
};
// 修改
const edit = {
  url: reqPath + "Edit",
  method: "put"
};
// 详情
const detail = {
  url: reqPath + "{ID}",
  method: "get"
};
const exportExcel = {
  url: reqPath + "ExportExcel",
  method: "post",
  contentType: contentType.stream
};
const exportExcelByIds = {
  url: reqPath + "ExportExcelByIds",
  method: "post",
  contentType: contentType.stream
};
const getExcelTemplate = {
  url: reqPath + "GetExcelTemplate",
  method: "get",
  contentType: contentType.stream
};
// 导入
const imported = {
  url: reqPath + "Import",
  method: "post"
};

const getDicByStatus = {
  url: optionPath + "getdicoption?code=EXEC_STATUS",
  method: "get",
  dataType: "array"
};
const getDicByType = {
  url: optionPath + "getdicoption?code=CMD_TYPE",
  method: "get",
  dataType: "array"
};

export default {
  getDicByStatus,
  getDicByType,

  search,
  add,
  deleted,
  batchDelete,
  edit,
  detail,
  exportExcel,
  exportExcelByIds,
  getExcelTemplate,
  imported
};
