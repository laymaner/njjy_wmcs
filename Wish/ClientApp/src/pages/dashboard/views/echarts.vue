<template>
  <div class="charts-wrap">
    <div class="chart-title">{{ $t("dialogTitle.binStatistics") }}</div>
    <div class="charts-draw" ref="chart"></div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import axios from 'axios';

const echarts = require("echarts/lib/echarts");
// 引入柱状图
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
      const url = "/api/baswbin/getbinstatisticsasync";
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
        const dataFromApi = data.status === "0"? data.data : [];
        const rowCount = Array.isArray(dataFromApi)? dataFromApi.length : 0;
        const defaultData = [
          { value: 10, name: this.$t("dialogTitle.binEmpty") },
          { value: 15, name: this.$t("dialogTitle.binOccupy") },
          { value: 20, name: this.$t("dialogTitle.binAbnormal") }
        ];
        const dataToUse = rowCount === 0? defaultData : dataFromApi;
        console.log("接口赋值：", JSON.stringify(dataToUse));
        this.chartInstance = echarts.init(this.$refs.chart);
        const option = {
          title: {
            text: "面积模式玫瑰图",
            subtext: "纯属虚构",
            left: "center"
          },
          tooltip: {
            trigger: "item",
            formatter: "{a} <br/>{b} : {c} ({d}%)"
          },
          legend: {
            left: "center",
            top: "bottom",
            data: [this.$t("dialogTitle.binEmpty"), this.$t("dialogTitle.binOccupy"), this.$t("dialogTitle.binAbnormal")]
          },
          toolbox: {
            show: true,
            feature: {
              mark: { show: true },
              dataView: { show: true, ReadOnly: false },
              magicType: {
                show: true,
                type: ["pie", "funnel"]
              },
              restore: { show: true },
              saveAsImage: { show: true }
            }
          },
          series: [
            {
              name: "面积模式",
              type: "pie",
              radius: [30, 110],
              center: ["50%", "50%"],
              roseType: "area",
              data: dataToUse
            }
          ]
        };
        this.chartInstance.setOption(option);
        window.addEventListener('resize', this.handleResize);
      } else {
        console.error('接口响应状态码不是 200');
      }
    } catch (error) {
      console.error('接口调用出错', error);
      const defaultData = [
        { value: 10, name: this.$t("dialogTitle.binEmpty") },
        { value: 15, name: this.$t("dialogTitle.binOccupy") },
        { value: 20, name: this.$t("dialogTitle.binAbnormal") }
      ];
      this.chartInstance = echarts.init(this.$refs.chart);
      const defaultOption = {
        title: {
          text: "面积模式玫瑰图",
          subtext: "纯属虚构",
          left: "center"
        },
        tooltip: {
          trigger: "item",
          formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        legend: {
          left: "center",
          top: "bottom",
          data: [this.$t("dialogTitle.binEmpty"), this.$t("dialogTitle.binOccupy"), this.$t("dialogTitle.binAbnormal")]
        },
        toolbox: {
          show: true,
          feature: {
            mark: { show: true },
            dataView: { show: true, ReadOnly: false },
            magicType: {
              show: true,
              type: ["pie", "funnel"]
            },
            restore: { show: true },
            saveAsImage: { show: true }
          }
        },
        series: [
          {
            name: "面积模式",
            type: "pie",
            radius: [30, 110],
            center: ["50%", "50%"],
            roseType: "area",
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