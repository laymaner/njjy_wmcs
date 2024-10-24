<template>
  <div class="charts-wrap">
    <div class="chart-title">{{ $t("dialogTitle.currentTask") }}</div>
    <div class="charts-draw" ref="chart"></div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import axios from 'axios';

const echarts = require("echarts/lib/echarts");
// 引入饼图
require("echarts/lib/chart/pie");
// 引入提示框和标题组件
require("echarts/lib/component/tooltip");
require("echarts/lib/component/legend");

@Component({
  name: "echarts"
})
export default class extends Vue {
  chartInstance: any = null;
  async mounted() {
    try {
      const url = '/api/srmcmd/getcurrenttask';
      const response = await axios.post(url, {}, {
        headers: {
          'Content-Type': 'application/json'
        }
      });
      if (response.status === 200) {
        const data = response.data;
        const apiData = data.status === "0"? data.data : [];
        const rowCount = Array.isArray(apiData)? apiData.length : 0;
        let defaultData = [
          { value: 30, name: this.$t("dialogTitle.inStore") },
          { value: 20, name: this.$t("dialogTitle.outStore") }
        ];
        const dataToUse = rowCount === 0? defaultData : apiData;
        this.chartInstance = echarts.init(this.$refs.chart);
        const option = {
          title: {
            text: "饼状图示例",
            left: "center"
          },
          tooltip: {
            trigger: "item",
            formatter: "{a} <br/>{b} : {c} ({d}%)"
          },
          legend: {
            orient: "horizontal",// 设置为水平方向
            bottom: "0", // 将图例放置在底部
            left: "center"
          },
          series: [
            {
              name: "数据系列",
              type: "pie",
              data: dataToUse
            }
          ]
        };
        option.title.text = "统计图标题 - " + option.title.text;
        this.chartInstance.setOption(option);
        window.addEventListener('resize', this.handleResize);
      } else {
        console.error('接口响应状态码不是 200');
      }
    } catch (error) {
      console.error('接口调用出错', error);
      let defaultData = [
        { value: 30, name: this.$t("dialogTitle.inStore") },
        { value: 20, name: this.$t("dialogTitle.outStore") }
      ];
      this.chartInstance = echarts.init(this.$refs.chart);
      const defaultOption = {
        title: {
          text: "饼状图示例",
          left: "center"
        },
        tooltip: {
          trigger: "item",
          formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        legend: {
          orient: "horizontal",// 设置为水平方向
          bottom: "0", // 将图例放置在底部
          left: "center"
        },
        series: [
          {
            name: "数据系列",
            type: "pie",
            data: defaultData
          }
        ]
      };
      this.chartInstance.setOption(defaultOption);
    }
  }

  beforeDestroy() {
    window.removeEventListener('resize', this.handleResize);
  }

  handleResize = () => {
    if (this.chartInstance) {
      this.chartInstance.resize();
    }
  }
}
</script>

<style lang="less" scoped>
.charts-wrap {
.chart-title {
    font-size: 22px;
    font-weight: bold;
    margin-bottom: 20px;
  }
.charts-draw {
    width: 100%;
    min-height: 300px;
  }
}
</style>