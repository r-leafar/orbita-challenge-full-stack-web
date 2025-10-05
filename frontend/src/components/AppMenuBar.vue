<template>
  <v-app theme="light">
    <v-navigation-drawer v-if="props.showMenu" v-model="isDrawerOpen">
      <v-list>
        <v-list-subheader>Menu</v-list-subheader>
        <v-list-item prepend-icon="mdi-home">
          Pagina Inicial
        </v-list-item>
        <v-list-item prepend-icon="mdi-account-school">
          Alunos
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
    <v-app-bar flat class="border-b" color="grey-lighten-1">
      <v-app-bar-nav-icon v-if="props.showMenu" @click="isDrawerOpen = !isDrawerOpen" />
      <v-toolbar-title><v-img src="/src/assets/grupo-a-logo.png" width="100"></v-img> </v-toolbar-title>
      <template v-if="props.showMenu" #append>
        <v-btn icon>
          <v-badge>
            <v-icon>mdi-bell-outline</v-icon>
          </v-badge>
        </v-btn>
        <v-menu>
          <template #activator="{ props }">
            <v-avatar v-bind="props" class="ml-4">
              <img src="https://randomuser.me/api/portraits/men/1.jpg" />
            </v-avatar>
          </template>
          <v-card>
            <v-list>
              <v-list-item>
                <v-btn @click="logout"><v-list-item-title>Sair</v-list-item-title></v-btn>
              </v-list-item>
            </v-list>
          </v-card>
        </v-menu>
      </template>
    </v-app-bar>
    <slot />
  </v-app>
</template>

<script setup lang="ts">
/* -------------------- Imports -------------------- */
import { ref } from 'vue'
import { useAuth } from '@/composables/useAuth'

const { logout } = useAuth()

/* -------------------- Props -------------------- */
const props = withDefaults(
  defineProps<{
    showMenu?: boolean
  }>(),
  { showMenu: false }
)

/* -------------------- Refs / State -------------------- */
const isDrawerOpen = ref(false)
</script>
