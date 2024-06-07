<template>
  <v-app id="inspire">


  <Drawer/>

    <v-main>
      <NuxtLayout>
        <NuxtPage/>
      </NuxtLayout>
    </v-main>

  </v-app>
</template>

<script setup>
  </script>


<style>
  html {
    scroll-behavior: smooth;
  }
  .circle {
    max-width: min-content;
  }
</style>