<template>
  <v-lazy>
    <v-navigation-drawer v-model="drawer"
      class="bg-gray border-0"
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
      <template #append>
          <Profile/>
      </template>
    </v-navigation-drawer>
  </v-lazy>
</template>

<script setup>
  const drawer = ref(null)

    const links = [
    ['mdi-view-dashboard-outline', 'Dashboard'],
    ['mdi-chart-box-outline', 'Analitycs'],
    ['mdi-wallet-bifold-outline', 'My Wallet'],
    ['mdi-cog-outline', 'Settings'],
  ]
</script>