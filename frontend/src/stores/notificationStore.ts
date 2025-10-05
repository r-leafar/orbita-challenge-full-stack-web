import { defineStore } from 'pinia';
import { ref } from 'vue';

export const useNotificationStore = defineStore('notification', () => {
    const show = ref(false);
    const message = ref('');
    const color = ref('success');

    function showSnackbar(msg: string, type = 'success') {
        message.value = msg;
        color.value = type === 'error' ? 'error' : 'success'; // Define a cor
        show.value = true;

        // Auto-esconder após 3 segundos
        setTimeout(() => {
            show.value = false;
        }, 3000);
    }

    return { show, message, color, showSnackbar };
});