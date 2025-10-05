<template>
  <v-app>
    <v-main>
      <router-view />
      <v-snackbar v-model="notificationStore.show" :color="notificationStore.color" timeout="5000" location="top">
        {{ notificationStore.message }}
        <template #actions>
          <v-btn :color="notificationStore.color === 'error' ? 'white' : 'black'" variant="text"
            @click="notificationStore.show = false">
            Fechar
          </v-btn>
        </template>
      </v-snackbar>
    </v-main>
  </v-app>
</template>

<script lang="ts" setup>
import { useNotificationStore } from '@/stores/notificationStore'; // Ajuste o caminho

const notificationStore = useNotificationStore();
</script>
