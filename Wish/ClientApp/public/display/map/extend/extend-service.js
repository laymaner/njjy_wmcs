// 建立连接
//const serviceUrl = "http://10.12.115.70:20001/api/WCSMS/monitor";
// const serviceUrl = "http://10.15.35.38:20001/api/WCSMS/monitor";
const serviceUrl = "http://127.0.0.1:6338/api/WCSMS/monitor";
let connection = "";
console.log("连接地址"+serviceUrl);
const signalRConnect = () => {
  connection = new signalR.HubConnectionBuilder()
    .withUrl(serviceUrl)
    .withAutomaticReconnect()
    .build();
  connection.keepAliveIntervalInMilliseconds = 5000;
  connection.onreconnected(async (id) => {
    const message = "Layout|01";
    connection.invoke("Connected", message);

    console.info("已自动重新连接:" + message);
  });
  connection.onclose(async (error) => {
    console.log("连接状态"+error);
    if (error) {
      // 异常断开时,需要手动重新连接
      await connection.start();
    } else {
      console.info("已断开连接");
    }
  });
};
// signalR初始化,接收函数
const signalRMonitorInit = () => {
  connection
    .start()
    .then(() => {
      const message = "Layout|01";
      connection.invoke("Connected", message);
    })
    .catch((err) => {
      console.log(err);
    });

  connection.on("SendMessage", (msg) => {
    let response = JSON.parse(msg);
    console.log("res:----------------------------------------", response);
    coverDataToNew(response);
  });
};

function startUpdate() {
  signalRConnect();
  signalRMonitorInit();
}
