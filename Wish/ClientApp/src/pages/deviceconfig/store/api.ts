import config from "@/config/index";
import { contentType } from "@/config/enum";
const reqPath = config.headerApi + "/deviceconfig/";
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

const getStandardDevice = {
  url: reqPath + "getStandardDevices",
  method: "get",
  dataType: "array"
}; 
const getPlcConfig = {
  url: reqPath + "getPlcConfigs",
  method: "get",
  dataType: "array"
}; 

const getDicByPlcStep = {
  url: optionPath + "getdicoption?code=PLC_STEP",
  method: "get",
  dataType: "array"
}; 
const getDicByWcsStep = {
  url: optionPath + "getdicoption?code=WCS_SETP",
  method: "get",
  dataType: "array"
}; 
const getDicByMode = {
  url: optionPath + "getdicoption?code=MODE",
  method: "get",
  dataType: "array"
}; 
const deviceopen = {
  url: reqPath + "deviceopen",
  method: "post"
};
const deviceclose = {
  url: reqPath + "deviceclose",
  method: "post"
};
const devicestepinit = {
  url: reqPath + "devicestepinit",
  method: "post"
};
const devicewait = {
  url: reqPath + "devicewait",
  method: "post"
};

export default {
getStandardDevice,
getPlcConfig,
getDicByPlcStep,
getDicByWcsStep,
getDicByMode,
deviceopen,
deviceclose,
devicestepinit,
devicewait,

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
