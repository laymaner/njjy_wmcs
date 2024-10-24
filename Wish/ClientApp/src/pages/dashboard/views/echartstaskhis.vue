<template>
  <div class="charts-wrap">
    <div class="chart-title">{{ $t("dialogTitle.taskHistory") }}</div>
    <div class="charts-draw" ref="chart"></div>
    <div class="time-selector-box">
      <button
        class="time-button"
        :class="{ active: timePeriod === 'week' }"
        @click="selectTimePeriod('week')"
      >{{ $t("dialogTitle.week") }}</button>
      <button
        class="time-button"
        :class="{ active: timePeriod === 'month' }"
        @click="selectTimePeriod('month')"
      >{{ $t("dialogTitle.month") }}</button>
      <button
        class="time-button"
        :class="{ active: timePeriod === 'quarter' }"
        @click="selectTimePeriod('quarter')"
      >{{ $t("dialogTitle.quarter") }}</button>
      <button
        class="time-button"
        :class="{ active: timePeriod === 'year' }"
        @click="selectTimePeriod('year')"
      >{{ $t("dialogTitle.year") }}</button>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import axios from 'axios';

const echarts = require("echarts/lib/echarts");
// 引入折线图
require("echarts/lib/chart/line");
// 引入提示框和标题组件
require("echarts/lib/component/tooltip");
require("echarts/lib/component/legend");

@Component({
  name: "echarts"
})
export default class extends Vue {
  chartInstance: any = null;
  timePeriod: string = 'week';
  async mounted() {
    this.initChart();
    // 监听浏览器窗口的 resize 事件
    window.addEventListener('resize', this.handleResize);
  }

  beforeDestroy() {
    // 在组件销毁前移除事件监听器
    window.removeEventListener('resize', this.handleResize);
  }

  handleResize = () => {
    if (this.chartInstance) {
      this.chartInstance.resize();
    }
  }

  async initChart() {
    try {
      const url = `/api/srmcmd/gethistorytask?timePeriod=${this.timePeriod}`;
      const response = await axios.post(url, {}, {
        headers: {
          'Content-Type': 'application/json'
        }
      });
      if (response.status === 200) {
        const data = response.data;
        const apiData = data.status === "0"? data.data : [];
        let defaultData: any[] = [];
        if (this.timePeriod === 'week') {
          defaultData = [
            { name: this.$t("dialogTitle.monday"), value1: 20, value2: 30 },
            { name: this.$t("dialogTitle.tuesday"), value1: 15, value2: 25 },
            { name: this.$t("dialogTitle.wednesday"), value1: 10, value2: 20 },
            { name: this.$t("dialogTitle.thursday"), value1: 18, value2: 22 },
            { name: this.$t("dialogTitle.friday"), value1: 22, value2: 28 },
            { name: this.$t("dialogTitle.saturday"), value1: 16, value2: 24 },
            { name: this.$t("dialogTitle.sunday"), value1: 14, value2: 26 }
          ];
        } else if (this.timePeriod === 'month') {
          defaultData = [
            { name: this.$t("dialogTitle.firstWeek"), value1: 30, value2: 40 },
            { name: this.$t("dialogTitle.secondWeek"), value1: 25, value2: 35 },
            { name: this.$t("dialogTitle.thirdWeek"), value1: 20, value2: 30 },
            { name: this.$t("dialogTitle.fourthWeek"), value1: 28, value2: 38 },
            { name: this.$t("dialogTitle.fifthWeek"), value1: 22, value2: 32 }
          ];
        } else if (this.timePeriod === 'quarter') {
          defaultData = [
            { name: this.$t("dialogTitle.firstMonth"), value1: 40, value2: 50 },
            { name: this.$t("dialogTitle.secondMonth"), value1: 35, value2: 45 },
            { name: this.$t("dialogTitle.thirdMonth"), value1: 30, value2: 40 }
          ];
        } else if (this.timePeriod === 'year') {
          defaultData = [
            { name: this.$t("dialogTitle.firstQuarter"), value1: 60, value2: 70 },
            { name: this.$t("dialogTitle.secondQuarter"), value1: 55, value2: 65 },
            { name: this.$t("dialogTitle.thirdQuarter"), value1: 50, value2: 60 },
            { name: this.$t("dialogTitle.firthQuarter"), value1: 45, value2: 55 }
          ];
        }
        const rowCount = Array.isArray(apiData)? apiData.length : 0;
        let dataToUse = rowCount === 0? defaultData : apiData;
        this.chartInstance = echarts.init(this.$refs.chart);
        const option = {
          title: {
            text: "折线图示例",
            left: "center"
          },
          tooltip: {
            trigger: "axis"
          },
          legend: {
            left: "center",
            top: "bottom",
            data: [this.$t("dialogTitle.inStore"), this.$t("dialogTitle.outStore")]
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
              name: this.$t("dialogTitle.inStore"),
              type: "line",
              data: dataToUse.map(item => item.value1),
              itemStyle: {
                color: 'blue'
              }
            },
            {
              name: this.$t("dialogTitle.outStore"),
              type: "line",
              data: dataToUse.map(item => item.value2),
              itemStyle: {
                color: 'green'
              }
            }
          ]
        };
        option.title.text = "统计图标题 - " + option.title.text;
        this.chartInstance.setOption(option);
      } else {
        console.error('接口响应状态码不是 200');
      }
    } catch (error) {
      console.error('接口调用出错', error);
      let defaultData: any[] = [];
      if (this.timePeriod === 'week') {
        defaultData = [
          { name: this.$t("dialogTitle.monday"), value1: 20, value2: 30 },
          { name: this.$t("dialogTitle.tuesday"), value1: 15, value2: 25 },
          { name: this.$t("dialogTitle.wednesday"), value1: 10, value2: 20 },
          { name: this.$t("dialogTitle.thursday"), value1: 18, value2: 22 },
          { name: this.$t("dialogTitle.friday"), value1: 22, value2: 28 },
          { name: this.$t("dialogTitle.saturday"), value1: 16, value2: 24 },
          { name: this.$t("dialogTitle.sunday"), value1: 14, value2: 26 }
        ];
      } else if (this.timePeriod === 'month') {
        defaultData = [
          { name: this.$t("dialogTitle.firstWeek"), value1: 30, value2: 40 },
          { name: this.$t("dialogTitle.secondWeek"), value1: 25, value2: 35 },
          { name: this.$t("dialogTitle.thirdWeek"), value1: 20, value2: 30 },
          { name: this.$t("dialogTitle.fourthWeek"), value1: 28, value2: 38 },
          { name: this.$t("dialogTitle.fifthWeek"), value1: 22, value2: 32 }
        ];
      } else if (this.timePeriod === 'quarter') {
        defaultData = [
          { name: this.$t("dialogTitle.firstMonth"), value1: 40, value2: 50 },
          { name: this.$t("dialogTitle.secondMonth"), value1: 35, value2: 45 },
          { name: this.$t("dialogTitle.thirdMonth"), value1: 30, value2: 40 }
        ];
      } else if (this.timePeriod === 'year') {
        defaultData = [
          { name: this.$t("dialogTitle.firstQuarter"), value1: 60, value2: 70 },
          { name: this.$t("dialogTitle.secondQuarter"), value1: 55, value2: 65 },
          { name: this.$t("dialogTitle.thirdQuarter"), value1: 50, value2: 60 },
          { name: this.$t("dialogTitle.firthQuarter"), value1: 45, value2: 55 }
        ];
      }
      this.chartInstance = echarts.init(this.$refs.chart);
      const defaultOption = {
        title: {
          text: "折线图示例",
          left: "center"
        },
        tooltip: {
          trigger: "axis"
        },
        legend: {
          left: "center",
          top: "bottom",
          data: [this.$t("dialogTitle.inStore"), this.$t("dialogTitle.outStore")]
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
            name: this.$t("dialogTitle.inStore"),
            type: "line",
            data: defaultData.map(item => item.value1),
            itemStyle: {
              color: 'blue'
            }
          },
          {
            name: this.$t("dialogTitle.outStore"),
            type: "line",
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

  selectTimePeriod(period: string) {
    this.timePeriod = period;
    this.initChart();
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
.time-selector-box {
    display: flex;
    background-color: #f5f5f5;
    border-radius: 5px;
    padding: 5px;
    button {
      background-color: transparent;
      border: none;
      padding: 5px 10px;
      cursor: pointer;
      &.active {
        background-color: #007bff;
        color: #fff;
      }
    }
  }
}
</style>