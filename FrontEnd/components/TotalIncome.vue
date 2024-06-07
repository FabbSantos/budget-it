<script setup>
  const transactionsData = inject('transactionsData');

  const totalIncome = computed(() => {
    return transactionsData.value.reduce((acc, transaction) => {
      if (transaction.Tipo === 'D') {
        return acc + transaction.Valor;
      }
      return acc;
    }, 0);
  });

  // Define constants for days in milliseconds
  const LAST_DAY = 24 * 60 * 60 * 1000; // 1 day in milliseconds
  const PREVIOUS_DAY = 2 * LAST_DAY; // 2 days in milliseconds

  // Calculate total income percentage change
  const totalIncomePercentage = computed(() => {
    const transactions = transactionsData.value;

    // Filter transactions for the last 1 day
    const lastDayTransactions = transactions.filter((transaction) => {
      const transactionDate = new Date(transaction.Data);
      const today = new Date();

      return (today - transactionDate) <= LAST_DAY;
    });

    // Calculate total income for the last 1 day
    const currentTotalIncome = lastDayTransactions.reduce((acc, transaction) => {
      return acc + transaction.Valor;
    }, 0);

    // Filter transactions for the previous 1 day
    const previousDayTransactions = transactions.filter((transaction) => {
      const transactionDate = new Date(transaction.Data);
      const today = new Date();

      return (today - transactionDate) > LAST_DAY && (today - transactionDate) <= PREVIOUS_DAY;
    });

    // Calculate total income for the previous 1 day
    const previousTotalIncome = previousDayTransactions.reduce((acc, transaction) => {
      return acc + transaction.Valor;
    }, 0);

    // Calculate percentage change
    if (previousTotalIncome === 0) {
      return 0; // Avoid division by zero
    }

    const percentageChange = ((currentTotalIncome - previousTotalIncome) / previousTotalIncome) * 100;
    return percentageChange.toFixed(2);
  });
</script>

<template>
        <v-lazy
        transition="slide-y-transition"
        >
                <v-card class="px-4 py-3">
                        <div class="bg-bgIncomeGreen rounded-circle d-flex circle pa-2">
                                <v-icon
                                class=""
                                size="30"
                                color="incomeGreen"
                                >
                                mdi-currency-usd-off
                        </v-icon>
                        </div>
                        <v-card-title class="text-body-2 pl-0 pb-0 text-label">Total Income</v-card-title>
                        <v-card-text class="d-flex ga-4 pl-0 py-0">
                                <p class="font-weight-bold text-h6">R${{totalIncome}}</p>

                                <v-tooltip text="Últimos 2 dias (dia anterior vs. último dia)" location="bottom">
                                        <template v-slot:activator="{ props }">
                                                <v-chip v-bind="props" small :color="totalIncomePercentage >= 0 ? 'incomeGreen' : 'error'" class="cursor-help">
                                                        {{ totalIncomePercentage >= 0 ? '+' : '' }}{{ totalIncomePercentage }}%
                                                </v-chip>          
                                        </template>
                                </v-tooltip>    
                        </v-card-text>
                </v-card>
        </v-lazy>
</template>