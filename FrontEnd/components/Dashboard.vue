<script setup>


  const transactionsData = ref([]);

  const fetchTransactions = async () => {
    const transactions = await useFetch('https://webapplication120240607152445.azurewebsites.net//transacao');
    transactionsData.value = transactions.data.value;
  }

  fetchTransactions();

  provide('transactionsData', transactionsData);

</script>

<template>
        <v-container class="py-8 px-6" fluid :key="transactionsData">
                <v-row class="d-flex justify-space-between align-center">
                        <v-col cols="12" md="9">
                                <Greeting />
                        </v-col>

                        <v-col cols="12" md="2">
                                <DialogForm />
                        </v-col>
                        <v-col cols="12" md="1">
                                <v-btn icon>
                                        <v-icon @click="fetchTransactions">mdi-reload</v-icon>
                                </v-btn>
                        </v-col>
                        <v-col cols="12" md="6">
                                <TotalBalance />
                        </v-col>

                        <v-col cols="6" md="3">
                                <TotalIncome />
                        </v-col>

                        <v-col cols="6" md="3">
                                <TotalExpense />
                        </v-col>
                </v-row>

                <v-row>
                        <v-col cols="12">
                                <TransactionsTable />
                        </v-col>
                </v-row>

        </v-container>
</template>