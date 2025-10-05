import { defineStore } from 'pinia'

export const useAuthUserStore = defineStore('auth', {
    state: () => ({
        authenticated: false,
        name: 'Rafael'
    }),
    actions: {
        login() {
            this.authenticated = true
        },
        logout() {
            this.authenticated = false
        }

    },
    getters: {
        isAuthenticated: (state) => state.authenticated,
        getName: (state) => state.name
    },
    persist: true,
})
