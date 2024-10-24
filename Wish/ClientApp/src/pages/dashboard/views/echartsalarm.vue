<template>
  <div class="charts-wrap">
    <div class="chart-title">{{ $t("dialogTitle.alarmInformation") }}</div>
    <table class="data-table">
      <thead>
        <tr>
          <th style="width: 70px; font-size: 16px;">{{ $t("dialogTitle.serialNumber") }}</th>
          <th style="width: 100px; font-size: 16px;">{{ $t("dialogTitle.deviceName") }}</th>
          <th style="width: 180px; font-size: 16px;">{{ $t("dialogTitle.alarmInformation") }}</th>
          <th style="width: 180px; font-size: 16px;">{{ $t("dialogTitle.alarmTime") }}</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(item, index) in tableData" :key="index" :class="{ even: index % 2 === 0, odd: index % 2 === 1 }">
          <td style="font-size: 14px;">{{ index + 1 }}</td>
          <td style="font-size: 14px;">{{ item.name }}</td>
          <td style="font-size: 14px;">{{ item.value1 }}</td>
          <td style="font-size: 14px;">{{ item.value2 }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import axios from 'axios';

@Component({
  name: "echarts"
})
export default class extends Vue {
  tableData: any[] = [];
  async mounted() {
    try {
      const url = '/api/devicealarmlog/getalarmlog';
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
          { name: "SRM01", value1: 20, value2: new Date().toLocaleString() },
          { name: "SRM02", value1: 15, value2: new Date().toLocaleString() },
          { name: "SRM03", value1: 10, value2: new Date().toLocaleString() },
          { name: "SRM01", value1: 12, value2: new Date().toLocaleString() },
          { name: "SRM02", value1: 18, value2: new Date().toLocaleString() },
          { name: "SRM03", value1: 14, value2: new Date().toLocaleString() }
        ];
        this.tableData = rowCount === 0? defaultData : apiData;
      } else {
        console.error('接口响应状态码不是 200');
      }
    } catch (error) {
      console.error('接口调用出错', error);
      let defaultData = [
        { name: "SRM01", value1: 20, value2: new Date().toLocaleString() },
        { name: "SRM02", value1: 15, value2: new Date().toLocaleString() },
        { name: "SRM03", value1: 10, value2: new Date().toLocaleString() },
        { name: "SRM01", value1: 12, value2: new Date().toLocaleString() },
        { name: "SRM02", value1: 18, value2: new Date().toLocaleString() },
        { name: "SRM03", value1: 14, value2: new Date().toLocaleString() }
      ];
      this.tableData = defaultData;
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
.data-table {
    width: 100%;
    border-collapse: collapse;
    th, td {
      border: 1px solid #ddd;
      padding: 12px;
      text-align: left;
    }
    th {
      background-color: #f2f2f2;
    }
    tr.even {
      background-color: #f9f9f9;
    }
    tr.odd {
      background-color: #fff;
    }
  }
}
</style>