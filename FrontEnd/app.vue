<template>
  <v-app id="inspire">

    <v-navigation-drawer v-model="drawer" permanent
      class="bg-gray"
    >
      <v-card
        class="pa-4 d-flex jusitfy-center align-center ga-4"
        color="primaryPink"
      >
        <v-avatar
          class=""
          size="50"
          image="/Logo.png"
        ></v-avatar>

        <div class="font-weight-bold">Budget it</div>
      </v-card>

      <v-list>
        <v-list-item
          v-for="[icon, text] in links"
          :key="icon"
          :prepend-icon="icon"
          :title="text"
          link
          class="text-label"
        ></v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-main>
      <v-container
        class="py-8 px-6"
        fluid
      >
    <v-row>
      <v-col cols="12">
        <p class=" text-h5">Hello, <span class="font-weight-bold">{{user[0].Nome}}</span>! How are you?</p>
        <!-- {{ transactions }} -->
      </v-col>
      <v-col cols="12" md="6">
        <v-card class="d-flex align-center px-4 py-2">
          <div class="bg-secondaryPink rounded-circle h-auto pa-4">
            <v-icon
            class=""
            size="40"
            color="primaryPink"
            >
            mdi-wallet-bifold
          </v-icon>
        </div>
          <v-card-item class="mt-4">
              <v-card-title class="text-body-1 text-label">Total Balance</v-card-title>
              <v-card-text class="d-flex ga-4 ps-0">
                <p class="font-weight-bold text-h6">R${{transactionsData}}</p>
                <v-chip x-small color="success">+1.29%</v-chip>
              </v-card-text>
          </v-card-item>
        </v-card>
      </v-col>

      <v-col cols="6" md="3">
        <v-card class="px-4 py-3 d-flex flex-column justify-center">
           <div class="bg-bgIncomeGreen rounded-circle d-flex circle pa-2">
              <v-icon
              class=""
              size="30"
              color="incomeGreen"
              >
              mdi-currency-usd
            </v-icon>
          </div>
          <v-card-title class="text-body-2 pl-0 text-label pb-0">Total Income</v-card-title>
          <v-card-text class="d-flex ga-4 pl-0 py-0">
            <p class="font-weight-bold text-h6">$632.000</p>
            <v-chip small color="expenseRed">+129%</v-chip>
          </v-card-text>
        </v-card>
      </v-col>

      <v-col cols="6" md="3">
        <v-card class="px-4 py-3">
           <div class="bg-bgExpenseRed rounded-circle d-flex circle pa-2">
              <v-icon
              class=""
              size="30"
              color="expenseRed"
              >
              mdi-currency-usd-off
            </v-icon>
          </div>
          <v-card-title class="text-body-2 pl-0 pb-0 text-label">Total Expenses</v-card-title>
          <v-card-text class="d-flex ga-4 pl-0 py-0">
            <p class="font-weight-bold text-h6">$632.000</p>
            <v-chip small color="expenseRed">+129%</v-chip>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
        <!-- <v-row>
          <v-col
            v-for="card in cards"
            :key="card"
            cols="12"
          >
            <v-card>
              <v-list lines="two">
                <v-list-subheader :title="card"></v-list-subheader>

                <template v-for="n in 6" :key="n">
                  <v-list-item>
                    <template v-slot:prepend>
                      <v-avatar color="grey-darken-1"></v-avatar>
                    </template>

                    <v-list-item-title :title="`Message ${n}`"></v-list-item-title>

                    <v-list-item-subtitle title="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nihil repellendus distinctio similique"></v-list-item-subtitle>
                  </v-list-item>

                  <v-divider
                    v-if="n !== 6"
                    :key="`divider-${n}`"
                    inset
                  ></v-divider>
                </template>
              </v-list>
            </v-card>
          </v-col>
        </v-row> -->
      </v-container>
    </v-main>
  </v-app>
</template>

<script setup>
  import { ref, computed } from 'vue';
  // import { useFetch } from '@nuxtjs/fetch'; // Assuming you're using Nuxt.js
  const users = await useFetch('http://localhost:5231/usuario')
  const transactions = await useFetch('http://localhost:5231/transacao')



  const user = users.data
  const transactionsData = transactions.data



  const cards = ['Today', 'Yesterday']
  const links = [
    ['mdi-view-dashboard-outline', 'Dashboard'],
    ['mdi-chart-box-outline', 'Analitycs'],
    ['mdi-wallet-bifold-outline', 'My Wallet'],
    ['mdi-cog-outline', 'Settings'],
  ]


  const drawer = ref(null)
</script>


<style>
  html {
    scroll-behavior: smooth;
  }
  .circle {
    max-width: min-content;
  }
</style>