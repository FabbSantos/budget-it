<template>
  <v-lazy>
    <div>
      <v-card class="pb-8">
        <v-card-title class="text-label ml-8 mt-2 font-weight-regular">
          Transactions overview
        </v-card-title>
        <canvas id="transactionChart"></canvas>
      </v-card>
    </div>
  </v-lazy>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { Chart, registerables } from 'chart.js/auto';

Chart.register(...registerables);

const transactionsData = ref([]);

onMounted(async () => {
  const response = await fetch('https://webapplication120240607152445.azurewebsites.net//transacao');
  const data = await response.json();
  transactionsData.value = data;

  const groupedByDate = transactionsData.value.reduce((acc, transaction) => {
    const date = transaction.Data;
    if (!acc[date]) {
      acc[date] = { count: 0 };
    }
    acc[date].count++;
    return acc;
  }, {});

  const dates = Object.keys(groupedByDate).sort();
  const counts = dates.map(date => groupedByDate[date].count);

  // Formata as datas para o formato DD/MM/YYYY
  const formattedDates = dates.map(formatDate);

  renderChart(formattedDates, counts);
});

function renderChart(dates, counts) {
  const ctx = document.getElementById('transactionChart').getContext('2d');
  new Chart(ctx, {
    type: 'bar',
    data: {
      labels: dates,
      datasets: [{
        label: 'Transactions per day',
        data: counts,
        backgroundColor: '#FFD4F4',
        borderColor: 'rgba(75, 192, 192, 1)',
        borderWidth: 1,
      }],
    },
    options: {
      scales: {
        y: {
          beginAtZero: true
        }
      },
    }
  });
}

function formatDate(dateString) {
  const date = new Date(dateString);
  const day = String(date.getDate()).padStart(2, '0');
  const month = String(date.getMonth() + 1).padStart(2, '0'); // Os meses são indexados a partir de 0
  const year = date.getFullYear();

  return `${day}/${month}/${year}`;
}
</script>

<style scoped>
canvas {
  width: 70vw;
  height: 70vh;
  margin-inline: auto;
}
</style>