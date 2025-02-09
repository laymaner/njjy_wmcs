/**
 * 配置文件
 */
const config = {
  headerApi: "/api",
  /**
   * token 名称
   * @TODO vuex 持久化
   */
  tokenKey: "token",
  /**
   * token 名称
   */
  tenantCodeKey: "TENANT_CODE",
  /**
   * global cookies
   */
  globalKey: "global",
  /**
   * 组件大小 medium / small / mini
   */
  elSize: "small",
  /**
   * iframe 嵌入页面标示
   */
  staticPage: "@StaticPage",

  /**
   * debugger调试
   */
  development: false
  ,
  /**
  * 租户模式
  */
  isTenant: false,
  /**
   *  cookies 过期时间
   */
  cookiesExpires: 365
};
// prod环境非调试
if (process.env.NODE_ENV === "production") {
  config.development = false;
}
export default config;

/**
 * 默认样式配置
 */
export const style = {
  menuBg: "transparent", // "#304156",
  menuText: "#efefef", // "#bfcbd9",
  menuActiveText: "#409eff",
  theme: "#1890ff"
};
