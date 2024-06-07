<script setup>
  const transactionsData = inject('transactionsData');

  const totalExpense = computed(() => {
    return transactionsData.value.reduce((acc, transaction) => {
      if (transaction.Tipo === 'R') {
        return acc + Math.abs(transaction.Valor);
      }
      return acc;
    }, 0);
  });

  // Define constants for days in milliseconds
  const LAST_DAY = 24 * 60 * 60 * 1000; // 1 day in milliseconds
  const PREVIOUS_DAY = 2 * LAST_DAY; // 2 days in milliseconds

  // Calculate total expense percentage change
  const totalExpensePercentage = computed(() => {
    const transactions = transactionsData.value;

    // Filter transactions for the last 1 day
    const lastDayTransactions = transactions.filter((transaction) => {
      const transactionDate = new Date(transaction.Data);
      const today = new Date();

      return (today - transactionDate) <= LAST_DAY;
    });

    // Calculate total expense for the last 1 day
    const currentTotalExpense = lastDayTransactions.reduce((acc, transaction) => {
      if (transaction.Tipo === 'R') {
        return acc + Math.abs(transaction.Valor);
      }
      return acc;
    }, 0);

    // Filter transactions for the previous 1 day
    const previousDayTransactions = transactions.filter((transaction) => {
      const transactionDate = new Date(transaction.Data);
      const today = new Date();

      return (today - transactionDate) > LAST_DAY && (today - transactionDate) <= PREVIOUS_DAY;
    });

    // Calculate total expense for the previous 1 day
    const previousTotalExpense = previousDayTransactions.reduce((acc, transaction) => {
      if (transaction.Tipo === 'R') {
        return acc + Math.abs(transaction.Valor);
      }
      return acc;
    }, 0);

    // Calculate percentage change
    if (previousTotalExpense === 0) {
      return 0; // Avoid division by zero
    }

    const percentageChange = ((currentTotalExpense - previousTotalExpense) / previousTotalExpense) * 100;
    return percentageChange.toFixed(2);
  });
</script>

<template>
        <v-lazy
        transition="slide-y-transition"
        >
                <v-card class="px-4 py-3">
                        <div class="bg-bgExpenseRed rounded-circle d-flex circle pa-2">
                                <v-icon
                                class=""
                                size="30"
                                color="expenseRed"
                                >
                                mdi-currency-usd
                        </v-icon>
                        </div>
                        <v-card-title class="text-body-2 pl-0 pb-0 text-label">Total Expense</v-card-title>
                        <v-card-text class="d-flex ga-4 pl-0 py-0">
                                <p class="font-weight-bold text-h6">- R${{totalExpense}}</p>
                                <v-tooltip text="Últimos 2 dias (dia anterior vs. último dia)" location="bottom">
                                        <template v-slot:activator="{ props }">
                                                <v-chip v-bind="props" small :color="totalExpensePercentage >= 0 ? 'error' : 'success'" class="cursor-help">
                                                        {{ totalExpensePercentage >= 0 ? '+' : '' }}{{ totalExpensePercentage }}%
                                                </v-chip>          
                                        </template>
                                </v-tooltip>              
                        </v-card-text>
                </v-card>
        </v-lazy>
</template>