//堆垛机

var Stacker = {
  /*
	id 序列号
	column 列数
	*/

  createNew: function () {
    var stacker = {};
    var num = 1;
    var step = 34;
    var index = 1;
    var pos = Array(); //运行路线的坐标集合
    var angle = 1; //角度 1水平，2垂直
    var id = 0; //设备ID
    var xstep = 1; //水平移动等比缩放
    var ystep = 1; //垂直移动等比缩放
    var column = 0;
    //-----------------------
    /*
        添加堆垛机
        @int id      设备编号
        @int tray    是否有托盘   0无托盘  1有托盘
        @int column  设备所在位置，0位初始化位置 0至N这个表示列数
        @int connect 设备与PLC的连接状态 0 断开， 1 连接
        @int alt     设备报警状态  0 无报警  大于0有报警
        @int taskid  任务号
         */
    stacker.add = function (
      tray,
      column,
      connect,
      alt,
      taskid,
      code,
      alarmMessage,
      initPos,
      obj
    ) {
      if (pos.length < 1) pos = initPos;
      var x = pos[column][0];
      var y = pos[column][1];

      x = (x * step) / xstep;
      y = (y * step) / ystep + (num * step) / 2;

      var map = $("#zd_map");

      var titlePos = "";
      if (_.has(obj, "titlePosition")) {
        titlePos =
          obj.titlePosition !== "center" ? `${obj.titlePosition}:-37px` : "";
      }
      var iconArrow = `<div class="icon-arrow-${obj.titlePosition}"></div>`;
      var titleBox = `<span class="dev-code srm-title" style="${titlePos}">${
        code || ""
      }${iconArrow}</span>`;
      var box = `<div id="stacker${id}" class="stacker-items dev-main" style="width:34px; height:34px;background:#390;position:absolute;background:no-repeat">${titleBox}</div>`;

      if ($("#stacker" + id + "").length == 0) {
        map.append(box);
        $("#stacker" + id + "").css({ x: x + "px", y: y + "px" });
      } else {
        $("#stacker" + id + "").css({ x: x + "px", y: y + "px" });
      }

      if (angle == 1) {
        $("#stacker" + id + "").css(
          "background-image",
          "url(../map/images/stacker_1.png)"
        );
      }

      if (angle == 2) {
        $("#stacker" + id + "").css(
          "background-image",
          "url(../map/images/stacker_2.png)"
        );
      }

      alt = parseInt(alt);
      connect = parseInt(connect);

      const connectMsg = connect ? "设备已连接" : "设备断开";

      if (alt == 29) {
        layer.msg(code + "火警触发", { time: 5000, icon: 6 });
      }

      //报警图标显示
      stacker.iconAdd(column, "alert", alt);

      //与PLC连接状态
      stacker.iconAdd(column, "connect", connect);

      //托盘
      if (angle == 1) {
        stacker.iconAdd(column, "tray_horizontal", tray);
      } else {
        stacker.iconAdd(column, "tray_vertical", tray);
      }

      //事件生成
      if ($("#stackerevent" + id + "").length == 1) {
        $("#stackerevent" + id + "").css({ x: x + "px", y: y + "px" });
      } else {
        var eventBox =
          '<div id="stackerevent' +
          id +
          '"class="stacker-items" style="width:68px; height:34px;position:absolute;"></div>';
        map.append(eventBox);
        $("#stackerevent" + id + "").css({ x: x + "px", y: y + "px" });
        $("#stackerevent" + id + "").css("z-index", "9999");

        //鼠标事件
        $("#stackerevent" + id + "").on({
          mouseover: function () {
            const msg = `堆垛机：${code}<br>网络状态：${connectMsg}`;
            index = layer.tips(msg, "#stacker" + id + "", { tips });
            $("#stackerevent" + id + "").css("cursor", "hand");
          },
          mouseout: function () {
            layer.close(index);
            $("#stackerevent" + id + "").css("cursor", "auto");
          },
        });
      }
      //if(column>1){
      stacker.ftask(taskid, code, connect, alarmMessage, obj);
      //}
    };

    //-----------------------
    /*
        刷新点击事件和任务号
         */
    stacker.ftask = function (taskid, code, connect, alarmMessage, obj) {
      var taskid = taskid;
      if (taskid == null) {
        taskid = 0;
      }
      $("#stackerevent" + id + "").unbind();

      if (!obj.usedFlag) {
        $("#stacker" + id + "").addClass("disabled-device");
      } else {
        $("#stacker" + id + "").removeClass("disabled-device");
      }

      const cmdNo = `任务号：${obj.cmdNo || ""}`;
      const connectMsg = `网络状态：${connect ? "设备已连接" : "设备断开"}`;
      const altMsg = alarmMessage ? `告警：${alarmMessage}` : "";
      const auto = `运行模式：${obj.isAuto ? "自动" : "手动"}`;
      const free = `空闲状态：${obj.isFree ? "空闲" : "不空闲"}`;
      const palletBarcode = `托盘号：${obj.palletBarcode || " "}`;
      const codetxt = `设备编号：${obj.code}`;
      const name = `名称：${obj.name}`;
      const usedFlag = `启用状态：${obj.usedFlag ? "启用" : "禁用"}`;
      const msg = `堆垛机<br>${codetxt}<br>${name}<br>${cmdNo}<br>${palletBarcode}<br>${altMsg}<br>${auto}<br>${free}<br>${connectMsg}<br>${usedFlag}`;

      //事件
      $("#stackerevent" + id + "").on({
        mouseover: function () {
          index = layer.tips(msg, "#stacker" + id + "", { tips });
          $("#stackerevent" + id + "").css("cursor", "hand");
        },
        mouseout: function () {
          layer.close(index);
          $("#stackerevent" + id + "").css("cursor", "auto");
        },
        click: function () {
          //var url = "stackerInfo.php?task=" + taskid + "&id=" + id + "&code=" + code;
          var url =
            deviceInfo_url +
            "?type=stack&code=" +
            code +
            "&rdm=" +
            Math.random();
          //iframe窗
          layer.open({
            type: 2,
            title: "状态和指令控制窗口",
            shadeClose: true,
            shade: 0.6,
            maxmin: true, //开启最大化最小化按钮
            area: ["1050px", "700px"],
            content: url,
          });
        },
      });
    };

    //删除堆垛机
    stacker.remove = function () {
      $("#stacker" + id + "").remove();
      $("#stackerevent" + id + "").remove();
      //stacker.iconRemove(id);
    };

    /*
        设置堆垛机的运行路线，同时设置初始化坐标位置和方向
        @int    count 总长度
        @string x 初始化x位置
        @string y 初始化y位置
        @int    ang  角度 1水平，2垂直
        @int    dire 方向 right从左往右  left从右往左  down从上到下  up从下到上
        @int    movestep   移动缩小比例
         */
    stacker.setPos = function (
      deviceid,
      x,
      y,
      count,
      ang,
      dire,
      movestep,
      number = 0
    ) {
      num = number;
      id = deviceid;

      var x = x;
      var y = y;

      if (ang != null) {
        angle = ang;
      }

      if (movestep == null) {
        movestep = 1;
      }

      if (angle == 1) {
        xstep = movestep;
        if (dire == "right") {
          for (var i = 0; i <= count; i++) {
            pos[i] = [x * movestep + i + 1, y];
          }
        }

        if (dire == "left") {
          for (var i = 0; i <= count; i++) {
            pos[i] = [x * movestep - i + 1, y];
          }
        }
      }
      if (angle == 2) {
        ystep = movestep;
        if (dire == "down") {
          for (var i = 0; i <= count; i++) {
            pos[i] = [x, y * movestep - 1 + i];
          }
        }

        if (dire == "up") {
          for (var i = 0; i <= count; i++) {
            pos[i] = [x, y * movestep + 1 - i];
          }
        }
      }
    };

    //添加ICON
    stacker.iconAdd = function (column, key, value) {
      var x = pos[column][0];
      var y = pos[column][1];

      x = (x * step) / xstep;
      y = (y * step) / ystep + (num * step) / 2;

      var map = $("#zd_map");

      $("#stacker" + key + id + "").remove();

      if (key == "alert" && value > 0) {
        var icon1 =
          '<div id="stacker' +
          key +
          id +
          '" class="stacker-items" style="width:30px; height:30px;position:absolute;background:url(../map/images/icon_alert.png) no-repeat;z-index:9000;"></div>';
        map.append(icon1);
        if (angle == 1) {
          $("#stacker" + key + id + "").css({
            x: x + 8 + "px",
            y: y + 2 + "px",
          });
        } else {
          $("#stacker" + key + id + "").css({ x: x + 2 + "px", y: y + "px" });
        }
      }

      if (key == "alert" && value == 0) {
        $("#stacker" + key + id + "").remove();
      }

      if (key == "connect" && value == 0) {
        var icon2 =
          '<div id="stacker' +
          key +
          id +
          '" class="stacker-items" style="width:11px; height:11px;position:absolute;background:url(../map/images/ulink.png) no-repeat;z-index:8500;"></div>';
        map.append(icon2);
        $("#stacker" + key + id + "").css({ x: x + "px", y: y + "px" });
      }

      if (key == "connect" && value == 1) {
        var icon2 =
          '<div id="stacker' +
          key +
          id +
          '" class="stacker-items" style="width:11px; height:11px;position:absolute;background:url(../map/images/link.png) no-repeat;z-index:8500;"></div>';
        map.append(icon2);
        $("#stacker" + key + id + "").css({ x: x + "px", y: y + "px" });
      }

      if (key == "tray_horizontal" && value == 1) {
        var icon3 =
          '<div id="stacker' +
          key +
          id +
          '"class="stacker-items" style="width:30px; height:30px;position:absolute;background:url(../map/images/tray_1.png) no-repeat;z-index:6666;"></div>';
        map.append(icon3);
        $("#stacker" + key + id + "").css({ x: x + 8 + "px", y: y + 2 + "px" });
      }

      if (key == "tray_horizontal" && value == 0) {
        $("#stacker" + key + id + "").remove();
      }

      if (key == "tray_vertical" && value == 1) {
        var icon3 =
          '<div id="stacker' +
          key +
          id +
          '" class="stacker-items" style="width:30px; height:30px;position:absolute;background:url(../map/images/tray_2.png) no-repeat;z-index:6666;"></div>';
        map.append(icon3);
        $("#stacker" + key + id + "").css({ x: x + 2 + "px", y: y - 8 + "px" });
      }

      if (key == "tray_vertical" && value == 0) {
        $("#stacker" + key + id + "").remove();
      }
    };

    //删除ICON
    stacker.iconRemove = function (key) {
      $("#stacker" + key + id + "").remove();
    };

    return stacker;
  },
};
