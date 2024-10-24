<template>
  <div class="charts-wrap">
    <div class="chart-title">{{ $t("dialogTitle.roadwayStatistics") }}</div>
    <div class="charts-draw" ref="chart"></div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import axios from 'axios';

const echarts = require("echarts/lib/echarts");
// 引入柱状图
require("echarts/lib/chart/bar");
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
      const url = '/api/baswbin/getroadwaystatisticsasync';
      const response = await axios.post(url, {}, {
        headers: {
          'Content-Type': 'application/json'
        }
      });
      if (response.status === 200) {
        const data = response.data;
        console.log("接口返回1：", response);
        console.log("接口返回2：", response.status);
        console.log("接口返回3：", JSON.stringify(data));
        console.log("接口返回4：", JSON.stringify(data.item));
        const apiData = data.status === "0"? data.data : [];
        const rowCount = Array.isArray(apiData)? apiData.length : 0;
        const defaultData = [
          { name: this.$t("dialogTitle.roadway1"), value1: 20, value2: 30 },
          { name: this.$t("dialogTitle.roadway2"), value1: 15, value2: 25 },
          { name: this.$t("dialogTitle.roadway3"), value1: 10, value2: 20 }
        ];
        const dataToUse = rowCount === 0? defaultData : apiData;
        this.chartInstance = echarts.init(this.$refs.chart);
        const option = {
          title: {
            text: "柱状图示例",
            left: "center"
          },
          tooltip: {
            trigger: "item",
            formatter: "{a} <br/>{b} : {c}"
          },
          legend: {
            left: "center",
            top: "bottom",
            data: [this.$t("dialogTitle.binEmpty"), this.$t("dialogTitle.binOccupy")]
          },
          xAxis: {
            type: 'category',
            data: dataToUse.map(item => item.name)
          },
          yAxis: {
            type: 'value'
          },
          series: [
            {
              name: this.$t("dialogTitle.binEmpty"),
              type: "bar",
              data: dataToUse.map(item => item.value1),
              itemStyle: {
                color: 'blue'
              }
            },
            {
              name: this.$t("dialogTitle.binOccupy"),
              type: "bar",
              data: dataToUse.map(item => item.value2),
              itemStyle: {
                color: 'green'
              }
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
      const defaultData = [
        { name: this.$t("dialogTitle.roadway1"), value1: 20, value2: 30 },
        { name: this.$t("dialogTitle.roadway2"), value1: 15, value2: 25 },
        { name: this.$t("dialogTitle.roadway3"), value1: 10, value2: 20 }
      ];
      this.chartInstance = echarts.init(this.$refs.chart);
      const defaultOption = {
        title: {
          text: "柱状图示例",
          left: "center"
        },
        tooltip: {
          trigger: "item",
          formatter: "{a} <br/>{b} : {c}"
        },
        legend: {
          left: "center",
          top: "bottom",
          data: [this.$t("dialogTitle.binEmpty"), this.$t("dialogTitle.binOccupy")]
        },
        xAxis: {
          type: 'category',
          data: defaultData.map(item => item.name)
        },
        yAxis: {
          type: 'value'
        },
        series: [
          {
            name: this.$t("dialogTitle.binEmpty"),
            type: "bar",
            data: defaultData.map(item => item.value1),
            itemStyle: {
              color: 'blue'
            }
          },
          {
            name: this.$t("dialogTitle.binOccupy"),
            type: "bar",
            data: defaultData.map(item => item.value2),
            itemStyle: {
              color: 'green'
            }
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