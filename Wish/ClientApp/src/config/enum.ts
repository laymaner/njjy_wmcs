// 请求类型
export enum contentType {
  form = "application/x-www-form-urlencoded; charset=UTF-8",
  json = "application/json",
  multipart = "multipart/form-data",
  stream = "application/json",
  arraybuffer = "arraybuffer" // 图片buffer
}
// 弹出框类型 actionType
export enum actionType {
  detail = "detail",
  edit = "edit",
  add = "add",
  //自定义
  dealsrmtaskout = "dealsrmtaskout",
  createinventory="createinventory",
  createinvoice="createinvoice"
}
// 按钮类型
export enum butType {
  edit = "edit",
  add = "add",
  delete = "delete",
  deleted = "delete",
  imported = "imported",
  export = "export",
  //自定义按钮
  taskreload="taskreload",
  taskfinish="taskfinish",
  taskclose="taskclose",
  createinvoice="createinvoice",
  createinventory="createinventory",
  dealcmdsend="dealcmdsend",
  dealsrmpick="dealsrmpick",
  dealsrmput="dealsrmput",
  dealsrmrefuse="dealsrmrefuse",
  dealsrmtaskin="dealsrmtaskin",
  dealsrmtaskout="dealsrmtaskout",
  deviceopen="deviceopen",
  deviceclose="deviceclose",
  devicestepinit="devicestepinit",
  plcopen="plcopen",
  plcclose="plcclose",
  handleinter="handleinter",
  cmdresend="cmdresend",
  devicewait="devicewait",
  manualout="manualout",
  openbin="openbin",
  closebin="closebin"
}
